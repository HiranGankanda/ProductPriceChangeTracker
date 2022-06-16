﻿using Microsoft.AspNetCore.Identity;
using System;

namespace PPCT.DataAccessLayer
{
    public class ApplicationUser : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
