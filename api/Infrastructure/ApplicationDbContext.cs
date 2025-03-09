using System.Text;

namespace api.Infrastructure
{
    public class ApplicationDbContext : IApplicationDbService
    {
        // Using HashSet to avoid duplicate entries
        private readonly Dictionary<string, HashSet<string>> _synonymDict = [];

        public List<string> GetSynonyms(string word)
        {
            HashSet<string> synonymSet = [];
            GetSynonymRecursive(word, synonymSet);
            synonymSet.Remove(word);
            return [.. synonymSet];
        }

        private void GetSynonymRecursive(string word, HashSet<string> synonymSet)
        {
            if (_synonymDict.TryGetValue(word, out var synonymList))
            {
                foreach (var synonym in synonymList)
                {
                    if (synonymSet.Add(synonym))
                    {
                        GetSynonymRecursive(synonym, synonymSet);
                    }
                }
            }
        }

        public void AddSynonym(string word, string synonym)
        {
            // Add the word and synonym in both directions for quick lookup
            AddToDictionary(word, synonym);
            AddToDictionary(synonym, word);
        }

        private void AddToDictionary(string word, string synonym)
        {
            if (!_synonymDict.TryGetValue(word, out _))
            {
                _synonymDict.Add(word, []);
            }
            
            _synonymDict[word].Add(synonym);
        }

        public List<string> GetAllSynonyms()
        {
            List<string> resultList = [];
            foreach (var word in _synonymDict.Keys)
            {
                resultList.Add(CreateSynonymSummary(word, _synonymDict[word]));
            }

            return resultList;
        }

        private static string CreateSynonymSummary(string word, HashSet<string> synonymList)
        {
            var result = new StringBuilder();
            result.Append(word);
            result.Append(": ");
            foreach(var synonym in synonymList)
            {
                result.Append(synonym);
                result.Append(' ');
            }

            return result.ToString().TrimEnd();
        }

        public void ClearAllSynonyms()
        {
            _synonymDict.Clear();
        }
    }
}
