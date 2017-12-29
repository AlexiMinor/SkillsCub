using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkillsCub.MVC.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }
    }
}