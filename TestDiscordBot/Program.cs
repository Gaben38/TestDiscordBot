using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace TestDiscordBot
{
    class Program
    {
        static CommandsNextModule commands;
        static DiscordClient discord;
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "NDI2NzgzNzk2MDE3NDk2MDgy.DZbcdw.NR8BxdjEcso0oa6ZfGKt1UIljLQ",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });

            commands.RegisterCommands<MyCommands>();

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("test"))
                    await e.Message.RespondAsync("хуй");
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
