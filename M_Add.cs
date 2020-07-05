﻿using System;
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
    public partial class M_Add : Form
    {
        public M_Add()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate () { new Administrators().ShowDialog(); });
            th.Start();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string name = textBox1.Text.Trim();//用户ID
            string _Fname = textBox3.Text.Trim();//父亲ID
            //string _Mname=textB
            string _Gname = textBox3.Text.Trim();//族谱ID

            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand com = new SqlCommand("select M_ID from Member where M_ID='" + name + "'", con);
            // 建立SqlDataAdapter和DataSet对象
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            int n = da.Fill(ds, "Memebr");

            SqlCommand com1 = new SqlCommand("Select M_ID from Member where M_ID='" + _Fname + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            DataSet ds1 = new DataSet();
            int n1 = da1.Fill(ds1, "Member");

            SqlCommand com2 = new SqlCommand("Select G_ID from Genealogy where G_ID='" + _Gname + "'", con);
            SqlDataAdapter da2 = new SqlDataAdapter(com2);
            DataSet ds2 = new DataSet();
            int n2 = da2.Fill(ds2, "Genealogy");


            if (n != 0)
            {
                MessageBox.Show("ID已存在！", "提示");
                textBox1.Text = "";
            }
            else if (textBox1.TextLength > 15)
            {
                MessageBox.Show("ID太长，我怕你记不住，请换个短的吧！", "提示");
            }

            else if (textBox1.Text == "" || textBox3.Text == "" )
            {
                MessageBox.Show("族谱ID或用户ID不能为空！", "提示");
            }
            else if (n2 == 0)
            {
                MessageBox.Show("不存在的族谱ID！", "提示");
                //textBox1.Text = "";
                textBox3.Text = "";
            }
            else
            {
                // 指定SQL语句
                string s = "insert into Member(M_ID,M_Name,G_ID,M_FatherID,M_MotherID,M_SpouseID," +
                 "M_Sex,M_Birth,M_Ranking,M_Generation,M_Birthplace,M_Liveplace,R_Bool,R_psw) values ('"
                     + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "'" +
                     ",'" + textBox4.Text + "','" + textBox5.Text + "'" +
                     ",'" + textBox8.Text + "','" + textBox10.Text + "'," +
                     "'" + textBox11.Text + "','" + textBox12.Text + "','" + textBox13.Text + "'," +
                     "'" + textBox14.Text + "','" + textBox15.Text + "','0','" + textBox1.Text + "')";
                com = new SqlCommand(s, con);
                // 建立SqlDataAdapter和DataSet对象
                n = com.ExecuteNonQuery();


                com = null;
                com1 = null;
                com2 = null;

                if (n > 0)
                {
                    MessageBox.Show("添加成功！", "提示");
                }
                else
                    MessageBox.Show("添加失败！", "提示");
                //this.Close();

            }
            con.Close();

        }
    }
}
