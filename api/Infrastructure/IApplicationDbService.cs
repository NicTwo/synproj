namespace api.Infrastructure
{
    public interface IApplicationDbService
    {
        public List<string> GetSynonyms(string word);
        public void AddSynonym(string word, string synonym);
        public List<string> GetAllSynonyms();
    }
}
