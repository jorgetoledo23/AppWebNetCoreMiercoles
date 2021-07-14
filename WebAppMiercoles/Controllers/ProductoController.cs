using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppMiercoles.Models;

namespace WebAppMiercoles.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class ProductoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        public ProductoController(AppDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment; ;
        }

        public IActionResult addProducto()
        {

            ViewData["CategoriaId"] = new SelectList(_context.tblCategorias.ToList(), "CategoriaId", "Nombre");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> addProducto(Producto P)
        {
            if (ModelState.IsValid)
            {
                if (P.imagenFile == null)
                {
                    P.Imagen = "no-disponible.png";
                }
                else
                {
                    string wwwRootPath = _hostEnviroment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(P.imagenFile.FileName);
                    string extension = Path.GetExtension(P.imagenFile.FileName);
                    P.Imagen = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + extension;
                    //drama16062021151230.jpg
                    string path = Path.Combine(wwwRootPath + "/images/" + P.Imagen);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await P.imagenFile.CopyToAsync(fileStream);
                    }
                }
                _context.Add(P);
                await _context.SaveChangesAsync();
                //return RedirectToAction("ListaCategorias", "Home");
                return RedirectToAction(nameof(ListaProductos));
            }

            return View(P);

        }



        public IActionResult ListaProductos()
        {
            return View(_context.tblProductos.ToList());
        }
    }
}
