using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExcelsiorConsole.Users.ioanb7
{
    public class TorrentJObject
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }

    //INSTALL DEPENDENCIES:
    //[0]: https://www.microsoft.com/en-us/download/details.aspx?id=44266
    //[1]: https://pypi.python.org/pypi/lxml/3.3.5#downloads
    //[2]: https://sourceforge.net/projects/pywin32/files/pywin32/Build%20220/pywin32-220.win32-py2.7.exe/download

    class piratebayCmd : Command
    {
        List<TorrentJObject> torrents = null;

        public string python = @"C:\Python27\python.exe";
        public string jsonResultPath = @"../temp/resultpiratebay.json";
        string thisDir = @"C:\Users\484327\Documents\GitHub\ExcelsiorConsole\Users\ioanb7";
        public string scrapy = @"C:\Python27\Scripts\scrapy.exe";
        public piratebayCmd(ConsoleWindow c) : base(c)
        {
            Label = "piratebay";

            jsonResultPath = thisDir + jsonResultPath;
        }

        public void PopulateTorrents(string query = "sense8")
        {
            if (Args != null && Args[0].Trim() != "") query = Args[0];
            System.IO.File.WriteAllText(jsonResultPath, "");
            string result = RunProcess("cmd.exe", ("/c scrapy runspider " + thisDir + "\\PirateBay\\myspider.py -a query=" + query + " -o resultpiratebay.json"));

            //get the file
            string pathh = thisDir + "\\..\\..\\bin\\Debug\\resultpiratebay.json";

            torrents = JsonConvert.DeserializeObject<List<TorrentJObject>>(System.IO.File.ReadAllText(pathh));
            if (torrents != null)
            {
                foreach (var torrent in torrents)
                {
                    Console.WriteLine(string.Format("[{0}]: {1}", torrents.IndexOf(torrent), torrent.Title), System.Drawing.Color.AntiqueWhite);
                }
            }
        }

        public override void Execute()
        {
            PopulateTorrents();
            if (torrents == null)
            {
                Console.WriteLine("An error occured", System.Drawing.Color.Red);
            }
            else
            {
                base.Execute();
            }
        }
        
        public override void Console_RecievedCommand(object sender, ConsoleWindow.CommandEventArgs e)
        {
            int id = 0;
            int.TryParse(e.Label, out id);

            if (id > -1 && id < torrents.Count)
            {
                Process.Start(torrents[id].Url);
                base.Exit();
            }
        }
    }
}