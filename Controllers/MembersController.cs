using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EnvyTestApp.Models;
using Microsoft.AspNet.Identity;
using EnvyTestApp.Helpers;

namespace EnvyTestApp.Controllers
{
   
    [Authorize(Roles = "Admin, Member")]
    public class MembersController : Controller
    {

        private ApplicationDbContext _context;

        public MembersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Members
       
        public ActionResult Index()
        {
            var members = _context.Members
                .Include(m => m.Rank)
                .Include(m => m.PreviousRank).ToList();
            return View(members);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            var ranks = _context.Rank.ToList();
            var viewModel = new NewMemberViewModel
            {
                Member = new Members(),
                Ranks = ranks               

            };            
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Members member)
        {

            var memberlist = _context.Members.SingleOrDefault(c => c.Name == member.Name);



            if (!ModelState.IsValid)
            {
                var viewModel = new NewMemberViewModel
                {
                    Member = member,
                    Ranks = _context.Rank.ToList()
                };
                return View("New", viewModel);
            }


            if (member.Id == 0)
            {
                if (memberlist != null)
                {
                    ModelState.AddModelError("Name", "User Already Exists");
                    var viewModel = new NewMemberViewModel
                    {
                        Member = member,
                        Ranks = _context.Rank.ToList()
                    };
                    return View("New", viewModel);

                }
                if (member.DateJoin == null)
                {
                    
                    member.DateJoin = DateTime.Now;
                }                
                member.LastRankId = 8;
                member.LastChangedBy = User.Identity.GetDisplayName();
                _context.Members.Add(member);
            }

            else
            {
                if (member.Id != 0)
                {

                    var customerinDb = _context.Members.Single(c => c.Id == member.Id);

                    if(customerinDb.Name != member.Name)
                    {
                        if (memberlist != null)
                        {
                            ModelState.AddModelError("Name", "User Already Exists");
                            var viewModel = new NewMemberViewModel
                            {
                                Member = member,
                                Ranks = _context.Rank.ToList()
                            };
                            return View("Edit", viewModel);

                        }
                    }

                    customerinDb.LastRankId = customerinDb.LastRankId;
                    if (customerinDb.RankId != member.RankId)
                    {
                        customerinDb.DateLastRankChange = DateTime.Now;
                        customerinDb.LastRankId = customerinDb.RankId;
                    }
                    
                    customerinDb.Name = member.Name;
                    customerinDb.DateJoin = member.DateJoin;
                    customerinDb.RankId = member.RankId;
                    customerinDb.LastChangedBy = User.Identity.GetDisplayName();
                    customerinDb.Notes = member.Notes;

                }
        
                
            }
           // Console.WriteLine(member.RankId);
            _context.SaveChanges();
            return RedirectToAction("Index", "Members");
        
        }

        public ActionResult Edit(int Id)
        {
            var members = _context.Members.SingleOrDefault(c => c.Id == Id);

            if (members == null)
            {
                return HttpNotFound();
            }

            var viewModel = new NewMemberViewModel
            {
                Member = members,
                Ranks = _context.Rank.ToList()
            };
            return View("Edit", viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var member = _context.Members.SingleOrDefault(c => c.Id == id);
            if (member == null)
            {
                return HttpNotFound();
            }            

            var viewModel = new NewMemberViewModel
            {
                Member = member,
                Ranks = _context.Rank.ToList()
            };
            return View("Delete", viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var member = _context.Members.SingleOrDefault(c => c.Id == id);

                if (member == null)
                {
                    return HttpNotFound();      
                }

                var result = _context.Members.Remove(member);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}