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
using Newtonsoft.Json;

namespace ForsaWebAPI.Controllers
{
    public class UserTypeController : ApiController
    {
        private ForsaEntities db = new ForsaEntities();

        // GET: api/UserType
        [HttpGet]
        public IHttpActionResult GettblUserTypes()
        {
          //  return db.tblUserTypes;
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(db.tblUserTypes)) });

        }

        // GET: api/UserType/5
        //[ResponseType(typeof(tblUserType))]
        [HttpGet]
        public IHttpActionResult GettblUserType(int id)
        {
            tblUserType tblUserType = db.tblUserTypes.Find(id);
            if (tblUserType == null)
            {
                return NotFound();
            }

            // return Ok(tblUserType);
            return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(tblUserType)) });

        }

        // PUT: api/UserType/5
        //[ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PuttblUserType(int id, tblUserType tblUserType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUserType.UserTypeId)
            {
                return BadRequest();
            }

            db.Entry(tblUserType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserTypeExists(id))
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

        // POST: api/UserType
        //[ResponseType(typeof(tblUserType))]
        [HttpPost]
        public IHttpActionResult PosttblUserType(tblUserType tblUserType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUserTypes.Add(tblUserType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblUserType.UserTypeId }, tblUserType);
        }

        // DELETE: api/UserType/5
        //[ResponseType(typeof(tblUserType))]
        [HttpDelete]
        public IHttpActionResult DeletetblUserType(int id)
        {
            tblUserType tblUserType = db.tblUserTypes.Find(id);
            if (tblUserType == null)
            {
                return NotFound();
            }

            db.tblUserTypes.Remove(tblUserType);
            db.SaveChanges();

            return Ok(tblUserType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserTypeExists(int id)
        {
            return db.tblUserTypes.Count(e => e.UserTypeId == id) > 0;
        }
    }
}