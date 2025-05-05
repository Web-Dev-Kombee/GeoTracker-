using MapTask.Models;
using MapTask.Services.Interfaces;

namespace MapTask.Services
{
    public class SearchHistoryService : ISearchHistoryService
    {
        private static readonly List<Location> _history = new List<Location>();

        public List<Location> GetHistory()
        {
            return _history.ToList(); 
        }

        public void AddToHistory(Location location)
        {
            _history.Insert(0, location);
            if (_history.Count > 10)
                _history.RemoveAt(10);
        }
    }
}
