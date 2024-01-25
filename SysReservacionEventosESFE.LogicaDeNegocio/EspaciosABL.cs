using SysReservacionEventosESFE.AccesoADatos;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.LogicaDeNegocio
{
    public class EspaciosABL
    {
        public async Task<int> CrearAsync(EspaciosA pEspaciosA)
        {
            return await EspaciosADAL.CrearAsync(pEspaciosA);
        }
        public async Task<int> ModificarAsync(EspaciosA pEspaciosA)
        {
            return await EspaciosADAL.ModificarAsync(pEspaciosA);
        }
        public async Task<int> EliminarAsync(EspaciosA pEspaciosA)
        {
            return await EspaciosADAL.EliminarAsync(pEspaciosA);
        }
        public async Task<EspaciosA> ObtenerPorIdAsync(EspaciosA pEspaciosA)
        {
            return await EspaciosADAL.ObtenerPorIdAsync(pEspaciosA);
        }
        public async Task<List<EspaciosA>> ObtenerTodosAsync()
        {
            return await EspaciosADAL.ObtenerTodosAsync();
        }
        public async Task<List<EspaciosA>> BuscarAsync(EspaciosA pEspaciosA)
        {
            return await EspaciosADAL.BuscarAsync(pEspaciosA);
        }
    }
}
