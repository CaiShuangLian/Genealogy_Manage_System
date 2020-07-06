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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //连接并打开数据库：
            string connStr = @"Server=.; Initial Catalog=Member; Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();


            //查找父亲id：
            string sql = @"select M_FatherID from Member wherw [M_ID]='" + textBox1.Text + "'";



            //将父亲、儿子、我的信息收录进一个表：
            SqlDataAdapter s = new SqlDataAdapter("select * from Member where ((M_FatherID=“textbox1”)or(M_ID=”sql”)or(M_ID=”textbox1”))",conn);
            

            //将此表填满表格控件
            DataSet d = new DataSet();
            
            s.Fill(d, "t");
            
            dataGridView1.DataSource = d.Tables["t"];


            conn.Close();
        }
    }
}
