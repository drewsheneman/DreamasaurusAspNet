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
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public DreamsController()
        {
            _unitOfWork = new UnitOfWork();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_unitOfWork.DreamsRepository.Context));
        }

        // GET: Dreams
        public ViewResult Index()
        {
            var dreams = _unitOfWork.DreamsRepository.Get();
            return View(dreams.ToList());
        }

        // GET: Dreams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dream dream = _unitOfWork.DreamsRepository.GetById(id);
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
                _unitOfWork.DreamsRepository.Insert(dream);
                _unitOfWork.Save();
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
            Dream dream = _unitOfWork.DreamsRepository.GetById(id);
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
                _unitOfWork.DreamsRepository.Update(dream);
                _unitOfWork.Save();
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
            Dream dream = _unitOfWork.DreamsRepository.GetById(id);
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
            // ReSharper disable once UnusedVariable
            Dream dream = _unitOfWork.DreamsRepository.GetById(id);
            _unitOfWork.DreamsRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
