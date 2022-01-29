using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Data;
using pizza_mama.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pizza_mama.Pages
{
    public class MenuPizModel : PageModel
    {
        private readonly DataContext _context;

        public MenuPizModel(DataContext context)
        {
            this._context = context;
        }

        public IList<Pizza> Pizza { get; set; }

        public async Task OnGetAsync()
        {
            Pizza = await _context.Pizzas.ToListAsync();
            Pizza = Pizza.OrderBy(p => p.prix).ToList();
        }
    }
}
