using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMiercoles.Models;

namespace WebAppMiercoles.Controllers
{
    [Authorize(Policy = "PolicyCliente")]
    public class PedidoController : Controller
    {

        private readonly AppDbContext _context;
        private readonly CarroCompra _carro;
        private readonly UserManager<Cliente> _userManager;
        private readonly SignInManager<Cliente> _signInManager;

        public PedidoController(AppDbContext context, CarroCompra carro, UserManager<Cliente> userManager, SignInManager<Cliente> signInManager)
        {
            _context = context;
            _carro = carro;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //Resumen Pedido

        [Authorize(Policy = "PolicyCliente")]
        public async Task<IActionResult> Index()
        {

            var Cliente = await _userManager.GetUserAsync(HttpContext.User);
            var Items = _carro.GetItemsCarro();
            var Total = _carro.GetTotalCarro();

            PedidoViewModel Pvm = new PedidoViewModel
            {
                Cliente = Cliente,
                ItemsCarro = Items,
                Total = Total
            };

            return View(Pvm);
        }

        [Authorize(Policy = "PolicyCliente")]
        public IActionResult PedidoCompletado()
        {
            return View();
        }


        public async Task<IActionResult> FinalizarPedido() {
            
            var Cliente = await _userManager.GetUserAsync(HttpContext.User);
            var Items = _carro.GetItemsCarro();
            var Total = _carro.GetTotalCarro();

            if (Items.Count > 0)
            {
                var T = _context.Database.BeginTransaction();
                try
                {
                    Pedido P = new Pedido
                    {
                        Cliente = Cliente,
                        Fecha = DateTime.Now,
                        Total = Convert.ToInt32(Total)
                    };
                
                    _context.Add(P);
                    _context.SaveChanges();

                    foreach (var item in Items)
                    {
                        DetallePedido Dt = new DetallePedido
                        {
                            Cantidad = item.Cantidad,
                            PedidoId = P.PedidoId,
                            ProductoId = item.ProductoId
                        };
                        _context.Add(Dt);
                        _context.SaveChanges();
                    }
                    T.Commit();
                    _carro.VaciarCarro();
                }
                catch (Exception)
                {
                    T.Rollback();
                }
                
            }
            return RedirectToAction(nameof(PedidoCompletado));
        }
    }
}
