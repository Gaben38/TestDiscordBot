using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace TestDiscordBot
{
    public class MyCommands
    {
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
        [Description("Камень-ножницы-бумага")]
        public async Task Knb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            int value1, value2;
            Random rnd = new Random();
            value1 = rnd.Next(1, 4);
            value2 = rnd.Next(1, 4);
            string responce = "";

            switch (value1)
            {
                case 1: responce+=("Бот: Камень\n"); break;
                case 2: responce+=("Бот: Ножницы\n"); break;
                case 3: responce+=("Бот: Бумага\n"); break;
                default: break;
            }

            switch (value2)
            {
                case 1: responce+= ($"{ctx.User.Mention}: Камень\n"); break;
                case 2: responce+= ($"{ctx.User.Mention}: Ножницы\n"); break;
                case 3: responce+= ($"{ctx.User.Mention}: Бумага\n"); break;
                default: break;
            }

            if (value1 == value2)
            {
                responce+= ("Результат: Ничья!");
            }
            else
            {
                int diff = Math.Max(value1, value2) - Math.Min(value1, value2);
                if(diff == 1)
                {
                    if(value1 == Math.Min(value1, value2))
                    {
                        responce += ("Победил Бот!");
                    }
                    else
                    {
                        responce += ($"Победил {ctx.User.Mention}!");
                    }
                }
                else
                {
                    if (value1 == Math.Max(value1, value2))
                    {
                        responce += ("Победил Бот!");
                    }
                    else
                    {
                        responce += ($"Победил {ctx.User.Mention}!");
                    }
                }
            }

            try
            {
                await ctx.RespondAsync(responce);
            }


            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
    }
}
