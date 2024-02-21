using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SysReservacionEventosESFE.UI.AppWebAspNetCore.Controllers
{
    public class ErrorController : Controller
    {
        // GET: CalendarController
        public ActionResult AccesoDenegado(string acceso,string smsError)        {
            ViewBag.acceso = acceso;    
            ViewBag.smsError = smsError;
            return View();
        }

    
    }
}
