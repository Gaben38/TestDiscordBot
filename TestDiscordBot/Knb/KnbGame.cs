using MyDiscordBot.Models;
using System;

namespace MyDiscordBot.Knb
{
    public static class KnbGame
    {
        public static KnbResult Play()
        {
            var result = new KnbResult();
            Random rnd = new Random();
            result.Player1Gesture = (KnbGesture)rnd.Next((int)KnbGesture.Rock, (int)KnbGesture.Scissors + 1);
            result.Player2Gesture = (KnbGesture)rnd.Next((int)KnbGesture.Rock, (int)KnbGesture.Scissors + 1);

            if (result.Player1Gesture == result.Player2Gesture)
            {
                result.Result = KnbResultType.Draw;
            }
            else
            {
                int max = Math.Max((int)result.Player1Gesture, (int)result.Player2Gesture);
                int min = Math.Min((int)result.Player1Gesture, (int)result.Player2Gesture);

                int delta = max - min;

                if (delta == 1)
                {
                    if ((int)result.Player1Gesture == min)
                    {
                        result.Result = KnbResultType.Player1Won;
                    }
                    else
                    {
                        result.Result = KnbResultType.Player2Won;
                    }
                }
                else
                {
                    if ((int)result.Player1Gesture == max)
                    {
                        result.Result = KnbResultType.Player1Won;
                    }
                    else
                    {
                        result.Result = KnbResultType.Player2Won;
                    }
                }
            }

            return result;
        }
    }
}
