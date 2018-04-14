using System;
using System.Threading.Tasks;
using System.IO;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace TestDiscordBot
{
    class Program
    {
        static CommandsNextModule commands;
        static DiscordClient discord;
        const string tokenFilename = "token.txt";
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            string[] tokenStr;
            try
            {
                tokenStr = File.ReadAllLines(tokenFilename);
            }
            
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = tokenStr[0],
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
