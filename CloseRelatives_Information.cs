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
    public partial class CloseRelatives_Information : Form
    {
        public CloseRelatives_Information()
        {
            InitializeComponent();
        }

        private void CloseRelatives_Information_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(delegate () { new OrdinaryUsers().ShowDialog(); });
            th.Start();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            string constr = "Server=.;Database=Genealogy Management System;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //成员查找
            string sql = "select * from Member where M_ID='" + textBox1.Text.Trim() + "' and G_ID=(select G_ID from Member where M_ID ='"+Globaldate.ID+"')";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            //判断成员ID是否存在以及是否是同族人员
            if (!dr.Read())
            {
                MessageBox.Show("不存在该成员或者他不是本族人员！","提示");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
            }

            else
            {

                //显示相应事件信息
                textBox2.Text = dr["M_Name"].ToString();
                textBox3.Text = dr["M_Sex"].ToString();
                textBox10.Text = dr["M_Birth"].ToString();
                textBox9.Text = dr["M_Generation"].ToString();//字辈
                textBox8.Text = dr["M_Ranking"].ToString();
                textBox11.Text = dr["M_Birthplace"].ToString();

                //保存当前登录用户的父母亲
                string FatherID="";
                string MotherID="";
                string SpouseID="";
                string myFatherID="";
                string myMotherID="";
                string mySpouseID="";

                dr.Close();
                //母亲姓名和ID查找
                sql = "select M_ID,M_Name from Member where M_ID=(select M_MotherID from Member where M_ID ='" + textBox1.Text + "')";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                {
                    textBox5.Text = "";
                    MotherID = "";
                }
                else
                {
                    textBox5.Text = dr["M_Name"].ToString();
                    MotherID = dr["M_ID"].ToString();
                }
                dr.Close();
                //父亲姓名查找
                sql = "select M_ID,M_Name from Member where M_ID=(select M_FatherID from Member where M_ID ='" + textBox1.Text + "')";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                {
                    textBox4.Text =null;
                    FatherID =null;
                }
                else
                {
                    textBox4.Text = dr["M_Name"].ToString();
                    FatherID = dr["M_ID"].ToString();
                }
                
                dr.Close();
                //配偶姓名查找
                sql = "select * from Member where M_ID=(select M_SpouseID from Member where M_ID ='" + textBox1.Text + "')";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                {
                    textBox6.Text = "";
                    SpouseID = "";
                }
                else
                {
                    textBox6.Text = dr["M_Name"].ToString();
                    SpouseID= dr["M_ID"].ToString(); 
                }
                    

                //
                string myID = Globaldate.ID;//myID是用户的ID，从登录界面读取

                dr.Close();
                sql = "select M_FatherID from Member where M_ID='" + Globaldate.ID + "'";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                    myFatherID = "";
                else
                { 
                    myFatherID = dr["M_FatherID"].ToString();
                   // MessageBox.Show(myFatherID);
                }//myFatherID是用户父亲的ID，

                dr.Close();
                sql = "select M_MotherID from Member where M_ID='" + Globaldate.ID + "'";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                    myMotherID = "";
                else
                    myMotherID = dr["M_MotherID"].ToString();//mymatherID是用户母亲的ID，

                dr.Close();
                sql = "select M_SpouseID from Member where M_ID='" + Globaldate.ID + "'";
                cmd = new SqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (!dr.Read())
                    mySpouseID = "";
                else
                    mySpouseID = dr["M_SpouseID"].ToString();//mymatherID是用户母亲的ID，

                //显示近亲与自己的关系(两服)
                if (FatherID == Globaldate.ID || MotherID == Globaldate.ID)
                {
                    if(textBox3.Text=="男")
                        textBox7.Text = "这是我儿";
                    else
                        textBox7.Text = "这是我的女儿";
                }
                else if ((FatherID == myFatherID || myMotherID == MotherID) )
                {
                    if (textBox3.Text == "女")
                        textBox7.Text = "这是我姐妹儿";
                    if (textBox3.Text == "男")
                        textBox7.Text = "这是我兄弟";
                    if( Globaldate.ID == textBox1.Text)
                        textBox7.Text = "这是你自己";
                }
                
                else if (textBox1.Text == myMotherID)
                {
                    textBox7.Text = "她是我的母亲";
                }
                
                else if (textBox1.Text== myFatherID)
                {
                    textBox7.Text = "他是我爹";
                }
                else if (SpouseID == myID)
                {
                    if (textBox3.Text == "女")
                        textBox7.Text = "我的妻子！";
                    else
                        textBox7.Text = "我的丈夫！";
                }

                else
                {
                    textBox7.Text = "系统君已晕，他是你的同宗！";
                }

                dr.Close();


            }
        }
    }
}
