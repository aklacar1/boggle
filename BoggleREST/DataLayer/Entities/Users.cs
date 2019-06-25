using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BoggleREST
{
    public partial class Users : IdentityUser
    {
        internal virtual ICollection<FirebaseTokens> FirebaseTokens { get; set; }
        internal virtual ICollection<GameParticipants> GameParticipants { get; set; }
        internal virtual ICollection<GameWords> GameWords { get; set; }
    }
}
