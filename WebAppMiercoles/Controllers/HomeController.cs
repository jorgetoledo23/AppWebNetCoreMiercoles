using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAppMiercoles.Models;

namespace WebAppMiercoles.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int Page)
        {
            IndexViewModel indexViewModel = new IndexViewModel();

            if (Page == 0)
            {
                indexViewModel.Pagina = 1;
            }
            else {
                indexViewModel.Pagina = Page;
            }

            int muestra = 3;
            int cantidad = _context.tblCategorias.ToList().Count / muestra; // 10 / 3 = 3.3333
            if (cantidad % muestra == 0)
            {
                indexViewModel.CantidadPaginas = cantidad;
            }
            else {
                indexViewModel.CantidadPaginas = cantidad + 1;
            }

            indexViewModel.ListaCategorias = _context.tblCategorias
                .Skip((indexViewModel.Pagina - 1) * muestra) // 0 ---- 3 ------ 6 ------- 9 
                .Take(3).ToList();
            TempData["NextPage"] = indexViewModel.Pagina + 1;
            TempData["PreviousPage"] = indexViewModel.Pagina - 1;
            return View(indexViewModel);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ErrorPage()
        {
            return View();
        }

    }
}
