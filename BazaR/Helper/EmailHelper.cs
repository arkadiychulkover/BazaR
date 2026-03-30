using MimeKit;
using MailKit.Net.Smtp;

namespace BazaR.Helper
{
    public class EmailHelper
    {
        private async Task<bool> SendEmailAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("bazarcontactsup@gmail.com", "crklhfbjjxuvatuc");

                try
                {
                    await client.SendAsync(message);
                    return true;
                }
                catch (Exception)
                {
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
            return false;
        }

        public async Task<bool> SendEmailPasswordReset(string userEmail, string link)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("BAZA-R Support", "bazarcontactsup@gmail.com"));
            message.To.Add(new MailboxAddress(userEmail, userEmail));
            message.Subject = "Скидання паролю - BAZA-R";

            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, Helvetica, sans-serif; background:#f4f6f8; padding:40px;'>
                    <div style='max-width:600px; margin:auto; background:white; border-radius:10px; padding:30px; box-shadow:0 4px 12px rgba(0,0,0,0.08);'>
                        <h2 style='color:#0b2545; margin-top:0;'>BAZA-R</h2>
                        <h3 style='color:#333;'>Скидання паролю</h3>
                        <p style='color:#555; font-size:15px; line-height:1.6;'>
                            Ми отримали ваш запит на зміну паролю.
                        </p>
                        <div style='text-align:center; margin:30px 0;'>
                            <a href='{link}'
                               style='background:#98BE2A;
                                      color:white;
                                      text-decoration:none;
                                      padding:14px 28px;
                                      border-radius:6px;
                                      font-weight:bold;
                                      display:inline-block;'>
                                Змінити пароль
                            </a>
                        </div>
                        <p style='word-break:break-all; color:#3E77AA; font-size:14px;'>
                            {link}
                        </p>
                        <hr style='margin:30px 0; border:none; border-top:1px solid #eee;'>
                        <p style='color:#999; font-size:12px;'>
                            &copy; {DateTime.Now.Year} BAZA-R. Усі права захищені.
                        </p>
                    </div>
                </div>";

            message.Body = bodyBuilder.ToMessageBody();

            return await SendEmailAsync(message);
        }
    }
}
