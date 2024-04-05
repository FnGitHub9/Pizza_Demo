using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages.Pizzas
{
    public class EditModel : PageModel
    {
        private readonly PizzaService _service;

        public EditModel(PizzaService service)
        {
            _service = service;
        }

        [BindProperty]
        public Pizza Pizzas { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var pizza = _service.Pizzas.FirstOrDefault(p => p.Id == itemid);
            if (pizza == null)
            {
                return NotFound();
            }
            Pizzas = pizza;
            return Page();
        }
        // Thisi is a change on git

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdatePizza(Pizzas.Id, Pizzas);
            return RedirectToPage(nameof(Index));
        }
    }
}
