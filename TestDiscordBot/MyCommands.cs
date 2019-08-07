using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MyDiscordBot.Infrastructure;
using MyDiscordBot.Knb;
using MyDiscordBot.Models;
using MyDiscordBot.Utility;

namespace TestDiscordBot
{
    public class MyCommands : BaseCommandModule
    {
        public int KnbSessionWaitTime { get; } = 30;
        private KnbState KnbState
        {
            get
            {
                lock (_stateLock)
                {
                    return _knbState;
                }
            }
            set
            {
                lock (_stateLock)
                {
                    _knbState = value;
                }
            }
        }

        private KnbState _knbState = null;
        private readonly object _stateLock = new object();

        [Command("hi")]
        [Description("Поздороваться с ботом")]
        public async Task HiCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Здарова, {ctx.User.Mention}!");
        }

        [Command("goodshit")]
        [Description("👌👌")]
        public async Task GoodShitCommand(CommandContext ctx)
        {
            var msg = "birds that bob their head back and forth and peep really fast when they fly away 👌👀👌👀👌👀👌👀👌👀 good shit go౦ԁ sHit👌 thats ✔ some good👌👌shit right👌👌th 👌 ere👌👌👌 right✔there ✔✔if i do ƽaү so my selｆ 💯  i say so 💯  thats what im talking about right there right there (chorus: ʳᶦᵍʰᵗ ᵗʰᵉʳᵉ) mMMMMᎷМ💯 👌👌 👌НO0ОଠＯOOＯOОଠଠOoooᵒᵒᵒᵒᵒᵒᵒᵒᵒ👌 👌👌 👌 💯 👌 👀 👀 👀 👌👌Good shit";
            await ctx.RespondAsync(msg);
        }

        [Command("N")]
        public async Task BruhCommand(CommandContext ctx)
        {
            var emoji = DiscordEmoji.FromName(ctx.Client, ":HYPERBRUH:");
            await ctx.RespondAsync($"{ctx.User.Mention}, {emoji}");
        }

        [Command("roll")]
        [Description("Случайное число в диапазоне")]
        public async Task RollCommand(CommandContext ctx, int min = 0, int max = 100)
        {
            if (min > max)
            {
                await ctx.RespondAsync($"Неверный диапазон чисел.");
            }
            else
            {
                var rnd = new Random();
                await ctx.RespondAsync($"🎲 Ролл: {rnd.Next(min, max + 1)}, {ctx.User.Mention}");
            }
        }

        [Command("knbSp")]
        [Description("Камень-ножницы-бумага с ботом")]
        public async Task KnbSinglePlayerCommand(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var result = KnbGame.Play();
            var response = FormKnbResultResponse(result, ctx.User.Mention, "Бот");

            try
            {
                await ctx.RespondAsync(response);
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        [Command("knb")]
        [Description("Камень-ножницы-бумага на двоих")]
        public async Task KnbMultiPlayerCommand(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            if (KnbState == null)
            {
                KnbState = new KnbState(ctx.Member, new CancellationTokenSource());
                await ctx.RespondAsync($"Player 1 = {KnbState.FirstPlayer.Mention}. Ждем второго игрока {KnbSessionWaitTime} секунд. Для присоединения напишите !knb2p.");

                await Task.Run(() => ResetPlayer(ctx, TimeSpan.FromSeconds(KnbSessionWaitTime), KnbState.Cts.Token));
                return;
            }
            else
            {
                KnbState.Cts.Cancel();

                if (KnbState.FirstPlayer.Id == ctx.Member.Id)
                {
                    await ctx.RespondAsync("Необходим второй игрок! Игровая сессия отменена.");
                }
                else
                {                   
                    var result = KnbGame.Play();
                    var response = FormKnbResultResponse(result, KnbState.FirstPlayer.Mention, ctx.User.Mention);

                    try
                    {
                        await ctx.RespondAsync(response);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                KnbState = null;
            }
        }

        [Command("xD")]
        [Description("Иск де")]
        public async Task JdCommand(CommandContext ctx)
        {
            var msg = "😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂\n😂🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒😂\n😂🆒💯🆒🆒🆒💯🆒💯💯💯🆒🆒🆒😂\n😂🆒💯💯🆒💯💯🆒💯🆒💯💯🆒🆒😂\n😂🆒🆒💯🆒💯🆒🆒💯🆒🆒💯💯🆒😂\n😂🆒🆒💯💯💯🆒🆒💯🆒🆒🆒💯🆒😂\n😂🆒🆒🆒💯🆒🆒🆒💯🆒🆒🆒💯🆒😂\n😂🆒🆒💯💯💯🆒🆒💯🆒🆒🆒💯🆒😂\n😂🆒🆒💯🆒💯🆒🆒💯🆒🆒💯💯🆒😂\n😂🆒💯💯🆒💯💯🆒💯🆒💯💯🆒🆒😂\n😂🆒💯🆒🆒🆒💯🆒💯💯💯🆒🆒🆒😂\n😂🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒😂\n😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂";

            await ctx.TriggerTypingAsync();

            try
            {
                await ctx.RespondAsync(msg);
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Command("version")]
        [Description("Вывод версии бота.")]
        public async Task VersionCommand(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var asmVersion = VersionHelper.AssemblyVersion;

            try
            {
                await ctx.RespondAsync($"Версия бота: {asmVersion}");
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Command("news")]
        [Description("Свежак с медузы.")]
        public async Task NewsCommand(CommandContext ctx, int? articleCount = 30)
        {
            await ctx.TriggerTypingAsync();

            if (articleCount == null)
                articleCount = 30;
            var newsReader = new MeduzaNewsReader();
            var news = await newsReader.GetNewsAsync(articleCount);

            var response = "";
            foreach (var article in news)
            {
                response += $"**{article.Title.Text}**" + Environment.NewLine;
                response += $"`{article.Summary.Text}`" + Environment.NewLine;
                response += $"`Фулл: {article.Links[0].Uri}`" + Environment.NewLine;
                response += Environment.NewLine;
            }

            await ctx.RespondAsync(response);
        }

        private async Task ResetPlayer(CommandContext ctx, TimeSpan delay, CancellationToken tkn)
        {
            try
            {
                await Task.Delay(delay, tkn);
                KnbState = null;
                await ctx.RespondAsync("Прошел период ожидания второго игрока. Игровая сессия отменена.");
            }
            catch (OperationCanceledException)
            {

            }

            return;
        }

        private string FormKnbResultResponse(KnbResult result, string player1Name, string player2Name)
        {
            var response = "";
            response += $"{player1Name}: {result.Player1Gesture.ToString()}" + Environment.NewLine;
            response += $"{player2Name}: {result.Player2Gesture.ToString()}" + Environment.NewLine;

            switch (result.Result)
            {
                case KnbResultType.Player1Won: response += $"Победил {player1Name}!"; break;
                case KnbResultType.Player2Won: response += $"Победил {player2Name}!"; break;
                case KnbResultType.Draw: response += $"Ничья!"; break;
                default: response += "Шото пошло не так. Не смог вычислить победителя."; break;
            }

            return response;
        }
    }
}
