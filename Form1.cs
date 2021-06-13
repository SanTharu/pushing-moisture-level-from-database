using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        delegate void serialCalback(string val);
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                try
                {
                   
                    serialPort1.Open();
                    button1.Enabled = false;
                    button2.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button1.Enabled = true;
            button2.Enabled = false;
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string incomString = serialPort1.ReadLine();
            Send_data(incomString);
        }
        

        public void Send_data(string moistureData) {

            try
            {
                TcpClient tcpclnt = new TcpClient(); //create object using TcpClient
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("68.183.180.35", 8000);//passing server IP and port

                Console.WriteLine("Connected");
                

                Stream stm = tcpclnt.GetStream();//creating stm object from Stream and assign stream data from tcpclient

                ASCIIEncoding asen = new ASCIIEncoding(); //create object from ACSII
                byte[] ba = asen.GetBytes(moistureData); //creating byte array "ba" and assign bytes data from moistre data (convert to byte array)


                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length); //write data to the stream (Sending Data)

                byte[] bb = new byte[100]; //create new byte array with 100 index size (capture response)
                int k = stm.Read(bb, 0, 100);

                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i])); //convert  byte array to sequence of char

                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

    }
    }

    
   
        













