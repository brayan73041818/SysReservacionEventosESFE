﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Accesos> Accesos { get; set; }
        public DbSet<RolAccesos> RolAccesos { get; set; }
        public DbSet<EspaciosA> EspaciosA { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


        {

            optionsBuilder.UseSqlServer(@"workstation id=SysReservacionesEsfe.mssql.somee.com;packet size=4096;user id=JavierGarcia_SQLLogin_1;pwd=aibjbvtba6;data source=SysReservacionesEsfe.mssql.somee.com;persist security info=False;initial catalog=SysReservacionesEsfe;Encrypt=False;TrustServerCertificate=False;");
        }

    }
}
