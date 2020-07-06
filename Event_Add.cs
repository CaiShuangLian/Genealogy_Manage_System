using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Genealogy_Management_System
{
    public partial class Event_Add : Form
    {
        public Event_Add()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate () { new Event_Manage().ShowDialog(); });
            th.Start();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();//族谱ID
            string _Fname = textBox2.Text.Trim();//事件ID
            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand com = new SqlCommand("select G_ID from Genealogy where G_ID='" + name + "'", con);
            // 建立SqlDataAdapter和DataSet对象
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            int n = da.Fill(ds, "Genealogy");

            SqlCommand com1 = new SqlCommand("Select E_ID from Event where E_ID='" + _Fname + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            DataSet ds1 = new DataSet();
            int n1 = da1.Fill(ds1, "Event");



            if (n == 0)
            {
                MessageBox.Show("不存在该族谱ID！", "提示");
                textBox1.Text = "";
                
            }
            else if (textBox2.TextLength > 15)
            {
                MessageBox.Show("ID太长，我怕你记不住，请换个短的吧！", "提示");
            }

            else if (textBox1.Text == "" || textBox2.Text == "" )
            {
                MessageBox.Show("族谱ID或事件不能为空！", "提示");
            }
            else if (n1 != 0)
            {
                MessageBox.Show("事件ID已存在！", "提示");
                //textBox1.Text = "";
                textBox3.Text = "";
            }
           
            else
            {
                // 指定SQL语句
                com = new SqlCommand("insert into Event(G_ID,E_ID,E_Date,E_Name,E_Event,E_Note) values ('"
                    + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + richTextBox1.Text+"','"+richTextBox2.Text+"')", con);
                // 建立SqlDataAdapter和DataSet对象
                n = com.ExecuteNonQuery();

                if (n > 0)
                {
                     MessageBox.Show("添加成功！", "提示");
                }
                else
                    MessageBox.Show("注册失败！", "提示");

                com = null;
                com1 = null;
            }
            con.Close();

        }

        private void Event_Add_Load(object sender, EventArgs e)
        {

        }
    }
}
