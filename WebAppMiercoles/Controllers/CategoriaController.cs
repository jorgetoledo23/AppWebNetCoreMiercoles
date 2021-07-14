using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAppMiercoles.Models;

namespace WebAppMiercoles.Controllers
{
    [Authorize(Policy = "PolicyAdmin")]
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        public CategoriaController(AppDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        public IActionResult ListaCategorias()
        {
            /*
            var PVM = new ProductoViewModel();
            PVM.listaCategorias = _context.tblCategorias.ToList();
            PVM.listaProducto = _context.tblProductos.ToList(); */

            var listadoCategorias = _context.tblCategorias.ToList();
            return View(listadoCategorias);
        }

        [HttpGet]
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
                    if (C.ImagenFile == null)
                    {
                        C.Imagen = "no-disponible.png";
                    }
                    else
                    {
                        string wwwRootPath = _hostEnviroment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(C.ImagenFile.FileName);
                        string extension = Path.GetExtension(C.ImagenFile.FileName);
                        C.Imagen = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + extension;
                        //drama16062021151230.jpg
                        string path = Path.Combine(wwwRootPath + "/images/" + C.Imagen);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await C.ImagenFile.CopyToAsync(fileStream);
                        }
                    }
                    _context.Add(C);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction("ListaCategorias", "Home");
                    TempData["Mensaje"] = "Categoria agregada exitosamente!";
                    return RedirectToAction(nameof(ListaCategorias));
                }
                else {
                    ModelState.AddModelError("", "La Categoria Ingresada ya Existe!");
                }
            }
            return View(C);
        }

        [HttpGet]
        public IActionResult EditCategoria(int CategoriaId) {
            var C = _context.tblCategorias.Where(c => c.CategoriaId.Equals(CategoriaId)).FirstOrDefault();
            if (C != null)
            {
                return View(C);
            }
            else
            {
                return RedirectToAction("ErrorPage","Home");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> EditCategoria(Categoria C) {

            if (ModelState.IsValid)
            {
                var CatEditada = _context.tblCategorias.Where(c => c.CategoriaId.Equals(C.CategoriaId)).FirstOrDefault();
                
                if (C.ImagenFile == null)
                {
                    CatEditada.Imagen = "no-disponible.png";
                }
                else
                {
                    string wwwRootPath = _hostEnviroment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(C.ImagenFile.FileName);
                    string extension = Path.GetExtension(C.ImagenFile.FileName);
                    CatEditada.Imagen = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + extension;
                    //drama16062021151230.jpg
                    string path = Path.Combine(wwwRootPath + "/images/" + CatEditada.Imagen);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await C.ImagenFile.CopyToAsync(fileStream);
                    }
                }

                CatEditada.Nombre = C.Nombre;
                CatEditada.Descripcion = C.Descripcion;
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Categoria editada con exito!";
                return RedirectToAction(nameof(ListaCategorias));
            }
            else {
                return View(C);
            }

        }

        //TODO: Eliminar o Desactivar Categoria (1 Punto para la Proxima Evaluacion (3))
        [HttpGet]
        public IActionResult deleteCategoria(int CategoriaId)
        {
            var C = _context.tblCategorias.Where(c => c.CategoriaId.Equals(CategoriaId)).FirstOrDefault();
            return View(C);
        }

        [HttpPost]
        public async Task<IActionResult> deleteCategoria(Categoria C) {
            var catEliminada = _context.tblCategorias.Where(c => c.CategoriaId.Equals(C.CategoriaId)).FirstOrDefault();
            if (catEliminada != null)
            {
                _context.Entry(catEliminada).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Categoria eliminada con exito!";
                return RedirectToAction(nameof(ListaCategorias));
            }
            else {
                return NotFound();
            }
        }

    }
}
