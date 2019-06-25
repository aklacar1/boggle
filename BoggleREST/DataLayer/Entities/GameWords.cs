using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoggleREST
{
    public partial class GameWords
    {
        public long Id { get; set; }
        public string Word { get; set; }
        public long GameRoomId { get; set; }
        public string UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual GameRoom GameRoom { get; set; }
    }
}
