﻿using System;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            git g = new git(textBox1.Text);
            Image i = g.CreateChartLOC();
            i.Save("chart.png", ImageFormat.Png);
            button1.Text = "saved";
        }
    }
}
