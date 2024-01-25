using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.EntidadesDeNegocio
{
    public class Carrera
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCarrera { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string? Nombre { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Evento>? Evento { get; set; }
    }
}
