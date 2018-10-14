using LuisBot.Helper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Bot.Sample.LuisBot
{
    [Serializable]
    public class CarouselCardsDialog : IDialog<object>
    {
        private readonly Category _category;

        public CarouselCardsDialog(Category category)
        {
            _category = category;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetHeroCardAttachments();

            await context.PostAsync(reply);

            context.EndConversation("");
        }

        private IList<Attachment> GetHeroCardAttachments()
        {
            if (_category == Category.Men)
            {
                return new List<Attachment>
                {
                    GetHeroCard(
                        "HUGO BOSS",
                        "Anzug 2-teilig mit Karo",
                        "CHF 899.00",
                        new CardImage( "https://imgsrv.pkz.ch/content/products/338542/00281795_338542_marine_front.550.jpg?404=default-image.png"),
                        new CardAction(ActionTypes.OpenUrl, "Mehr anzeigen", value: "https://www.pkz.ch/de/anzug-2-teilig-mit-karo-2")),
                    GetHeroCard(
                        "ARTIGIANO",
                        "Hemd mit Haifischkragen",
                        "CHF 249.00",
                        new CardImage("https://imgsrv.pkz.ch/content/products/263239/263239_marine_front.550.jpg?404=default-image.png"),
                        new CardAction(ActionTypes.OpenUrl, "Zum Produkt", value: "https://www.pkz.ch/de/61094-hemd-mit-haifischkragen")),
                    GetThumbnailCard(
                        "WOOLRICH",
                        "Daunenparka mit Pelzkragen ARCTIC PARKA",
                        "CHF 899.00",
                        new CardImage( "https://imgsrv.pkz.ch/content/products/197596/197596_rot_front.550.jpg?404=default-image.png"),
                        new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://www.pkz.ch/de/490110-daunenparka-mit-pelzkragen-arctic-parka")),
                    GetThumbnailCard(
                        "TIGER OF SWEDEN",
                        "Gilet slim fit",
                        "CHF 299.00",
                        new CardImage( "https://imgsrv.pkz.ch/content/products/338529/338529_blau_front.550.jpg?404=default-image.png"),
                        new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://www.pkz.ch/de/554848-gilet-slim-fit"))
                };
            }

            return new List<Attachment>
            {
                GetHeroCard(
                    "TALBOT RUNHOF",
                    "Cocktailkleid",
                    "CHF 1179.00",
                    new CardImage( "https://imgsrv.pkz.ch/content/products/830557/00291067_830557_rot_front.550.jpg?404=default-image.png"),
                    new CardAction(ActionTypes.OpenUrl, "Mehr anzeigen", value: "https://www.pkz.ch/de/cocktailkleid-4")),
                GetThumbnailCard(
                    "AKRIS PUNTO",
                    "Reversibler Lammfellmantel",
                    "CHF 2990.00",
                    new CardImage("https://imgsrv.pkz.ch/content/products/806323/00296288_806323_schwarz_front.550.jpg?404=default-image.png"),
                    new CardAction(ActionTypes.OpenUrl, "Zum Produkt", value: "https://www.pkz.ch/de/reversibler-lammfellmantel"))
            };
        }

        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage> { cardImage },
                Buttons = new List<CardAction> { cardAction }
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage> { cardImage },
                Buttons = new List<CardAction> { cardAction }
            };

            return heroCard.ToAttachment();
        }
    }
}