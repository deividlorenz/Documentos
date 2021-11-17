using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Documentos.Models
{
    public class DocumentoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Código do Documento")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Título do Documento")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Revisão do Documento")]
        public string Revisao { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Data do Documento")]
        public DateTime DataRevisao { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Valor do Documento")]
        public float Valor { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Nome do Documento")]
        public string FileName { get; set; }



    }
}