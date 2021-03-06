﻿using System;
using System.Collections.Generic;

namespace BoggleREST
{
    public partial class GameParticipants
    {
        public long Id { get; set; }
        public long GameRoomId { get; set; }
        public string UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual GameRoom GameRoom { get; set; }
    }
}
