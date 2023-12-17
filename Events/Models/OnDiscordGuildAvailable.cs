using DSharpPlus;
using DSharpPlus.EventArgs;

namespace FredBot.Events.Models;
public class OnDiscordGuildAvailable : MediatR.INotification
{
    public DiscordClient Sender { get; }
    public GuildCreateEventArgs Args { get; }

    public OnDiscordGuildAvailable(DiscordClient sender, GuildCreateEventArgs args)
    {
        Sender = sender;
        Args = args;
    }
}