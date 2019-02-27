using ProjectBlogCore.Data;
using System;

namespace ProjectBlogCore.Models.ViewModels
{
    public class UserClaimsViewModel
    {
        public int UserClaimId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }
               
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
