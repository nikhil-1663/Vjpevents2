using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Vjpevents2.Models;

namespace Vjpevents2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // This is YOUR Gmail (must match the one you use in SMTP auth)
                    //var fromAddress = new MailAddress("subhaschandnikhil@gmail.com", "Website Contact Form");
                    //var toAddress = new MailAddress("subhaschandnikhil@gmail.com"); // Where you receive messages
                    //string fromPassword = "briwazepmyzufhcd"; // Gmail App Password
                    var fromAddress = new MailAddress("vjpevents22@gmail.com", "Website Contact Form");
                    var toAddress = new MailAddress("vjpevents22@gmail.com"); // Where you receive messages
                    string fromPassword = "mqwcgkefeuddyldx"; // Gmail App Password

                    string subject = "New Enquiry";
                    string body = $"You received a new message from your website:\n\n" +
                                  $"Name: {model.Name}\n" +
                                  $"Email: {model.Email}\n\n" +
                                  $"Number: {model.Number}\n\n" +
                                  $"Message:\n{model.Message}";

                    var smtp = new SmtpClient("smtp.gmail.com", 587)
                    {
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        // ? This makes sure replies go to the sender’s email
                        message.ReplyToList.Add(new MailAddress(model.Email));

                        smtp.Send(message);
                    }

                    TempData["Success"] = "✅ Your message has been sent!";
                    return RedirectToAction("Index"); // <-- Redirect avoids resubmission

                }
                catch
                {
                    TempData["Error"] = "❌ Sorry, something went wrong. Please try again later.";
                }
                return RedirectToAction("Index");

            }

            return View("Index", model);
        }
    }
}