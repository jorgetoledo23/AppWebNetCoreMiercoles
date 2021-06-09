using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMiercoles.Models;

namespace WebAppMiercoles.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ListaCategorias()
        {
            var listadoCategorias = _context.tblCategorias.ToList();
            return View(listadoCategorias);
        }

        [HttpPost]
        public IActionResult GuardarCategorias() {
            return View();
        }

        public IActionResult EditarCategoria() {
            return View();
        }

        [HttpPost]
        public IActionResult EditarCategoria(Categoria C)
        {
            return View();
        }

        public IActionResult CrearCategoria() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria(Categoria C)
        {
            if (ModelState.IsValid)
            {
                var existe = _context.tblCategorias.Where(c => c.Nombre.Equals(C.Nombre)).FirstOrDefault();
                if (existe == null)
                {
                    _context.Add(C);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ListaCategorias");
                }
                else {
                    ModelState.AddModelError("", "La Categoria Ingresada ya Existe!");
                }
            }
            return View(C);
        }



    }
}
