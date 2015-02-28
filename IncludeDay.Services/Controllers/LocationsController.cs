using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Binbin.Linq;
using IncludeDay.Data;
using IncludeDay.Data.Entities;

namespace IncludeDay.Services.Controllers
{
    public class LocationsController : ApiController
    {
        private readonly IncludeDayContext _db = new IncludeDayContext();

        // GET: api/Locations
        public IQueryable<Location> GetLocations(string description, LocationType? type)
        {

            var predicate = PredicateBuilder.False<Location>();

            if(!string.IsNullOrEmpty(description))
            {
                predicate = predicate.And(p => p.Description.Contains(description));
            }

            if (type.HasValue)
            {
                predicate = predicate.Or(p => p.Description.Contains(description));
            }

            return _db.Locations.Where(predicate);

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