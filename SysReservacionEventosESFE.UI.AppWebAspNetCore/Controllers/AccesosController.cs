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

namespace SysReservacionEventosESFE.UI.AppWebAspNetCore.ContAccesoslers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AccesosContAccesosler : Controller
    {
        AccesosBL AccesosBL = new AccesosBL();
        // GET: AccesosContAccesosler
        public async Task<IActionResult> Index(Accesos pAccesos = null)
        {
            if (pAccesos == null)
                pAccesos = new Accesos();
            if (pAccesos.Top_Aux == 0)
                pAccesos.Top_Aux = 10;
            else if (pAccesos.Top_Aux == -1)
                pAccesos.Top_Aux = 0;
            var Accesoses = await AccesosBL.BuscarAsync(pAccesos);
            ViewBag.Top = pAccesos.Top_Aux;
            return View(Accesoses);
        }

        // GET: AccesosContAccesosler/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var Accesos = await AccesosBL.ObtenerPorIdAsync(new Accesos { IdAccesos = id });
            return View(Accesos);
        }

        // GET: AccesosContAccesosler/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: AccesosContAccesosler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Accesos pAccesos)
        {
            try
            {
                int result = await AccesosBL.CrearAsync(pAccesos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAccesos);
            }
        }

        // GET: AccesosContAccesosler/Edit/5
        public async Task<IActionResult> Edit(Accesos pAccesos)
        {
            var Accesos = await AccesosBL.ObtenerPorIdAsync(pAccesos);
            ViewBag.Error = "";
            return View(Accesos);
        }

        // POST: AccesosContAccesosler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Accesos pAccesos)
        {
            try
            {
                int result = await AccesosBL.ModificarAsync(pAccesos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAccesos);
            }
        }

        // GET: AccesosContAccesosler/Delete/5
        public async Task<IActionResult> Delete(Accesos pAccesos)
        {
            ViewBag.Error = "";
            var Accesos = await AccesosBL.ObtenerPorIdAsync(pAccesos);
            return View(Accesos);
        }

        // POST: AccesosContAccesosler/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Accesos pAccesos)
        {
            try
            {
                int result = await AccesosBL.EliminarAsync(pAccesos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAccesos);
            }
        }
    }
}
