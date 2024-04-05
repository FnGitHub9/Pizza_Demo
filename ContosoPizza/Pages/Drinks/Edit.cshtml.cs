using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages.Drinks
{
    public class EditModel : PageModel
    {
        private readonly DrinkService _service;

        public EditModel(DrinkService service)
        {
            _service = service;
        }

        [BindProperty]
        public Drink Drinks { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var drink = _service.Drinks.FirstOrDefault(p => p.Id == itemid);
            if (drink == null)
            {
                return NotFound();
            }
            Drinks = drink;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateDrink(Drinks.Id, Drinks);
            return RedirectToPage(nameof(Index));
        }
    }
}
