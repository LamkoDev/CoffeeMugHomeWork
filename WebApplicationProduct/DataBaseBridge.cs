using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationProduct.Models;
using System.Data.Sql;
using System.Diagnostics;

namespace WebApplicationProduct
{
    public class DataBaseBridge
    {
        protected IDataBase DbImplementation;
        public void UseSqlDbImplementation()
        {
            string server = @"DESKTOP-3L9UIL8\SQLEXPRESS";
            string database = "CoffeeMugHomeWork";
            string user = "Visual";
            string password = "1231";
            DbImplementation = new SqlDbApiConcrete(server,database,user,password);
        }
        public List<Models.Product> GetProducts() 
        {
            return DbImplementation.GetProducts();
        }
        public Guid AddProduct(ProductCreateRequestDto request)
        {
            return DbImplementation.AddProduct(request);
        }
        public void Delete(Guid id)
        {
            DbImplementation.DeleteProduct(id);
        }
        public Product GetProduct(Guid id)
        {
            return DbImplementation.GetProduct(id);
        }
        public void Update(ProductUpdateRequestDto request)
        {
            DbImplementation.UpdateProduct(request);
        }
    }
    public interface IDataBase
    {
        List<Models.Product> GetProducts();
        Models.Product GetProduct(Guid id);
        Guid AddProduct(Models.ProductCreateRequestDto request);
        void UpdateProduct(Models.ProductUpdateRequestDto request);
        void DeleteProduct(Guid id);
    }
}
