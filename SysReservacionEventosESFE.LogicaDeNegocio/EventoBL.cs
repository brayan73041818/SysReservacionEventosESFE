using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysReservacionEventosESFE.AccesoADatos;
using SysReservacionEventosESFE.EntidadesDeNegocio;

namespace SysReservacionEventosESFE.LogicaDeNegocio
{
     public class EventoBL
    {
        public async Task<int> CrearAsync(Evento pEvento)
        {
            return await EventoDAL.CrearAsync(pEvento);
        }
        public async Task<int> ModificarAsync(Evento pEvento)
        {
            return await EventoDAL.ModificarAsync(pEvento);
        }
        public async Task<int> EliminarAsync(Evento pEvento)
        {
            return await EventoDAL.EliminarAsync(pEvento);
        }
        public async Task<Evento> ObtenerPorIdAsync(Evento pEvento)
        {
            return await EventoDAL.ObtenerPorIdAsync(pEvento);
        }
        public async Task<List<Evento>> ObtenerTodosAsync()
        {
            return await EventoDAL.ObtenerTodosAsync();
        }
        public async Task<List<Evento>> BuscarAsync(Evento pEvento)
        {
            return await EventoDAL.BuscarAsync(pEvento);

        }
        public async Task<List<Evento>> BuscarIncluirUsuarioCarreraInstitucionEspaciosAAsync(Evento pEvento)
        {
            return await EventoDAL.BuscarIncluirUsuarioCarreraInstitucionEspaciosAAsync(pEvento);

        }
    }
}
