using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Bot.Sample.LuisBot
{
    [Serializable]
    public class InternetVideoDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            var replyMessage = context.MakeMessage();

            var attachment = GetInternetVideoAttachment();
            replyMessage.Text = "Video from the internet";

            replyMessage.Attachments = new List<Attachment> { attachment };

            JsonConvert.SerializeObject(context.PostAsync(replyMessage));

            return Task.CompletedTask;
        }

        public static Attachment GetInternetVideoAttachment()
        {
            return new VideoCard("Build a great conversationalist", "Bot Demo Video",
                    "Build a great conversationalist",
                    media: new[] { new MediaUrl(@"https://bot-framework.azureedge.net/videos/skype-hero-050517-sm.mp4") })
                .ToAttachment();
        }
    }
}