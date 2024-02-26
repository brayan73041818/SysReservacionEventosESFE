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
    public class InstitucionController : Controller
    {
        InstitucionBL InstitucionBL = new InstitucionBL();
        // GET: InstitucionController
        [AuthorizeUser(IdAcceso: 5)]
        public async Task<IActionResult> Index(Institucion pInstitucion = null)
        {
            if (pInstitucion == null)
                pInstitucion = new Institucion();
            if (pInstitucion.Top_Aux == 0)
                pInstitucion.Top_Aux = 10;
            else if (pInstitucion.Top_Aux == -1)
                pInstitucion.Top_Aux = 0;
            var Instituciones = await InstitucionBL.BuscarAsync(pInstitucion);
            ViewBag.Top = pInstitucion.Top_Aux;
            return View(Instituciones);
        }

        [AuthorizeUser(IdAcceso: 5)]
        // GET: InstitucionContInstitucionler/Details/5
        public async Task<IActionResult> Details(int IdInstitucion)
        {
            var Institucion = await InstitucionBL.ObtenerPorIdAsync(new Institucion { IdInstitucion = IdInstitucion });
            return View(Institucion);
        }

        [AuthorizeUser(IdAcceso: 5)]
        // GET: InstitucionContInstitucionler/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        [AuthorizeUser(IdAcceso: 5)]
        // POST: InstitucionContInstitucionler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Institucion pInstitucion)
        {
            try
            {
                int result = await InstitucionBL.CrearAsync(pInstitucion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pInstitucion);
            }
        }

        [AuthorizeUser(IdAcceso: 5)]
        // GET: InstitucionContInstitucionler/Edit/5
        public async Task<IActionResult> Edit(Institucion pInstitucion)
        {
            var Institucion = await InstitucionBL.ObtenerPorIdAsync(pInstitucion);
            ViewBag.Error = "";
            return View(Institucion);
        }

        [AuthorizeUser(IdAcceso: 5)]
        // POST: InstitucionContInstitucionler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdAsinacion, Institucion pInstitucion)
        {
            try
            {
                int result = await InstitucionBL.ModificarAsync(pInstitucion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pInstitucion);
            }
        }

        [AuthorizeUser(IdAcceso: 5)]
        // GET: InstitucionContInstitucionler/Delete/5
        public async Task<IActionResult> Delete(Institucion pInstitucion)
        {
            ViewBag.Error = "";
            var Institucion = await InstitucionBL.ObtenerPorIdAsync(pInstitucion);
            return View(Institucion);
        }

        [AuthorizeUser(IdAcceso: 5)]
        // POST: InstitucionContInstitucionler/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdInstitucion, Institucion pInstitucion)
        {
            try
            {
                int result = await InstitucionBL.EliminarAsync(pInstitucion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pInstitucion);
            }
        }
    }
}
