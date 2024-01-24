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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


        {
            //optionsBuilder.UseSqlServer(@"workstation id=SysInventarioFacturacion503.mssql.somee.com;packet size=4096;user id=romerooscar_SQLLogin_1;pwd=awaosafn8m;data source=SysInventarioFacturacion503.mssql.somee.com;persist security info=False;initial catalog=SysInventarioFacturacion503;Encrypt=False;TrustServerCertificate=False;");

            optionsBuilder.UseSqlServer(@"workstation id=BDComercialMartinez.mssql.somee.com;packet size=4096;user id=José0407_SQLLogin_1;pwd=rz363vdvcw;data source=BDComercialMartinez.mssql.somee.com;persist security info=False;initial catalog=BDComercialMartinez;Encrypt=False;TrustServerCertificate=False;");
        }

    }
}
