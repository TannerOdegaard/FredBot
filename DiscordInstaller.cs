using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.SlashCommands;
using FredBot.Attributes;
using FredBot.Installers;
using FredBot.Models;
using FredBot.Services;
using Serilog;

[Installer(true, 1000)]
public class DiscordInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<DiscordConfiguration>().Configure(x => 
        {
            x.LoggerFactory = new LoggerFactory().AddSerilog();
            x.MinimumLogLevel = LogLevel.Warning;
            x.Token = config["Discord:Token"]!;
            x.Intents = DiscordIntents.All;
        });

        services.AddOptions<CommandsNextConfiguration>().Configure(x => 
        {
            var scope = services.BuildServiceProvider().CreateScope();

            x.PrefixResolver = (msg) => {


                return Task.FromResult(msg.GetStringPrefixLength("!"));
            };
            x.Services = scope.ServiceProvider;
        });

        services.AddOptions<SlashCommandsConfiguration>().Configure(x => { });

        services.AddSingleton<DiscordStartupConfiguration>();
        services.AddHostedService<DiscordBot>();


    }
}