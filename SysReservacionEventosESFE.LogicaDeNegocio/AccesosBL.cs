
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysReservacionEventosESFE.AccesoADatos;
using SysReservacionEventosESFE.EntidadesDeNegocio;

namespace SysReservacionEventosESFE.LogicaDeNegocio
{
    public class AccesosBL
    {
        public async Task<int> CrearAsync(Accesos pAccesos)
        {
            return await AccesosDAL.CrearAsync(pAccesos);
        }
        public async Task<int> ModificarAsync(Accesos pAccesos)
        {
            return await AccesosDAL.ModificarAsync(pAccesos);
        }
        public async Task<int> EliminarAsync(Accesos pAccesos)
        {
            return await AccesosDAL.EliminarAsync(pAccesos);
        }
        public async Task<Accesos> ObtenerPorIdAsync(Accesos pAccesos)
        {
            return await AccesosDAL.ObtenerPorIdAsync(pAccesos);
        }
        public async Task<List<Accesos>> ObtenerTodosAsync()
        {
            return await AccesosDAL.ObtenerTodosAsync();
        }
        public async Task<List<Accesos>> BuscarAsync(Accesos pAccesos)
        {
            return await AccesosDAL.BuscarAsync(pAccesos);
        }
    }
}

