﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.MainSystem
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }
    }
}