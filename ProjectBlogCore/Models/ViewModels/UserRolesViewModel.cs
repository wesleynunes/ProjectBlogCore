using ProjectBlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogCore.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public int UserRolesViewModelId { get; set; }

        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public string UserName { get; set; }
        
        public string Name { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole ApplicationRoles { get; set; }
    }
}
