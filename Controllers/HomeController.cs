using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using Antlr.Runtime.Misc;
using Homework03.Models;
using Newtonsoft.Json;

namespace Homework03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        public async Task<ActionResult> Index()
        {
            var studentss = await db.students.ToListAsync();
        
            var bookss = await db.books
                       .Include(t => t.authors)
                       .Include(t => t.borrows)
                       .Include(t => t.types).ToListAsync();

            var viewModel = new Students_Books
            {
                studentList = studentss,
                booksList = bookss,
                StatusList = bookss.Select(book =>
                {
                    if (book.borrows.Any(borrow => borrow.broughtDate == null))
                    {
                        return "Out";
                    }
                    return "Available";
                }).ToList()
            };

            return View(viewModel);
        }

        public async Task<ActionResult> Report()
        {
            // Get the top 10 students who borrowed the most books
            var topStudents = db.borrows.GroupBy(b => b.studentId)
                                        .Select(g => new
                                        {
                                            StudentId = g.Key,
                                            NumOfBooksTaken = g.Count()
                                        })
                                        .OrderByDescending(s => s.NumOfBooksTaken)
                                        .Take(10)
                                        .ToList();

            // Retrieve the student details (name and surname) and store in a List
            List<LibraryVM> topStudentDetails = new List<LibraryVM>();
            foreach (var student in topStudents)
            {
                var studentDetails = db.students.FirstOrDefault(s => s.studentId == student.StudentId);
                if (studentDetails != null)
                {
                    topStudentDetails.Add(new LibraryVM
                    {
                        mystudentsList = new List<students> { studentDetails },
                        numOfBookstaken = new List<string> { student.NumOfBooksTaken.ToString() }
                    });
                }
            }

            // Get data for the pie chart
            var typeData = db.types.Select(t => new
            {
                Name = t.name,
                NumOfBooks = t.books.Count
            }).ToList();

            // Pass the data to the view
            ViewBag.TopStudents = topStudentDetails;
            ViewBag.TypeData = JsonConvert.SerializeObject(typeData);

            return View();
        }

        public async Task<ActionResult> Maintain()
        {
            var viewModel = new MaintainModelView
            {
                borrowsList = await db.borrows
                .Include(t => t.students)
                .Include(t => t.books)
                .ToListAsync(),
                authorsList = await db.authors.ToListAsync(),
                typesList = await db.types.ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "studentId,name,surname,birthdate,gender,class,point")] students students)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(students);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(students);
        }
   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
