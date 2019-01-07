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
    [Table("Posts")]
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }

        [Required(ErrorMessage = "O Titulo é obrigatório")]
        [DisplayName("Titulo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O Conteudo do post é obrigatório")]
        [DisplayName("Conteudo")]
        public string Content { get; set; }
        
        //[ScaffoldColumn(false)] // O scaffolding ignora a coluna para criação de views
        [DisplayName("Horario Criado")]
        [Required]
        public DateTime CreateTime { get; set; }
        
        //[ScaffoldColumn(false)] // O scaffolding ignora a coluna para criação de views
        [DisplayName("Horario Atualizado")]
        [Required]
        public DateTime UpdateTime { get; set; }

        [DisplayName("Tags")]
        public string Tag { get; set; }

        [Required(ErrorMessage = "O campo categoria é requerido!!")]
        [DisplayName("Categoria")]
        public Guid CategoryId { get; set; }

        public virtual Category Categories { get; set; }

        [Required(ErrorMessage = "O campo Usuário é requerido!!")]
        public Guid UserId { get; set; }

        [DisplayName("Usuário")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Imagens")]
        [DataType(DataType.ImageUrl)]
        public string ImageFile { get; set; }
    }
}
