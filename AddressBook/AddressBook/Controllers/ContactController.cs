using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AddressBook;

namespace AddressBook.Controllers
{
    public class ContactController : Controller
    {
        private AddressDBEntities db = new AddressDBEntities();

        // GET: /Contact/
        public ActionResult Index(string searchString)
        {
            
            if (Session["loggedInUserId"] == null)
            {
                return View();
            }
            else
            {

                int loggedInUserId = Convert.ToInt32(Session["loggedInUserId"].ToString());

                var contacts = from c in db.Contacts
                               where c.UserId == loggedInUserId
                               select c;

                if (!String.IsNullOrEmpty(searchString))
                {
                    contacts = from c in db.Contacts
                               where c.UserId == loggedInUserId && c.FirstName.Contains(searchString)
                               select c;
                }

                ViewBag.Message = searchString;
                return View(contacts.ToList());
            }        
        }
       

        // GET: /Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: /Contact/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: /Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="FirstName,LastName,UserId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);     
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", contact.UserId);
            return View(contact);
        }

        // GET: /Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", contact.UserId);
            return View(contact);
        }

        // POST: /Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FirstName,LastName,UserId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", contact.UserId);
            return View(contact);
        }

        // GET: /Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AddEmail(string emailAddress, string ContactId)
        {

            int UserId = Convert.ToInt32(Session["loggedInUserId"].ToString());

            ContactEmail email = new ContactEmail();
            email.EmailAddress = emailAddress;
            email.ContactId = Convert.ToInt32(ContactId);

            if (ModelState.IsValid)
            {
                db.ContactEmails.Add(email);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult DeleteEmail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact contact = db.Contacts.Find(Convert.ToInt32(Session["loggedInUserId"].ToString()));

            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ContactEmail email = db.ContactEmails.Find(id);
                db.ContactEmails.Remove(email);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }


       

        public ActionResult AddNumber(string numberString, string ContactId)
        {

            int UserId = Convert.ToInt32(Session["loggedInUserId"].ToString());

            ContactNumber numberObj = new ContactNumber();
            numberObj.Number = numberString;
            numberObj.ContactId = Convert.ToInt32(ContactId);

            if (ModelState.IsValid)
            {
                db.ContactNumbers.Add(numberObj);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult DeleteNumber(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact contact = db.Contacts.Find(Convert.ToInt32(Session["loggedInUserId"].ToString()));

            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                ContactNumber number = db.ContactNumbers.Find(id);
                db.ContactNumbers.Remove(number);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
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
