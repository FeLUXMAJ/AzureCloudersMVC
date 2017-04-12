using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureClouderMVC.Configuration;
using AzureClouders.AzureStorage.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureClouderMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AzureStorageSettings _azureSettings;

        private readonly IAzureFileHandlerFactory _azureFileHandlerFactory;

        public HomeController(
            IOptions<AzureStorageSettings> azureSettings,
            IAzureFileHandlerFactory azureFileHandlerFactory)
        {
            _azureSettings = azureSettings.Value;
            _azureFileHandlerFactory = azureFileHandlerFactory;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            #region Save File At azure
            var azureFileService = _azureFileHandlerFactory.GetService(
                _azureSettings.ConnectionString,
                _azureSettings.ImagesContainer
            );
            var url = await azureFileService.SaveFileAsync(file);
            #endregion

            ViewBag.Message = $"It's Ok, File Uploaded, your image link is {url}";

            #region Queue Message
            //var azureFileService = _azureFileHandlerFactory.GetService(
            //    _azureSettings.ConnectionString,
            //    _azureSettings.ImagesContainer
            //);
            //var url = await azureFileService.SaveFileAsync(file);
            #endregion

            return View();
        }

        public IActionResult Games()
        {
            throw new NotImplementedException();
        }

        public IActionResult Games(string title)
        {
            throw new NotImplementedException();
        }
    }
}
