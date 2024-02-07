//using AspNetCore;
using DemoAppMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace DemoAppMvc.Controllers
{
    public class StaffController : Controller
    {
        //private static List<Staff> staffList = new List<Staff>();
        //commented them so i can use the data injection directly from the demoAppContext
        //private static int index = 1;

        private DemoAppDbContext ctx; //called from the DemoAppDbContext model


        public StaffController(DemoAppDbContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Header = "Staff Page";
            return View();
        }

        public IActionResult List()
        {
            ViewBag.Header = "Staff List Page";
            List<Staff> staffList = ctx.StaffAll.ToList();
            return View(staffList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Header = "Create Staff";
            return View();
        }

        [HttpPost]
        public IActionResult Create(string FirstName, string LastName, int Age)
        {

            //used when connecting to a database
            Staff st = new Staff()
            {

                FirstName = FirstName,
                LastName = LastName,
                Age = Age
            };
            ctx.StaffAll.Add(st);
            ctx.SaveChanges();
            return RedirectToAction("List");
        }

        //used when not connecting a database
        /*
        Staff staff = new Staff()
        {
            Id = index++,
            FirstName = FirstName,
            LastName = LastName,
            Age = Age
        };
        staffList.Add(staff);
        return RedirectToAction("List");
        */


        /*
        [HttpPost]
        public IActionResult Create(Staff st) 
        {
            ctx.staffs.Add(st);
            ctx.SaveChanges();
            return RedirectToAction("List");
        }
        */


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Staff? st = ctx.StaffAll.Find(id);
            if(st == null)
            {
                return NotFound();
            }
            ctx.StaffAll.Remove(st);
            ctx.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            ViewBag.Header = "Staff Details";

            Staff? staff = ctx.StaffAll.Find(id);
            return View(staff);
        }

        /*public IActionResult View(int id) {
            Staff st = new Staff();
            foreach (Staff s in staffList)
            {
                if (s.Id == id)
                {
                    st = s;
                    break;
                }
            }
            return View(st);
        }*/

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            ViewBag.Header = "Edit Staff Details";
            Staff? st = ctx.StaffAll.Find(id);

            Staff? staff1 = new Staff()
            {

                FirstName = st.FirstName,
                LastName = st.LastName,
                Age = st.Age
            };
            return View(staff1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Staff st)
        {
           if (ModelState.IsValid)
            {
                ctx.StaffAll.Update(st);
                ctx.SaveChanges();
                return RedirectToAction("List");
            }
           return View("Edit", st);
        }
    }
}
