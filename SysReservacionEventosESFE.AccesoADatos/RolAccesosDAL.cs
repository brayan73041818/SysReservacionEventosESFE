using Microsoft.EntityFrameworkCore;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysReservacionEventosESFE.AccesoADatos
{
    public class RolAccesosDAL
    {
        public static async Task<int> CrearAsync(RolAccesos pRolAccesos)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pRolAccesos);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(RolAccesos pRolAccesos)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var RolAccesos = await bdContexto.RolAccesos.FirstOrDefaultAsync(s => s.IdRolAcceso == pRolAccesos.IdRolAcceso);
                bdContexto.Update(RolAccesos);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(RolAccesos pRolAccesos)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var RolAccesos = await bdContexto.RolAccesos.FirstOrDefaultAsync(s => s.IdRolAcceso == pRolAccesos.IdRolAcceso);
                bdContexto.RolAccesos.Remove(RolAccesos);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<RolAccesos> ObtenerPorIdAsync(RolAccesos pRolAccesos)
        {
            var RolAccesos = new RolAccesos();
            using (var bdContexto = new BDContexto())
            {
                RolAccesos = await bdContexto.RolAccesos.FirstOrDefaultAsync(s => s.IdRolAcceso == pRolAccesos.IdRolAcceso);
            }
            return RolAccesos;
        }
        public static async Task<List<RolAccesos>> ObtenerTodosAsync()
        {
            var RolAccesoses = new List<RolAccesos>();
            using (var bdContexto = new BDContexto())
            {
                RolAccesoses = await bdContexto.RolAccesos.Include(r =>r.Rol).Include(r => r.Accesos).ToListAsync();
            }
            return RolAccesoses;
        }
        internal static IQueryable<RolAccesos> QuerySelect(IQueryable<RolAccesos> pQuery, RolAccesos pRolAccesos)
        {
            if (pRolAccesos.IdRolAcceso > 0)
                pQuery = pQuery.Where(s => s.IdRolAcceso == pRolAccesos.IdRolAcceso);
            if (pRolAccesos.IdRol > 0)
                pQuery = pQuery.Where(s => s.IdRol == pRolAccesos.IdRol);
            if (pRolAccesos.IdAcceso > 0)
                pQuery = pQuery.Where(s => s.IdAcceso == pRolAccesos.IdAcceso);
            pQuery = pQuery.OrderByDescending(s => s.IdRolAcceso).AsQueryable();
            if (pRolAccesos.Top_Aux > 0)
                pQuery = pQuery.Take(pRolAccesos.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<RolAccesos>> BuscarAsync(RolAccesos pRolAccesos)
        {
            var RolAccesoses = new List<RolAccesos>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.RolAccesos.AsQueryable();
                select = QuerySelect(select, pRolAccesos);
                RolAccesoses = await select.ToListAsync();
            }
            return RolAccesoses;
        }

        public static async Task<List<RolAccesos>> BuscarIncluirAccesoYRolAsync(RolAccesos pRolAccesos)
        {
            var RolAccesos = new List<RolAccesos>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.RolAccesos.AsQueryable();
                select = QuerySelect(select, pRolAccesos).Include(s => s.Accesos).Include(s => s.Rol).AsQueryable();
                RolAccesos = await select.ToListAsync();
            }
            return RolAccesos;
        }
    }
}
