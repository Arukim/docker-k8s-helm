using DNATrack.Persistence;
using DNATrack.Persistence.Entities;
using DNATrack.Web.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DNATrack.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoDbConfiguration dbConfig;
        public HomeController(IOptions<MongoDbConfiguration> dbConfig)
        {
            this.dbConfig = dbConfig.Value;
        }

        public async Task<IActionResult> Index(int skip)
        {
            skip = Math.Max(skip, 0);

            var client = new MongoClient(dbConfig.Endpoint);
            var database = client.GetDatabase(dbConfig.Database);
            var collection = database.GetCollection<Trace>("traces");

            var vm = new HomeViewModel
            {
                Skip = skip,
                Traces = new ReadOnlyCollection<Trace>(await collection
                    .Find(new BsonDocument())
                    .Sort(Builders<Trace>.Sort.Descending("$natural"))
                    .Skip(skip)
                    .Limit(100)
                    .ToListAsync())
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DropTable()
        {
            var client = new MongoClient(dbConfig.Endpoint);
            var database = client.GetDatabase(dbConfig.Database);
            var collection = database.GetCollection<Trace>("traces");

            await collection.DeleteManyAsync(new BsonDocument());

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
