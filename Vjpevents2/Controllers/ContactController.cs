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
    public class ContactController : Controller
    {
        // GET: Contact
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        // POST: Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fromAddress = new MailAddress("vjpevents22@gmail.com", "Website Contact Form");
                    var toAddress = new MailAddress("vjpevents22@gmail.com");
                    string fromPassword = "mqwcgkefeuddyldx";

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
                        message.ReplyToList.Add(new MailAddress(model.Email));
                        smtp.Send(message);
                    }

                    TempData["Success"] = "✅ Your message has been sent!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["Error"] = "❌ Sorry, something went wrong. Please try again later.";
                    return RedirectToAction("Index");
                }
            }

            // If model is invalid, show form again
            return View(model);
        }

    }
}