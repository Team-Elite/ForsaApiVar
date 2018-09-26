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
using ForsaWebAPI.Models;

namespace ForsaWebAPI.Controllers
{
    public class LanguageController : ApiController
    {
        private ForsaEntities db = new ForsaEntities();

        // GET: api/Language
        [HttpGet]
        public IQueryable<tblLanguage> GettblLanguages()
        {
            return db.tblLanguages;
        }

        // GET: api/Language/5
        //[ResponseType(typeof(tblLanguage))]
        [HttpGet]
        public IHttpActionResult GettblLanguage(int id)
        {
            tblLanguage tblLanguage = db.tblLanguages.Find(id);
            if (tblLanguage == null)
            {
                return NotFound();
            }

            return Ok(tblLanguage);
        }

        // PUT: api/Language/5
        //[ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PuttblLanguage(int id, tblLanguage tblLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblLanguage.LanguageId)
            {
                return BadRequest();
            }

            db.Entry(tblLanguage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblLanguageExists(id))
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

        // POST: api/Language
        //[ResponseType(typeof(tblLanguage))]
        [HttpPost]
        public IHttpActionResult PosttblLanguage(tblLanguage tblLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblLanguages.Add(tblLanguage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblLanguage.LanguageId }, tblLanguage);
        }

        // DELETE: api/Language/5
        //[ResponseType(typeof(tblLanguage))]
        [HttpDelete]
        public IHttpActionResult DeletetblLanguage(int id)
        {
            tblLanguage tblLanguage = db.tblLanguages.Find(id);
            if (tblLanguage == null)
            {
                return NotFound();
            }

            db.tblLanguages.Remove(tblLanguage);
            db.SaveChanges();

            return Ok(tblLanguage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblLanguageExists(int id)
        {
            return db.tblLanguages.Count(e => e.LanguageId == id) > 0;
        }
    }
}