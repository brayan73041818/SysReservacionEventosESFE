using Microsoft.EntityFrameworkCore;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SysReservacionEventosESFE.AccesoADatos
{
    public class InstitucionDAL
    {
        public static async Task<int> CrearAsync(Institucion pInstitucion)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pInstitucion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Institucion pInstitucion)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Institucion = await bdContexto.Institucion.FirstOrDefaultAsync(s => s.IdInstitucion == pInstitucion.IdInstitucion);
                Institucion.Nombre = pInstitucion.Nombre;
                bdContexto.Update(Institucion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Institucion pInstitucion)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Institucion = await bdContexto.Institucion.FirstOrDefaultAsync(s => s.IdInstitucion == pInstitucion.IdInstitucion);
                bdContexto.Institucion.Remove(Institucion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Institucion> ObtenerPorIdAsync(Institucion pInstitucion)
        {
            var Institucion = new Institucion();
            using (var bdContexto = new BDContexto())
            {
                Institucion = await bdContexto.Institucion.FirstOrDefaultAsync(s => s.IdInstitucion == pInstitucion.IdInstitucion);
            }
            return Institucion;
        }
        public static async Task<List<Institucion>> ObtenerTodosAsync()
        {
            var Instituciones = new List<Institucion>();
            using (var bdContexto = new BDContexto())
            {
                Instituciones = await bdContexto.Institucion.ToListAsync();
            }
            return Instituciones;
        }
        internal static IQueryable<Institucion> QuerySelect(IQueryable<Institucion> pQuery, Institucion pInstitucion)
        {
            if (pInstitucion.IdInstitucion > 0)
                pQuery = pQuery.Where(s => s.IdInstitucion == pInstitucion.IdInstitucion);
            if (!string.IsNullOrWhiteSpace(pInstitucion.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pInstitucion.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.IdInstitucion).AsQueryable();
            if (pInstitucion.Top_Aux > 0)
                pQuery = pQuery.Take(pInstitucion.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Institucion>> BuscarAsync(Institucion pInstitucion)
        {
            var Instituciones = new List<Institucion>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Institucion.AsQueryable();
                select = QuerySelect(select, pInstitucion);
                Instituciones = await select.ToListAsync();
            }
            return Instituciones;
        }
    }
}
