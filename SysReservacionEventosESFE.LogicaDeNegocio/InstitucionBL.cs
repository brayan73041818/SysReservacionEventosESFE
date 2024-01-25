using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysReservacionEventosESFE.AccesoADatos;
using SysReservacionEventosESFE.EntidadesDeNegocio;

namespace SysReservacionEventosESFE.LogicaDeNegocio
{
    public class InstitucionBL
    {
        public async Task<int> CrearAsync(Institucion pInstitucion)
        {
            return await InstitucionDAL.CrearAsync(pInstitucion);
        }
        public async Task<int> ModificarAsync(Institucion pInstitucion)
        {
            return await InstitucionDAL.ModificarAsync(pInstitucion);
        }
        public async Task<int> EliminarAsync(Institucion pInstitucion)
        {
            return await InstitucionDAL.EliminarAsync(pInstitucion);
        }
        public async Task<Institucion> ObtenerPorIdAsync(Institucion pInstitucion)
        {
            return await InstitucionDAL.ObtenerPorIdAsync(pInstitucion);
        }
        public async Task<List<Institucion>> ObtenerTodosAsync()
        {
            return await InstitucionDAL.ObtenerTodosAsync();
        }
        public async Task<List<Institucion>> BuscarAsync(Institucion pInstitucion)
        {
            return await InstitucionDAL.BuscarAsync(pInstitucion);
        }

    }
}
