using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace clientForm
{
    public partial class calculatrice : Form
    {
        String nb1, nb2, signe, resultat="";
        bool op=false;
        clientForm.testUdpClient proc;
        public calculatrice()
        {
            InitializeComponent();
            proc = new clientForm.testUdpClient(8080, 8020, "127.0.0.1");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            signe = b.Text;
            nb1 = result.Text;
            op = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            nb2 = result.Text;
            result.Clear();
            
                
                string command = nb1 + ":" + signe + ":" + nb2;
                proc.Execute(command, ref resultat);
                result.Text = resultat;
            op = true;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (result.Text=="0" || op==true)
            {
                result.Clear();

            }

            Button b = (Button)sender;
            result.Text = result.Text + b.Text;
            op = false;
        }

        private void ce(object sender, EventArgs e)
        {
            result.Text = "0";
        }
    }
}
