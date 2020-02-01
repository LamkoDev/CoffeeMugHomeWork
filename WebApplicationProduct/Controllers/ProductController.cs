using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unity;
using WebApplicationProduct.Models;


namespace WebApplicationProduct.Controllers
{
    [Produces("application/json")]
    public class ProductController : Controller//, IModelBinder
    {
        DataBaseBridge dataBaseBridge;
        public ProductController(IUnityContainer container)
        {
            dataBaseBridge = CreateAndSetDb();
            Debug.Assert(null != container);
        }
        private DataBaseBridge CreateAndSetDb()
        {
            DataBaseBridge dataBase = new DataBaseBridge();
            dataBase.UseSqlDbImplementation();
            return dataBase;
        }
        public IActionResult Index()
        {
            ViewBag.Products = Get();
            return View();
        }
        [HttpGet]
        [Route("~/GetAll")]
        public List<Product> Get()
        {
            Trace.WriteLine("Get");
          
            return dataBaseBridge.GetProducts();
        }
        [HttpGet]
        [Route("~/Get")]
        public Product Get(Guid id)
        {
            Trace.WriteLine("Get id");
            return dataBaseBridge.GetProduct(id);
        }
        [HttpPost]
        [Route("~/Post")]
        public IActionResult Post(ProductCreateRequestDto request)
        {
            if (ModelState.IsValid)
            {
            Trace.WriteLine("Post");
                Response.StatusCode = 200;
                return Content(dataBaseBridge.AddProduct(request).ToString());
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
                dataBaseBridge.Update(request);
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
            dataBaseBridge.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProduct(Guid id)
        {
            Trace.WriteLine("Delete");
            dataBaseBridge.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ViewResult AddForm()
        {
            return View();
        }
        public ViewResult AddForm(ProductCreateRequestDto request)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product(request.Name, request.Price);
                product.Id = dataBaseBridge.AddProduct(request);
                return View("ProductAdded", product);
            }
            return View();
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
            Product product = dataBaseBridge.GetProduct(id);
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
                dataBaseBridge.Update(request);
                return View("ProductUpdated", request);
            }
            Trace.WriteLine("Not valid");
            return View();
        }
        public ViewResult ProductUpdated(ProductUpdateRequestDto updatedProduct)
        {
            if (ModelState.IsValid)
            {
                return View("ProductUpdated", updatedProduct);
            }
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
