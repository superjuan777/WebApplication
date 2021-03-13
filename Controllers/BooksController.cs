using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        //[HttpPost]
       // public ActionResult<Book> Create(Book book)
        //{
           // _bookService.Create(book);

           // return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        //}

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }
        
         [HttpPost]
        public IActionResult EnviarEmail()
        {
            string emailDestinatario = "pcliente836@gmail.com";
            SendMail(emailDestinatario);
            return RedirectToAction("Index");
        }

        public bool SendMail(string email)
        {
            try
            {
                MailMessage _mailMessage = new MailMessage();

                _mailMessage.From = new MailAddress("pcliente836@gmail.com");

                _mailMessage.CC.Add(email);
                _mailMessage.Subject = "Estado cuenta Monolegal";
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = "<b>Aviso infromacion factura monolegal</b><p>se le informa que su cuenta se encuentra en mora por favor cancelar factura</p>";

                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("pcliente836@gmail.com", "monolegal");
                _smtpClient.EnableSsl = true;
                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
