using Microsoft.EntityFrameworkCore;
namespace diagnoseApp.Model
{
    public class SymptomerDB : DbContext
    {
        protected SymptomerDB(DbContextOptions<SymptomerDB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Symptomer> Symptomers { get; set; }
    }
}
