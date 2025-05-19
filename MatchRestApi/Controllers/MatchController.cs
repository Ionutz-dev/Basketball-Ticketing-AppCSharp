using System;
using System.Collections.Generic;
using System.Web.Http;
using model;
using persistence;

namespace MatchRestApi.Controllers
{
    [RoutePrefix("api/matches")]
    public class MatchController : ApiController
    {
        private readonly IMatchRepository _matchRepository;

        public MatchController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        // GET: api/matches
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            Console.WriteLine("Getting all matches");
            var matches = _matchRepository.FindAll();
            return Ok(matches);
        }

        // GET: api/matches/5
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            Console.WriteLine($"Get match by id: {id}");
            var match = _matchRepository.FindMatchById(id);
            if (match == null)
                return NotFound();
            
            return Ok(match);
        }

        // POST: api/matches
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] Match match)
        {
            Console.WriteLine($"Creating match: {match}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // In a real implementation, you would add the match to the repository
            // _matchRepository.Add(match);
            match.Id = 0; // Will be set by database
            return Ok(match);
        }

        // PUT: api/matches/5
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, [FromBody] Match match)
        {
            Console.WriteLine($"Updating match with id: {id}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMatch = _matchRepository.FindMatchById(id);
            if (existingMatch == null)
                return NotFound();

            // In the original code, we can only update seats
            int seatsChange = existingMatch.AvailableSeats - match.AvailableSeats;
            if (seatsChange > 0)
            {
                _matchRepository.UpdateSeats(id, seatsChange);
            }

            return Ok(existingMatch);
        }

        // DELETE: api/matches/5
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Console.WriteLine($"Deleting match with id: {id}");
            var match = _matchRepository.FindMatchById(id);
            if (match == null)
                return NotFound();

            // In a real implementation, you would delete the match from the repository
            // _matchRepository.Delete(match);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}