using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.AccesoADatos
{
    public class EspaciosADAL
    {

        public static async Task<int> CrearAsync(EspaciosA pEspaciosA)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pEspaciosA);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(EspaciosA pEspaciosA)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var EspaciosA = await bdContexto.EspaciosA.FirstOrDefaultAsync(s => s.IdEspaciosA == pEspaciosA.IdEspaciosA);
                EspaciosA.Nombre = pEspaciosA.Nombre;
                EspaciosA.Status = pEspaciosA.Status;
                bdContexto.Update(EspaciosA);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(EspaciosA pEspaciosA)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var EspaciosA = await bdContexto.EspaciosA.FirstOrDefaultAsync(s => s.IdEspaciosA == pEspaciosA.IdEspaciosA);
                bdContexto.EspaciosA.Remove(EspaciosA);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<EspaciosA> ObtenerPorIdAsync(EspaciosA pEspaciosA)
        {
            var EspaciosA = new EspaciosA();
            using (var bdContexto = new BDContexto())
            {
                EspaciosA = await bdContexto.EspaciosA.FirstOrDefaultAsync(s => s.IdEspaciosA == pEspaciosA.IdEspaciosA);
            }
            return EspaciosA;
        }
        public static async Task<List<EspaciosA>> ObtenerTodosAsync()
        {
            var EspaciosAes = new List<EspaciosA>();
            using (var bdContexto = new BDContexto())
            {
                EspaciosAes = await bdContexto.EspaciosA.ToListAsync();
            }
            return EspaciosAes;
        }
        internal static IQueryable<EspaciosA> QuerySelect(IQueryable<EspaciosA> pQuery, EspaciosA pEspaciosA)
        {
            if (pEspaciosA.IdEspaciosA > 0)
                pQuery = pQuery.Where(s => s.IdEspaciosA == pEspaciosA.IdEspaciosA);
            if (!string.IsNullOrWhiteSpace(pEspaciosA.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pEspaciosA.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.IdEspaciosA).AsQueryable();
            if (pEspaciosA.Top_Aux > 0)
                pQuery = pQuery.Take(pEspaciosA.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<EspaciosA>> BuscarAsync(EspaciosA pEspaciosA)
        {
            var EspaciosAes = new List<EspaciosA>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.EspaciosA.AsQueryable();
                select = QuerySelect(select, pEspaciosA);
                EspaciosAes = await select.ToListAsync();
            }
            return EspaciosAes;
        }
    }

}

