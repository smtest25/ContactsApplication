using Microsoft.AspNetCore.Mvc;
using ContactsApplication.Models;
using System.Linq.Dynamic.Core;
using ContactsApplication.Services;

namespace ContactsApplication.Controllers
{
    public class ContactController(IContactService contactService) : Controller
    {
        private readonly IContactService _contactService = contactService;

        public async Task<IActionResult> Index(string sort, string filter)
        {
            var contacts = await _contactService.GetContactsAsync(sort, filter);

            ViewData["Sort"] = sort;
            ViewData["Filter"] = filter;

            ProcessFilters(filter);

            return View(contacts);
        }

        private void ProcessFilters(string filter)
        {
            foreach (var filterExpr in (filter ?? string.Empty).Split("|"))
            {
                var filterProp = filterExpr.Split(":").First();
                var filterVal = filterExpr.Split(":").Last();

                ViewData[$"Filter{filterProp}"] = filterVal;
            }
        }

        public IActionResult Create(string? sort, string? filter)
        {
            ViewData["Sort"] = sort;
            ViewData["Filter"] = filter;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Phone,Email,City")] ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                if (await _contactService.CreateContactAsync(contactModel) == true)
                    return RedirectToAction(nameof(Index));
                else
                    return StatusCode(500);
            }
            return View(contactModel);
        }

        public async Task<IActionResult> Detail(int? id, string? sort, string? filter)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _contactService.GetContactAsync(id.Value);

            if (contactModel == null)
            {
                return NotFound();
            }

            ViewData["Sort"] = sort;
            ViewData["Filter"] = filter;

            return View(contactModel);
        }

        public async Task<IActionResult> Edit(int? id, string? sort, string? filter)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _contactService.GetContactAsync(id.Value);
            if (contactModel == null)
            {
                return NotFound();
            }

            ViewData["Sort"] = sort;
            ViewData["Filter"] = filter;

            return View(contactModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string? sort, string? filter, [Bind("ID,FirstName,LastName,Phone,Email,City")] ContactModel contactModel)
        {
            if (id != contactModel.ID)
            {
                return NotFound();
            }

            ViewData["Sort"] = sort;
            ViewData["Filter"] = filter;

            if (ModelState.IsValid)
            {
                if (await _contactService.UpdateContactAsync(contactModel) == true)
                    return RedirectToAction(nameof(Index), new { sort, filter } );
                else
                    return StatusCode(500);
            }
            return View(contactModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, string? sort, string? filter)
        {
            await _contactService.DeleteContactAsync(id);

            return RedirectToAction(nameof(Index), new { sort, filter });
        }
    }
}
