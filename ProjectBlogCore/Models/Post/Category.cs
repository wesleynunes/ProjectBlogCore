using ProjectBlogCore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogCore.Models.Post
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "O campo categoria é Obrigátorio")]
        [MaxLength(50, ErrorMessage = "O campo Categoria recebe no máximo 50 Caracteres")]
        [DisplayName("Categoria")]
        public string Name { get; set; }

        [DisplayName("Horario Criado")]
        public DateTime CreateTime { get; set; }

        [DisplayName("Horario Atualizado")]
        public DateTime UpdateTime { get; set; }

        [Required(ErrorMessage = "O campo Usuário é requerido!!")]        
        public Guid UserId { get; set; }

        [DisplayName("Usuário")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
      
    }
}
