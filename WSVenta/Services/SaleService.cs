using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Request;

namespace WSVenta.Services
{
    public class SaleService : ISaleService
    {
        public void Add(SaleRequest model)
        {
                using (var db = new VENTA_REALContext())
                {
                    //aca especificas que se trata de una transaccion por entity framework, aqui se inicia la transacción 
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var sale = new Sale();
                            sale.Total = model.Conceptos.Sum(d => d.Amount * d.UnitPrice);
                            sale.Date = DateTime.Now;
                            sale.IdCustomer = model.IdCustomer;
                            db.Sale.Add(sale);
                            //en esta siguiente instruccion la propia base de datos le agraga un id
                            db.SaveChanges();

                            foreach (var modelConcepto in model.Conceptos)
                            {
                                var concepto = new Models.Concept();
                                concepto.Amount = modelConcepto.Amount;
                                concepto.IdProduct = modelConcepto.IdProduct;
                                concepto.TotalPrice = modelConcepto.TotalPrice;
                                concepto.UnitPrice = modelConcepto.UnitPrice;

                                concepto.IdSale = sale.Id;

                                db.Concept.Add(concepto);
                                //en el save changes es en el momento en el que se toma la base de datos y no te daja seguir si este no suelta la tabla antes 
                                db.SaveChanges();
                            }
                            transaction.Commit();                            
                        }
                        catch (Exception)
                        {
                            //aca en caso de que alguna instruccion falle se llevara acabo este lado
                            transaction.Rollback();
                            //esto se realiza para que al momento de invocarlo en el controlador este pueda tener razon de el si es que llega a tener algun error 
                            throw new Exception("Ocurrio un error en la insercion");
                        };
                    }
                }
        }
    }
}
