﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureClouderMVC.Configuration;
using AzureClouders.AzureStorage.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace AzureClouderMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AzureStorageSettings _azureSettings;

        private readonly IAzureFileHandlerFactory _azureFileHandlerFactory;

        private readonly IDistributedCache _cache;

        public HomeController(
            IOptions<AzureStorageSettings> azureSettings,
            IAzureFileHandlerFactory azureFileHandlerFactory,
            IDistributedCache cache)
        {
            _cache = cache;
            _azureSettings = azureSettings.Value;
            _azureFileHandlerFactory = azureFileHandlerFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string value = _cache.GetString("CacheTime");

            if (value == null)
            {
                value = DateTime.Now.ToString();

                var options = new DistributedCacheEntryOptions();
                options.SetSlidingExpiration(TimeSpan.FromMinutes(1));
                _cache.SetString("CacheTime", value, options );
            }

            ViewData["CacheTime"] = value;
            ViewData["CurrentTime"] = DateTime.Now.ToString();
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
            return View();
        }

        [HttpGet]
        public IActionResult Games()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Games(string title)
        {
            throw new NotImplementedException();
        }
    }
}
