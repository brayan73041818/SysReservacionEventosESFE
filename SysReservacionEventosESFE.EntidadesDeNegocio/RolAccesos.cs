using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.EntidadesDeNegocio
{
    public class RolAccesos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRolAccesos { get; set; }

        public Rol Rol { get; set; }
        [ForeignKey("Rol")]
        [Required(ErrorMessage = "Rol es obligatorio")]
        [Display(Name = "Rol")]
        public int IdRol
        { get; set; }

        public Accesos Accesos { get; set; }
        [ForeignKey("Accesos")]
        [Required(ErrorMessage = "Accesos es obligatorio")]
        [Display(Name = "Accesos")]
        public int IdAccesos  { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
