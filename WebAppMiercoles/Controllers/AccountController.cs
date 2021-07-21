using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMiercoles.Models;

namespace WebAppMiercoles.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Cliente> _userManager;
        private readonly SignInManager<Cliente> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context, UserManager<Cliente> userManager, SignInManager<Cliente> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel Lvm) {

            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(Lvm.Email);
                if (User != null)
                {
                    var resultado = await _signInManager.PasswordSignInAsync(User, Lvm.Pass, false, false);
                    if (resultado.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else {
                ModelState.AddModelError("", "Email NO encotrado");
            }
            return View(Lvm);
        }


        public IActionResult Registro() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel Rvm)
        {
            if (ModelState.IsValid)
            {
                var User = new Cliente()
                {
                    Email = Rvm.Email,
                    UserName = Rvm.Rut,
                    Rut = Rvm.Rut,
                    Nombres = Rvm.Nombres,
                    Apellidos = Rvm.Apellidos,
                    Telefono = Rvm.Telefono,
                    PhoneNumber = Rvm.Telefono,
                    Ciudad = Rvm.Ciudad,
                    Direccion = Rvm.Direccion,
                    CodigoPostal = Rvm.CodigoPostal
                };
                var resultado = await _userManager.CreateAsync(User, Rvm.Pass);
                if (resultado.Succeeded)
                {
                    //TODOS LOS USUARIOS REGISTRADOS SE INGRESEN CON ESE CLAIM
                    await _userManager.AddClaimAsync(User, new System.Security.Claims.Claim("Cliente", "10"));
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();

        }

        public async Task<IActionResult> CerrarSesion() {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> MisPedidos() {

            var Cliente = await _userManager.GetUserAsync(HttpContext.User);

            MisPedidosViewModel Mvm = new MisPedidosViewModel
            {
                Cliente = Cliente,
                ListaPedidos = _context.tblPedidos
                    .Where(p => p.Cliente.Id == Cliente.Id).ToList()
            };

            return View(Mvm);
        }

        public IActionResult DetallePedido(int PedidoId) {

            DetallePedidoViewModel Dvm = new DetallePedidoViewModel {
                Pedido = _context.tblPedidos.Where(p => p.PedidoId == PedidoId).FirstOrDefault(),
                ListaDetalle = _context.tblDetallePedido.Where(dt => dt.PedidoId == PedidoId)
                    .Include(dt => dt.Producto)
                .ToList()
            };
            return View(Dvm);
            
        }


    }
}
