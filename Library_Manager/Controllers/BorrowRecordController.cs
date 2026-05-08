using Library_Manager_BLL;
using Microsoft.AspNetCore.Mvc;

namespace Library_Manager.Controllers
{
    public class BorrowRecordController : Controller
    {
        public IActionResult ShowAll()
        {
            return View(clsBorrowRecord_BLL.GetAllBorrowRecord()??new List<clsBorrowRecord_BLL>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Books = clsBook_BLL.GetAllBooks();

            return View();
        }

        [HttpPost]
        public IActionResult Create(clsBorrowRecord_BLL BR)
        {

            ModelState.Remove("BorrowDate");
            ModelState.Remove("ReturnDate");
            ModelState.Remove("Status");
            ViewBag.Books = clsBook_BLL.GetAllBooks();

            if (!ModelState.IsValid)
            {

                return View(BR);
            }
            if (BR.AddBorrowRecord())
            {
                return RedirectToAction("ShowAll");
            }
            return View(BR);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Books = clsBook_BLL.GetAllBooks();

            clsBorrowRecord_BLL BR =clsBorrowRecord_BLL.GetBorrowRecordByID(id);
            if (BR != null)
                return View(BR);
            else
                return RedirectToAction("ShowAll");
        }

        [HttpPost]
        public IActionResult Edit(clsBorrowRecord_BLL BR)
        {
            ViewBag.Books = clsBook_BLL.GetAllBooks();

            if (!ModelState.IsValid)
            {

                return View(BR);
            }
            if (BR.UpdateBorrowRecord())
            {
                return RedirectToAction("ShowAll");
            }
            return View(BR);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            clsBorrowRecord_BLL.DeleteBorrowRecord(id);
            return RedirectToAction("ShowAll");

        }
    }
}
