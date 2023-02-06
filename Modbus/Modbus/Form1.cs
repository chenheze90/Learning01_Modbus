using NModbus;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Modbus
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private IModbusMaster master;
        private IModbusFactory factory;
        public CancellationTokenSource tokenSource = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
            factory = new ModbusFactory();
            client = new TcpClient();
            client.Connect("127.0.0.1", 502);
            master = factory.CreateMaster(client);
        }

        private void ReadColDatas(bool[] values)
        {
            richTextBox1.AppendText(string.Join(",", values) + "\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool[] res = master.ReadCoils(1, 1, 5);
            ReadColDatas(res);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool[] res = master.ReadInputs(1, 0, 6);
            ReadColDatas(res);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 写线圈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            master.WriteSingleCoil(1, 1, true);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
