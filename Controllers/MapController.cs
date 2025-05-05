using MapTask.Helpers;
using MapTask.Models;
using MapTask.Services.Interfaces;
using MapTask.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MapTask.Controllers
{
    public class MapController : BaseController
    {
        
        private readonly ISearchHistoryService _searchHistoryService;

        public MapController(ISearchHistoryService searchHistoryService)
        {
            _searchHistoryService = searchHistoryService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionKeys.UserEmail) == null)
            {

                return RedirectToLogin();
            }

            var vm = new MapViewModel
            {
                Location = new Location(),
                SearchHistory = _searchHistoryService.GetHistory(),
                ShowMap = false
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(Location location, bool fromHistory = false)
        {
            if (HttpContext.Session.GetString(SessionKeys.UserEmail) == null)
            {

                  return RedirectToLogin(); 
            }

            if (!ModelState.IsValid)
            {
                return View(new MapViewModel
                {
                    Location = location,
                    SearchHistory = _searchHistoryService.GetHistory(),
                    ShowMap = false
                });
            }

            if (!fromHistory)
            {
                _searchHistoryService.GetHistory().Insert(0, new Location
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                });

                if (_searchHistoryService.GetHistory().Count > 10)
                    _searchHistoryService.GetHistory().RemoveAt(10);
            }

            return View(new MapViewModel
            {
                Location = location,
                SearchHistory = _searchHistoryService.GetHistory(),
                ShowMap = true
            });
        }
    }
}
