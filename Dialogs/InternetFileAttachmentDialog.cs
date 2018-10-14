using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Bot.Sample.LuisBot
{
    [Serializable]
    public class InternetFileAttachmentDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            var replyMessage = context.MakeMessage();

            var attachment = GetInternetFileAttachment();
            replyMessage.Text = "PDF from the internet";

            replyMessage.Attachments = new List<Attachment> { attachment };

            JsonConvert.SerializeObject(context.PostAsync(replyMessage));

            return Task.CompletedTask;
        }

        public static Attachment GetInternetFileAttachment()
        {
            return new Attachment
            {
                ContentType = "application/pdf",
                ContentUrl = "https://qconlondon.com/london-2017/system/files/presentation-slides/microsoft_bot_framework_best_practices.pdf",
                Name = "Microsoft Bot Framework Best Practices"
            };
        }
    }
}