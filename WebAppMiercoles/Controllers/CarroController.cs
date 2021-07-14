using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMiercoles.Models;

namespace WebAppMiercoles.Controllers
{
    public class CarroController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CarroCompra _carro;

        public CarroController(AppDbContext context, CarroCompra carro)
        {
            _context = context;
            _carro = carro;
        }

        
        public IActionResult Index()
        {
            CarroCompraViewModel carroCompraViewModel = new CarroCompraViewModel()
            {
                ItemsCarro = _carro.GetItemsCarro(),
                TotalCarro = _carro.GetTotalCarro()
            };

            return View(carroCompraViewModel);
        }


        public RedirectToActionResult Add(int ProductoId) {
            var P = _context.tblProductos.Where(p => p.ProductoId == ProductoId).FirstOrDefault();
            if (P != null)
            {
                _carro.AddItem(P);
            }
            return RedirectToAction(nameof(Index));
        }

        public RedirectToActionResult Del(int ProductoId)
        {
            var P = _context.tblProductos.Where(p => p.ProductoId == ProductoId).FirstOrDefault();
            if (P != null)
            {
                _carro.delItem(P);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
