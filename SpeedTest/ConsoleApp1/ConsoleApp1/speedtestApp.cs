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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConsoleApp1
{
    public partial class speedtestApp : Form
    {
        public Dictionary<string, string> GetInternSpeedInfo()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Process process = null;

            try
            {
                process = new Process();

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                process.StartInfo.FileName = "speedtest.exe";

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                List<string> list_output = new List<string>();
                list_output = output.Split('\n').ToList();

                result["Server"] = list_output[3].Split(':')[1].Trim();
                result["ISP"] = list_output[4].Split(':')[1].Trim();
                result["IdleLatency"] = list_output[5].Split(':')[1].Trim();
                result["Download"] = list_output[6].Replace("Download:", "").Trim();
                result[" "] = list_output[7];
                result["Upload"] = list_output[8].Replace("Upload:", "").Trim();
                result["J"] = list_output[9];
                result["PacketLoss"] = list_output[10].Split(':')[1].Trim();


                string pertama = string.Join(output, list_output[3]);
                textBox1= new System.Windows.Forms.TextBox();
                textBox1.Text = pertama;
   


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                if (process != null) process.Dispose();
            }
            return result;
        }
        static void Main(string[] args)
        {
            var result = new Program().GetInternSpeedInfo();
            Console.WriteLine("Server : " + result?["Server"]);
            Console.WriteLine("ISP : " + result?["ISP"]);
            Console.WriteLine("IdleLatency : " + result?["IdleLatency"]);
            Console.WriteLine("Download : " + result?["Download"]);
            Console.WriteLine("Jitter : " + result?[" "]);
            Console.WriteLine("Upload : " + result?["Upload"]);
            Console.WriteLine("Jitter2 : " + result?["J"]);
            Console.WriteLine("PacketLoss : " + result?["PacketLoss"]);
            Console.ReadLine();



        }
        public speedtestApp()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }

}
