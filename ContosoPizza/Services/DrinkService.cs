using ContosoPizza.DataContexts;
using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public class DrinkService
    {
        private readonly DrinkContext _context = default!;
        public IList<Drink> Drinks { get; set; }
        public DrinkService(DrinkContext context)
        {
            _context = context;
            Drinks = GetDrinks();
        }

        public IList<Drink> GetDrinks()
        {
            if (_context.Drinks != null)
            {
                return _context.Drinks.ToList();
            }
            return new List<Drink>();
        }

        public void AddDrink(Drink drink)
        {
            if (_context.Drinks != null)
            {
                _context.InsertDrink(drink);

            }
        }
        public void UpdateDrink(int id, Drink drink)
        {
            if (_context.Drinks != null)
            {
                _context.UpdateDrink(id, drink);
            }
        }

        public void DeleteDrink(int id)
        {
            if (_context.Drinks != null)
            {
                var drink = _context.Drinks.Find(p => p.Id == id);
                if (drink != null)
                {
                    _context.DeleteDrink(id);

                }
            }
        }
    }
}
