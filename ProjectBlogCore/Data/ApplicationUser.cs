using Microsoft.AspNetCore.Identity;
using ProjectBlogCore.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogCore.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
