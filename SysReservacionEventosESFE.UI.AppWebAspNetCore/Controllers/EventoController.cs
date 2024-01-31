using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/********************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using SysReservacionEventosESFE.LogicaDeNegocio;

namespace SysReservacionEventosESFE.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class EventoController : Controller
    {
        EventoBL EventoBL = new EventoBL();
        CarreraBL CarreraBL = new CarreraBL();
        EspaciosABL EspaciosABL = new EspaciosABL();
        UsuarioBL UsuarioBL = new UsuarioBL();
        InstitucionBL InstitucionBL = new InstitucionBL();


        // GET: EventoController
        public async Task<IActionResult> Index(Evento pEvento = null)
        {
            if (pEvento == null)
                pEvento = new Evento();
            if (pEvento.Top_Aux == 0)
                pEvento.Top_Aux = 10;
            else if (pEvento.Top_Aux == -1)
                pEvento.Top_Aux = 0;
            var taskBuscar = EventoBL.BuscarIncluirUsuarioCarreraInstitucionEspaciosAAsync(pEvento);
            var taskObtenerTodos = CarreraBL.ObtenerTodosAsync();
            var taskObtenerTodosE = EspaciosABL.ObtenerTodosAsync();
            var taskObtenerTodosU = UsuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosI = InstitucionBL.ObtenerTodosAsync();
            var Eventos = await taskBuscar;
            ViewBag.Carrera = await taskObtenerTodos;
            ViewBag.Espacios = await taskObtenerTodosE;
            ViewBag.Usuario = await taskObtenerTodosU;
            ViewBag.Institucion = await taskObtenerTodosI;
            ViewBag.Top = pEvento.Top_Aux;
            return View(Eventos);
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int IdEvento)
        {
            var Evento = await EventoBL.ObtenerPorIdAsync(new Evento { IdEvento = IdEvento });
            Evento.Carrera = await CarreraBL.ObtenerPorIdAsync(new Carrera { IdCarrera = Evento.IdCarrera });
            Evento.EspaciosA = await EspaciosABL.ObtenerPorIdAsync(new EspaciosA { IdEspaciosA = Evento.IdEspaciosA });
            Evento.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Evento.IdUsuario });
            Evento.Institucion = await InstitucionBL.ObtenerPorIdAsync(new Institucion { IdInstitucion = Evento.IdInstitucion });
            return View(Evento);
        }

        // GET: ProductoController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
            ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
            ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();

            ViewBag.Error = "";
            return View();
        }


        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento pEvento)
        {
            try
            {

                int result = await EventoBL.CrearAsync(pEvento);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
                ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
                ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
                return View(pEvento);
            }
        }

        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Edit(Evento pEvento)
        {
            var taskObtenerPorId = EventoBL.ObtenerPorIdAsync(pEvento);
            var taskObtenerTodos = CarreraBL.ObtenerTodosAsync();
            var taskObtenerTodosE = EspaciosABL.ObtenerTodosAsync();
            var taskObtenerTodosU = UsuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosI = InstitucionBL.ObtenerTodosAsync();
            var Evento = await taskObtenerPorId;
            ViewBag.Espacios = await taskObtenerTodosE;
            ViewBag.Usuario = await taskObtenerTodosU;
            ViewBag.Institucion = await taskObtenerTodosI;
            ViewBag.Carrera = await taskObtenerTodos;
            ViewBag.Error = "";
            return View(Evento);
        }

        // POST: EventoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdEvento, Evento pEvento)
        {
            try
            {
                int result = await EventoBL.ModificarAsync(pEvento);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
                ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
                ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();
                return View(pEvento);
            }
        }

        // GET: EventoController/Delete/5
        public async Task<IActionResult> Delete(Evento pEvento)
        {
            var Evento = await EventoBL.ObtenerPorIdAsync(pEvento);
            Evento.Carrera = await CarreraBL.ObtenerPorIdAsync(new Carrera { IdCarrera = Evento.IdCarrera });
            Evento.EspaciosA = await EspaciosABL.ObtenerPorIdAsync(new EspaciosA { IdEspaciosA = Evento.IdEspaciosA });
            Evento.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Evento.IdUsuario });
            Evento.Institucion = await InstitucionBL.ObtenerPorIdAsync(new Institucion { IdInstitucion = Evento.IdInstitucion });
            ViewBag.Error = "";
            return View(Evento);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdEvento, Evento pEvento)
        {
            try
            {
                int result = await EventoBL.EliminarAsync(pEvento);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var Evento = await EventoBL.ObtenerPorIdAsync(pEvento);
                if (Evento == null)
                    Evento = new Evento();
                if (Evento.IdCarrera > 0)
                 Evento.Carrera = await CarreraBL.ObtenerPorIdAsync(new Carrera { IdCarrera = Evento.IdCarrera });
                if (Evento.IdCarrera > 0)
                    Evento.Carrera = await CarreraBL.ObtenerPorIdAsync(new Carrera { IdCarrera = Evento.IdCarrera });
                if (Evento.IdEspaciosA > 0)
                    Evento.EspaciosA = await EspaciosABL.ObtenerPorIdAsync(new EspaciosA { IdEspaciosA = Evento.IdEspaciosA });
                if (Evento.IdUsuario > 0)
                    Evento.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Evento.IdUsuario });
                if (Evento.IdInstitucion > 0)
                    Evento.Institucion = await InstitucionBL.ObtenerPorIdAsync(new Institucion { IdInstitucion = Evento.IdInstitucion });
                return View(Evento);
            }
        }

        public async Task<IActionResult> Reserva()
        {
            ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
            ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
            ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();

            ViewBag.Error = "";
            return View();
        }

        // POST: ProductoController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Reserva(Evento pEvento)
        //{
        //    try
        //    {
        //        List<Evento> reservasExist = await EventoBL.BuscarAsync(pEvento);

        //        if (ValidarReserva(pEvento, reservasExist))
        //        {
        //            int result = await EventoBL.CrearAsync(pEvento);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            // Aquí podrías lanzar una excepción o manejar de otra manera el error
        //            ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
        //            ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
        //            ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
        //            ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();
        //            ViewBag.Error = "La fecha ya está reservada para ese lugar.";
        //            return View(pEvento);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
        //        ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
        //        ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
        //        ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();
        //        ViewBag.Error = ex.Message;
        //        return View(pEvento);
        //    }
        //}

        private bool ValidarReserva(Evento nuevaReserva, List<Evento> reservasExist)
        {
            foreach (var reservaExistente in reservasExist)
            {
                // Verifica si hay solapamiento de fechas para el mismo lugar
                if (nuevaReserva.IdEvento != reservaExistente.IdEvento && // Para evitar comparar con la misma reserva
                    nuevaReserva.IdEspaciosA == reservaExistente.IdEspaciosA && // Compara con el mismo lugar
                    ((nuevaReserva.HoraInicio >= reservaExistente.HoraInicio && nuevaReserva.HoraInicio < reservaExistente.HoraFin) ||
                    (nuevaReserva.HoraFin > reservaExistente.HoraFin && nuevaReserva.HoraFin <= reservaExistente.HoraFin)))
                {
                    return false; // Hay solapamiento
                }
            }

            return true; // No hay solapamiento
        }


        private async Task<bool> ValidarHoras(Evento nuevaReserva)
        {
            // Obtener todas las reservas existentes para el mismo espacio
            var reservasExist = await EventoBL.BuscarAsync(new Evento { IdEvento = nuevaReserva.IdEvento });

            foreach (var reservaExistente in reservasExist)
            {
                // Verificar si hay solapamiento de horas para la misma reserva
                if (nuevaReserva.IdEvento != reservaExistente.IdEvento && // Para evitar comparar con la misma reserva
                    nuevaReserva.IdEspaciosA == reservaExistente.IdEspaciosA) // Compara con el mismo lugar
                {
                    // Verificar si la hora de inicio o la hora de fin de la nueva reserva se solapa con alguna reserva existente
                    if ((nuevaReserva.HoraInicio >= reservaExistente.HoraInicio && nuevaReserva.HoraInicio < reservaExistente.HoraFin) ||
                        (nuevaReserva.HoraFin > reservaExistente.HoraInicio && nuevaReserva.HoraFin <= reservaExistente.HoraFin))
                    {
                        return false; // Hay solapamiento
                    }
                }
            }

            return true; // No hay solapamiento
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserva(Evento pEvento)
        {
            try
            {
                // Validar las horas de la nueva reserva
                bool horasValidas = await ValidarHoras(pEvento);

                if (horasValidas)
                {
                    // Si las horas son válidas, intenta crear la reserva
                    int result = await EventoBL.CrearAsync(pEvento);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si las horas no son válidas, mostrar un mensaje de error
                    ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
                    ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
                    ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
                    ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();
                    ViewBag.Error = "La hora de inicio o fin de la reserva se solapa con otra reserva existente para el mismo lugar.";
                    return View(pEvento);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
                ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
                ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
                return View(pEvento);
            }
        }

    }
}



