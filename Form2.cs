using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 期末作业_族谱
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = @"Server=.; Initial Catalog=Member; Integrated Security=True";
            string id = textBox1.Text;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string sql = @"SELECT * FROM Member WHERE [M_ID]='" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (!dr.Read())
            {
                MessageBox.Show("不存在该成员！");
                return;
            }

            textBox2.Text = dr["M_Name"].ToString();
            textBox3.Text = dr["G_ID"].ToString();
            textBox4.Text = dr["M_FatherID"].ToString();
            textBox8.Text = dr["M_SpouseID"].ToString();
            textBox10.Text = dr["M_Sex"].ToString();
            textBox11.Text = dr["M_Birth"].ToString();
            textBox12.Text = dr["M_Ranking"].ToString();
            textBox13.Text = dr["M_Generation"].ToString();
            textBox14.Text = dr["M_Birthplace"].ToString();
            textBox15.Text = dr["M_Liveplace"].ToString();


            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connStr = @"Server=.; Initial Catalog=Member; Integrated Security=True";
            string id = textBox1.Text;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = @"DELETE FROM Member WHERE [M_ID]='" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            string sq = string.Format(@"insert into Member values ('" + "{0}" + "','" +
                            "" + "{1}" + "','" + "{2}" + "','" + "{3}" + "','" +
                            "{4}" + "','" + "{5}" + "','" + "{6}" + "','" + "{8}" + "','" + "{9}" + "','" + "{10}" + "')",
                            textBox1.Text, textBox9.Text, textBox4.Text,
                            textBox3.Text, textBox2.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text, textBox15.Text);
             cmd = new SqlCommand(sq, conn);


            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("信息修改成功！");
            }
            catch (Exception msg)
            {
                MessageBox.Show("修改失败！\n出错原因：" + msg.Message);
            }

            conn.Close();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connStr = @"Server=.; Initial Catalog=Member; Integrated Security=True";
            string id = textBox1.Text;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            string sql = @"DELETE FROM Member WHERE [M_ID]='" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.Read())
            {
                MessageBox.Show("不存在该成员！");
                return;
            }
            conn.Close();
        }
    }
}
