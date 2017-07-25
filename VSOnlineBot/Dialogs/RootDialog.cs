using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace VSOnlineBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            var attachment = GetThumbnailCard();
            var message = context.MakeMessage();
            message.Attachments.Add(attachment);     
            await context.PostAsync(message);
            context.Wait(this.MessageReceivedAsync);

        }
        private static Attachment GetThumbnailCard()
        {
            var heroCard = new ThumbnailCard
            {
                Title = "Welcome to Itera VSOnline",
                Subtitle = "Build and release by chatting",
                Text = "Build and connect intelligent bots to interact with your users naturally wherever they are, from text/sms to Skype, Slack, Office 365 mail and other popular services.",
                Images = new List<CardImage> { new CardImage("http://www.itera.dk/Static/img/logo_social.png") },
                Buttons = new List<CardAction> { new CardAction(){ Title = "Build", Type=ActionTypes.ImBack, Value="Build" },
                        new CardAction(){ Title = "Release", Type=ActionTypes.ImBack, Value="Release" },
                        new CardAction(){ Title = "List", Type=ActionTypes.ImBack, Value="List" }
                    }
            };

            return heroCard.ToAttachment();
        }
    }
}