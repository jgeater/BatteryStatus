using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace BatteryStatus
{
    public partial class Form1 : Form
    {
        int MinBatLev = 60;
        
        public Form1()
        {
            InitializeComponent();

        }
        Dictionary<UInt16, string> StatusCodes;
        private void Form1_Load(object sender, EventArgs e)
        {

            StatusCodes = new Dictionary<ushort, string>();
            StatusCodes.Add(1, "The battery is discharging. Please Plug in the Charger!!");
            StatusCodes.Add(2, "The system has access to AC. So the battery is Charging.");
            StatusCodes.Add(3, "Fully Charged");
            StatusCodes.Add(4, "Low");
            StatusCodes.Add(5, "Critical");
            StatusCodes.Add(6, "The battery is Charging");
            StatusCodes.Add(7, "Charging and High");
            StatusCodes.Add(8, "Charging and Low");
            StatusCodes.Add(9, "Undefined");
            StatusCodes.Add(10,"Partially Charged");

            PowerStatus p = SystemInformation.PowerStatus;
            int a = (int)(p.BatteryLifePercent * 100);

            timer1.Enabled = true;

            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery");
            foreach (ManagementObject mo in mos.Get())
            {
                lblBatteryName.Text = a.ToString()+"%";
                lblBatteryName.Text = mo["Name"].ToString();
                label6.Text = a + "%" ;
                UInt16 statuscode = (UInt16)mo["BatteryStatus"];
                string statusString = StatusCodes[statuscode];
                label5.Text = statusString;
                label2.Text = "The Battery must be changed to at least " + MinBatLev +"% to continue";
                label4.Text = "The task sequence will continue automatically when the battery reaches " + MinBatLev +"% charge.";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            PowerStatus p = SystemInformation.PowerStatus;
            int a = (int)(p.BatteryLifePercent * 100);

            if (a > MinBatLev)
            {
                Environment.Exit(0);
            }

            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Battery where Name='"+lblBatteryName.Text+"'");
            foreach (ManagementObject mo in mos.Get())
            {

                lblBatteryName.Text = a.ToString() + "%";
                lblBatteryName.Text = mo["Name"].ToString();
                label6.Text = a + "%";
                UInt16 statuscode = (UInt16)mo["BatteryStatus"];
                string statusString = StatusCodes[statuscode];
                label5.Text = statusString;

            }

        }

    }
}
