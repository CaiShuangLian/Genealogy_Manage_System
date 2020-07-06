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
    public partial class Personal_Information : Form
    {
        public Personal_Information()
        {
            InitializeComponent();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        //查询
        private void Personal_Information_Load(object sender, EventArgs e)
        {
            textBox1.Text = Globaldate.ID;

            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = "select * from Member where M_ID='" + Globaldate.ID + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.Read())
            {

            }
            else
            {
                textBox2.Text = dr["M_Name"].ToString();
                //textBox3.Text = dr["G_ID"].ToString();
                textBox3.Text = dr["M_FatherID"].ToString();
                textBox5.Text = dr["M_MotherID"].ToString();
                textBox7.Text = dr["M_SpouseID"].ToString();
                textBox9.Text = dr["M_Sex"].ToString();
                textBox10.Text = dr["M_Birth"].ToString();
                textBox12.Text = dr["M_Generation"].ToString();//字辈
                textBox11.Text = dr["M_Ranking"].ToString();
                textBox13.Text = dr["M_Birthplace"].ToString();
                textBox14.Text = dr["M_Liveplace"].ToString();

                dr.Close();
                //父亲姓名查找
                sql = "select M_Name from Member where M_ID='" + textBox3.Text + "'";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                    textBox4.Text = "";
                else
                {
                            textBox4.Text = dr["M_Name"].ToString();
                }

                dr.Close();
                //母亲姓名查找
                sql = "select M_Name from Member where M_ID='" + textBox5.Text + "'";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                    textBox6.Text = "";
                else
                    textBox6.Text = dr["M_Name"].ToString();

                dr.Close();
                //配偶姓名查找
                sql = "select M_Name from Member where M_ID='" + textBox7.Text + "'";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                    textBox8.Text = "";
                else
                    textBox8.Text = dr["M_Name"].ToString();

            }


            dr.Close();
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate () { new OrdinaryUsers().ShowDialog(); });
            th.Start();
            this.Close();
        }
        //修改自己的个人信息
        private void button1_Click(object sender, EventArgs e)
        {
            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql;
            sql = "update Member set M_Name='" + textBox2.Text + "',M_FatherID='" + textBox3.Text + "',M_MotherID='" + textBox5.Text + "',M_SpouseID='" + textBox7.Text + "',M_Sex='" + textBox9.Text + "',M_Birth='" + textBox10.Text + "',M_Generation='" + textBox12.Text + "',M_Ranking='" + textBox11.Text + "',M_Birthplace='" + textBox13.Text + "',M_Liveplace='" + textBox14.Text + "'where M_ID='" + textBox1.Text + "'";

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
            
            com1 = null;
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
