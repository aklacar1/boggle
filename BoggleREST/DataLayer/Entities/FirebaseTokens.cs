﻿using System;
using System.Collections.Generic;

namespace BoggleREST
{
    public partial class FirebaseTokens
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }

        public virtual Users User { get; set; }
    }
}
