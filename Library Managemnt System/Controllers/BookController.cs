using Library_Managemnt_System.Models;
using Library_Managemnt_System.Repositories;
using Library_Managemnt_System.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Library_Managemnt_System.Controllers
{
    public class BookController : Controller
    {
        IBookRepository bookRepository;
        ICategoryRepository categoryRepository;
		private readonly IWebHostEnvironment webHostEnvironment;
		public BookController(IBookRepository _bookRepository, ICategoryRepository _categoryRepository, IWebHostEnvironment _webHostEnvironment) { bookRepository = _bookRepository; categoryRepository = _categoryRepository; webHostEnvironment = _webHostEnvironment; }
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Categories"]=categoryRepository.GetAll();
            return View("Add");
        }
        [HttpPost]
        public IActionResult SaveAdd(BookViewModel bookrequest)  
        {
            if(ModelState.IsValid)
            {
				Book book = new Book();
                string uniqueFileName=null;

                //// Save image to "wwwroot/images" folder
                if (bookrequest.Image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
					 uniqueFileName = Guid.NewGuid().ToString() + "_" + bookrequest.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
						bookrequest.Image.CopyTo(fileStream);
						fileStream.Close();
					}
                    book.Image = "/Images/" + uniqueFileName;

                }
                //book.Id = bookrequest.Id;
                book.Name = bookrequest.Name;
                book.Author = bookrequest.Author;
                book.CategoryId = bookrequest.CategoryId;
                book.Quantity = bookrequest.Quantity;
                book.Image = "/Images/" + uniqueFileName;
                bookRepository.Add(book);
				bookRepository.Save();
				return RedirectToAction("GetAll");
			}
            ViewData["Categories"] = categoryRepository.GetAll();
            return View("Add", bookrequest);
        }
        public IActionResult GetAll() 
        {
           return View( "Books",bookRepository.GetAll());

        }
        [HttpGet]
        public IActionResult Update(int id) 
        {
            var book = bookRepository.GetById(id);
            ViewData["Categories"] = categoryRepository.GetAll();
            return View("Edit", book);
        }
        [HttpPost]
        public IActionResult SaveUpdate(Book book)
        {
            if (ModelState.IsValid)
            {
                Book BookFromDb = bookRepository.GetById(book.Id);
                BookFromDb.Name=book.Name;
                BookFromDb.Author=book.Author;
                BookFromDb.CategoryId=book.CategoryId;
                BookFromDb.Image=book.Image;
                BookFromDb.Quantity=book.Quantity;
                bookRepository.Update(BookFromDb);
                bookRepository.Save();
                return RedirectToAction("GetAll");
            }
            //var deptlist = dbContext.departments.ToList();
            ViewData["Categories"] = categoryRepository.GetAll();
            return View("Edit", book);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Book BookFromDb = bookRepository.GetById(id);
            bookRepository.Delete(BookFromDb);
            bookRepository.Save();
            return RedirectToAction("GetAll");
        }
		public IActionResult uploadImage()
		{
			return View();
		}
		[HttpPost]
		public IActionResult uploadImage(IFormFile Image)
		{
			string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");

			string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
			string filePath = Path.Combine(uploadsFolder, uniqueFileName);
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				Image.CopyTo(fileStream);
				fileStream.Close();
			}

			return View();
		}


	}
}
