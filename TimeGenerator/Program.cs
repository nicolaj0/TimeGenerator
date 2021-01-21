using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodaTime;
using NodaTime.Extensions;

namespace TimeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var holidays = new List<DateTime>()
            {
                new DateTime(2020, 11, 1),
                new DateTime(2020, 11, 11),
                new DateTime(2020, 12, 25),
            };

            int hoursPerDay = 9;

            var projects = new List<Project>
            {
                new Project {Name = "WZ_AUTO_DEV", Pct = 10},
                new Project {Name = "WZ_AUTO_RUN", Pct = 30},
                new Project {Name = "WZ_SANTE_DEV", Pct = 10},
                new Project {Name = "WZ_SANTE_RUN", Pct = 45},
                new Project {Name = "WZ_Architecture", Pct = 5},
            };


            var res = new StringBuilder();


            foreach (var project in projects)
            {
                foreach (var dateTime in AllDatesInMonth(2020, 11))
                {
                    var isWorkingDay = !holidays.Contains(dateTime) &&
                                       dateTime.ToLocalDateTime().DayOfWeek != IsoDayOfWeek.Saturday &&
                                       dateTime.ToLocalDateTime().DayOfWeek != IsoDayOfWeek.Sunday;


                    var projectPct = 9 * project.Pct / 100;
                    var value = isWorkingDay ? projectPct : 0;
                    res.Append(value);
                    res.Append(";");
                }

                res.Append(Environment.NewLine);
            }


            Console.WriteLine(res.ToString());
        }

        public static IEnumerable<DateTime> AllDatesInMonth(int year, int month)
        {
            var days = DateTime.DaysInMonth(year, month);
            for (var day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }
    }

    internal class Project
    {
        public string Name { get; set; }
        public decimal Pct { get; set; }
    }
}