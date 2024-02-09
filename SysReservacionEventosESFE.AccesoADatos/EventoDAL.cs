using Microsoft.EntityFrameworkCore;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.AccesoADatos
{
    public class EventoDAL
    {

        public static async Task<int> CrearAsync(Evento pEvento)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pEvento);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Evento pEvento)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Evento = await bdContexto.Evento.FirstOrDefaultAsync(s => s.IdEvento == pEvento.IdEvento);
                Evento.IdCarrera = pEvento.IdCarrera;
                Evento.IdEspaciosA = pEvento.IdEspaciosA;
                Evento.IdInstitucion = pEvento.IdInstitucion;
                Evento.IdUsuario = pEvento.IdUsuario;
                Evento.HoraFin = pEvento.HoraFin;
                Evento.HoraInicio = pEvento.HoraInicio;
                Evento.Responsable = pEvento.Responsable;
                Evento.Descripcion = pEvento.Descripcion;
                Evento.NAsistentes = pEvento.NAsistentes;
                bdContexto.Update(Evento);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Evento pEvento)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Evento = await bdContexto.Evento.FirstOrDefaultAsync(s => s.IdEvento == pEvento.IdEvento);
                bdContexto.Evento.Remove(Evento);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Evento> ObtenerPorIdAsync(Evento pEvento)
        {
            var Evento = new Evento();
            using (var bdContexto = new BDContexto())
            {
                Evento = await bdContexto.Evento.FirstOrDefaultAsync(s => s.IdEvento == pEvento.IdEvento);
            }
            return Evento;
        }
        public static async Task<List<Evento>> ObtenerTodosAsync()
        {
            var Eventos = new List<Evento>();
            using (var bdContexto = new BDContexto())
            {
                Eventos = await bdContexto.Evento.Include(s => s.EspaciosA).ToListAsync();
            }
            return Eventos;
        }
        internal static IQueryable<Evento> QuerySelect(IQueryable<Evento> pQuery, Evento pEvento)
        {
            if (pEvento.IdEvento > 0)
                pQuery = pQuery.Where(s => s.IdEvento == pEvento.IdEvento);
            if (pEvento.IdCarrera > 0)
                pQuery = pQuery.Where(s => s.IdCarrera == pEvento.IdCarrera);
            if (pEvento.IdInstitucion > 0)
                pQuery = pQuery.Where(s => s.IdInstitucion == pEvento.IdInstitucion);
            if (pEvento.IdEspaciosA > 0)
                pQuery = pQuery.Where(s => s.IdEspaciosA == pEvento.IdEspaciosA);
            if (pEvento.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pEvento.IdUsuario);
            if (pEvento.NAsistentes > 0)
                pQuery = pQuery.Where(s => s.NAsistentes == pEvento.NAsistentes);
            if (!string.IsNullOrWhiteSpace(pEvento.Responsable))
                pQuery = pQuery.Where(s => s.Responsable.Contains(pEvento.Responsable));
            pQuery = pQuery.OrderByDescending(s => s.IdEvento).AsQueryable();
            if (pEvento.Top_Aux > 0)
                pQuery = pQuery.Take(pEvento.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Evento>> BuscarAsync(Evento pEvento)
        {
            var Eventos = new List<Evento>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Evento.AsQueryable();
                select = QuerySelect(select, pEvento);
                Eventos = await select.ToListAsync();
            }
            return Eventos;
        }

        public static async Task<List<Evento>> BuscarIncluirUsuarioCarreraInstitucionEspaciosAAsync(Evento pEvento)
        {
            var Evento = new List<Evento>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Evento.AsQueryable();
                select = QuerySelect(select, pEvento).Include(s => s.Usuario).Include(s => s.Carrera).Include(s => s.Institucion).Include(s => s.EspaciosA).AsQueryable();
                Evento = await select.ToListAsync();
            }
            return Evento;
        }

    }
}
