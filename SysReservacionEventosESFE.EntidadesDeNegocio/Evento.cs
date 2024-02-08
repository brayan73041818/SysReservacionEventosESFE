using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.EntidadesDeNegocio
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEvento { get; set; }

        [Required(ErrorMessage = "Responsable es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 100 caracteres")]
        public string? Responsable { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 100 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Numero de asistentes es obligatorio")]
        public int? NAsistentes { get; set; }

        [Display(Name = "Fecha y Hora de fin")]
        public TimeSpan HoraFin { get; set; }

        [Display(Name = "Fecha y Hora de inicio")]
        public TimeSpan HoraInicio { get; set; }

        [Display(Name = "Fecha y Hora de inicio")]
        public DateOnly FechaEvento { get; set; }

        [ForeignKey("EspaciosA")]
        [Required(ErrorMessage = "Espacio es obligatorio")]
        [Display(Name = "Espacios")]
        public int IdEspaciosA { get; set; }

        [ForeignKey("Institucion")]
        [Required(ErrorMessage = "Institucion es obligatorio")]
        [Display(Name = "Carrera")]
        public int IdInstitucion { get; set; }

        [ForeignKey("Carrera")]
        [Required(ErrorMessage = "Carrera es obligatorio")]
        [Display(Name = "Carrera")]
        public int IdCarrera { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public Carrera Carrera { get; set; }

        public Institucion Institucion { get; set; }

        public EspaciosA EspaciosA { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
