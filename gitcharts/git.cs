using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace gitcharts
{
    class Git
    {
        string SendGitCommand(string cmd, string directory)
        {
            Log("GIT: git {0}", cmd);

            ProcessStartInfo psi = new ProcessStartInfo(Settings.Instance.GitCommand, cmd);
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.WorkingDirectory = directory;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            var p = Process.Start(psi);
            p.WaitForExit();
            var r = p.StandardOutput.ReadToEnd();
            var e = p.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(e))
            {
                r += e;
            }
            return r;
        }

        string url;

        public Git(string url_)
        {
            url = url_;
        }

        public string GitVersion { get { return SendGitCommand("version", "."); } }

        static StreamWriter swq = null;

        public static void Log(string s, params object[] p)
        {
             if (swq == null)
             {
                 var q = DateTime.Now;
                 var fname = string.Format("Log_{0}.txt", ConvertToTimestamp(q).ToString());
                 swq = new StreamWriter(fname);
             }
            var txt = string.Format(s, p);
            swq.WriteLine(txt);
            swq.Flush();
            System.Diagnostics.Debug.WriteLine(txt);
        }

        class GitNode
        {
            public DateTime time;
            public SortedDictionary<string, long> loc;
        }

        public Image CreateChartLOC()
        {
            Log("Git version: {0}", GitVersion);

            var dir = url;

            var rere = SendGitCommand("checkout master", dir);
            Log("reset: {0}", rere);

            List<GitNode> nodes = new List<GitNode>();

            int count = 0;

            int limit = Settings.Instance.RevisionLimit;

            while (true)
            {
                DateTime time = GetDateTime(SendGitCommand("log -n 1 --pretty=format:%at", dir));

                var size = CalculateLOC(dir,"");

                nodes.Add(new GitNode() { loc = size, time = time });

                count++;
                Log("Reading: {0}", count);

                var prev = SendGitCommand("checkout HEAD^", dir);

                if (prev.Contains("error: pathspec 'HEAD^' did not match any file(s) known to git"))
                {
                    break;
                }
                if (--limit < 0) break;
            }

            return CreateGraph(nodes);
        }

        private static double ConvertToTimestamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (double)span.TotalSeconds;
        }
        private static DateTime ConvertFromTimestamp(double v)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = new TimeSpan(0, 0, 0, (int)v);
            return date + span;
        }
        private Image CreateGraph(List<GitNode> nodes)
        {
            StreamWriter sw = new StreamWriter("graph.txt");
            /*Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Cyan, Color.DarkRed, Color.Olive,
                                     Color.DarkBlue, Color.SeaShell, Color.DarkGreen, Color.Yellow, Color.DimGray,
                                 Color.Chocolate, Color.Orange, Color.DeepSkyBlue, Color.Salmon,Color.DarkCyan,Color.DeepPink};*/

            int width = size.Width;
            int height = size.Height;

            DateTime min = nodes.Min(p => p.time);
            DateTime max = nodes.Max(p => p.time);

            double timeW = ConvertToTimestamp(max) - ConvertToTimestamp(min);
            double timeL = ConvertToTimestamp(min);

            int maxloc = (int)nodes.Max(p => p.loc.Sum(q => q.Value));

            UpdateEmptyNodes(nodes);

            nodes.Sort((a, b) => DateTime.Compare(a.time, b.time));

            Log("History from {0} to {1}, max LOC = {2}", min, max, maxloc);

            foreach (var entry in nodes)
            {
                double xx = (ConvertToTimestamp(entry.time) - timeL) / timeW;
                double sum = 0;
                sw.Write(xx.ToString());
                sw.Write(" 0 ");
                foreach (var module in entry.loc)
                {
                    sum += module.Value;
                    sw.Write(sum.ToString() + " ");
                }
                sw.WriteLine();
            }
            sw.Close();

            return Graph.Plot(nodes.First().loc.Keys.ToList(), "graph.txt", size.Width, size.Height, maxloc);
        }

        private void UpdateEmptyNodes(List<GitNode> nodes)
        {
            List<string> modules = new List<string>();
            foreach (var g in nodes)
            {
                foreach (var m in g.loc.Keys)
                {
                    if (!modules.Contains(m)) modules.Add(m);
                }
            }
            foreach (var g in nodes)
            {
                foreach (var m in modules)
                {
                    if (!g.loc.ContainsKey(m))
                    {
                        g.loc.Add(m, 0);
                    }
                }
            }
        }

        List<string> _ignoredDirs = null;
        List<string> _twolevelDirs = null;

        private SortedDictionary<string, long> CalculateLOC(string dir, string app)
        {
            if (_ignoredDirs == null)
            {
                _ignoredDirs = ("QWERTYUIOPASDGFHJKLZXCVBNM;" + Settings.Instance.Ignore.Trim(';')).Split(';').ToList();
                _twolevelDirs = ("QWERTYUIOPASDGFHJKLZXCVBNM;" + Settings.Instance.TwoLevels.Trim(';')).Split(';').ToList();
            }
            SortedDictionary<string, long> ret = new SortedDictionary<string, long>();

            var root = new DirectoryInfo(dir);
            foreach (var d in root.GetDirectories())
            {
                bool ignored = _ignoredDirs.Contains(d.Name);
                bool twolevel = _twolevelDirs.Contains(d.Name);
                if (!ignored)
                {
                    if (twolevel)
                    {
                        foreach (var q in CalculateLOC(dir + "\\" + d.Name, d.Name + "\\"))
                        {
                            ret.Add(q.Key, q.Value);
                        }
                    }
                    else
                    {
                        ret.Add(app + d.Name, CalculateSize(d.FullName));
                    }
                }
            }
            if (app == "")
            {
                foreach (var q in ret)
                {
                    Log("* {0}: {1}", q.Key, q.Value);
                }
            }
            return ret;
        }

        private long CalculateSize(string p)
        {
            long loc = 0;
            foreach (var d in new DirectoryInfo(p).GetDirectories())
            {
                loc += CalculateSize(d.FullName);
            }
            foreach (var f in new DirectoryInfo(p).GetFiles())
            {
                if (IsIncluded(f.Extension))
                {
                    loc += CalcLOC(f.FullName);
                }
            }
            return loc;
        }

        private long CalcLOC(string p)
        {
            string s = File.ReadAllText(p);
            return s.Split('\n').Length;
        }

        private List<string> allowedExt = null;
        private bool IsIncluded(string p)
        {
            if (allowedExt == null)
            {
                allowedExt = Settings.Instance.AllowedFiles.Split(',').ToList();
            }
            if (p.Length < 2) return false;
            return allowedExt.Contains(p.Substring(1));
        }

        private DateTime GetDateTime(string p)
        {
            DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(double.Parse(p));
            return dateTime;
        }
        Size size;
        public void SetSize(int w, int h)
        {
            size = new Size(w, h);            
        }
    }
}
