using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Infrastructure
{
    public interface IApplicationDbService
    {
        public List<string> GetSynonyms(string word);
        public void AddSynonym(string word, string synonym);
        // public IEnumerable<(string word, string synonym)> GetAllSynonyms();
        public IEnumerable<string> GetAllSynonyms();  
    }
}