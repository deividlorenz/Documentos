using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Documentos
{
    [Table("documentos")]
    public class Documento
    {
               
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "*")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "*")]
        public string Revisao { get; set; }

        [Required(ErrorMessage = "*")]
        public string DateRevisao { get; set; }

        [Required(ErrorMessage = "*")]
        public float Valor { get; set; }

        [Required(ErrorMessage = "*")]
        public string FileName { get; set; }

    }
}
