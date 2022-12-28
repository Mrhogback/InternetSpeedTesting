using System.Diagnostics;

namespace Speed_Test_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            var result = GetInternSpeedInfo();
            richTextBox1.Text = result["Server"];
            richTextBox2.Text = result["ISP"];
            richTextBox3.Text = result["IdleLatency"];
            richTextBox4.Text = result["Download"];
            richTextBox5.Text = result[" "];
            richTextBox6.Text = result["Upload"];
            richTextBox7.Text = result["J"];
            richTextBox8.Text = result["PacketLoss"];
        }
    }
}