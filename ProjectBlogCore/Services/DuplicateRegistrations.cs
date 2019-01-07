using Microsoft.AspNetCore.Mvc;
using ProjectBlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogCore.Services
{
    public class DuplicateRegistrations 
    {

        private readonly ApplicationDbContext _context;

        public DuplicateRegistrations(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CheckCategory(string name, Guid id)
        {
                var DoesExistcategory = (from u in _context.Categories
                                         where u.Name == name
                                         where u.CategoryId != id
                                         select u).FirstOrDefault();
                if (DoesExistcategory != null)
                    return true;
                else
                    return false;
        }
    }
}


