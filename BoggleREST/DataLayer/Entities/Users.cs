using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BoggleREST
{
    public partial class Users : IdentityUser
    {
        public virtual ICollection<FirebaseTokens> FirebaseTokens { get; set; }
        public virtual ICollection<GameParticipants> GameParticipants { get; set; }
    }
}
