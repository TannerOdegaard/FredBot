//#define USE_SLASH_COMMANDS
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using DSharpPlus;
using System.Reflection;
using DSharpPlus.Interactivity.Extensions;
using MediatR;
using FredBot.Models;
using System.Diagnostics;
using FredBot.Commands.Converters;
using FredBot.Events.Models;

namespace FredBot.Services;

public class DiscordBot : IHostedService
{
    readonly ILogger<DiscordBot> _logger;
    readonly DiscordClient _client;
    readonly CommandsNextExtension _commands;
    readonly SlashCommandsExtension _slash;
    readonly IMediator _mediator;
    readonly LeagueCustomsService _leagueCustomsService;

    public DiscordBot(ILogger<DiscordBot> logger,
        DiscordStartupConfiguration config, 
        IMediator mediator, 
        LeagueCustomsService leagueCustomsService)
    {
        _logger = logger;
        _mediator = mediator;
        _leagueCustomsService = leagueCustomsService;
        _client = new(config.DiscordConfiguration);
        _commands = _client.UseCommandsNext(config.CommandsNextConfiguration);
        _client.UseInteractivity();
        _commands.RegisterCommands(Assembly.GetExecutingAssembly());
        _commands.RegisterConverter(new CustomArgumentConverter());


#if USE_SLASH_COMMANDS
         _slash = _client.UseSlashCommands(config.SlashCommandsConfiguration);
         _slash.RegisterCommands(Assembly.GetExecutingAssembly());
#endif
        RegisterEventHandlers();
        
    }
    private void RegisterEventHandlers()
    {
        _client.ComponentInteractionCreated += async (sender, args) =>
        {
            await Console.Out.WriteLineAsync("----");
            await _leagueCustomsService.OnButtonClick(args);
        };
        _client.GuildAvailable              += (sender, args) => _mediator.Publish(new OnDiscordGuildAvailable(sender, args));
        _client.MessageCreated              += (sender, args) => _mediator.Publish(new OnDiscordMessageCreated(sender, args));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var started = Stopwatch.GetTimestamp();
        _logger.LogInformation("Discord bot starting..");
        await _client.ConnectAsync();
        _logger.LogInformation("Took {ms} ms to start.", Stopwatch.GetElapsedTime(started).Milliseconds);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var started = Stopwatch.GetTimestamp();
        _logger.LogInformation("Discord bot stopping..");
        await _client.DisconnectAsync();
        _logger.LogInformation("Took {ms} ms to stop.", Stopwatch.GetElapsedTime(started).Milliseconds);
        _client.Dispose();
    }
}
