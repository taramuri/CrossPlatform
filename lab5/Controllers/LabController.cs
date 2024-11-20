using Microsoft.AspNetCore.Mvc;

namespace lab5.Controllers
{
    public class LabController : Controller
    {
        [HttpGet]
        [Route("Lab1")]
        public IActionResult Lab1()
        {
            return View();
        }

        [HttpPost]
        [Route("Lab1")]
        public IActionResult Lab1(string inputData)
        {
            try
            {
                var labsRunner = new LabsRunner("Lab1");
                string result = labsRunner.ProcessData(inputData);

                ViewBag.Result = result;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        [Route("Lab2")]
        public IActionResult Lab2()
        {
            return View();
        }

        [HttpPost]
        [Route("Lab2")]
        public IActionResult Lab2(string inputData)
        {
            try
            {
                var labsRunner = new LabsRunner("Lab2");
                string result = labsRunner.ProcessData(inputData);

                ViewBag.Result = result;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        [Route("Lab3")]
        public IActionResult Lab3()
        {
            return View();
        }

        [HttpPost]
        [Route("Lab3")]
        public IActionResult Lab3(string inputData)
        {
            try
            {
                var labsRunner = new LabsRunner("Lab3");
                string result = labsRunner.ProcessData(inputData);

                ViewBag.Result = result;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}