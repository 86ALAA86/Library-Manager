using Library_Manager_BLL;
using Microsoft.AspNetCore.Mvc;

namespace Library_Manager.Controllers
{
    public class BookController : Controller
    {
        public IActionResult ShowAll()
        {
            return View(clsBook_BLL.GetAllBooks() ?? new List<clsBook_BLL>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(clsBook_BLL B)
        {
            if (!ModelState.IsValid)
            {
                return View(B);
            }
            if (B.AddBook())
            {
                return RedirectToAction("ShowAll");
            }

            return View(B);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            clsBook_BLL B = clsBook_BLL.GetBookByID(id);
            if (B == null)
            {
                return RedirectToAction("ShowAll");
            }
            return View(B);
        }

        [HttpPost]
        public IActionResult Edit(clsBook_BLL B)
        {
            if (!ModelState.IsValid)
            {
                return View(B);
            }
            if (B.UpdateBook())
            {
               return RedirectToAction("ShowAll");
            }
           
                return View(B);
            
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {

            clsBook_BLL.DeleteBook(id);
            return RedirectToAction("ShowAll");

        }

    }
}
