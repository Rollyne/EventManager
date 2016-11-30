using EventManager.Tools.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FreePeople> peopleCount = new List<FreePeople>();
            peopleCount.Add(new FreePeople { Date = DateTime.Now, FreePeopleCount = 1 });
            peopleCount.Add(new FreePeople { Date = DateTime.Now.AddDays(2), FreePeopleCount = 3 });
            peopleCount.Add(new FreePeople { Date = DateTime.Now.AddDays(3), FreePeopleCount = 4 });
            peopleCount.Add(new FreePeople { Date = DateTime.Now.AddDays(4), FreePeopleCount = 9 });
            peopleCount.Add(new FreePeople { Date = DateTime.Now.AddDays(5), FreePeopleCount = 10 });
            peopleCount.Add(new FreePeople { Date = DateTime.Now.AddDays(6), FreePeopleCount = 2 });
            peopleCount.Add(new FreePeople { Date = DateTime.Now.AddDays(8), FreePeopleCount = 5 });
            peopleCount.Add(new FreePeople { Date = DateTime.Now.AddDays(24), FreePeopleCount = 1 });

            var structuredJson = new
            {
                MaxPeople = 20,
                Dates = peopleCount.Select(x => new { Day = x.Date.Day, Month = x.Date.Month, Year = x.Date.Year, x.FreePeopleCount }).ToList()
            };

            //var structuredJson = peopleCount.Select(x => new { Day = x.Date.Day, Month = x.Date.Month, Year = x.Date.Year, x.FreePeopleCount, MaxPeople = 20 }).ToList();

            var json = JsonConvert.SerializeObject(structuredJson, Formatting.Indented);
            var path = Path.Combine(
                    Environment.CurrentDirectory,
                    @"EventDates\");
            Directory.CreateDirectory(path);

            using (StreamWriter file = new StreamWriter(path + "Dates.json"))
            {
                file.WriteLine(json);
            }
        }
    }
}