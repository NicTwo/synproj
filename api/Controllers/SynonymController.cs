using api.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/synonym")]
    public class SynonymController(IApplicationDbService dbContext) : ControllerBase
    {
        private readonly IApplicationDbService _dbContext = dbContext;

        [HttpPut]
        public void AddSynonym(string word, string synonym)
        {
            _dbContext.AddSynonym(word, synonym);
        }

        [HttpGet]
        public IActionResult GetSynonym(string word)
        {
            var synonyms = _dbContext.GetSynonyms(word);

            if (synonyms == null || synonyms.Count() == 0)
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
        
        [HttpDelete]
        public void ClearAllSynonyms()
        {
            _dbContext.ClearAllSynonyms();
        }
    }
}
