using Microsoft.EntityFrameworkCore;


namespace diagnoseApp.Model
{
    public class PersonDB : DbContext
    {
        public PersonDB (DbContextOptions<PersonDB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> personer { get; set; }
    }
}
