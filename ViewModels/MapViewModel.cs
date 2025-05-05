namespace MapTask.ViewModels
{
    public class MapViewModel
    {
        public Models.Location Location { get; set; } = new();
        public List<Models.Location> SearchHistory { get; set; } = new();
        public bool ShowMap { get; set; }
    }
}
