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
using SysReservacionEventosESFE.UI.AppWebAspNetCore.Models;

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
        public async Task<IActionResult> Index(DateTime fInicio, DateTime fFinal, Evento pEvento = null)
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
            if (fInicio.Year != 1 && fFinal.Year != 1)
            {
                ViewBag.Evento = Eventos.Where(r => r.FechaEvento.Date >= fInicio.Date && r.FechaEvento.Date <= fFinal.Date).ToList();
            }
            else
            {
                ViewBag.Evento = Eventos;
            }
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
        public async Task<IActionResult> Edit(int IdEvento, Evento pEvento,int IdEspaciosA)
        {
            try
            {
                // Obtener todos los eventos existentes
                var todosEventos = await EventoBL.ObtenerTodosAsync();

                // Verificar si hay algún evento existente con superposición de horas en el mismo lugar y fecha
                foreach (var evento in todosEventos)
                {
                    if (evento.FechaEvento.Date == pEvento.FechaEvento.Date && // Compara solo la fecha sin la hora
                        evento.EspaciosA.IdEspaciosA == IdEspaciosA && // Compara el nombre del lugar
                        SeSuperponenHoras(pEvento.HoraInicio, pEvento.HoraFin, evento.HoraInicio, evento.HoraFin))
                    {
                        string mensaje = "Las horas seleccionadas se superponen con otro evento en el mismo lugar y fecha. Por favor, elige otras horas.";

                        ViewBag.Mensaje = mensaje;

                        // Devolver la vista con el mensaje de error
                        return RedirectToAction(nameof(YaReservado));



                    }
                }


                pEvento.IdUsuario = global.idu;

                int result = await EventoBL.ModificarAsync(pEvento);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserva(Evento pEvento, int IdEspaciosA)
        {
            try
            {
                // Obtener todos los eventos existentes
                var todosEventos = await EventoBL.ObtenerTodosAsync();

                // Verificar si hay algún evento existente con superposición de horas en el mismo lugar y fecha
                foreach (var evento in todosEventos)
                {
                    if (evento.FechaEvento.Date == pEvento.FechaEvento.Date && // Compara solo la fecha sin la hora
                        evento.EspaciosA.IdEspaciosA == IdEspaciosA && // Compara el nombre del lugar
                        SeSuperponenHoras(pEvento.HoraInicio, pEvento.HoraFin, evento.HoraInicio, evento.HoraFin))
                    {
                        // Si hay superposición de horas, devolver la vista con el mensaje de error
                        return RedirectToAction(nameof(YaReservado));
                    }
                }

                // Si no hay superposición de horas, guardar el evento
                pEvento.IdUsuario = global.idu;
                int result = await EventoBL.CrearAsync(pEvento);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                ViewBag.Carrera = await CarreraBL.ObtenerTodosAsync();
                ViewBag.Espacios = await EspaciosABL.ObtenerTodosAsync();
                ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Institucion = await InstitucionBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
                return View(pEvento);
            }
        }


        // Método para verificar si las horas se superponen
        private bool SeSuperponenHoras(DateTime horaInicio1, DateTime horaFin1, DateTime horaInicio2, DateTime horaFin2)
        {
            return horaInicio1 < horaFin2 && horaFin1 > horaInicio2 ;
        }

     
        public async Task<IActionResult> YaReservado()
        {
    
            return View();
        }


    }
}
