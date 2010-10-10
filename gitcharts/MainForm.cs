using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace gitcharts
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            cURL.Text = Settings.Instance.Path;
            cWidth.Text = Settings.Instance.ChartWidth.ToString();
            cHeight.Text = Settings.Instance.ChartHeight.ToString();

            cIgnore.Text = Settings.Instance.Ignore;
            cInclude.Text = Settings.Instance.AllowedFiles;
            c2Level.Text = Settings.Instance.TwoLevels;

            cGIT.Text = Settings.Instance.GitCommand;
            cGnuplot.Text = Settings.Instance.GnuplotCommand;
            cRevs.Text = Settings.Instance.RevisionLimit.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Instance.Ignore = cIgnore.Text;
            Settings.Instance.TwoLevels = c2Level.Text;
            Settings.Instance.AllowedFiles = cInclude.Text;

            Settings.Instance.Path = cURL.Text;
            Settings.Instance.ChartWidth = int.Parse(cWidth.Text);
            Settings.Instance.ChartHeight = int.Parse(cHeight.Text);

            Settings.Instance.GnuplotCommand = cGnuplot.Text;
            Settings.Instance.GitCommand = cGIT.Text;
            Settings.Instance.RevisionLimit = int.Parse(cRevs.Text);

            Settings.Instance.Save();
            Git g = new Git(cURL.Text);
            g.SetSize(int.Parse(cWidth.Text), int.Parse(cHeight.Text));
            Image i = g.CreateChartLOC();
            i.Save("chart.png", ImageFormat.Png);
            BackgroundImage = i;
            BackgroundImageLayout = ImageLayout.Zoom;
            button1.Text = "saved";

            List<Control> cc = new List<Control>();
            foreach (Control c in Controls)
            {
                cc.Add(c);
            }
            foreach (var c in cc)
            {
                Controls.Remove(c);
            }
        }
    }
}
