using ContosoPizza.DataContexts;
using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        //private readonly PizzaContext _context = default!;
        private readonly PizzaContext _context = default!;
        public IList<Pizza> Pizzas { get; set; }
        public PizzaService(PizzaContext context)
        {
            _context = context;
            Pizzas = GetPizzas();
        }

        public IList<Pizza> GetPizzas()
        {
            if (_context.Pizzas != null)
            {
                return _context.Pizzas.ToList();
            }
            return new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            if (_context.Pizzas != null)
            {
                _context.InsertPizza(pizza);

            }
        }
        public void UpdatePizza(int id, Pizza pizza)
        {
            if (_context.Pizzas != null)
            {
                _context.UpdatePizza(id, pizza);
            }
        }

        public void DeletePizza(int id)
        {
            if (_context.Pizzas != null)
            {
                var pizza = _context.Pizzas.Find(p => p.Id == id);
                if (pizza != null)
                {
                    _context.DeletePizza(id);

                }
            }
        }
    }
}
