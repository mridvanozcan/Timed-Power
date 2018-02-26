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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                label3.Text = "Time Seted: " + textBox1.Text;
                timed = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur! \nConnect to Developer ");
            }

        }
        private void button2_Click(object sender, EventArgs e) => timer1.Enabled = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            ltime.Text = time.ToString();
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
    }
}
