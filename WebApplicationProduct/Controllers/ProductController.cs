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
using System.Data.SqlClient;

namespace WebApplicationProduct.Controllers
{
    public class ProductController : Controller//, IModelBinder
    {

        public IActionResult Index()
        {

            ProductCreateRequestDto request = new ProductCreateRequestDto();
            request.Name = "Pralka";
            request.Price = 1200.25M;

            ViewBag.Products = Get();
            Trace.WriteLine("");
            Trace.WriteLine("");
            Trace.WriteLine("index");
            Trace.WriteLine("");
            Trace.WriteLine("");
            // Post(request);

            return View();
        }
        [HttpPost]
        public Guid Post(ProductCreateRequestDto request)
        {
            Trace.WriteLine("");
            Trace.WriteLine("");
            Trace.WriteLine(request.Name);
            Trace.WriteLine(request.Price);
            Trace.WriteLine("");
            Trace.WriteLine("");
            DataBaseBridge dataBase = new DataBaseBridge();
            string connectionString = @"Server = DESKTOP-3L9UIL8\SQLEXPRESS; " +
                                       "Database = CoffeeMugHomeWork; " +
                                       "User Id = Visual; " +
                                       "Password = 1231;";
            dataBase.UseSqlDbImplementation(connectionString);
            return dataBase.AddProduct(request);
        }

        
        public List<Product> Get()
        {
            DataBaseBridge dataBase = new DataBaseBridge();
            string connectionString = @"Server = DESKTOP-3L9UIL8\SQLEXPRESS; " +
                                       "Database = CoffeeMugHomeWork; " +
                                       "User Id = Visual; " +
                                       "Password = 1231;";
            dataBase.UseSqlDbImplementation(connectionString);
            return dataBase.GetProducts();
        }



        [HttpGet]
        public ViewResult AddProduct()
        {
            return View();
        }
        public Guid AddProduct(ProductCreateRequestDto request)
        {
            Trace.WriteLine("");
            Trace.WriteLine("");
            Trace.WriteLine(request.Name);
            Trace.WriteLine(request.Price);
            Trace.WriteLine("");
            Trace.WriteLine("");
            return Post(request);
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
