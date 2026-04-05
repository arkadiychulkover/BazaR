using BazaR.Interfaces;
using BazaR.Models;

namespace BazaR.Services
{
    public class MailingGeneratorService : IMailingGeneratorService
    {
        private readonly Random _random = new();
        private readonly MessageTemplateLibrary _templates = new();

        public List<(string Title, string Text)> GenerateMessages(MailingSetting settings, User user)
        {
            var messages = new List<(string Title, string Text)>();
            var context = new MessageContext(user);

            if (settings.NewsAndUpdates)
            {
                messages.Add(GenerateNewsMessage(context));
            }

            if (settings.SpecialOffers)
            {
                messages.Add(GenerateSpecialOfferMessage(context));
            }

            if (settings.PersonalRecommendations)
            {
                messages.Add(GeneratePersonalRecommendationsMessage(context));
            }

            if (settings.ProductAlerts)
            {
                messages.Add(GenerateProductAlertMessage(context));
            }

            if (settings.WeeklyDigest)
            {
                messages.Add(GenerateWeeklyDigestMessage(context));
            }

            return messages;
        }

        private (string Title, string Text) GenerateNewsMessage(MessageContext context)
        {
            var template = _templates.NewsTemplates[_random.Next(_templates.NewsTemplates.Length)];
            var title = Pick(_templates.NewsTitles);
            var text = template.Build(context, this);

            return (title, text);
        }

        private (string Title, string Text) GenerateSpecialOfferMessage(MessageContext context)
        {
            var template = _templates.SpecialOfferTemplates[_random.Next(_templates.SpecialOfferTemplates.Length)];
            var title = Pick(_templates.SpecialOfferTitles);
            var text = template.Build(context, this);

            return (title, text);
        }

        private (string Title, string Text) GeneratePersonalRecommendationsMessage(MessageContext context)
        {
            var template = _templates.PersonalRecommendationsTemplates[_random.Next(_templates.PersonalRecommendationsTemplates.Length)];
            var title = Pick(_templates.PersonalRecommendationsTitles);
            var text = template.Build(context, this);

            return (title, text);
        }

        private (string Title, string Text) GenerateProductAlertMessage(MessageContext context)
        {
            var template = _templates.ProductAlertTemplates[_random.Next(_templates.ProductAlertTemplates.Length)];
            var title = Pick(_templates.ProductAlertTitles);
            var text = template.Build(context, this);

            return (title, text);
        }

        private (string Title, string Text) GenerateWeeklyDigestMessage(MessageContext context)
        {
            var template = _templates.WeeklyDigestTemplates[_random.Next(_templates.WeeklyDigestTemplates.Length)];
            var title = Pick(_templates.WeeklyDigestTitles);
            var text = template.Build(context, this);

            return (title, text);
        }

        internal string Pick(string[] items) => items[_random.Next(items.Length)];
    }

    // Контекст сообщения с информацией о пользователе
    internal class MessageContext
    {
        public User User { get; }
        public string UserName { get; }
        public TimeOfDay TimeOfDay { get; }

        public MessageContext(User user)
        {
            User = user;
            UserName = GetUserDisplayName(user);
            TimeOfDay = GetTimeOfDay();
        }

        private string GetUserDisplayName(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Name))
                return user.Name;

            if (!string.IsNullOrWhiteSpace(user.UserName))
                return user.UserName;

