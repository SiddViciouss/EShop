using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            //var test = HttpContext.Session.GetString("_test");
            return View();
        }


    }
}