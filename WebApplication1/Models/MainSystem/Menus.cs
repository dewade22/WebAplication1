using System;
using System.Collections.Generic;

namespace WebApplication1.Models.MainSystem
{
    public partial class Menus
    {
        public Menus()
        {
            LinkRolesMenus = new HashSet<LinkRolesMenu>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }

        public ICollection<LinkRolesMenu> LinkRolesMenus { get; set; }
    }
}
