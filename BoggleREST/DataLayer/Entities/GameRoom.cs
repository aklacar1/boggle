using System;
using System.Collections.Generic;

namespace BoggleREST
{
    public partial class GameRoom
    {
        public GameRoom()
        {
            GameLetters = new HashSet<GameLetters>();
            GameParticipants = new HashSet<GameParticipants>();
            GameWords = new HashSet<GameWords>();
        }

        public long Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        internal virtual ICollection<GameLetters> GameLetters { get; set; }
        internal virtual ICollection<GameParticipants> GameParticipants { get; set; }
        internal virtual ICollection<GameWords> GameWords { get; set; }
    }
}
