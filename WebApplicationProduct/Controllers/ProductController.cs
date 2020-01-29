using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationProduct.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.SQLite;

namespace WebApplicationProduct.Controllers
{
    public class ProductController : Controller//, IModelBinder
    {
        public IActionResult Index()
        {
            ViewBag.Products = Get();
            CreateDb();
            return View();
        }

        [HttpPost]
        public object Index(ProductCreateRequestDto request)
        {
    
            ViewBag.Products = Get();
            if (!ModelState.IsValid)
            {
                Trace.WriteLine("Request BAD!!");
                return View("Index");
            }
            Trace.WriteLine("Name: "+request.Name+" Price: "+request.Price);
            Trace.WriteLine("Request OK");
            request.Id = Post(request);
            Trace.WriteLine("GUID: "+ request.Id);
            return View("AddProduct", request);
        }
        [HttpPost]
        public Guid Post(ProductCreateRequestDto request)
        {
            Product product = new Product(request.Name,request.Price);
            return product.GetId();
        }
        public List<Product> Get()
        {
            try
            {
                string cs = @"URI=file:C:\Users\Xeni\Desktop\test.db";
                using var con = new SQLiteConnection(cs);
                con.Open();
                string stm = "SELECT * FROM products LIMIT 5";
                using var cmd = new SQLiteCommand(stm, con);
                using SQLiteDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Trace.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetInt32(2)}");
                }
                con.Close();
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex);
            }
           
            return MakeFakeProducts();
        }

        public void CreateDb()
        {
            string cs = @"URI=file:C:\Users\Xeni\Desktop\test.db";
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = "DROP TABLE IF EXISTS products";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE products(id INTEGER PRIMARY KEY,
                    name TEXT, price INT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO products(name, price) VALUES('Audi',52642)";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Product> MakeFakeProducts()
        {
            List<Product> products = new List<Product>();

            Product product0 = new Product("Kawa", 2);
            Product product1 = new Product("Iphone", 1500);
            Product product2 = new Product("Samsung", 1900);
            products = new List<Product>();
            products.Add(product0);
            products.Add(product1);
            products.Add(product2);
            return products;
        }

        [HttpGet]
        public ViewResult AddProduct()
        {
            return View();
        }
        public ViewResult AddProduct(ProductCreateRequestDto request)
        {

            return View("Index");
        }





        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
