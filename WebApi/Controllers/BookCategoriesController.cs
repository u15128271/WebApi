using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class BookCategoriesController : ApiController
    {
        private ScriptersDb_newEntities db = new ScriptersDb_newEntities();

        // GET: api/BookCategories
        public IQueryable<BookCategory> GetBookCategories()
        {
            return db.BookCategories;
        }

        // GET: api/BookCategories/5
        [ResponseType(typeof(BookCategory))]
        public IHttpActionResult GetBookCategory(int id)
        {
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            return Ok(bookCategory);
        }

        // PUT: api/BookCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBookCategory(int id, BookCategory bookCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(bookCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BookCategories
        [ResponseType(typeof(BookCategory))]
        public IHttpActionResult PostBookCategory(BookCategory bookCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookCategories.Add(bookCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bookCategory.Id }, bookCategory);
        }

        // DELETE: api/BookCategories/5
        [ResponseType(typeof(BookCategory))]
        public IHttpActionResult DeleteBookCategory(int id)
        {
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return NotFound();
            }

            db.BookCategories.Remove(bookCategory);
            db.SaveChanges();

            return Ok(bookCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookCategoryExists(int id)
        {
            return db.BookCategories.Count(e => e.Id == id) > 0;
        }
    }
}