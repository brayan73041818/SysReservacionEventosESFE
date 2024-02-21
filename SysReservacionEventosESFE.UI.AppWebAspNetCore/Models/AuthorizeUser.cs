//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using SysReservacionEventosESFE.AccesoADatos;
//using SysReservacionEventosESFE.EntidadesDeNegocio;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using SysReservacionEventosESFE.UI.AppWebAspNetCore.Models; // Asegúrate de importar el espacio de nombres correcto para tus modelos
//using SysReservacionEventosESFE.LogicaDeNegocio;

//namespace SysReservacionEventosESFE.UI.AppWebAspNetCore.Models
//{


//                                                                                                                         , AllowMultiple = false)]
//    public class AuthorizeUser : AuthorizeAttribute
//    {
//        private BDContexto db = new BDContexto();
//        private int IdAccesos;

//        public AuthorizeUser(int IdAccesos = 0)
//        {
//            this.IdAccesos = IdAccesos;
//        }

//        public override void OnAuthorization(AuthorizationFilterContext filterContext)
//        {
//            try
//            {
//                int Idrols = global.idr;
//                var rolacces = new RolAccesosBL();
//                var lstMisAccesos = rolacces.ObtenerTodosAsync();

//                // Verificar si el usuario tiene acceso al recurso
//                var tieneAcceso = lstMisAccesos.(m => m.IdRol == Idrols && m.IdAccesos == IdAccesos);
//                if (!tieneAcceso)
//                {
//                    // El usuario no tiene acceso, redireccionar a la página de acceso denegado
//                    filterContext.Result = new RedirectResult("~/Error/AccesoDenegado");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Manejar la excepción relacionada con la consulta a la base de datos
//                filterContext.Result = new RedirectResult("~/Error/DatabaseError");
//            }
//        }
//    }
//}