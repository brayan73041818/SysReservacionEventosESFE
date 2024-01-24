using Microsoft.EntityFrameworkCore;
using SysReservacionEventosESFE.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SysReservacionEventosESFE.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Carrera> Carrera { get; set; }
        public DbSet<Institucion> Institucion { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


        {
            //optionsBuilder.UseSqlServer(@"workstation id=SysInventarioFacturacion503.mssql.somee.com;packet size=4096;user id=romerooscar_SQLLogin_1;pwd=awaosafn8m;data source=SysInventarioFacturacion503.mssql.somee.com;persist security info=False;initial catalog=SysInventarioFacturacion503;Encrypt=False;TrustServerCertificate=False;");

            optionsBuilder.UseSqlServer(@"workstation id=SysReservacionesEsfe.mssql.somee.com;packet size=4096;user id=JavierGarcia_SQLLogin_1;pwd=aibjbvtba6;data source=SysReservacionesEsfe.mssql.somee.com;persist security info=False;initial catalog=SysReservacionesEsfe;Encrypt=False;TrustServerCertificate=False;");
        }

    }
}
