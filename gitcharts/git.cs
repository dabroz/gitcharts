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
    class git
    {
        static string SendGitCommand(string cmd, string directory)
        {
            ProcessStartInfo psi = new ProcessStartInfo("git", cmd);
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

        public git(string url_)
        {
            url = url_;       
        }

        public static string GitVersion { get { return SendGitCommand("version", "."); } }

        void Log(string s, params object[] p)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(s, p));
        }

        class GitNode
        {
            public DateTime time;
            public SortedDictionary<string, long> loc;
        }

        public Image CreateChartLOC()
        {
            Log("Git version: {0}", GitVersion);

            var dir = "gittemp" + DateTime.Now.ToBinary().ToString();
            Directory.CreateDirectory(dir);

            Log("clone(): {0}", SendGitCommand("clone " + url, dir));

            dir += "\\" + new DirectoryInfo(dir).GetDirectories()[0].Name;

            List<GitNode> nodes = new List<GitNode>();

            int count = 0;

            while (true)
            {
                DateTime time = GetDateTime(SendGitCommand("log -n 1 --pretty=format:%at", dir));

                var size = CalculateLOC(dir);

                nodes.Add(new GitNode() { loc = size, time = time });

                count++;
                Log("Reading: {0}", count);
               // if (count > 20) break;

                var prev = SendGitCommand("checkout HEAD^", dir);

                if (prev.Contains("error: pathspec 'HEAD^' did not match any file(s) known to git"))
                {
                    break;
                }
            }

            return CreateGraph(nodes);
        }

        private double ConvertToTimestamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (double)span.TotalSeconds;

        }
        private Image CreateGraph(List<GitNode> nodes)
        {
            int width = size.Width;
            int height = size.Height;
            int marginR = 60;
            int marginB = 90;

            DateTime min = nodes.Min(p => p.time);
            DateTime max = nodes.Max(p => p.time);

            double timeW = ConvertToTimestamp(max) - ConvertToTimestamp(min);
            double timeL = ConvertToTimestamp(min);

            long maxloc = nodes.Max(p => p.loc.Sum(q => q.Value));

            UpdateEmptyNodes(nodes);

            nodes.Sort((a, b) => DateTime.Compare(a.time, b.time));

            Log("History from {0} to {1}, max LOC = {2}", min, max, maxloc);

            double hecc = height - marginB; int hecci = (int)hecc;
            double wecc = width - marginR; int wecci = (int)wecc;

            int count = 0;

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                bool first = true;
                GitNode prev = null;
                //Dictionary<string, float> last;

                Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Cyan, Color.DarkRed, Color.Olive,
                                     Color.DarkBlue, Color.SeaShell, Color.DarkGreen, Color.Yellow, Color.DimGray,
                                 Color.Chocolate, Color.Orange, Color.DeepSkyBlue, Color.Salmon,Color.DarkCyan,Color.DeepPink};

                DrawNodesPolygons(nodes, timeW, timeL, maxloc, hecc, hecci, wecc, ref count, g, ref first, ref prev, colors);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawLine(new Pen(Color.Black, 3), 0, height - marginB, width - marginR, height - marginB);
                g.DrawLine(new Pen(Color.Black, 3), width - marginR, 0, width - marginR, height - marginB);

                int timespace = 80;
                int timecount = (int)(Math.Floor((float)wecci / (float)timespace));
                timespace = wecci / timecount;

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.SmoothingMode = SmoothingMode.HighQuality;
         
                int tx = 0;
                var pen = new Pen(new SolidBrush(Color.FromArgb(50, Color.Gray)), 1);
                pen.DashStyle = DashStyle.Dash;
                for (int i = 0; i <= timecount; i++)
                {
                    double tim = timeL + timeW * (float)i / (float)timecount;
                    var timm = GetDateTime(tim.ToString());
                    var ct = g.Transform;
                    g.TranslateTransform(tx+16, hecci);
                    g.RotateTransform(90);                    
                    g.DrawString(timm.ToShortDateString(), new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new PointF(0, 0));
                    g.Transform=ct;

                    if (i!=0 && i!=timecount)
                    {
                        g.DrawLine(pen, tx, 0, tx, hecci);
                    }

                    tx += timespace;
                }

                int locspace = 80;
                int loccount = (int)(Math.Ceiling((float)hecci / (float)locspace));
                locspace = hecci / loccount;

                int ty = 0;

                for (int i = 0; i <= loccount; i++)
                {
                    float z = (float)maxloc * (1.0f - (float)i / (float)loccount);
                    string xx = ((int)(z)).ToString();
                    g.DrawString(xx, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new PointF(wecci, ty));

                    if (i != 0 && i != loccount)
                    {
                        g.DrawLine(pen, 0, ty, wecci, ty);
                    }

                    ty += locspace;
                }

                long ff = 0;
                var ffx = new Font("Arial", 12, FontStyle.Bold);
                foreach (var q in prev.loc)
                {
                    long y1 = ff;
                    long y2 = ff + q.Value;
                    long y = (y1 + y2) / 2;

                    double yy = (float)y / (float)maxloc;
                    yy *= hecc;

                    g.FillRectangle(new SolidBrush(Color.FromArgb(170, Color.Black)), wecci - 150, hecci - (float)yy - 15, 140, 30);

                    var qsize = g.MeasureString(q.Key, ffx);
                    g.DrawString(q.Key, ffx, Brushes.White, wecci - 80 - qsize.Width/2, hecci - (float)yy-10);

                    ff += q.Value;
                }
            }

            return bmp;
        }

        private void DrawNodesPolygons(List<GitNode> nodes, double timeW, double timeL, long maxloc, double hecc, int hecci, double wecc, ref int count, Graphics g, ref bool first, ref GitNode prev, Color[] colors)
        {
            foreach (var n in nodes)
            {
                if (!first)
                {
                    double px1 = ConvertToTimestamp(prev.time) - timeL;
                    double px2 = ConvertToTimestamp(n.time) - timeL;
                    px1 /= timeW;
                    px2 /= timeW;
                    int x1 = (int)(wecc * px1);
                    int x2 = (int)(wecc * px2);
                    int y1 = 0, y2 = 0;
                    int i = 0;
                    foreach (var m in n.loc)
                    {
                        long m1 = prev.loc[m.Key];
                        long m2 = m.Value;

                        double p1 = (double)m1 / (double)maxloc;
                        double p2 = (double)m2 / (double)maxloc;

                        int u1 = (int)(p1 * hecc);
                        int u2 = (int)(p2 * hecc);

                        u1 += y1;
                        u2 += y2;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                        g.FillPolygon(new SolidBrush(colors[i]), new Point[]
                            {
                                new Point(x1, hecci-y1), new Point(x1,hecci-u1),new Point(x2,hecci-u2), new Point(x2,hecci-y2)
                            });
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.DrawLine(new Pen(colors[i], 3), x1, hecci - y1, x2, hecci - y2);
                        g.DrawLine(new Pen(colors[i], 3), x1, hecci - u1, x2, hecci - u2);

                        y1 = u1;
                        y2 = u2;
                        i++;
                    }
                }

                first = false;
                prev = n;

                count++;
                Log("Drawing: {0}", count);
            }
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

        private SortedDictionary<string, long> CalculateLOC(string dir)
        {
            SortedDictionary<string, long> ret = new SortedDictionary<string, long>();

            foreach (var d in new DirectoryInfo(dir).GetDirectories())
            {
                if (!d.Name.Contains(".git") && !d.Name.Contains("Includes") && !d.Name.Contains("Libs") && !d.Name.Contains("Build"))
                {
                    ret.Add(d.Name, CalculateSize(d.FullName));
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

        private bool IsIncluded(string p)
        {
            string[] allowed = { "c", "h", "cpp", "m", "mm", "cg","cs","res" };
            if (p.Length < 2) return false;
            return allowed.Contains(p.Substring(1));
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
