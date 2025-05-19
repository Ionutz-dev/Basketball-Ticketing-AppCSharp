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
        private readonly MatchRestRepository _matchRestRepository;

        public MatchController(IMatchRepository matchRepository)
        {
            Console.WriteLine("MatchController created");
            _matchRepository = matchRepository;
            _matchRestRepository = (MatchRestRepository)matchRepository;
        }
        
        [HttpGet]
        [Route("")]
        public IEnumerable<Match> GetAll()
        {
            Console.WriteLine("GET all matches");
            return _matchRepository.FindAll();
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            Console.WriteLine($"GET match by id: {id}");
            var match = _matchRepository.FindMatchById(id);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }
        
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Match match)
        {
            Console.WriteLine($"POST new match: {match}");
            if (match == null)
            {
                return BadRequest("Match data is null");
            }
            
            match.Id = 0;
            
            var createdMatch = _matchRestRepository.Save(match);
            return Ok(createdMatch);
        }
        
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] Match match)
        {
            Console.WriteLine($"PUT update match: {id}");
            if (match == null)
            {
                return BadRequest("Match data is null");
            }
            
            var existingMatch = _matchRepository.FindMatchById(id);
            if (existingMatch == null)
            {
                return NotFound();
            }
            
            var updatedMatch = _matchRestRepository.Update(id, match);
            return Ok(updatedMatch);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            Console.WriteLine($"DELETE match: {id}");
            
            var existingMatch = _matchRepository.FindMatchById(id);
            if (existingMatch == null)
            {
                return NotFound();
            }
            
            bool deleted = _matchRestRepository.Delete(id);
            if (deleted)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}