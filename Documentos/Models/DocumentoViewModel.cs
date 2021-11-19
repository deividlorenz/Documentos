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
        public string Code { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Título do Documento")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Revisão do Documento")]
        public string Rev { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Data do Documento")]
        public DateTime Planned_Date { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Valor do Documento")]
        public float DocValue { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Nome do Documento")]
        public string FileName { get; set; }



    }
}