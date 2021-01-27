using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using LachlanBarclayNet.ViewModel;
using LachlanBarclayNet.DAO;
using System.Configuration;
using System.Threading.Tasks;

using System.Web;

namespace LachlanBarclayNet.Controllers
{
    public class ContactController : Controller
    {
        [Route("contact")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("contact")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(IndexContactViewModel ViewModel)
        {
            if (ViewModel == null)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                ContactDAO contactDAO = new ContactDAO();
                contactDAO.InsertContact(ViewModel.Name, ViewModel.Email, ViewModel.Message);

                ViewModel.EmailSent = true;
            }

            return View(ViewModel);
        }

    }

}
