using ContactsApplication.Data;
using ContactsApplication.Models;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace ContactsApplication.Repositories
{
    public class ContactRepository(ContactContext context) : IContactRepository
    {
        private readonly ContactContext _context = context;

        public async Task<bool?> UpdateContactAsync(ContactModel contactModel)
        {
            try
            {
                _context.Update(contactModel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Contacts.FindAsync(contactModel.ID) == null)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool?> CreateContactAsync(ContactModel contactModel)
        {
            _context.Add(contactModel);
            var added = await _context.SaveChangesAsync();
            return added > 0;
        }

        public async Task<ContactModel?> GetContactAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<List<ContactModel>> GetContactsAsync(string sort, string filter)
        {
            var sortProp = sort?.Split(":").First() ?? "ID";
            var sortDir = sort?.Split(":").Last();

            var contacts = _context.Contacts.AsQueryable();

            foreach (var filterExpr in (filter ?? string.Empty).Split("|"))
            {
                var filterProp = filterExpr.Split(":").First();
                var filterVal = filterExpr.Split(":").Last();
                if (typeof(ContactModel).GetProperty(filterProp) != null)
                {
                    contacts = contacts.Where($"{filterProp}.Contains(@0)", filterVal);
                }
            }

            if (typeof(ContactModel).GetProperty(sortProp) != null)
                contacts = contacts.OrderBy($"{sortProp} {(sortDir == "d" ? "desc" : "asc")}");

            return await contacts.ToListAsync();
        }

        public async Task<bool?> DeleteContactAsync(int id)
        {
            var contactModel = await _context.Contacts.FindAsync(id);
            if (contactModel != null)
            {
                _context.Contacts.Remove(contactModel);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
