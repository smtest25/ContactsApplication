using System.Threading.Tasks;
using ContactsApplication.Models;
using ContactsApplication.Repositories;

namespace ContactsApplication.Services
{
    public class ContactService(IContactRepository contactRepository) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<bool?> UpdateContactAsync(ContactModel contactModel)
        {
            return await _contactRepository.UpdateContactAsync(contactModel);
        }
        public async Task<bool?> DeleteContactAsync(int id)
        {
            return await _contactRepository.DeleteContactAsync(id);
        }

        public async Task<bool?> CreateContactAsync(ContactModel contactModel)
        {
            return await _contactRepository.CreateContactAsync(contactModel);
        }

        public async Task<ContactModel?> GetContactAsync(int id)
        {
            return await _contactRepository.GetContactAsync(id);
        }

        public async Task<List<ContactModel>> GetContactsAsync(string sort, string filter)
        {
            return await _contactRepository.GetContactsAsync(sort, filter);
        }
    }
}
