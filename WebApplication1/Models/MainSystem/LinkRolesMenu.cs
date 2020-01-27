using System;
using System.Collections.Generic;

namespace WebApplication1.Models.MainSystem
{
    public partial class LinkRolesMenu
    {
        public int Id { get; set; }
        public int RolesId { get; set; }
        public int MenusId { get; set; }

        public Menus Menus { get; set; }
    }
}
