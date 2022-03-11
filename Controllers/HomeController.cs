using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website_Credenciall.Controllers
{
    public class HomeController : Controller
    {
        credenciall_websiteEntities _context = new credenciall_websiteEntities();
        ConfigDB db = new ConfigDB();
        public ActionResult Index()
        {
            db.Connect("ecspassdb");
            var homeHeaderData = _context.Highlights
                .First();
            ViewData["HeaderTitle"] = homeHeaderData.hl_header;
            ViewData["HeaderText"] = homeHeaderData.hl_subtitle;
            ViewData["Slogan"] = homeHeaderData.hl_slogan;
            ViewData["PostHeader"] = homeHeaderData.hl_post_header;
            ViewData["PostText"] = homeHeaderData.hl_post_text;
            ViewData["CTA"] = homeHeaderData.hl_cta;
            //db.ExecuteSelected("SELECT * from `clients` WHERE code='" + "9999" + "'" + ";");
            //if (db.Count() == 1)
            //{
            //    ViewData["Client"] = db.Results(0, "name");
            //}
            //else
            //{
            //    ViewData["Client"] = "";
            //}
            List<string> userList = new List<string>();
            db.ExecuteSelected("SELECT * FROM `users`;");
            if (db.Count() > 0)
            {
                int users = db.Count();
                for (int i = 0; i < users; i++)
                {
                    userList.Add(db.Results(i, "name"));
                }
            }
            ViewBag.userList = userList;

            return View();
        }
        public ActionResult AdminIndexHighlight()
        {
            var listOfHighlights = _context.Highlights.ToList();
            return View(listOfHighlights);
        }
        [HttpGet]
        public ActionResult AdminCreateHighlight()
        {
            return View();
        }
        public ActionResult AdminCreateHighlight(Highlight highlight)
        {
            _context.Highlights.Add(highlight);
            _context.SaveChanges();
            ViewBag.Message = "Dados salvos com sucesso!";
            return View();
        }
        [HttpGet]
        public ActionResult AdminEditHighlight(int id)
        {
            var data = _context.Highlights.Where(x => x.id == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult AdminEditHighlight(Highlight highlight)
        {
            var data = _context.Highlights.Where(x => x.id == highlight.id).FirstOrDefault();
            if (data != null)
            {
                data.hl_header = highlight.hl_header;
                data.hl_subtitle = highlight.hl_subtitle;
                data.hl_post_header = highlight.hl_post_header;
                data.hl_post_text = highlight.hl_post_text;
                data.hl_cta = highlight.hl_cta;
                data.active = highlight.active;
                _context.SaveChanges();
            }
            return RedirectToAction("AdminIndexHighlight");
        }
        public ActionResult AdminDetailHighlight(int id)
        {
            var data = _context.Highlights.Where(x => x.id == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult AdminDeleteHighlight(int id)
        {
            var data = _context.Highlights.Where(x => x.id == id).FirstOrDefault();
            _context.Highlights.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Dado excluído com sucesso!";
            return RedirectToAction("AdminIndexHighlight");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}