using System.Diagnostics;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using FredBot.Services;

namespace FredBot.Commands.SlashCommands;

[SlashModuleLifespan(SlashModuleLifespan.Singleton)]
public class AdminSlashCommands : ApplicationCommandModule
{

    private readonly LeagueCustomsService _customsService;
    private readonly ILogger<AdminSlashCommands> _logger;
    long StartTime;

    public AdminSlashCommands(LeagueCustomsService customsService, ILogger<AdminSlashCommands> logger)
    {
        _customsService = customsService;
        _logger = logger;
    }
    public override Task<bool> BeforeSlashExecutionAsync(InteractionContext ctx)
    {
        StartTime = Stopwatch.GetTimestamp();
        return base.BeforeSlashExecutionAsync(ctx);
    }

    public override Task AfterSlashExecutionAsync(InteractionContext ctx)
    {
        _logger.LogInformation("Command {cmd} took {seconds} seconds to execute ", ctx.CommandName, Stopwatch.GetElapsedTime(StartTime).Seconds);
        return base.AfterSlashExecutionAsync(ctx);
    }




    [SlashCommand("merge", "merge team 1 and 2 into a lobby")]
    public async Task MergeCommand(InteractionContext ctx)
    {
        await _customsService.MergeChannels(ctx.Guild);
    }

    [SlashCommand("team", "set team")]
    public async Task SetTeamsChannel(InteractionContext ctx, DiscordChannel channel)
    {
    }

    public void RandomizeTeams(InteractionContext ctx)
    {

    }
}
