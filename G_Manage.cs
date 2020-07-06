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
    public partial class G_Manage : Form
    {
        public G_Manage()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate () { new Administrators().ShowDialog(); });
            th.Start();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _Fname = textBox1.Text.Trim();//族谱ID
            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql;

            //判断族谱ID是否存在
            SqlCommand com1 = new SqlCommand("Select G_ID from Genealogy where G_ID='" + _Fname + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            DataSet ds1 = new DataSet();
            int n1 = da1.Fill(ds1, "Genealogy");



            if (n1 > 0)
            {
                sql = "update Genealogy set G_Name ='" + textBox2.Text + "',G_Surname='" + textBox3.Text + "',G_GenerationIntro='" + textBox4.Text + "',G_Educ='" + richTextBox1.Text + "',G_Intro='" + richTextBox2.Text + "'where G_ID='" + textBox1.Text + "'";
                com1 = new SqlCommand(sql, con);

                try
                {
                    com1.ExecuteNonQuery();
                    MessageBox.Show("信息修改成功！");
                }
                catch (Exception msg)
                {
                    MessageBox.Show("修改失败！\n出错原因：" + msg.Message);
                }
            }
            else
            {
                MessageBox.Show("族谱ID不存在！", "提示");
                textBox2.Text = "";
            }
            com1 = null;
            con.Close();
        }
        //查询
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();//家谱ID
            //string _Fname = textBox3.Text.Trim();//族谱ID
            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = "select * from Genealogy where G_ID='" + name + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            //判断事件ID是否存在
            if (!dr.Read())
            {
                MessageBox.Show("不存在该事件");
                textBox1.Text = "";
            }

            else
            {
                //显示相应事件信息
                textBox2.Text = dr["G_Name"].ToString();
                textBox3.Text = dr["G_Surname"].ToString();
                textBox4.Text = dr["G_GenerationIntro"].ToString();
                richTextBox1.Text = dr["G_Educ"].ToString();
                richTextBox2.Text = dr["G_Intro"].ToString();


            }
            dr.Close();
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = "delete from Genealogy  where G_ID='" + textBox1.Text + "'";

            SqlCommand com1 = new SqlCommand(sql, con);

            try
            {
                com1.ExecuteNonQuery();
                MessageBox.Show("信息修改成功！");
            }
            catch (Exception msg)
            {
                MessageBox.Show("修改失败！\n出错原因：" + msg.Message);
            }

            con.Close();
        }
    }
}
