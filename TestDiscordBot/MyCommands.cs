using System;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Здарова, {ctx.User.Mention}!");
        }

        [Command("goodshit")]
        [Description("👌👌")]
        public async Task GoodShit(CommandContext ctx)
        {
            string msg = "birds that bob their head back and forth and peep really fast when they fly away 👌👀👌👀👌👀👌👀👌👀 good shit go౦ԁ sHit👌 thats ✔ some good👌👌shit right👌👌th 👌 ere👌👌👌 right✔there ✔✔if i do ƽaү so my selｆ 💯  i say so 💯  thats what im talking about right there right there (chorus: ʳᶦᵍʰᵗ ᵗʰᵉʳᵉ) mMMMMᎷМ💯 👌👌 👌НO0ОଠＯOOＯOОଠଠOoooᵒᵒᵒᵒᵒᵒᵒᵒᵒ👌 👌👌 👌 💯 👌 👀 👀 👀 👌👌Good shit";
            await ctx.RespondAsync(msg);
        }

        [Command("N")]
        public async Task N(CommandContext ctx)
        {
            var emoji = DiscordEmoji.FromName(ctx.Client, ":HYPERBRUH:");
            await ctx.RespondAsync($"{ctx.User.Mention}, {emoji}");
        }

        [Command("roll")]
        [Description("Случайное число в диапазоне")]
        public async Task Random(CommandContext ctx, int min, int max)
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

        [Command("knb")]
        [Description("Камень-ножницы-бумага с ботом")]
        public async Task Knb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var result = KnbGame.Play();
            var responce = FormKnbResultResponce(result, ctx.User.Mention, "Бот");

            try
            {
                await ctx.RespondAsync(responce);
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        [Command("knb2p")]
        [Description("Камень-ножницы-бумага на двоих")]
        public async Task Knb2p(CommandContext ctx)
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
                    var responce = FormKnbResultResponce(result, KnbState.FirstPlayer.Mention, ctx.User.Mention);

                    try
                    {
                        await ctx.RespondAsync(responce);
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
        public async Task xD(CommandContext ctx)
        {
            string msg = "😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂\n😂🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒😂\n😂🆒💯🆒🆒🆒💯🆒💯💯💯🆒🆒🆒😂\n😂🆒💯💯🆒💯💯🆒💯🆒💯💯🆒🆒😂\n😂🆒🆒💯🆒💯🆒🆒💯🆒🆒💯💯🆒😂\n😂🆒🆒💯💯💯🆒🆒💯🆒🆒🆒💯🆒😂\n😂🆒🆒🆒💯🆒🆒🆒💯🆒🆒🆒💯🆒😂\n😂🆒🆒💯💯💯🆒🆒💯🆒🆒🆒💯🆒😂\n😂🆒🆒💯🆒💯🆒🆒💯🆒🆒💯💯🆒😂\n😂🆒💯💯🆒💯💯🆒💯🆒💯💯🆒🆒😂\n😂🆒💯🆒🆒🆒💯🆒💯💯💯🆒🆒🆒😂\n😂🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒🆒😂\n😂😂😂😂😂😂😂😂😂😂😂😂😂😂😂";

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
            string asmVersion = VersionHelper.AssemblyVersion;

            try
            {
                await ctx.RespondAsync($"Версия бота: {asmVersion}");
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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

        private string FormKnbResultResponce(KnbResult result, string player1Name, string player2Name)
        {
            string responce = "";
            responce += $"{player1Name}: {result.Player1Gesture.ToString()}" + Environment.NewLine;
            responce += $"{player2Name}: {result.Player2Gesture.ToString()}" + Environment.NewLine;

            switch (result.Result)
            {
                case KnbResultType.Player1Won: responce += $"Победил {player1Name}!"; break;
                case KnbResultType.Player2Won: responce += $"Победил {player2Name}!"; break;
                case KnbResultType.Draw: responce += $"Ничья!"; break;
                default: responce += "Шото пошло не так. Не смог вычислить победителя."; break;
            }

            return responce;
        }
    }
}
