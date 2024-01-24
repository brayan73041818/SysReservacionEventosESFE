using Microsoft.EntityFrameworkCore;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysReservacionEventosESFE.AccesoADatos
{
    public class CarreraDAL
    {
        public static async Task<int> CrearAsync(Carrera pCarrera)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pCarrera);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Carrera pCarrera)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var carrera = await bdContexto.Carrera.FirstOrDefaultAsync(s => s.IdCarrera == pCarrera.IdCarrera);
                carrera.Nombre = pCarrera.Nombre;
                bdContexto.Update(carrera);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Carrera pCarrera)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var carrera = await bdContexto.Carrera.FirstOrDefaultAsync(s => s.IdCarrera == pCarrera.IdCarrera);
                bdContexto.Carrera.Remove(carrera);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Carrera> ObtenerPorIdAsync(Carrera pCarrera)
        {
            var carrera = new Carrera();
            using (var bdContexto = new BDContexto())
            {
                carrera = await bdContexto.Carrera.FirstOrDefaultAsync(s => s.IdCarrera == pCarrera.IdCarrera);
            }
            return carrera;
        }
        public static async Task<List<Carrera>> ObtenerTodosAsync()
        {
            var carreras = new List<Carrera>();
            using (var bdContexto = new BDContexto())
            {
                carreras = await bdContexto.Carrera.ToListAsync();
            }
            return carreras;
        }
        internal static IQueryable<Carrera> QuerySelect(IQueryable<Carrera> pQuery, Carrera pCarrera)
        {
            if (pCarrera.IdCarrera > 0)
                pQuery = pQuery.Where(s => s.IdCarrera == pCarrera.IdCarrera);
            if (!string.IsNullOrWhiteSpace(pCarrera.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pCarrera.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.IdCarrera).AsQueryable();
            if (pCarrera.Top_Aux > 0)
                pQuery = pQuery.Take(pCarrera.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Carrera>> BuscarAsync(Carrera pCarrera)
        {
            var carreras = new List<Carrera>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Carrera.AsQueryable();
                select = QuerySelect(select, pCarrera);
                carreras = await select.ToListAsync();
            }
            return carreras;
        }
    }
}
