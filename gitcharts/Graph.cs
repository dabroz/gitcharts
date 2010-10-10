using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace gitcharts
{
    class Graph
    {
        internal static Image Plot(List<string> names, string filename, int width, int height, int maxloc)
        {
            ProcessStartInfo psi = new ProcessStartInfo(Settings.Instance.GnuplotCommand, "");
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.WorkingDirectory = Application.StartupPath;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            var p = Process.Start(psi);
            p.StandardInput.WriteLine("set terminal png nocrop enhanced font verdana 12 size {0}, {1}", width, height);
            p.StandardInput.WriteLine("set output \"output.png\"");
            p.StandardInput.WriteLine("set key outside top right reverse");
            p.StandardInput.WriteLine("set y2tics "+ ((float)maxloc / 10.0f).ToString());
            p.StandardInput.WriteLine("set ytics nomirror");
            p.StandardInput.WriteLine("unset ytics");
            p.StandardInput.WriteLine("set y2range [0:" + maxloc.ToString() + "]");            

            string plot = "plot ";

            for (int i = 0; i < names.Count; i++)
            {
                int i2 = names.Count - i - 1;
                string q = names[i2];
                if (q.IndexOf("\\") != -1)
                {
                    q = q.Substring(q.IndexOf("\\") + 1);
                }
                if (i > 0) plot += ", ";
                plot += string.Format("'" + filename + "' u 1:{0}:{1} w filledcu axis x1y2 title '{2}'", i2 + 2, i2 + 3, q);
            }

            Git.Log("PLOT: {0}", plot);

            p.StandardInput.WriteLine(plot);
            p.StandardInput.Close();

            p.WaitForExit();
            var r = p.StandardOutput.ReadToEnd();
            var e = p.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(e))
            {
                r += e;
            }

          /*  var psi2 = new ProcessStartInfo(Settings.Instance.ConvertCommand, "output.svg output.png");
            psi2.RedirectStandardError = true;
            psi2.RedirectStandardOutput = true;
            psi2.WorkingDirectory = Application.StartupPath;
            psi2.UseShellExecute = false;
            psi2.WindowStyle = ProcessWindowStyle.Hidden;
            psi2.CreateNoWindow = true;
            var p2 = Process.Start(psi2);
            p2.WaitForExit();
         var  r2 = p2.StandardOutput.ReadToEnd();
         var  e2 = p2.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(e2))
            {
                r += e2;
            }
            if (!string.IsNullOrEmpty(r2))
            {
                r += r2;
            }*/

            return new Bitmap("output.png");
        }


    }
}