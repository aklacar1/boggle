using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoggleREST
{
    public partial class GameLetters
    {
        public long Id { get; set; }
        public long GameRoomId { get; set; }
        public string Letter { get; set; }
        public int Position { get; set; }

        public virtual GameRoom GameRoom { get; set; }
    }
}
