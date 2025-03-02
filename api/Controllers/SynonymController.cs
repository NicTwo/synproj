using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/synonym")]
    [ApiController]
    public class SynonymController(IApplicationDbService dbContext) : ControllerBase
    {
        private readonly IApplicationDbService _dbContext = dbContext;

        [HttpGet]
        public IActionResult GetSynonym(string word)
        {
            var synonyms = _dbContext.GetSynonyms(word);

            if (synonyms == null || synonyms.Count == 0)
            {
                return NotFound();
            }

            return Ok(synonyms);
        }

        [HttpGet("all")]
        public IActionResult GetAllSynonyms()
        {
            return Ok(_dbContext.GetAllSynonyms());
        }

        [HttpPut]
        public void AddSynonym(string word, string synonym)
        {
            _dbContext.AddSynonym(word, synonym);
        }
    }
}
