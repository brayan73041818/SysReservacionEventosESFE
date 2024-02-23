using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SysReservacionEventosESFE.AccesoADatos;
using System;
using System.Linq;

namespace SysReservacionEventosESFE.UI.AppWebAspNetCore.Models
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : Attribute, IAuthorizationFilter
    {
        private readonly int IdAcceso;
        private readonly BDContexto db;

        public AuthorizeUser(int IdAcceso = 0)
        {
            this.IdAcceso = IdAcceso;
            this.db = new BDContexto();
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            string nombreAcceso = "";
            try
            {
                var lstAccesos = from m in db.RolAccesos where m.IdRol == global.idr && m.IdAcceso == IdAcceso select m;
                if (lstAccesos.ToList().Count() == 0)
                {
                    var Accesoss = db.Accesos.Find(IdAcceso);
                    filterContext.Result = new RedirectResult("~/Error/AccesoDenegado");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/AccesoDenegado");
            }
        }
    }
}
