﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models.MainSystem
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
    }
}
