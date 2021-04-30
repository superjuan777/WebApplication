using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;
using webapi.Services;
using System.Net;
using System.Net.Mail;

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

        [HttpPost]
        public ActionResult<Book> Create(Book book)
         {
           _bookService.Create(book);

           return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
         }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book em)
        {
            Book oRespuesta = new Book();

            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }


            string to = (string)em.email;
            string men = (string)em.mensaje;
            string sub = (string)em.cliente;
            int tot = em.totalfactura;


            MailMessage _mailMessage = new MailMessage();

            _mailMessage.From = new MailAddress("pcliente836@gmail.com");

            _mailMessage.CC.Add(to);
            _mailMessage.Subject = sub;
            _mailMessage.IsBodyHtml = true;
            if (tot == 0)
                _mailMessage.Body = "Señor cliente " + sub + men;
            else
                _mailMessage.Body = "Señor cliente " + sub + men + tot;
            SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential("pcliente836@gmail.com", "monolegal");
            _smtpClient.EnableSsl = true;
           

            try  
            {
                _smtpClient.Send(_mailMessage);
                oRespuesta.Exito = 1;
                _bookService.Update(id, em);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return Ok(oRespuesta);
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
        
    }
}
