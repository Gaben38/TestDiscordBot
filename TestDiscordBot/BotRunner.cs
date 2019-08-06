using System;
using System.Threading.Tasks;
using System.IO;

namespace TestDiscordBot
{
    public class BotRunner
    {
        public static string tokenFilename = "token.txt";
        public static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(true).GetAwaiter().GetResult();
        }
        public static async Task MainAsync(string[] args)
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
            var bot = new MyBot(tokenStr[0]);
            await bot.Run();
            await Task.Delay(-1);
        }
    }
}
