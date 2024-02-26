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
    public class CarreraController : Controller
    {
        CarreraBL carreraBL = new CarreraBL();

        // GET: RolController
        [AuthorizeUser(IdAcceso: 4)]

        public async Task<IActionResult> Index(Carrera pCarrera = null)
        {
            if (pCarrera == null)
                pCarrera = new Carrera();
            if (pCarrera.Top_Aux == 0)
                pCarrera.Top_Aux = 10;
            else if (pCarrera.Top_Aux == -1)
                pCarrera.Top_Aux = 0;
            var carreras = await carreraBL.BuscarAsync(pCarrera);
            ViewBag.Top = pCarrera.Top_Aux;
            return View(carreras);
        }

        // GET: RolController/Details/5
        [AuthorizeUser(IdAcceso: 4)]
        public async Task<IActionResult> Details(int IdCarrera)
        {
            var carrera = await carreraBL.ObtenerPorIdAsync(new Carrera { IdCarrera = IdCarrera });
            return View(carrera);
        }

        // GET: RolController/Create
        [AuthorizeUser(IdAcceso: 4)]
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: RolController/Create
        [AuthorizeUser(IdAcceso: 4)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carrera pCarrera)
        {
            try
            {
                int result = await carreraBL.CrearAsync(pCarrera);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCarrera);
            }
        }

        // GET: RolController/Edit/5
        [AuthorizeUser(IdAcceso: 4)]
        public async Task<IActionResult> Edit(Carrera pCarrera)
        {
            var carrera = await carreraBL.ObtenerPorIdAsync(pCarrera);
            ViewBag.Error = "";
            return View(carrera);
        }

        // POST: RolController/Edit/5
        [AuthorizeUser(IdAcceso: 4)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdCarrera, Carrera pCarrera)
        {
            try
            {
                int result = await carreraBL.ModificarAsync(pCarrera);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCarrera);
            }
        }

        // GET: RolController/Delete/5
        [AuthorizeUser(IdAcceso: 4)]
        public async Task<IActionResult> Delete(Carrera pCarrera)
        {
            ViewBag.Error = "";
            var carrera = await carreraBL.ObtenerPorIdAsync(pCarrera);
            return View(carrera);
        }

        // POST: RolController/Delete/5
        [AuthorizeUser(IdAcceso: 4)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdCarrera, Carrera pCarrera)
        {
            try
            {
                int result = await carreraBL.EliminarAsync(pCarrera);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCarrera);
            }
        }
    }
}
