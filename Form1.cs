using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace SW_Registry
{
    public partial class Installed : Form
    {
        public Installed()
        {
            InitializeComponent();
        }

        private void Installed_Load(object sender, EventArgs e)
        {
            RegistryKey rk=Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            RegistryKey rktemp;
            string[] SW_Name=rk.GetSubKeyNames();
            string[] SW_Publisher = rk.GetSubKeyNames();
            string[] SW_Version = rk.GetSubKeyNames();
            int i=0;
            string uninstall;
            string[] row = {"","",""};
            string[] rkstrings = rk.GetSubKeyNames();
            listView1.View = View.Details;
            
            listView1.Columns.Add("Software","Software");
            listView1.Columns["Software"].Width = 350;
            foreach (string x in rkstrings)
            {
                rktemp = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + x,false);
                //listBox1.Items.Add("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\" + x);
                //listView1.Items.Add("");

                SW_Name[i] = (string)rktemp.GetValue("DisplayName", "");
                


                    
                    rktemp = null;
                    i++;
                
                rktemp = null;
            }


            listView1.Columns.Add("Version", "Version");
            listView1.Columns["Version"].Width = 150;
            i = 0;


            foreach (string x in rkstrings)
            {
                rktemp = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + x, false);
                //listBox1.Items.Add("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\" + x);
                //listView1.Items.Add("");

                SW_Version[i] = (string)rktemp.GetValue("DisplayVersion", "");

                


                    rktemp = null;
                    i++;
                
                rktemp = null;
            }


            listView1.Columns.Add("Hersteller", "Hersteller");
            listView1.Columns["Hersteller"].Width = 350;
            i = 0;


            foreach (string x in rkstrings)
            {
                rktemp = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + x, false);
                //listBox1.Items.Add("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\" + x);
                //listView1.Items.Add("");

                SW_Publisher[i] = (string)rktemp.GetValue("Publisher", "");

 
                    
                    rktemp = null;
                    i++;
                
                rktemp = null;
            }

            for (i = 0; i < SW_Name.Count(); i++)
            {
                row[0] = SW_Name[i];
                row[1] = SW_Version[i];
                row[2] = SW_Publisher[i];
                if (!((row[0].Equals(String.Empty))&&(row[1].Equals(String.Empty))&&(row[2].Equals(String.Empty))))
                {
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
                }
            }
            //Resize and Position of Elements
            listView1.Width = Installed.ActiveForm.Width - 200;
            button1.Location = new System.Drawing.Point(Installed.ActiveForm.Width - 155, 32);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0,j=0;
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            RegistryKey rktemp;
            string uninstall;
            string[] rkstrings = rk.GetSubKeyNames();
            string[] SW_Name = rk.GetSubKeyNames();

            foreach (string x in rkstrings)
            {
                rktemp = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + x, false);
                //listBox1.Items.Add("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\" + x);
                //listView1.Items.Add("");

                SW_Name[i] = (string)rktemp.GetValue("DisplayName", "");




                rktemp = null;
                i++;

                rktemp = null;
            }

            for (j = 0; (j < i); j++)
            {
                //MessageBox.Show(listView1.SelectedItems[0].ToString());
                if (listView1.SelectedItems[0].ToString().Equals("ListViewItem: {" + SW_Name[j] +"}"))
                {
                    MessageBox.Show("WARNING: You are uninstalling \""+SW_Name[j]+"\" permanently from your PC!");
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = false;
                    
                    
                    rktemp = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\"+rkstrings[j]+"\\", false);
                    uninstall = (string)rktemp.GetValue("UninstallString", "nicht gefunden");
                    startInfo.FileName = "cmd.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    //uninstall.Replace("MsiExec.exe", "");
                    //MessageBox.Show(uninstall);
                    startInfo.Arguments = "/C \""+uninstall+"\"";
                    //MessageBox.Show(uninstall);
                    try
                    {
                        // Start the process with the info we specified.
                        // Call WaitForExit and then the using statement will close.
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                            exeProcess.WaitForExit();
                        }
                    }
                    catch
                    {
                        // Log error.
                    }
                }
            }

        }
    }
}
