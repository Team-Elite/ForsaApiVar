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
    public class CountryController : ApiController
    {
        private ForsaEntities db = new ForsaEntities();

        // GET: api/Country
        [HttpGet]
        public IQueryable<tblCountry> GettblCountries()
        {
            return db.tblCountries;
        }

        // GET: api/Country/5
        //[ResponseType(typeof(tblCountry))]
        [HttpGet]
        public IHttpActionResult GettblCountry(int id)
        {
            tblCountry tblCountry = db.tblCountries.Find(id);
            if (tblCountry == null)
            {
                return NotFound();
            }

            return Ok(tblCountry);
        }

        // PUT: api/Country/5
        //[ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PuttblCountry(int id, tblCountry tblCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCountry.CountryId)
            {
                return BadRequest();
            }

            db.Entry(tblCountry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCountryExists(id))
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

        // POST: api/Country
        //[ResponseType(typeof(tblCountry))]
        [HttpPost]
        public IHttpActionResult PosttblCountry(tblCountry tblCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCountries.Add(tblCountry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblCountry.CountryId }, tblCountry);
        }

        // DELETE: api/Country/5
        //[ResponseType(typeof(tblCountry))]
        [HttpDelete]
        public IHttpActionResult DeletetblCountry(int id)
        {
            tblCountry tblCountry = db.tblCountries.Find(id);
            if (tblCountry == null)
            {
                return NotFound();
            }

            db.tblCountries.Remove(tblCountry);
            db.SaveChanges();

            return Ok(tblCountry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCountryExists(int id)
        {
            return db.tblCountries.Count(e => e.CountryId == id) > 0;
        }
    }
}