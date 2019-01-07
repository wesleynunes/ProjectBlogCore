using ProjectBlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogCore.Models.ViewModels
{
    public class ClaimViewModel
    {
        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
