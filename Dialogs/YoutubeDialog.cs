using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Bot.Sample.LuisBot
{
    [Serializable]
    public class YoutubeDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            var replyMessage = context.MakeMessage();

            var attachment = GetinternetYoutubeAttachment();
            replyMessage.Text = "Video from Youtube";

            replyMessage.Attachments = new List<Attachment> { attachment };

            JsonConvert.SerializeObject(context.PostAsync(replyMessage));

            return Task.CompletedTask;
        }

        public static Attachment GetinternetYoutubeAttachment()
        {
            return new Attachment
            {
                ContentType = "video/mp4",
                ContentUrl = "https://youtu.be/RaNDktMQVWI"
            };
        }
    }
}