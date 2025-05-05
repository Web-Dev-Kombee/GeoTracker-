using MapTask.Models;

namespace MapTask.Services.Interfaces
{
    public interface ISearchHistoryService
    {
        List<Location> GetHistory();
        void AddToHistory(Location location);
    }
}
