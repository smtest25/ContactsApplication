using ContactsApplication.Models;

namespace ContactsApplication.Repositories
{
    public interface IContactRepository
    {
        public Task<List<ContactModel>> GetContactsAsync(string sort, string filter);
        public Task<ContactModel?> GetContactAsync(int id);
        public Task<bool?> CreateContactAsync(ContactModel contactModel);
        public Task<bool?> UpdateContactAsync(ContactModel contactModel);
        public Task<bool?> DeleteContactAsync(int id);

    }
}
