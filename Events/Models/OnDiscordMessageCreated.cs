using DSharpPlus;
using DSharpPlus.EventArgs;

namespace FredBot.Events.Models;

public class OnDiscordMessageCreated : MediatR.INotification
{
    public DiscordClient Sender { get; }
    public MessageCreateEventArgs Args { get; }


    public OnDiscordMessageCreated(DiscordClient sender, MessageCreateEventArgs args)
    {
        Sender = sender;
        Args = args;
    }


}
