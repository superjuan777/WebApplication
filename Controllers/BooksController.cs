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
        public IActionResult EnviarEmail(Book em)
        {
            string to = (string)em.email;
            string men = (string)em.mensaje;
            string sub = (string)em.cliente;
          
            MailMessage _mailMessage = new MailMessage();

            _mailMessage.From = new MailAddress("pcliente836@gmail.com");

            _mailMessage.CC.Add(to);
            _mailMessage.Subject = sub;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Body = "Señor cliente " + sub + men;

            SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential("pcliente836@gmail.com", "monolegal");
            _smtpClient.EnableSsl = true;
            _smtpClient.Send(_mailMessage);

            return RedirectToAction("Index");

        }
    }
}
