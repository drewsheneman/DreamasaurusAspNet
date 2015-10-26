using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Dreamasaurus.DAL;
using Dreamasaurus.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dreamasaurus.Controllers
{
    public class DreamsController : Controller
    {
        private readonly DreamsDbContext _db = new DreamsDbContext();
        private readonly UserManager<ApplicationUser> _userManager;

        public DreamsController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        // GET: Dreams
        public ActionResult Index()
        {
            return View(_db.Dreams.ToList());
        }

        // GET: Dreams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dream dream = _db.Dreams.Find(id);
            if (dream == null)
            {
                return HttpNotFound();
            }
            return View(dream);
        }

        // GET: Dreams/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dreams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text")] Dream dream)
        {
            var currentUser = _userManager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                dream.User = currentUser;
                _db.Dreams.Add(dream);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dream);
        }

        // GET: Dreams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dream dream = _db.Dreams.Find(id);
            if (dream == null)
            {
                return HttpNotFound();
            }
            return View(dream);
        }

        // POST: Dreams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text")] Dream dream)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(dream).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dream);
        }

        // GET: Dreams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dream dream = _db.Dreams.Find(id);
            if (dream == null)
            {
                return HttpNotFound();
            }
            return View(dream);
        }

        // POST: Dreams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dream dream = _db.Dreams.Find(id);
            _db.Dreams.Remove(dream);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
