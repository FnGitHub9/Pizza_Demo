using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages.Drinks
{
    public class IndexModel : PageModel
    {
        private readonly DrinkService _service;
        public IList<Drink> DrinkList { get; set; } = default!;

        [BindProperty]
        public Drink NewDrink { get; set; } = default!;

        public IndexModel(DrinkService service)
        {
            _service = service;
        }
        public void OnGet()
        {
            DrinkList = _service.GetDrinks();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewDrink == null)
            {
                return Page();
            }

            _service.AddDrink(NewDrink);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteDrink(id);
            return RedirectToAction("Get");
        }
    }
}
