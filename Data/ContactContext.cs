using Microsoft.EntityFrameworkCore;
using ContactsApplication.Models;

namespace ContactsApplication.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public void Initialize()
        {
            if (this.Contacts?.Count() == 0) // initial seed
            {
                this.Contacts.Add(
                    new ContactModel
                    {
                        FirstName = "Josef",
                        LastName = "Novák",
                        Phone = "+420 123 456 789",
                        Email = "josef.novak@email.cz",
                        City = "Praha"
                    });

                this.Contacts.Add(
                    new ContactModel
                    {
                        FirstName = "Jan",
                        LastName = "Dlouhý",
                        Phone = "333 222 111",
                        Email = "honza.d@email.cz",
                        City = "Brno"
                    });

                this.Contacts.Add(
                    new ContactModel
                    {
                        FirstName = "Karla",
                        LastName = "Nováková",
                        Phone = "987654321",
                        Email = "k.novakova@email.cz",
                        City = "Ostrava"
                    });

                this.SaveChanges();
            }
        }

        public DbSet<ContactModel> Contacts { get; set; }
    }
}
