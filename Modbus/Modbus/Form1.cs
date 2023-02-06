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

        private void ReadColDatas(ushort[] values)
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
            ushort[] res = master.ReadHoldingRegisters(1, (ushort)0, (ushort)4);
            ReadColDatas(res);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ushort[] res = master.ReadInputRegisters(1, (ushort)0, (ushort)4);
            ReadColDatas(res);
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

        /// <summary>
        /// 写入单寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            master.WriteSingleRegister(1, (ushort)4, (ushort)56);
        }
        /// <summary>
        /// 写入多线圈
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            master.WriteMultipleCoils(1, (ushort)0, new bool[] { true, true, false, true });
        }

        /// <summary>
        /// 写入多寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            master.WriteMultipleRegisters(1, (ushort)2, new ushort[] { 22, 33, 44, 55 });
        }
    }
}
