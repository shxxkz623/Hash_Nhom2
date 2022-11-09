using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Hash_Nhom2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public static String inputHash;
        public static byte[] arrayBytes;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                string content = sr.ReadToEnd();
                textBox.Text = content;
                fs.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Binary file");
            comboBox1.Items.Add("Text string");
            comboBox1.Items.Add("Hex string");
            comboBox1.SelectedIndex = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                button1.Enabled = false;
                textBox.Enabled = true;
            }
            else
            {
                button1.Enabled = true;
                textBox.Text = "";
                textBox.Enabled = false;
            }
        }
        public static string CreateMD5(byte[] inputBytes)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = MD5.Create();
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static byte[] FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        /*public static string GetBytesFromBinaryString(String input)
        {
            string result = "";
            while (input.Length > 0)
            {
                var first8 = input.Substring(0, 8);
                input = input.Substring(8);
                var number = Convert.ToInt32(first8, 2);
                result += (char)number;
            }
            return result;
        }*/

        public byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }

        public static string CreateSHA1(byte[] inputBytes)
        {
            // Use input string to calculate MD5 hash
            SHA1 sha1 = SHA1.Create();
            {
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash;
            }
        }

        public static string CreateSHA2(byte[] inputBytes)
        {
            // Use input string to calculate MD5 hash
            SHA256 sha2 = SHA256.Create();
            {
                byte[] hashBytes = sha2.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            arrayBytes = GetBytesFromBinaryString(textBox.Text);
                            break;
                        }
                    case 1:
                        {
                            arrayBytes= ASCIIEncoding.ASCII.GetBytes(textBox.Text);
                            break;
                        }
                    case 2:
                        {
                            arrayBytes = FromHexString(textBox.Text);
                            break;
                        }
                }
                if (checkBox1.Checked)
                {
                    tbMD5.Text = CreateMD5(arrayBytes);
                }
                if (checkBox2.Checked)
                {
                    tbSHA1.Text = CreateSHA1(arrayBytes);
                }
                if (checkBox3.Checked)
                {
                    tbSHA256.Text = CreateSHA2(arrayBytes);
                }
            }
            catch
            {
                MessageBox.Show("Thông tin đầu vào lỗi hoặc định dạng không đúng");
            }
        }
    }
}
