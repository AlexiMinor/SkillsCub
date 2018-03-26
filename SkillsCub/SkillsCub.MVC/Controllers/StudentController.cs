using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SkillsCub.MVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index() //same with get current course
        {
            return View();
        }
        //TODO read all questions
        //todo add answer to exercise 
        //todo watch current courses


        public async Task<IActionResult> GetCurrentTask()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> AddAnswer()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswer(int i)//add model
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetMarks()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetMarkDetail()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DownloadCertificate()
        {
            throw new NotImplementedException();
        }

    }
}