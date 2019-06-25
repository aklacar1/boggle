using System;
using System.Collections.Generic;

namespace BoggleREST
{
    public partial class GameRoom
    {
        public GameRoom()
        {
            GameLetters = new HashSet<GameLetters>();
        }

        public long Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual ICollection<GameLetters> GameLetters { get; set; }
    }
}
