using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace EnvyTestApp.Controllers
{
    public class FeedbackController : Controller
    {
        public ActionResult Feedback()
        {
            ViewBag.message = "Please submit your feedback.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitFeedback(string description)
        {

            if (!ModelState.IsValid)
            {

                return View("Feedback");
            }

                 
            var values = new Dictionary<string, string>
            {
                {"content", description }
            };
            var content = new FormUrlEncodedContent(values);
            var feedBackApi = Properties.Resources.feedbackAPI;
            var response = client.PostAsync(feedBackApi, content);

            return View("feedbackdone");
        }


        public ActionResult FeedbackDone()
        {
            return View();
        }


        #region Helpers

        private static HttpClient client = new HttpClient();

        #endregion

    }


}