using DSharpPlus;
using DSharpPlus.EventArgs;
using MediatR;

namespace FredBot.Events.Models;

public class OnGenericDiscordMessage : INotification
{
    public OnGenericDiscordMessage(DiscordClient sender, DiscordEventArgs args)
    {
        Sender = sender;
        Args = args;
    }

    public DiscordClient Sender { get; set; }
    public DiscordEventArgs Args { get; set; }
}
