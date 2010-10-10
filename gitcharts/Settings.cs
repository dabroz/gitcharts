using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace gitcharts
{
    [Serializable]
    public class Settings
    {
        public string GitCommand { get; set; }
        public string GnuplotCommand { get; set; }
        public string ConvertCommand { get; set; }

        public int ChartWidth { get; set; }
        public int ChartHeight { get; set; }

        public int RevisionLimit { get; set; }

        public string AllowedFiles { get; set; }
        public string TwoLevels { get; set; }
        public string Ignore { get; set; }
        public string Path { get; set; }

        public Settings()
        {            
            GitCommand = @"git.exe";
            GnuplotCommand = @"gnuplot.exe";
            ConvertCommand = @"convert.exe";

            ChartWidth = 640;
            ChartHeight = 480;

            RevisionLimit = 15;

            AllowedFiles = "c,h,cpp,cx,cc,m,mm,cg,cs,res,js,html,lua";
            TwoLevels = "";
            Ignore = ".git";
            Path = "";
        }

        public void Save()
        {
            using (XmlTextWriter xml = new XmlTextWriter("settings.xml", Encoding.UTF8))
            {
                XmlSerializer s = new XmlSerializer(this.GetType());
                s.Serialize(xml, this);
            }
        }

        static Settings _instance = null;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                    try
                    {
                        if (File.Exists("settings.xml"))
                        {
                            using (XmlTextReader xml = new XmlTextReader("settings.xml"))
                            {
                                XmlSerializer s = new XmlSerializer(typeof(Settings));
                                _instance = (Settings)s.Deserialize(xml);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                return _instance;
            }
        }
    }
}
