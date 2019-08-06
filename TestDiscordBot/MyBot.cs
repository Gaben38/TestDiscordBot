using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace TestDiscordBot
{
    public class MyBot
    {
        protected CommandsNextExtension Commands { get; set; }
        protected DiscordClient Client { get; set; }
        public MyBot(string token)
        {
            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            Commands = Client.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { "!" }
            });

            Commands.RegisterCommands<MyCommands>();

            Client.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("test"))
                    await e.Message.RespondAsync("yeet");
            };
        }

        public async Task Run() => await Client.ConnectAsync();
    }
}
