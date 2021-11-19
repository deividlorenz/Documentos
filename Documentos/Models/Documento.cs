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
        public string Code { get; set; }

        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*")]
        public string Rev { get; set; }

        [Required(ErrorMessage = "*")]
        public string Planned_Date { get; set; }

        [Required(ErrorMessage = "*")]
        public float DocValue { get; set; }

        [Required(ErrorMessage = "*")]
        public string FileName { get; set; }

    }
}
