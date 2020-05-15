/*   
 *   Timed Power  
 *
 *   Copyright (C) 2017  M.Ridvan Ozcan
 *
 *   This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimedPower.Properties;

namespace TimedPower
{

       
    public partial class Form1 : Form
    {

        int time = 0;
        int timed;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("PowerOff");
            comboBox1.Items.Add("Reboot");
            comboBox1.Items.Add("Hibernate");
            comboBox1.Items.Add("Suspend");
           
        }

        // Start Button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Start();
                label3.Text = "Time Seted: " + textBox1.Text;
                timed = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur! \nConnect to Developer ");
            }
            button1.Enabled = false;
            button2.Enabled = true;
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            label4.Enabled = true;
            counter.Enabled = true;
        }

        //Stop Button
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            time = 0;
            counter.Text = "0";
            label3.Text = "Program Stoped!";
            button2.Enabled = false;
            button1.Enabled = true;
            textBox1.Enabled = true;
            label4.Enabled = false;
            counter.Enabled = false;
            comboBox1.Enabled = true;
        }

        //Timer and actions
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            counter.Text = Convert.ToString(time);
            try
            {
                if (time == timed)
                {
                    timer1.Enabled = false;

                if (comboBox1.Text == "PowerOff")
                {
                    Process.Start("shutdown", "/s /t 0");
                }
                else if (comboBox1.Text == "Reboot")
                {
                    Process.Start("shutdown.exe", "-r -t 0");
                }
                else if (comboBox1.Text == "Hibernate")
                {
                    Application.SetSuspendState(PowerState.Hibernate, false, false);
                }
                else
                {
                    Application.SetSuspendState(PowerState.Suspend, false, false);
                }
                ActiveForm.Close();
            }

            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur! \nConnect to Developer ");
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void TrayMinimizerForm_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Timed Power";
            notifyIcon1.BalloonTipText = "Program minimized.";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }
    }
}
