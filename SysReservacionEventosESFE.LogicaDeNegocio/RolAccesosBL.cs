
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysReservacionEventosESFE.AccesoADatos;
using SysReservacionEventosESFE.EntidadesDeNegocio;

namespace SysReservacionEventosESFE.LogicaDeNegocio
{
    public class RolAccesosBL
    {
        public async Task<int> CrearAsync(RolAccesos pRolAccesos)
        {
            return await RolAccesosDAL.CrearAsync(pRolAccesos);
        }
        public async Task<int> ModificarAsync(RolAccesos pRolAccesos)
        {
            return await RolAccesosDAL.ModificarAsync(pRolAccesos);
        }
        public async Task<int> EliminarAsync(RolAccesos pRolAccesos)
        {
            return await RolAccesosDAL.EliminarAsync(pRolAccesos);
        }
        public async Task<RolAccesos> ObtenerPorIdAsync(RolAccesos pRolAccesos)
        {
            return await RolAccesosDAL.ObtenerPorIdAsync(pRolAccesos);
        }
        public async Task<List<RolAccesos>> ObtenerTodosAsync()
        {
            return await RolAccesosDAL.ObtenerTodosAsync();
        }
        public async Task<List<RolAccesos>> BuscarAsync(RolAccesos pRolAccesos)
        {
            return await RolAccesosDAL.BuscarAsync(pRolAccesos);
        }
        public async Task<List<RolAccesos>> BuscarIncluirAccesoYRolAsync(RolAccesos pRolAccesos)
        {
            return await RolAccesosDAL.BuscarIncluirAccesoYRolAsync(pRolAccesos);

        }
    }
}

