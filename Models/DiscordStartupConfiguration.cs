using DSharpPlus.CommandsNext;
using DSharpPlus;
using Microsoft.Extensions.Options;
using DSharpPlus.SlashCommands;

namespace FredBot.Models;

public class DiscordStartupConfiguration(
    IOptions<DiscordConfiguration> discordConfiguration, 
    IOptions<CommandsNextConfiguration> commandsNextConfiguration, 
    IOptions<SlashCommandsConfiguration> slashCommandsConfiguration)
{
    public SlashCommandsConfiguration SlashCommandsConfiguration { get; } = slashCommandsConfiguration.Value;
    public DiscordConfiguration DiscordConfiguration { get; } = discordConfiguration.Value;
    public CommandsNextConfiguration CommandsNextConfiguration { get; } = commandsNextConfiguration.Value;
}
