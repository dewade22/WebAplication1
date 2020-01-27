using System;
using System.Collections.Generic;

namespace WebApplication1.Models.MainSystem
{
    public partial class AspNetUserTokens
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
