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
using System.Net;


namespace WebApplicationProduct.Controllers
{
    [Produces("application/json")]
    public class ProductController : Controller//, IModelBinder
    {
        public IActionResult Index()
        {
            ViewBag.Products = Get();
            return View();
        }
        DataBaseBridge dataBase = new DataBaseBridge();


        [HttpGet]
        [Route("~/GetAll")]
        public List<Product> Get()
        {
            Trace.WriteLine("Get");
            DataBaseBridge dataBase = CreateAndSetDb();
            return dataBase.GetProducts();
        }
        [HttpGet]
        [Route("~/Get")]
        public Product Get(Guid id)
        {
            Trace.WriteLine("Get id");
            DataBaseBridge dataBase = CreateAndSetDb();
            return dataBase.GetProduct(id);
        }
        [HttpPost]
        [Route("~/Post")]
        public IActionResult Post(ProductCreateRequestDto request)
        {
            if (ModelState.IsValid)
            {
            Trace.WriteLine("Post");
                DataBaseBridge dataBase = CreateAndSetDb();
                Response.StatusCode = 200;
                return Content(dataBase.AddProduct(request).ToString());
            }
            Response.StatusCode = 400;
            return Content("Invalid request !!!");
        }

        [HttpPut]
        [Route("~/Put")]
        public IActionResult Put(ProductUpdateRequestDto request)
        {
            if (ModelState.IsValid)
            {
                Trace.WriteLine("Put");
                DataBaseBridge dataBase = CreateAndSetDb();
                dataBase.Update(request);
                return RedirectToAction("Index");
            }
            Response.StatusCode = 400;
            return Content("Invalid request !!!");
        }


        [HttpDelete]
        [Route("~/Delete")]
        public IActionResult Delete(Guid id)
        {
            Trace.WriteLine("Delete");
            DataBaseBridge dataBase = CreateAndSetDb();
            dataBase.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProduct(Guid id)
        {
            Trace.WriteLine("Delete");
            DataBaseBridge dataBase = CreateAndSetDb();
            dataBase.Delete(id);
            return RedirectToAction("Index");
        }

            private DataBaseBridge CreateAndSetDb()
        {
            DataBaseBridge dataBase = new DataBaseBridge();
            string connectionString = @"Server = DESKTOP-3L9UIL8\SQLEXPRESS; " +
                                       "Database = CoffeeMugHomeWork; " +
                                       "User Id = Visual; " +
                                       "Password = 1231;";
            dataBase.UseSqlDbImplementation(connectionString);
            return dataBase;
        }
        [HttpGet]
        public ViewResult AddForm()
        {
            return View();
        }
        public ViewResult AddForm(ProductCreateRequestDto request)
        {
            Product product = new Product(request.Name, request.Price);
            DataBaseBridge dataBase = CreateAndSetDb();
            product.Id = dataBase.AddProduct(request);
            return View("ProductAdded", product);
        }
        public ViewResult ProductAdded(Product request)
        {
            return View(request);
        }
            public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        [HttpGet]
        public ViewResult EditProduct(Guid id)
        {
            Trace.WriteLine("Edit");
            DataBaseBridge dataBase = CreateAndSetDb();
            Product product = dataBase.GetProduct(id);
            ProductUpdateRequestDto request = new ProductUpdateRequestDto();
            request.Id = product.Id;
            request.NewName = product.Name;
            request.NewPrice = product.Price;
            return View(request);
        }

        public ViewResult EditProduct(ProductUpdateRequestDto request)
        {
            if (ModelState.IsValid)
            {
                Trace.WriteLine("doUpdate");
                DataBaseBridge dataBase = CreateAndSetDb();
                dataBase.Update(request);
                return View("ProductUpdated", request);
            }
            Trace.WriteLine("Not valid");
            return View();
        }
        public ViewResult ProductUpdated(ProductUpdateRequestDto updatedProduct)
        {
            return View("ProductUpdated", updatedProduct);
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
