using DSharpPlus.Entities;
using System.Threading;

namespace MyDiscordBot.Models
{
    internal class KnbState
    {
        public DiscordMember FirstPlayer {
            get
            {
                lock (_lockTarget)
                {
                    return _firstPlayer;
                }
            }
            set
            {
                lock (_lockTarget)
                {
                    _firstPlayer = value;
                }
            }
        }
        public CancellationTokenSource Cts {
            get
            {
                lock (_lockTarget)
                {
                    return _cts;
                }
            }
            set
            {
                lock (_lockTarget)
                {
                    _cts = value;
                }
            }
        }

        public KnbState(DiscordMember firstPlayer, CancellationTokenSource cts)
        {
            FirstPlayer = firstPlayer;
            Cts = cts;
        }

        private DiscordMember _firstPlayer;
        private CancellationTokenSource _cts;
        private readonly object _lockTarget = new object();
    }
}
