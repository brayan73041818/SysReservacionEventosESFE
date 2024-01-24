
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysReservacionEventosESFE.AccesoADatos;
using SysReservacionEventosESFE.EntidadesDeNegocio;

namespace SysReservacionEventosESFE.LogicaDeNegocio
{
    public class CarreraBL
    {
        public async Task<int> CrearAsync(Carrera pCarrera)
        {
            return await CarreraDAL.CrearAsync(pCarrera);
        }
        public async Task<int> ModificarAsync(Carrera pCarrera)
        {
            return await CarreraDAL.ModificarAsync(pCarrera);
        }
        public async Task<int> EliminarAsync(Carrera pCarrera)
        {
            return await CarreraDAL.EliminarAsync(pCarrera);
        }
        public async Task<Carrera> ObtenerPorIdAsync(Carrera pCarrera)
        {
            return await CarreraDAL.ObtenerPorIdAsync(pCarrera);
        }
        public async Task<List<Carrera>> ObtenerTodosAsync()
        {
            return await CarreraDAL.ObtenerTodosAsync();
        }
        public async Task<List<Carrera>> BuscarAsync(Carrera pCarrera)
        {
            return await CarreraDAL.BuscarAsync(pCarrera);
        }
    }
}

