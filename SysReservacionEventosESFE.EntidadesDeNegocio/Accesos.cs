using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.EntidadesDeNegocio
{
    public class Accesos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAccesos { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string? Nombre { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        public List<RolAccesos>? RolAccesos { get; set; }
    }
}
