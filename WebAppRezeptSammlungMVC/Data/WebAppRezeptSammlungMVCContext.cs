using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppRezeptSammlungMVC.Models;

namespace WebAppRezeptSammlungMVC.Data
{
    public class WebAppRezeptSammlungMVCContext : DbContext
    {
        public WebAppRezeptSammlungMVCContext (DbContextOptions<WebAppRezeptSammlungMVCContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppRezeptSammlungMVC.Models.Rezept> Rezept { get; set; } = default!;
        public DbSet<WebAppRezeptSammlungMVC.Models.Lebensmittel> Lebensmittel { get; set; } = default!;
        public DbSet<WebAppRezeptSammlungMVC.Models.Zutat> Zutat { get; set; } = default!;
    }
}
