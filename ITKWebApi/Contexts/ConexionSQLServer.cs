using ITKWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITKWebApi.Contexts
{
    public class ConexionSQLServer : DbContext
    {
        public ConexionSQLServer(DbContextOptions<ConexionSQLServer> options) : base(options)
        {

        }
        public DbSet<ProductoITK> Producto { get; set; }
    }
}
