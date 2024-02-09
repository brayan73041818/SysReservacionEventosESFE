using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SysReservacionEventosESFE.UI.AppWebAspNetCore.Controllers
{
    public class CalendarController : Controller
    {
        // GET: CalendarController
        public ActionResult Calendar()
        {
            return View();
        }

        public IActionResult Events()
        {
            // Aquí deberías recuperar los eventos del calendario desde tu base de datos o alguna otra fuente de datos
            var events = new[]
            {
                new { title = "Evento 1", start = "2024-02-07" },
                new { title = "Evento 2", start = "2024-02-10" }
            };

            return Json(events);
        }
    }
}
