using Newtonsoft.Json;
using Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Services.Data
{
    public class SiteRepository : ISiteRepository
    {
        private Random Rand = new Random();
        public IEnumerable<Site> GetSites()
        {
            if (!File.Exists("Sites.json")) return null;
            var usersJson = File.ReadAllText("Sites.json");
            if (usersJson is null) return null;

            return JsonConvert.DeserializeObject<IEnumerable<Site>>(usersJson);
        }

        public Sample GetLastSample()
        {
            var sites = GetSites().ToList();
            if (sites is null) throw new Exception("No sites found");

            var randomSite = sites[Rand.Next(0, sites.Count)];
            Sample sample = new Sample
            {
                DeviceId = randomSite.Id,
                SampleDate = DateTime.Now,
            };
            sample.Samples.Add(new HumiditySampleUnit()
            {
                Value = Rand.Next(0, 100).ToString(),   
            });
            sample.Samples.Add(new TemperatureSampleUnit()
            {
                Value = Rand.Next(0, 40).ToString(),
            });

            return sample;

        }
    }
}
