using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CIT275_Back_end_interface.Models;
using DAL;
using PagedList;

namespace CIT275_Back_end_interface.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DataRepository _dc = new DataRepository();



        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CitySortParm = sortOrder == "City" ? "city_desc" : "City";
            ViewBag.ZipSortParm = sortOrder == "Zip" ? "zip_desc" : "Zip";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var clients = from s in db.Clients
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.CompanyName.StartsWith(searchString)
                                        || s.City.StartsWith(searchString)
                                       || s.State.StartsWith(searchString)
                                       || s.ZipCode.Equals(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    clients = clients.OrderBy(s => s.CompanyName);
                    break;
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.CompanyName);
                    break;
                case "City":
                    clients = clients.OrderBy(s => s.City);
                    break;
                case "city_desc":
                    clients = clients.OrderByDescending(s => s.City);
                    break;
                case "Zip":
                    clients = clients.OrderBy(s => s.ZipCode);
                    break;
                case "zip_desc":
                    clients = clients.OrderByDescending(s => s.ZipCode);
                    break;
                default:  // Name ascending 
                    clients = clients.OrderBy(s => s.CompanyName);
                    break;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));
        }

        // GET: Clients
        //[Authorize(Roles = "Admin, Staff")]
        //public ActionResult Index()
        //{

        //    var lst = _dc.GetClientList();
        //    return View(lst);
        //    // return View(db.Clients.ToList());
        //}




        //[Authorize(Roles = "Admin, Staff")]
        //[HttpPost]
        //public ActionResult Index(FormCollection fc)
        //{




        //    string _company = "", _city = "", _state = "";


        //    _company = fc["company"] != null ? fc["company"] : "";
        //    _city = fc["city"] != null ? fc["city"] : "";
        //    _state = fc["state"] != null ? fc["state"] : "";

           


        //    var lst = _dc.GetClientList(_company,_city,_state);

        //    return View(lst);

        //}



        // GET: Clients/Details/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Create()
        {
           // @Html.DropDownListFor(m => m.ClientId, (IEnumerable<SelectListItem>)ViewBag.CustList, "--Select One--")
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,CompanyName,Address1,Address2,City,State,ZipCode,Phone1,Phone1Type,Phone2,Phone2Type,Email,EffDate,Active,DeleteInd")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit([Bind(Include = "ClientID,CompanyName,Address1,Address2,City,State,ZipCode,Phone1,Phone1Type,Phone2,Phone2Type,Email,EffDate,Active,DeleteInd")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Staff")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
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
