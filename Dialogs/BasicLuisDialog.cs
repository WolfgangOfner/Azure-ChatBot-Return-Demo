using LuisBot.Helper;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"],
            ConfigurationManager.AppSettings["LuisAPIKey"],
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }

        [LuisIntent("InternetFile")]
        public async Task InternetDateiIntent(IDialogContext context, LuisResult result)
        {
            await context.Forward(new InternetFileAttachmentDialog(), null, context.Activity, CancellationToken.None);
        }

        [LuisIntent("InternetImage")]
        public async Task InternetBildIntent(IDialogContext context, LuisResult result)
        {
            await context.Forward(new InternetImageDialog(), null, context.Activity, CancellationToken.None);
        }

        [LuisIntent("InternetVideo")]
        public async Task InternetVideoIntent(IDialogContext context, LuisResult result)
        {
            await context.Forward(new InternetVideoDialog(), null, context.Activity, CancellationToken.None);
        }

        [LuisIntent("Mp3")]
        public async Task Mp3DialogIntent(IDialogContext context, LuisResult result)
        {
            await context.Forward(new Mp3Dialog(), null, context.Activity, CancellationToken.None);
        }

        [LuisIntent("Youtube")]
        public async Task YoutubeIntent(IDialogContext context, LuisResult result)
        {
            await context.Forward(new YoutubeDialog(), null, context.Activity, CancellationToken.None);
        }

        [LuisIntent("Bestseller")]
        public async Task BeststellerIntent(IDialogContext context, LuisResult result)
        {
            var gender = Category.Women;

            if (result.Entities[0] != null)
            {
                switch (result.Entities[0].Entity.ToLower())
                {
                    case "men":
                        gender = Category.Men;
                        break;
                    case "women":
                        gender = Category.Women;
                        break;
                }
            }

            await context.Forward(new CarouselCardsDialog(gender), null, context.Activity, CancellationToken.None);
        }
    }
}