using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foniste.Controllers
{
	public class SigninController : Controller
	{
		// GET: SigninController
		public ActionResult Index()
		{
			return View();
		}

		// GET: SigninController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: SigninController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: SigninController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: SigninController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: SigninController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: SigninController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: SigninController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
