using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectBlogCore.Models.Post;
using ProjectBlogCore.Data;

namespace ProjectBlogCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {  
            base.OnModelCreating(builder);          


            //DESABILITAR CASCATAS
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            builder.Entity<ApplicationUser>(u =>
            {
                u.ToTable("Users"); // altera o nome da tabela
                u.Property(i => i.Id).HasColumnName("UserId");         
            });

            builder.Entity<ApplicationRole>(r =>
            {
                r.ToTable("Roles"); // altera o nome da tabela
                r.Property(i => i.Id).HasColumnName("RoleId");
            });

            builder.Entity<IdentityUserRole<Guid>>(ur =>
            {
                ur.ToTable("UsersRoles"); // altera o nome da tabela
            });

            builder.Entity<IdentityUserLogin<Guid>>(ul =>
            {
                ul.ToTable("UsersLogins"); // altera o nome da tabela 

            });

            builder.Entity<IdentityUserClaim<Guid>>(uc =>
            {
                uc.ToTable("UsersClaims"); // altera o nome da tabela
                uc.Property(i => i.Id).HasColumnName("UserClaimId");
            });

            builder.Entity<IdentityRoleClaim<Guid>>(rc =>
            {
                rc.ToTable("RolesClaims"); // altera o nome da tabela
                rc.Property(i => i.Id).HasColumnName("RoleClaimId");
            });

            builder.Entity<IdentityUserToken<Guid>>(ut =>
            {
                ut.ToTable("UsersTokens"); // altera o nome da tabela               
            });

            builder.Entity<Category>(c =>
            {
                //c.HasIndex(i => i.Name).HasName("Category_Name_Index").IsUnique(); // inserir campo unico e o nome do index
                c.HasIndex(i => i.Name).IsUnique();
                c.Property(i => i.Name).HasMaxLength(50);               
            });
            

            builder.Entity<Post>(p =>
            {
                p.HasIndex(i => i.Title).IsUnique();
                p.Property(i => i.Title).HasMaxLength(100);
            });

            //DESABILITAR CASCATAS
            //var cascadeFKs = builder.Model.GetEntityTypes()
            //    .SelectMany(t => t.GetForeignKeys())
            //    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            //foreach (var fk in cascadeFKs)
            //    fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

    }
}
