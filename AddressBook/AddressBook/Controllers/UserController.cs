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
    public class UserController : Controller
    {
        private AddressDBEntities db = new AddressDBEntities();

        // GET: /User/
        public ActionResult Index()
        {
            Session.Clear();

            if (db.Users != null)
            {
                 return View(db.Users.ToList());
            }

            return View();    
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Login(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Session["loggedInUserId"] = user.Id.ToString();
            Session["loggedInUserName"] = user.FirstName.ToString();
            
            return RedirectToAction("Index", "Contact");
        }


        public ActionResult ResetDatabase()
        {
            db.ContactEmails.RemoveRange(db.ContactEmails);
            db.ContactNumbers.RemoveRange(db.ContactNumbers);
            db.Contacts.RemoveRange(db.Contacts);
            db.Users.RemoveRange(db.Users);
            db.SaveChanges();

            List<User> userlist = new List<User> 
            {
                new User { FirstName = "Zee",LastName = "Daw" },
                new User { FirstName = "Peter",LastName = "Kropotkin" },
                new User { FirstName = "Charles",LastName = "Darwin" }
            };

            db.Users.AddRange(userlist.ToList());
            db.SaveChanges();

            var usersDBList = db.Users.ToList();

            List<Contact> contactlist = new List<Contact> 
            {
                new Contact {  FirstName = "Sharon", LastName = "Nesquit", UserId = usersDBList[0].Id },
                new Contact {  FirstName = "Hermes", LastName = "Roma", UserId = usersDBList[0].Id },
                new Contact {  FirstName = "Abel", LastName = "West", UserId = usersDBList[0].Id  },
                new Contact {  FirstName = "Tommy", LastName = "Ava", UserId = usersDBList[1].Id},
                new Contact {  FirstName = "Simon", LastName = "Jones", UserId = usersDBList[1].Id  },
                new Contact {  FirstName = "Ryan", LastName = "Dick", UserId = usersDBList[2].Id  }
            };

            db.Contacts.Add(contactlist[0]);
            db.Contacts.Add(contactlist[1]);
            db.Contacts.Add(contactlist[2]);
            db.Contacts.Add(contactlist[3]);
            db.Contacts.Add(contactlist[4]);
            db.Contacts.Add(contactlist[5]);
            db.SaveChanges();

            var contactDBList = db.Contacts.ToList();

            List<ContactEmail> contactEmaillist = new List<ContactEmail> 
            {
                new ContactEmail { Id = 1, EmailAddress = "Jelo@Mellow.com", ContactId = contactDBList[0].Id},
                new ContactEmail { Id = 2, EmailAddress = "Young@Mellow.com", ContactId = contactDBList[0].Id},
                new ContactEmail { Id = 3, EmailAddress = "SnailMail@SnailMail.com", ContactId = contactDBList[1].Id},
                new ContactEmail { Id = 4, EmailAddress = "Ker@Frog.com", ContactId = contactDBList[1].Id}
            };

            
            db.ContactEmails.Add(contactEmaillist[0]);
            db.ContactEmails.Add(contactEmaillist[1]);
            db.ContactEmails.Add(contactEmaillist[2]);
            db.ContactEmails.Add(contactEmaillist[3]);
            db.SaveChanges();

            //var contactDBList2 = db.Contacts.ToList();
            //var contactEmailDBList = db.ContactEmails.ToList();

            //contactDBList2[0].ContactEmails.Add(contactEmailDBList[0]);
            //contactDBList2[0].ContactEmails.Add(contactEmailDBList[1]);
            //contactDBList2[1].ContactEmails.Add(contactEmailDBList[2]);
            //contactDBList2[1].ContactEmails.Add(contactEmailDBList[3]);
            //contactDBList2[2].ContactEmails.Add(contactEmailDBList[4]);

            //foreach (Contact c in contactDBList2)
            //{
            //    db.Entry(c).State = EntityState.Modified;
            //}

            //List<ContactNumber> contactNumberlist = new List<ContactNumber> 
            //{
            //    new ContactNumber { Id = 1, Number = "951267", ContactId = contactDBList[0].Id},
            //    new ContactNumber { Id = 2, Number = "675756", ContactId = contactDBList[0].Id},
            //    new ContactNumber { Id = 3, Number = "67675357", ContactId = contactDBList[2].Id},
            //    new ContactNumber { Id = 4, Number = "95324126367", ContactId = contactDBList[3].Id}
            //};

            //db.ContactNumbers.AddRange(contactNumberlist.ToList());

            //db.SaveChanges();
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
