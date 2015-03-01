using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using IncludeDay.Data;
using IncludeDay.Data.Entities;
using LinqKit;

namespace IncludeDay.Services.Controllers
{
    public class LocationsController : ApiController
    {
        private readonly IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Locations
        public List<Location> GetLocations([FromUri]Location filter)
        {

            var predicate = PredicateBuilder.True<Location>();

            if (filter != null && !string.IsNullOrEmpty(filter.Description))
            {
                predicate = predicate.And(p => p.Description.Contains(filter.Description));
            }

            if (filter != null && !string.IsNullOrEmpty(filter.LocationType))
            {
                predicate = predicate.And(p => p.LocationType.Contains(filter.LocationType));
            }

            if (filter != null && !string.IsNullOrEmpty(filter.City))
            {
                predicate = predicate.And(p => p.City.Contains(filter.City));
            }
            
            var list = _db.Locations.AsExpandable().Where(predicate);
            return list.ToList();

        }

        // GET: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocation(int id)
        {
            Location location = _db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            _db.Entry(location).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Locations.Add(location);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocation(int id)
        {
            Location location = _db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            _db.Locations.Remove(location);
            _db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationExists(int id)
        {
            return _db.Locations.Count(e => e.Id == id) > 0;
        }
    }
}