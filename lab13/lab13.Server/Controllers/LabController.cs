using Microsoft.AspNetCore.Mvc;
using LabLibrary;
using lab13.Server.Models;

namespace lab13.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabController : ControllerBase
    {
        [HttpGet("lab1")]
        public IActionResult GetLab1()
        {
            var model = new LabViewModel
            {
                LabNumber = 1,
                Title = "Лабораторна робота 1",
                Description = "Іван Іванович запросив на свій день народження багато гостей. Він написав на картках прізвища всіх гостей і розклав ці картки на столі, вважаючи, що кожен гість сяде там, де виявить картку зі своїм прізвищем (прізвища в усіх гостей різні). Проте гості не звернули уваги на картки та сіли за стіл у довільному порядку. При цьому Іван Іванович із подивом виявив, що жоден гість не сів на призначене місце.\r\n\r\nПотрібно написати програму, яка знайде скільки можна розсадити гостей так, щоб жоден з них не сидів там, де лежала картка з його прізвищем.",
                InputDescription = "У вхідному файлі INPUT.TXT задано ціле число N – кількість гостей (1 ≤ N ≤ 100).",
                OutputDescription = "Вихідний файл OUTPUT.TXT повинен містити одне ціле число – кількість способів розсадити гостей."               
            };
            return Ok(model);
        }

        [HttpPost]
        [Route("Lab1")]
        public IActionResult Lab1(string inputData)
        {
            try
            {
                var labRunner = new LabRunner("lab1");
                var result = labRunner.ProcessData(inputData);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("lab2")]
        public IActionResult GetLab2()
        {
            var model = new LabViewModel
            {
                LabNumber = 2,
                Title = "Лабораторна робота 2",
                Description = "Задано цілий прямокутний масив M×N. Необхідно визначити прямокутну область масиву, сума елементів якого максимальна.",
                InputDescription = "У першому рядку вхідного файлу INPUT.TXT записані два натуральні числа N і M (1 ≤ N, M ≤ 100) – кількість рядків і стовпців прямокутної матриці. Далі йдуть N рядків по M чисел, записаних через пропуск - елементи масиву, цілі числа, що не перевищують 100 по абсолютній величині.",
                OutputDescription = "У вихідний файл OUTPUT.TXT необхідно вивести цілу кількість – суму елементів знайденого прямокутного підмасиву. Підмасив має містити хоча б один елемент."
            };
            return Ok(model);
        }

        [HttpPost]
        [Route("Lab2")]
        public IActionResult Lab2(string inputData)
        {
            try
            {
                var labRunner = new LabRunner("lab2");
                var result = labRunner.ProcessData(inputData);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("lab3")]
        public IActionResult GetLab3()
        {
            var model = new LabViewModel
            {
                LabNumber = 3,
                Title = "Лабораторна робота 3",
                Description = "Дано орієнтований зважений граф. Вам необхідно знайти пару вершин, найкоротшу відстань від однієї з яких до іншої максимально серед усіх пар вершин.",
                InputDescription = "У першому рядку вхідного файлу INPUT.TXT записано одиницю N (1 ≤ N ≤ 100) - кількість вершин графа. У наступних N рядках записано N чисел - матриця суміжності графа, де -1 означає відсутність ребра між вершинами, а будь-яке невід'ємне число - присутність ребра з даною вагою. Елементи матриці - цілі числа в межах від -1 до 100. На головній діагоналі матриці завжди нулі. Гарантується, що в графі є хоча б одне ребро.",
                OutputDescription = "У вихідний файл OUTPUT.TXT потрібно вивести максимальну найкоротшу відстань."
            };
            return Ok(model);
        }

        [HttpPost]
        [Route("Lab3")]
        public IActionResult Lab3(string inputData)
        {
            try
            {
                var labRunner = new LabRunner("lab3");
                var result = labRunner.ProcessData(inputData);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("run")]
        public IActionResult RunLab([FromForm] RunLabRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Lab) || string.IsNullOrEmpty(request.InputData))
                {
                    return BadRequest(new { Error = "Lab number and input data are required" });
                }

                var labRunner = new LabRunner(request.Lab);
                var result = labRunner.ProcessData(request.InputData);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public class RunLabRequest
    {
        public string Lab { get; set; }
        public string InputData { get; set; }
    }
}