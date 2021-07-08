using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMiercoles.Models
{
    public class CarroCompra
    {
        private readonly AppDbContext _context;
        public string CarroCompraId { get; set; }

        public List<ItemCarro> ItemsCarro { get; set; }

        public CarroCompra(AppDbContext context)
        {
            _context = context;
        }

        public static CarroCompra GetCarroCompra(IServiceProvider services) {
            //Session
            ISession session = services
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext.Session;
            //Contexto de Datos
            var context = services.GetService<AppDbContext>();
            //Generar CarroId
            string CarroId = session.GetString("CarroId") ?? Guid.NewGuid().ToString();
            session.SetString("CarroId", CarroId);
            return new CarroCompra(context) { CarroCompraId = CarroId };
        }


        public void AddItem(Producto P) {

            var item = _context.tblItemsCarro
                .Where(i => i.ProductoId == P.ProductoId && i.CarroCompraId == this.CarroCompraId)
                .FirstOrDefault();

            if (item == null)
            {
                //No ha sido agregado el mismo producto al carro
                ItemCarro itemCarro = new ItemCarro()
                {
                    Producto = P,
                    Cantidad = 1,
                    CarroCompraId = this.CarroCompraId,
                    ProductoId = P.ProductoId
                };
                _context.Add(itemCarro);
            }
            else {
                item.Cantidad++;
            }
            _context.SaveChanges();
        }

        public void delItem(Producto P) {
            var item = _context.tblItemsCarro
                .Where(i => i.ProductoId == P.ProductoId && i.CarroCompraId == this.CarroCompraId)
                .FirstOrDefault();
            //TODO: Logica para restar uno a la cantidad
            _context.Remove(item);
            _context.SaveChanges();
        }


        public List<ItemCarro> GetItemsCarro() {

            return _context.tblItemsCarro
                .Where(i => i.CarroCompraId == this.CarroCompraId)
                .Include(i => i.Producto).ToList();
        
        }

        public void VaciarCarro() { 
            var items = _context.tblItemsCarro
                .Where(i => i.CarroCompraId == this.CarroCompraId)
                .ToList();
            _context.tblItemsCarro.RemoveRange(items);
            _context.SaveChanges();
        }

        public double GetTotalCarro() {
            return _context.tblItemsCarro.Where(i => i.CarroCompraId == this.CarroCompraId)
                .Select(i => i.Producto.Precio * i.Cantidad).Sum();
        }
    }
}
