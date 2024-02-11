using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using SysReservacionEventosESFE.LogicaDeNegocio;
using System.Data;

namespace SysReservacionEventosESFE.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Administrador")]
    public class EspaciosAController : Controller
    {
        EspaciosABL EspaciosABL = new EspaciosABL();
        // GET: EspaciosAController
        public async Task<IActionResult> Index(EspaciosA pEspaciosA = null)
        {
            if (pEspaciosA == null)
                pEspaciosA = new EspaciosA();
            if (pEspaciosA.Top_Aux == 0)
                pEspaciosA.Top_Aux = 10;
            else if (pEspaciosA.Top_Aux == -1)
                pEspaciosA.Top_Aux = 0;
            var EspaciosAes = await EspaciosABL.BuscarAsync(pEspaciosA);
            ViewBag.Top = pEspaciosA.Top_Aux;
            return View(EspaciosAes);
        }

        // GET: EspaciosAContEspaciosAler/Details/5
        public async Task<IActionResult> Details(int IdEspaciosA)
        {
            var EspaciosA = await EspaciosABL.ObtenerPorIdAsync(new EspaciosA { IdEspaciosA = IdEspaciosA });
            return View(EspaciosA);
        }

        // GET: EspaciosAContEspaciosAler/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: EspaciosAContEspaciosAler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EspaciosA pEspaciosA)
        {
            try
            {
                int result = await EspaciosABL.CrearAsync(pEspaciosA);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pEspaciosA);
            }
        }

        // GET: EspaciosAContEspaciosAler/Edit/5
        public async Task<IActionResult> Edit(EspaciosA pEspaciosA)
        {
            var EspaciosA = await EspaciosABL.ObtenerPorIdAsync(pEspaciosA);
            ViewBag.Error = "";
            return View(EspaciosA);
        }

        // POST: EspaciosAContEspaciosAler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdAsinacion, EspaciosA pEspaciosA)
        {
            try
            {
                int result = await EspaciosABL.ModificarAsync(pEspaciosA);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pEspaciosA);
            }
        }

        // GET: EspaciosAContEspaciosAler/Delete/5
        public async Task<IActionResult> Delete(EspaciosA pEspaciosA)
        {
            ViewBag.Error = "";
            var EspaciosA = await EspaciosABL.ObtenerPorIdAsync(pEspaciosA);
            return View(EspaciosA);
        }

        // POST: EspaciosAContEspaciosAler/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdEspaciosA, EspaciosA pEspaciosA)
        {
            try
            {
                int result = await EspaciosABL.EliminarAsync(pEspaciosA);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pEspaciosA);
            }
        }
    }
}