            return "користувачу";
        }

        private TimeOfDay GetTimeOfDay()
        {
            var hour = DateTime.Now.Hour;
            if (hour < 12) return TimeOfDay.Morning;
            if (hour < 17) return TimeOfDay.Afternoon;
            if (hour < 22) return TimeOfDay.Evening;
            return TimeOfDay.Night;
        }
    }

    internal enum TimeOfDay
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    // Базовый класс для шаблонов сообщений
    internal abstract class MessageTemplate
    {
        public abstract string Build(MessageContext context, MailingGeneratorService service);
    }

    // Библиотека всех шаблонов
    internal class MessageTemplateLibrary
    {
        // === НОВИНИ ТА ОНОВЛЕННЯ ===

        public string[] NewsTitles { get; } = new[]
        {
            "Важливі оновлення BazaR 🎉",
            "Новини платформи",
            "Що нового в BazaR",
            "Свіжі покращення сервісу",
            "Останні оновлення для вас",
            "Нові можливості вже доступні",
            "Оновлення, про які варто знати"
        };

        public MessageTemplate[] NewsTemplates { get; } = new MessageTemplate[]
        {
            new NewsTemplate1(),
            new NewsTemplate2(),
            new NewsTemplate3(),
            new NewsTemplate4(),
            new NewsTemplate5()
        };

        // === СПЕЦІАЛЬНІ ПРОПОЗИЦІЇ ===

        public string[] SpecialOfferTitles { get; } = new[]
        {
            "Вигідні пропозиції спеціально для вас 🎁",
            "Нові акції та знижки",
            "Гарячі пропозиції BazaR 🔥",
            "Ексклюзивні знижки сьогодні",
            "Спеціальна пропозиція для вас",
            "Акції, які не можна пропустити",
            "Суперціни на популярні товари"
        };

        public MessageTemplate[] SpecialOfferTemplates { get; } = new MessageTemplate[]
        {
            new SpecialOfferTemplate1(),
            new SpecialOfferTemplate2(),
            new SpecialOfferTemplate3(),
            new SpecialOfferTemplate4(),
            new SpecialOfferTemplate5()
        };

        // === ПЕРСОНАЛЬНІ РЕКОМЕНДАЦІЇ ===

        public string[] PersonalRecommendationsTitles { get; } = new[]
        {
            "Підібрали саме для вас ✨",
            "Персональні рекомендації",
            "Товари за вашими інтересами",
            "Індивідуальна добірка товарів",
            "Це може вам сподобатися",
            "Рекомендації на основі ваших переглядів",
            "Спеціально підібрано для вас"
        };

        public MessageTemplate[] PersonalRecommendationsTemplates { get; } = new MessageTemplate[]
        {
            new PersonalRecommendationsTemplate1(),
            new PersonalRecommendationsTemplate2(),
            new PersonalRecommendationsTemplate3(),
            new PersonalRecommendationsTemplate4(),
            new PersonalRecommendationsTemplate5()
        };

        // === СПОВІЩЕННЯ ПРО ТОВАРИ ===

        public string[] ProductAlertTitles { get; } = new[]
        {
            "Важлива новина про товари ⚡",
            "Оновлення наявності",
            "Товар знову в наявності!",
            "Нові надходження в каталозі",
            "Зміни по товарах, що вас цікавлять",
            "Очікуваний товар доступний",
            "Актуальні оновлення асортименту"
        };

        public MessageTemplate[] ProductAlertTemplates { get; } = new MessageTemplate[]
        {
            new ProductAlertTemplate1(),
            new ProductAlertTemplate2(),
            new ProductAlertTemplate3(),
            new ProductAlertTemplate4(),
            new ProductAlertTemplate5()
        };

        // === ЩОТИЖНЕВИЙ ДАЙДЖЕСТ ===

        public string[] WeeklyDigestTitles { get; } = new[]
        {
            "Ваш тижневий огляд BazaR 📊",
            "Головне за тиждень",
            "Підсумки тижня",
            "Щотижневий дайджест",
            "Усе найважливіше за тиждень",
            "Огляд подій тижня",
            "Ваш персональний дайджест"
        };

        public MessageTemplate[] WeeklyDigestTemplates { get; } = new MessageTemplate[]
        {
            new WeeklyDigestTemplate1(),
            new WeeklyDigestTemplate2(),
            new WeeklyDigestTemplate3(),
            new WeeklyDigestTemplate4(),
            new WeeklyDigestTemplate5()
        };
    }

    // ============================================
    // ШАБЛОНИ НОВИН ТА ОНОВЛЕНЬ
    // ============================================

    internal class NewsTemplate1 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var greetings = new[]
            {
                $"Вітаємо, {context.UserName}!",
                $"Доброго дня, {context.UserName}!",
                $"Привіт, {context.UserName}!",
                $"{context.UserName}, раді вітати вас!"
            };

            var main = new[]
            {
                "Ми запустили нові функції, які зроблять ваш досвід використання BazaR ще приємнішим. Оновлений інтерфейс, швидша навігація та розумніший пошук — усе для вашої зручності.",
                "У BazaR з'явилися покращення, про які ви просили: швидше завантаження сторінок, зручніше оформлення замовлень та оновлений каталог із кращою фільтрацією.",
                "Платформа BazaR стала ще потужнішою! Ми оптимізували продуктивність, додали нові фільтри пошуку та покращили мобільну версію для комфортних покупок у будь-який час."
            };

            var cta = new[]
            {
                "Спробуйте оновлений сервіс прямо зараз — переконайтеся самі, наскільки він став зручнішим!",
                "Завітайте на платформу та оцініть усі новинки на власні очі.",
                "Перейдіть до свого профілю та відкрийте для себе нові можливості вже сьогодні."
            };

            return $"{service.Pick(greetings)} {service.Pick(main)} {service.Pick(cta)}";
        }
    }

    internal class NewsTemplate2 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var intro = new[]
            {
                $"{context.UserName}, ділимося важливими новинами!",
                $"Маємо чудові новини для вас, {context.UserName}!",
                $"{context.UserName}, є що розповісти!"
            };

            var news = new[]
            {
                "Команда BazaR працювала над покращеннями, і результати вже доступні: персоналізовані рекомендації стали точнішими, з'явилася можливість порівнювати товари, а процес оформлення замовлення тепер займає на 30% менше часу.",
                "Ми прислухалися до ваших відгуків та внесли важливі зміни: розширили способи оплати, додали детальніші описи товарів із фото від покупців, та покращили роботу служби підтримки.",
                "Останнє оновлення включає покращену систему відстеження замовлень, нові категорії товарів та зручніший особистий кабінет, де ви можете керувати всіма своїми налаштуваннями."
            };

            var closing = new[]
            {
                "Усі оновлення вже активні — перевірте їх у дії!",
                "Ознайомтеся з новинками в своєму кабінеті.",
                "Не втрачайте часу — протестуйте покращення вже зараз!"
            };

            return $"{service.Pick(intro)} {service.Pick(news)} {service.Pick(closing)}";
        }
    }

    internal class NewsTemplate3 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var hook = new[]
            {
                $"{context.UserName}, BazaR став ще кращим!",
                $"Хороші новини, {context.UserName}!",
                $"{context.UserName}, готові до оновлень?"
            };

            var updates = new[]
            {
                "Нова версія платформи вже працює: ми переробили дизайн для кращої читабельності, додали розумні підказки при виборі товарів та інтегрували систему лояльності з накопичувальними бонусами.",
                "Свіже оновлення приносить багато корисного: інтерактивні огляди товарів, можливість зберігати списки бажань та ділитися ними, а також покращену систему знижок і акцій.",
                "У нас для вас цілий пакет покращень: від нових способів сортування товарів до зручнішої історії замовлень та можливості швидкого повторного замовлення улюблених позицій."
            };

            var action = new[]
            {
                "Відкрийте BazaR та побачите всі зміни на головній сторінці!",
                "Зайдіть у свій акаунт і дослідіть нові функції.",
                "Перейдіть на сайт та перевірте, що нового з'явилося саме для вас!"
            };

            return $"{service.Pick(hook)} {service.Pick(updates)} {service.Pick(action)}";
        }
    }

    internal class NewsTemplate4 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var greeting = new[]
            {
                $"Доброго дня, {context.UserName}!",
                $"Привіт, {context.UserName}!",
                $"{context.UserName}, вітаємо!"
            };

            var announcement = new[]
            {
                "Щойно завершили великий апдейт платформи! Тепер у вас є доступ до розширеної аналітики цін (можна відстежувати зміни вартості), покращеної системи відгуків з фільтрацією та нового розділу з порівнянням характеристик товарів.",
                "Запускаємо серйозне оновлення: інтелектуальний пошук з підказками під час набору, покращені фільтри за параметрами, інтеграція з популярними сервісами доставки та багато іншого.",
                "Представляємо оновлену версію BazaR з купою нововведень: програма лояльності для постійних клієнтів, система раннього доступу до акцій, персональні знижки та багато приємних сюрпризів."
            };

            var benefit = new[]
            {
                "Усе це створено, щоб ваші покупки були швидшими, вигіднішими та приємнішими.",
                "Кожна деталь продумана для максимального комфорту та економії вашого часу.",
                "Ми зробили все, щоб процес покупки приносив лише позитивні емоції."
            };

            return $"{service.Pick(greeting)} {service.Pick(announcement)} {service.Pick(benefit)}";
        }
    }

    internal class NewsTemplate5 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var intro = new[]
            {
                $"{context.UserName}, ми не стоїмо на місці!",
                $"Раді повідомити вам, {context.UserName}!",
                $"{context.UserName}, маємо відмінні новини!"
            };

            var details = new[]
            {
                "Останнє оновлення включає покращену безпеку платежів, нові варіанти доставки з вибором зручного часу, розширений каталог товарів та оновлену мобільну версію, яка працює навіть швидше за десктопну.",
                "Свіжий реліз містить довгоочікувані функції: можливість створювати власні колекції товарів, отримувати сповіщення про зміни цін, використовувати голосовий пошук та багато іншого.",
                "Нова версія платформи пропонує: автоматичне застосування найвигідніших знижок, систему кешбеку, інтеграцію з Apple Pay та Google Pay, а також покращену підтримку клієнтів через чат."
            };

            var invitation = new[]
            {
                "Заходьте на платформу та випробуйте всі новинки — вони вас приємно здивують!",
                "Не гайте часу, дослідіть оновлений BazaR вже зараз!",
                "Перевірте самі, наскільки зручнішим став сервіс!"
            };

            return $"{service.Pick(intro)} {service.Pick(details)} {service.Pick(invitation)}";
        }
    }

    // ============================================
    // ШАБЛОНИ СПЕЦІАЛЬНИХ ПРОПОЗИЦІЙ
    // ============================================

    internal class SpecialOfferTemplate1 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var hook = new[]
            {
                $"{context.UserName}, спеціально для вас — вигідні пропозиції!",
                $"Увага, {context.UserName}! Нові акції вже доступні!",
                $"{context.UserName}, знижки, про які ви мріяли!"
            };

            var offer = new[]
            {
                "Сьогодні стартує розпродаж популярних категорій зі знижками до 50%. Серед акційних товарів — електроніка, товари для дому, одяг та аксесуари. Пропозиція обмежена за часом!",
                "Ексклюзивні знижки на вибрані товари: від 20% до 60% на топові позиції. Діє тільки до кінця тижня, кількість товарів обмежена!",
                "Встигніть скористатися спеціальною пропозицією: при купівлі двох товарів — третій у подарунок! Акція діє на весь асортимент учасників."
            };

            var urgency = new[]
            {
                "Найкращі позиції розбирають швидко — не пропустіть свій шанс!",
                "Акція діє обмежений час, поспішайте!",
                "Кількість товарів за акційною ціною обмежена!"
            };

            return $"{service.Pick(hook)} {service.Pick(offer)} {service.Pick(urgency)}";
        }
    }

    internal class SpecialOfferTemplate2 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var greeting = new[]
            {
                $"Добрий день, {context.UserName}!",
                $"{context.UserName}, маємо для вас щось особливе!",
                $"Вітаємо, {context.UserName}!"
            };

            var promo = new[]
            {
                "Ми підготували персональну добірку акційних товарів на основі ваших інтересів. Знижки від 15% до 45%, а на деякі позиції — ще вигідніші умови з промокодом.",
                "Спеціальна пропозиція тижня: суперціни на найпопулярніші товари категорії. Економте до 40% на якісних брендових товарах.",
                "Флеш-розпродаж стартує зараз! Тисячі товарів за зниженими цінами, додаткові бонуси для постійних клієнтів та безкоштовна доставка при замовленні від певної суми."
            };

            var cta = new[]
            {
                "Перегляньте акційні пропозиції та оберіть найкраще для себе!",
                "Не втрачайте можливість заощадити — переходьте до акційних товарів!",
                "Відкрийте каталог та знайдіть свої ідеальні покупки за чудовими цінами!"
            };

            return $"{service.Pick(greeting)} {service.Pick(promo)} {service.Pick(cta)}";
        }
    }

    internal class SpecialOfferTemplate3 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var alert = new[]
            {
                $"🔥 Гаряча пропозиція для вас, {context.UserName}!",
                $"⚡ {context.UserName}, ексклюзивні знижки чекають!",
                $"💥 {context.UserName}, не пропустіть вигідну акцію!"
            };

            var details = new[]
            {
                "Тільки сьогодні та завтра: мегазнижки на вибрані товари до 70%! У акції беруть участь тисячі позицій з різних категорій. Час діяти — до кінця акції залишилося небагато!",
                "Стартує весняний розпродаж! Оновіть гардероб, техніку чи товари для дому зі знижками від 25%. Бонус: додаткові 10% на друге замовлення протягом тижня.",
                "Спеціальна акція для постійних клієнтів: ексклюзивний доступ до розпродажу з найкращими цінами року. Тисячі товарів, неймовірні знижки, обмежена кількість!"
            };

            var action = new[]
            {
                "Переходьте до розділу акцій та обирайте свої скарби!",
                "Каталог акційних пропозицій чекає на вас — встигніть скористатися!",
                "Перегляньте всі вигідні пропозиції прямо зараз!"
            };

            return $"{service.Pick(alert)} {service.Pick(details)} {service.Pick(action)}";
        }
    }

    internal class SpecialOfferTemplate4 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var intro = new[]
            {
                $"{context.UserName}, для вас є вигідна пропозиція!",
                $"Спеціально для вас, {context.UserName}!",
                $"{context.UserName}, ця акція створена для вас!"
            };

            var proposition = new[]
            {
                "На основі вашої активності ми відібрали найцікавіші акційні пропозиції. Серед них — товари зі знижками до 55%, спеціальні комплекти за вигідною ціною та ексклюзивні новинки з преміум-знижкою.",
                "Вашу увагу варті нові надходження за акційними цінами: топові бренди, перевірена якість, знижки від 20% до 50%. Плюс бонуси при оформленні замовлення сьогодні.",
                "Персональна пропозиція: товари з вашого списку бажань та схожі позиції тепер доступні зі знижками. Економте на тому, що вам дійсно потрібно!"
            };

            var reminder = new[]
            {
                "Пропозиція діє обмежений час — не відкладайте на потім!",
                "Найкращі товари закінчуються першими, поспішайте!",
                "Акційних товарів обмежена кількість — скористайтеся зараз!"
            };

            return $"{service.Pick(intro)} {service.Pick(proposition)} {service.Pick(reminder)}";
        }
    }

    internal class SpecialOfferTemplate5 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var announcement = new[]
            {
                $"Чудові новини, {context.UserName}!",
                $"{context.UserName}, ексклюзив тільки для вас!",
                $"Увага, {context.UserName}! Спеціальна акція!"
            };

            var content = new[]
            {
                "Запускаємо тижневу розпродаж з неймовірними цінами! Кожен день — нова категорія товарів зі знижками до 60%. Сьогодні в акції: електроніка та гаджети. Завтра — товари для дому та краси.",
                "Святкова акція вже почалася: тисячі товарів за суперцінами, додаткові бонуси на наступні покупки, безкоштовна доставка та можливість оплати частинами без переплат.",
                "Ранній доступ до розпродажу спеціально для вас! Найкращі пропозиції, найнижчі ціни, найширший вибір. Скористайтеся перевагою перед стартом загального розпродажу!"
            };

            var finale = new[]
            {
                "Відкривайте каталог акцій та робіть вигідні покупки прямо зараз!",
                "Перегляньте всі пропозиції та оберіть найкраще — встигніть до завершення акції!",
                "Переходьте до акційних товарів та насолоджуйтеся вигідними цінами!"
            };

            return $"{service.Pick(announcement)} {service.Pick(content)} {service.Pick(finale)}";
        }
    }

    // ============================================
    // ШАБЛОНИ ПЕРСОНАЛЬНИХ РЕКОМЕНДАЦІЙ
    // ============================================

    internal class PersonalRecommendationsTemplate1 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var intro = new[]
            {
                $"{context.UserName}, ми підібрали щось особливе для вас!",
                $"Персональна добірка для вас, {context.UserName}!",
                $"{context.UserName}, дивіться, що ми знайшли!"
            };

            var explanation = new[]
            {
                "На основі ваших переглядів та покупок наш алгоритм сформував список товарів, які ідеально відповідають вашим інтересам. Серед них — новинки та популярні позиції у ваших улюблених категоріях.",
                "Проаналізувавши ваші переваги, ми створили унікальну добірку товарів саме для вас. Тут зібрано те, що може вам сподобатися на основі вашої активності на платформі.",
                "Ваші інтереси — наш пріоритет! Ми відібрали товари, схожі на ті, що вас цікавили раніше, та додали нові позиції, які користуються популярністю серед схожих покупців."
            };

            var cta = new[]
            {
                "Ознайомтеся з рекомендаціями — можливо, ви знайдете саме те, що шукали!",
                "Перегляньте персональну добірку та відкрийте для себе цікаві товари!",
                "Перевірте рекомендації прямо зараз — вони створені спеціально для вас!"
            };

            return $"{service.Pick(intro)} {service.Pick(explanation)} {service.Pick(cta)}";
        }
    }

    internal class PersonalRecommendationsTemplate2 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var greeting = new[]
            {
                $"Вітаємо, {context.UserName}!",
                $"Доброго дня, {context.UserName}!",
                $"{context.UserName}, раді вас бачити!"
            };

            var recommendation = new[]
            {
                "Нова персональна добірка вже готова! Ми врахували вашу історію переглядів, улюблені бренди та популярні товари у ваших категоріях. Результат — точні рекомендації, які заощадять ваш час на пошук.",
                "Спеціально для вас ми сформували список рекомендованих товарів, які відповідають вашим смакам та потребам. Кожна позиція підібрана з урахуванням ваших попередніх виборів.",
                "На основі вашої активності та вподобань ми створили унікальний каталог товарів. Тут і новинки від улюблених брендів, і популярні позиції у ваших категоріях, і схожі товари на ті, що ви переглядали."
            };

            var benefit = new[]
            {
                "Економте час на пошук — найкраще вже підібрано для вас!",
                "Все найцікавіше зібрано в одному місці спеціально для вас!",
                "Персоналізований підхід допоможе знайти ідеальні товари швидше!"
            };

            return $"{service.Pick(greeting)} {service.Pick(recommendation)} {service.Pick(benefit)}";
        }
    }

    internal class PersonalRecommendationsTemplate3 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var hook = new[]
            {
                $"{context.UserName}, у нас є пропозиції саме для вас!",
                $"Персональні рекомендації готові, {context.UserName}!",
                $"{context.UserName}, подивіться, що ми підібрали!"
            };

            var content = new[]
            {
                "Наша система рекомендацій проаналізувала ваші переваги та відібрала товари, які з високою ймовірністю вам сподобаються. Серед них — бестселери ваших категорій, новинки від перевірених брендів та товари зі схожими характеристиками до тих, що вас цікавили.",
                "Розумний алгоритм підібрав для вас товари на основі детального аналізу ваших вподобань, історії покупок та поведінки схожих користувачів. Результат — релевантні рекомендації, які дійсно варті уваги.",
                "Ваша персональна добірка сформована з урахуванням багатьох факторів: ваші улюблені категорії, бренди, ціновий діапазон, а також нові надходження, які відповідають вашим інтересам."
            };

            var action = new[]
            {
                "Перегляньте добірку та знайдіть свої ідеальні покупки!",
                "Ознайомтеся з рекомендаціями — вони створені для вас!",
                "Відкрийте каталог персональних пропозицій прямо зараз!"
            };

            return $"{service.Pick(hook)} {service.Pick(content)} {service.Pick(action)}";
        }
    }

    internal class PersonalRecommendationsTemplate4 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var start = new[]
            {
                $"{context.UserName}, маємо для вас ідеальні пропозиції!",
                $"Спеціальна добірка чекає на вас, {context.UserName}!",
                $"{context.UserName}, ці товари обрані саме для вас!"
            };

            var description = new[]
            {
                "Ми помітили, що вас цікавлять певні категорії та бренди, тому підібрали саме ті товари, які найкраще відповідають вашим критеріям. Це не випадковий список — це персоналізована добірка на основі ваших реальних інтересів.",
                "Ваші переваги та вподобання стали основою для створення цієї унікальної добірки. Кожен товар тут з'явився не просто так — він відповідає вашим критеріям вибору та може бути вам корисним.",
                "На базі вашої історії взаємодії з платформою ми сформували каталог рекомендацій, який включає як перевірені позиції від знайомих вам брендів, так і нові цікаві товари, про які ви, можливо, ще не знали."
            };

            var closing = new[]
            {
                "Завітайте до розділу рекомендацій та оберіть найкраще!",
                "Перевірте добірку — серед позицій може бути саме те, що ви шукали!",
                "Ознайомтеся з персональними пропозиціями вже зараз!"
            };

            return $"{service.Pick(start)} {service.Pick(description)} {service.Pick(closing)}";
        }
    }

    internal class PersonalRecommendationsTemplate5 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var opener = new[]
            {
                $"Добрий день, {context.UserName}!",
                $"{context.UserName}, для вас готова нова добірка!",
                $"Вітаємо, {context.UserName}!"
            };

            var main = new[]
            {
                "Персоналізована система BazaR підібрала товари, які максимально відповідають вашим інтересам. Ми врахували все: від улюблених категорій до переглянутих позицій, від ваших покупок до популярних товарів серед схожих користувачів.",
                "Нова добірка товарів сформована ексклюзивно для вас! Тут зібрано найкраще з того, що може вас зацікавити: новинки від перевірених постачальників, популярні позиції ваших категорій та товари з високими рейтингами.",
                "Ваш персональний каталог оновлено! Завдяки розумному алгоритму підбору ми знайшли товари, які ідеально підходять під ваш профіль покупця. Від улюблених брендів до нових цікавих знахідок."
            };

            var invitation = new[]
            {
                "Перейдіть до персональних рекомендацій та дослідіть нові можливості!",
                "Відкрийте добірку та знайдіть щось особливе для себе!",
                "Перегляньте рекомендовані товари — можливо, серед них ваша наступна покупка!"
            };

            return $"{service.Pick(opener)} {service.Pick(main)} {service.Pick(invitation)}";
        }
    }

    // ============================================
    // ШАБЛОНИ СПОВІЩЕНЬ ПРО ТОВАРИ
    // ============================================

    internal class ProductAlertTemplate1 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var alert = new[]
            {
                $"{context.UserName}, важлива новина про товари!",
                $"Увага, {context.UserName}! Оновлення по товарах!",
                $"{context.UserName}, маємо новини для вас!"
            };

            var news = new[]
            {
                "Товари з вашого списку очікування знову в наявності! Поспішайте оформити замовлення, поки товар не розібрали — популярні позиції зазвичай швидко закінчуються.",
                "Хороші новини: товар, який вас цікавив, знову доступний для замовлення. Крім того, з'явилися схожі новинки, які теж можуть вам сподобатися.",
                "Оновлення наявності: позиції, за якими ви стежили, тепер у наличності та готові до відправлення. Не втрачайте можливість придбати те, що давно шукали!"
            };

            var action = new[]
            {
                "Перейдіть до каталогу та оформіть замовлення прямо зараз!",
                "Переглянути доступні товари та додати їх до кошика!",
                "Не відкладайте — перевірте наявність і замовте вже сьогодні!"
            };

            return $"{service.Pick(alert)} {service.Pick(news)} {service.Pick(action)}";
        }
    }

    internal class ProductAlertTemplate2 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var greeting = new[]
            {
                $"Доброго дня, {context.UserName}!",
                $"{context.UserName}, раді повідомити!",
                $"Вітаємо, {context.UserName}!"
            };

            var update = new[]
            {
                "У категоріях, які вас цікавлять, з'явилися нові товари та оновилась наявність популярних позицій. Серед новинок — продукція від перевірених брендів та ексклюзивні пропозиції.",
                "Відмінні новини: асортимент оновлено! Нові надходження у ваших улюблених категоріях вже доступні для замовлення. Плюс повернулися в наявність деякі популярні позиції.",
                "Товари, які могли вас зацікавити, тепер знову в каталозі. Крім цього, з'явилися цікаві новинки зі схожими характеристиками та кращими відгуками покупців."
            };

            var prompt = new[]
            {
                "Завітайте до каталогу та перегляньте оновлений асортимент!",
                "Перевірте наявність цікавих товарів прямо зараз!",
                "Ознайомтеся з новинками та доступними позиціями!"
            };

            return $"{service.Pick(greeting)} {service.Pick(update)} {service.Pick(prompt)}";
        }
    }

    internal class ProductAlertTemplate3 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var notification = new[]
            {
                $"⚡ {context.UserName}, термінове оновлення!",
                $"🔔 Важливо, {context.UserName}!",
                $"📢 {context.UserName}, новини про товари!"
            };

            var information = new[]
            {
                "Товар із вашого списка бажань знову доступний і за спеціальною ціною! Діє обмежена кількість за цією ціною, тому варто поспішити з оформленням замовлення.",
                "Довгоочікуване поповнення: популярні товари, які швидко розбирали, знову на складі. Наразі доступна обмежена партія — рекомендуємо не відкладати замовлення.",
                "Ексклюзивна інформація для вас: товари, за якими ви стежили, в наявності з можливістю швидкої доставки. Актуальна кількість обмежена, поспішайте!"
            };

            var urgency = new[]
            {
                "Встигніть замовити, поки товар знову не розібрали!",
                "Кількість обмежена — оформіть замовлення зараз!",
                "Не втрачайте шанс — товар може знову закінчитися!"
            };

            return $"{service.Pick(notification)} {service.Pick(information)} {service.Pick(urgency)}";
        }
    }

    internal class ProductAlertTemplate4 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var intro = new[]
            {
                $"{context.UserName}, нові надходження в каталозі!",
                $"Свіжі товари для вас, {context.UserName}!",
                $"{context.UserName}, оновлення асортименту!"
            };

            var details = new[]
            {
                "У ваших улюблених категоріях з'явилися нові цікаві товари від перевірених постачальників. Крім того, повернулися в наявність позиції, які раніше були недоступні.",
                "Каталог поповнився новими товарами, які відповідають вашим інтересам. Також оновилась інформація про наявність популярних позицій — багато з них знову можна замовити.",
                "Хороші новини з різних категорій: нові товари від топових брендів, оновлення наявності бестселерів та повернення популярних позицій, які довго були відсутні."
            };

            var cta = new[]
            {
                "Перегляньте оновлений каталог та знайдіть щось для себе!",
                "Відкрийте розділ новинок та оцініть свіжі надходження!",
                "Завітайте на платформу та дослідіть нові можливості!"
            };

            return $"{service.Pick(intro)} {service.Pick(details)} {service.Pick(cta)}";
        }
    }

    internal class ProductAlertTemplate5 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var header = new[]
            {
                $"Чудові новини, {context.UserName}!",
                $"{context.UserName}, для вас є важлива інформація!",
                $"Раді повідомити, {context.UserName}!"
            };

            var message = new[]
            {
                "Товари, на які ви підписалися для відстеження, тепер доступні! Більше того, деякі з них беруть участь в акціях, тож ви можете придбати їх за ще вигіднішою ціною.",
                "Сповіщення про наявність: всі позиції з вашого списка очікування знову можна замовити. Рекомендуємо перевірити актуальні ціни — можливо, зараз діють вигідні пропозиції.",
                "Великий день для вашого списка бажань! Більшість товарів, за якими ви стежили, повернулися в наявність. Крім того, є схожі новинки, які теж варті уваги."
            };

            var finale = new[]
            {
                "Не гайте часу — перевірте наявність та оформіть замовлення!",
                "Завітайте до свого списка та замовте потрібні товари прямо зараз!",
                "Перейдіть до каталогу та скористайтеся можливістю придбати бажане!"
            };

            return $"{service.Pick(header)} {service.Pick(message)} {service.Pick(finale)}";
        }
    }

    // ============================================
    // ШАБЛОНИ ЩОТИЖНЕВОГО ДАЙДЖЕСТУ
    // ============================================

    internal class WeeklyDigestTemplate1 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var greeting = new[]
            {
                $"Доброго дня, {context.UserName}!",
                $"Вітаємо, {context.UserName}!",
                $"{context.UserName}, підбиваємо підсумки!"
            };

            var summary = new[]
            {
                "Ось ваш тижневий огляд найважливішого: цього тижня на BazaR з'явилося багато нового — від покращень платформи до вигідних акцій. Нові товари в каталозі, оновлена система пошуку та спеціальні пропозиції саме для вас.",
                "Підсумки тижня готові! За останні 7 днів відбулося чимало цікавого: запуск нових функцій, поповнення асортименту популярними позиціями, старт сезонних акцій та покращення роботи служби доставки.",
                "Ваш персональний дайджест за тиждень: оновлення платформи, нові товари у ваших улюблених категоріях, стартували нові акції, а також з'явилися персональні знижки на основі ваших інтересів."
            };

            var highlights = new[]
            {
                "Найцікавіше: нові рекомендації для вас, повернення популярних товарів у наявність та ексклюзивні пропозиції на наступний тиждень.",
                "Що варто відзначити: покращена навігація каталогу, розширений асортимент, нові способи оплати та спеціальні умови доставки.",
                "Головні події: оновлення функціоналу, нові надходження брендових товарів, старт весняних акцій та персоналізовані пропозиції саме для вас."
            };

            return $"{service.Pick(greeting)} {service.Pick(summary)} {service.Pick(highlights)}";
        }
    }

    internal class WeeklyDigestTemplate2 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var intro = new[]
            {
                $"{context.UserName}, ваш огляд тижня готовий!",
                $"Тижневий дайджест для вас, {context.UserName}!",
                $"{context.UserName}, дивіться, що відбулося за тиждень!"
            };

            var content = new[]
            {
                "За останній тиждень на платформі з'явилося багато корисного: оновлені розділи каталогу з покращеною фільтрацією, нові товари від перевірених постачальників, стартували вигідні акції, а також ми запустили програму кешбеку для постійних клієнтів.",
                "Короткий огляд подій тижня: платформа BazaR отримала важливі оновлення (швидший пошук, кращі рекомендації), в каталог додано сотні нових товарів, стартували сезонні розпродажі, та з'явилися ексклюзивні пропозиції для вас.",
                "Цього тижня відбулося чимало важливого: ми оптимізували роботу сайту для швидшого завантаження, розширили асортимент популярних категорій, запустили нові акції та додали зручні функції в особистий кабінет."
            };

            var action = new[]
            {
                "Завітайте на платформу, щоб детальніше ознайомитися з усіма новинками!",
                "Перегляньте оновлення та скористайтеся новими можливостями!",
                "Перейдіть до каталогу та оцініть всі зміни на власні очі!"
            };

            return $"{service.Pick(intro)} {service.Pick(content)} {service.Pick(action)}";
        }
    }

    internal class WeeklyDigestTemplate3 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var header = new[]
            {
                $"📊 Підсумки тижня, {context.UserName}!",
                $"🗓️ Ваш тижневий огляд, {context.UserName}!",
                $"📰 {context.UserName}, головне за тиждень!"
            };

            var digest = new[]
            {
                "Стисло про головне: нові функції платформи (порівняння товарів, відстеження цін), поповнення каталогу топовими брендами, запуск щотижневих flash-розпродажів, покращена система лояльності з більшими бонусами.",
                "Що нового цього тижня: оновлений інтерфейс особистого кабінету, розширені можливості фільтрації товарів, нові категорії в каталозі, стартували вигідні акції на популярні позиції, додано нові способи доставки.",
                "Короткий дайджест останніх 7 днів: запуск персоналізованих знижок, оновлення асортименту у всіх основних категоріях, покращена робота мобільної версії, нові партнерства з провідними брендами та ексклюзивні пропозиції."
            };

            var lookAhead = new[]
            {
                "На наступний тиждень заплановано: нові надходження товарів, старт великого розпродажу та спеціальні бонуси для постійних клієнтів.",
                "Що чекає попереду: розширення асортименту, запуск нових акцій, покращення системи доставки та багато приємних сюрпризів.",
                "Наступного тижня очікуйте: нові ексклюзивні пропозиції, розширення каталогу новинками сезону та спеціальні умови для вас."
            };

            return $"{service.Pick(header)} {service.Pick(digest)} {service.Pick(lookAhead)}";
        }
    }

    internal class WeeklyDigestTemplate4 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var opening = new[]
            {
                $"Вітаємо, {context.UserName}!",
                $"{context.UserName}, час підбити підсумки!",
                $"Доброго дня, {context.UserName}!"
            };

            var recap = new[]
            {
                "Насичений тиждень на BazaR завершено! Ми запустили нові можливості (система бажань з сповіщеннями, розумний пошук з підказками), додали в каталог сотні нових товарів, стартували сезонні акції зі знижками до 50%, та покращили роботу служби підтримки.",
                "Тижневий огляд платформи: значні оновлення функціоналу (інтеграція з платіжними системами, покращена безпека), масштабне поповнення асортименту, старт великих розпродажів, додаткові бонуси для активних користувачів.",
                "Підсумки тижня: нова версія платформи з покращеним UX, розширення каталогу ексклюзивними брендами, запуск програми лояльності з накопичувальними бонусами, спеціальні пропозиції на основі ваших інтересів."
            };

            var personal = new[]
            {
                "Для вас персонально: оновлені рекомендації на основі нової активності, спеціальні знижки на товари з вашого списка бажань, ранній доступ до майбутніх акцій.",
                "Що стосується вас: нова добірка товарів за вашими інтересами, ексклюзивні пропозиції, бонуси за активність на платформі.",
                "Особисто для вас підготовлено: персоналізовані знижки, пріоритетний доступ до новинок, додаткові бонуси на наступні покупки."
            };

            return $"{service.Pick(opening)} {service.Pick(recap)} {service.Pick(personal)}";
        }
    }

    internal class WeeklyDigestTemplate5 : MessageTemplate
    {
        public override string Build(MessageContext context, MailingGeneratorService service)
        {
            var start = new[]
            {
                $"{context.UserName}, ваш персональний дайджест!",
                $"Тижневий огляд готовий, {context.UserName}!",
                $"{context.UserName}, підбиваємо підсумки тижня!"
            };

            var overview = new[]
            {
                "Всебічний огляд останнього тижня: технічні покращення платформи для швидшої роботи, оновлення дизайну з акцентом на зручність, поповнення каталогу новими брендами та категоріями, запуск масштабних акцій з різноманітними знижками, розширення географії доставки.",
                "Комплексний дайджест за тиждень: нові можливості для користувачів (списки покупок, історія переглядів, порівняння товарів), значне розширення асортименту, старт сезонних розпродажів, покращена система бонусів, ексклюзивні партнерства з провідними постачальниками.",
                "Повний огляд тижня на BazaR: запуск інноваційних функцій (голосовий пошук, AR-перегляд товарів), масштабне оновлення каталогу новинками, великі акції та розпродажі, нова програма кешбеку, покращена підтримка клієнтів."
            };

            var closure = new[]
            {
                "Детальніше про всі новинки дивіться на платформі — кожне оновлення створене для вашого комфорту!",
                "Завітайте до BazaR та ознайомтеся з усіма змінами детальніше!",
                "Перевірте всі оновлення на сайті та скористайтеся новими можливостями вже зараз!"
            };

            return $"{service.Pick(start)} {service.Pick(overview)} {service.Pick(closure)}";
        }
    }
}