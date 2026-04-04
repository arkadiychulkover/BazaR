using BazaR.Data;
using BazaR.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BazaR.HostedServices
{
    public class MailingBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MailingBackgroundService> _logger;

        public MailingBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<MailingBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MailingBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var generator = scope.ServiceProvider.GetRequiredService<IMailingGeneratorService>();
                    var sender = scope.ServiceProvider.GetRequiredService<IUserMessageService>();

                    var settingsList = await db.MailingSettings
                        .Include(x => x.User)
                        .ToListAsync(stoppingToken);

                    var now = DateTime.UtcNow;

                    foreach (var settings in settingsList)
                    {
                        if (settings.User == null)
                            continue;

                        var hours = GetFrequencyHours(settings.PreferredFrequency);

                        var shouldSend = settings.LastMailingSentAt == null ||
                                         settings.LastMailingSentAt.Value.AddHours(hours) <= now;

                        if (!shouldSend)
                            continue;

                        var messages = generator.GenerateMessages(settings, settings.User);

                        if (messages.Count == 0)
                            continue;

                        foreach (var msg in messages)
                        {
                            await sender.SendAsync(
                                settings.UserId,
                                msg.Title,
                                msg.Text,
                                stoppingToken);
                        }

                        settings.LastMailingSentAt = now;
                        settings.UpdatedAt = now;
                    }

                    await db.SaveChangesAsync(stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in MailingBackgroundService");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }

            _logger.LogInformation("MailingBackgroundService stopped.");
        }

        private static int GetFrequencyHours(string? frequency)
        {
            return frequency?.ToLower() switch
            {
                "daily" => 24,
                "everyday" => 24,
                "weekly" => 24 * 7,
                "monthly" => 24 * 30,
                _ => 24 * 7
            };
        }
    }
}