using Microsoft.EntityFrameworkCore;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysReservacionEventosESFE.AccesoADatos
{
    public class AccesosDAL
    {
        public static async Task<int> CrearAsync(Accesos pAccesos)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pAccesos);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Accesos pAccesos)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Accesos = await bdContexto.Accesos.FirstOrDefaultAsync(s => s.IdAcceso == pAccesos.IdAcceso);
                Accesos.Nombre = pAccesos.Nombre;
                bdContexto.Update(Accesos);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Accesos pAccesos)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Accesos = await bdContexto.Accesos.FirstOrDefaultAsync(s => s.IdAcceso == pAccesos.IdAcceso);
                bdContexto.Accesos.Remove(Accesos);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Accesos> ObtenerPorIdAsync(Accesos pAccesos)
        {
            var Accesos = new Accesos();
            using (var bdContexto = new BDContexto())
            {
                Accesos = await bdContexto.Accesos.FirstOrDefaultAsync(s => s.IdAcceso == pAccesos.IdAcceso);
            }
            return Accesos;
        }
        public static async Task<List<Accesos>> ObtenerTodosAsync()
        {
            var Accesoses = new List<Accesos>();
            using (var bdContexto = new BDContexto())
            {
                Accesoses = await bdContexto.Accesos.ToListAsync();
            }
            return Accesoses;
        }
        internal static IQueryable<Accesos> QuerySelect(IQueryable<Accesos> pQuery, Accesos pAccesos)
        {
            if (pAccesos.IdAcceso > 0)
                pQuery = pQuery.Where(s => s.IdAcceso == pAccesos.IdAcceso);
            if (!string.IsNullOrWhiteSpace(pAccesos.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pAccesos.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.IdAcceso).AsQueryable();
            if (pAccesos.Top_Aux > 0)
                pQuery = pQuery.Take(pAccesos.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Accesos>> BuscarAsync(Accesos pAccesos)
        {
            var Accesoses = new List<Accesos>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Accesos.AsQueryable();
                select = QuerySelect(select, pAccesos);
                Accesoses = await select.ToListAsync();
            }
            return Accesoses;
        }
    }
}
