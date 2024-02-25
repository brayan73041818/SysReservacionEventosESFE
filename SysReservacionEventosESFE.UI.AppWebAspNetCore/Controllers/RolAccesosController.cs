using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/********************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using SysReservacionEventosESFE.LogicaDeNegocio;
using SysReservacionEventosESFE.UI.AppWebAspNetCore.Models;


namespace SysComercialMartinez.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class RolAccesosController : Controller
    {

        RolAccesosBL RolAccesosBL = new RolAccesosBL();
        RolBL RolBL = new RolBL();
        AccesosBL AccesosBL = new AccesosBL();
        public static int idRol;

        // GET: ProductoController
        [AuthorizeUser(IdAcceso: 7)]
        public async Task<IActionResult> Index(RolAccesos pRolAccesos = null)
        {
            if (pRolAccesos == null)
                pRolAccesos = new RolAccesos();
            if (pRolAccesos.Top_Aux == 0)
                pRolAccesos.Top_Aux = 10;
            else if (pRolAccesos.Top_Aux == -1)
                pRolAccesos.Top_Aux = 0;
            var taskBuscar = RolAccesosBL.BuscarIncluirAccesoYRolAsync(pRolAccesos);
           
            var RolAccesoss = await taskBuscar;
    
            List<Rol> rol = await RolBL.ObtenerTodosAsync();
            List<Accesos> acc = await AccesosBL.ObtenerTodosAsync();
            ViewBag.Rol =  rol;
            ViewBag.Accesos = acc;
            ViewBag.Top = pRolAccesos.Top_Aux;
            return View(RolAccesoss);
        }

        // GET: RolAccesosController/Details/5
        public async Task<IActionResult> Details(int IdRolAcceso)
        {
            var RolAccesos = await RolAccesosBL.ObtenerPorIdAsync(new RolAccesos { IdRolAcceso = IdRolAcceso });
            RolAccesos.Rol = await RolBL.ObtenerPorIdAsync(new Rol { Id = RolAccesos.IdRol });
            RolAccesos.Accesos = await AccesosBL.ObtenerPorIdAsync(new Accesos { IdAcceso = RolAccesos.IdAcceso });
            return View(RolAccesos);
        }

        // GET: RolAccesosController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Rol = await RolBL.ObtenerTodosAsync();
            ViewBag.Accesos = await AccesosBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: RolAccesosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolAccesos pRolAccesos, string Rol, List<int> Acceso )
        {
            try
            {
                Rol objRol = new Rol();
                objRol.Nombre = Rol;
                await RolBL.CrearAsync(objRol);

                foreach (var acc in Acceso )
                {
                    RolAccesos nuevoRolAcceso = new RolAccesos();
                    nuevoRolAcceso.IdRol = objRol.Id;
                    nuevoRolAcceso.IdAcceso = acc;
                    await RolAccesosBL.CrearAsync(nuevoRolAcceso);
                }


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Rol = await RolBL.ObtenerTodosAsync();
                ViewBag.Accesos = await AccesosBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
                return View(pRolAccesos);
            }
        }

        // GET: RolAccesosController/Edit/5
        public async Task<IActionResult> Edit(RolAccesos pRolAccesos)
        {
            var taskObtenerPorIdRolAccesos = RolAccesosBL.ObtenerPorIdAsync(pRolAccesos);
            var taskObtenerTodosRol = RolBL.ObtenerTodosAsync();
            var taskObtenerTodosAccesos = AccesosBL.ObtenerTodosAsync();
            var RolAccesos = await taskObtenerPorIdRolAccesos;
            ViewBag.Rol = await taskObtenerTodosRol;
            ViewBag.Accesos = await taskObtenerTodosAccesos;
            ViewBag.Error = "";
            return View(RolAccesos);
        }

        // POST: RolAccesosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdRolAccesos, RolAccesos pRolAccesos)
        {
            try
            {
                int result = await RolAccesosBL.ModificarAsync(pRolAccesos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Rol = await RolBL.ObtenerTodosAsync();
                ViewBag.Accesos = await AccesosBL.ObtenerTodosAsync();
                return View(pRolAccesos);
            }
        }

        // GET: RolAccesosController/Delete/5
        public async Task<IActionResult> Delete(RolAccesos pRolAccesos)
        {
            var RolAccesos = await RolAccesosBL.ObtenerPorIdAsync(pRolAccesos);
            RolAccesos.Rol = await RolBL.ObtenerPorIdAsync(new Rol { Id = RolAccesos.IdRol });
            RolAccesos.Accesos = await AccesosBL.ObtenerPorIdAsync(new Accesos { IdAcceso = RolAccesos.IdAcceso });
            ViewBag.Error = "";
            return View(RolAccesos);
        }

        // POST: RolAccesosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdRolAccesos, RolAccesos pRolAccesos)
        {
            try
            {
                int result = await RolAccesosBL.EliminarAsync(pRolAccesos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var RolAccesos = await RolAccesosBL.ObtenerPorIdAsync(pRolAccesos);
                if (RolAccesos == null)
                    RolAccesos = new RolAccesos();
                if (RolAccesos.IdRolAcceso > 0)
                    RolAccesos.Rol = await RolBL.ObtenerPorIdAsync(new Rol { Id = RolAccesos.IdRol });
                if (RolAccesos.IdRolAcceso > 0)
                    RolAccesos.Accesos = await AccesosBL.ObtenerPorIdAsync(new Accesos { IdAcceso = RolAccesos.IdAcceso });
                return View(RolAccesos);
            }
        }

    }
}
