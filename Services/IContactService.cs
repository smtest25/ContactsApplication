using ContactsApplication.Models;

namespace ContactsApplication.Services
{
    public interface IContactService
    {
        public Task<List<ContactModel>> GetContactsAsync(string sort, string filter);
        public Task<ContactModel?> GetContactAsync(int id);
        public Task<bool?> CreateContactAsync(ContactModel contactModel);
        public Task<bool?> UpdateContactAsync(ContactModel contactModel);
        public Task<bool?> DeleteContactAsync(int id);
    }
}
