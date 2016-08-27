using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Net.Sockets;

namespace ExcelsiorConsole.Users.JColdFear
{
    class TimezoneCmd : Command
    {
        public TimezoneCmd(Console c) : base (c)
        {
            Label = "timezone";
            Aliases.Add("tz");
        }

        public override void Execute()
        {
            if (Args.Count == 0)
            {
                base.Execute();
                return;
            }

            if (Args.Count != 2)
            { 
                Console.WriteLine("Command not found", System.Drawing.Color.Red);
                return;
            }   

            PrintTimezones(Args[1]);
        }

        public override void Console_RecievedCommand(object sender, Console.CommandEventArgs e)
        {
            if (e.Label == "exit" || e.Label == "quit" || e.Label == "close")
                Exit();
            else
            {
                PrintTimezones(e.Label);
            }
        }

        public void PrintTimezones(string zone)
        {
            List<TimeZoneInfo> timezones = (from timezone in TimeZoneInfo.GetSystemTimeZones()
                                            where timezone.DisplayName.ToLower().Contains(zone.ToLower())
                                            select timezone).ToList();

            if (!timezones.Any())
            {
                Console.WriteLine("Couldn't find a timezone that contains '" + zone + "'", Color.Red);
                return;
            }

            foreach (var timezone in timezones)
            {
                if (!timezone.DisplayName.ToLower().Contains(zone.ToLower()))
                    continue;

                double n = timezone.BaseUtcOffset.TotalHours;
                double hours = Math.Truncate(n);
                double minutes = (n - hours) * 60;

                DateTime time = DateTime.Now;
                time = time.AddHours(hours);
                time = time.AddMinutes(Math.Abs(minutes));

                Console.WriteLine(timezone.StandardName, Color.White);
                Console.WriteLine(timezone.DisplayName, Color.White);
                Console.WriteLine(time.ToString(), Color.White);
            }
        }
    }
}
