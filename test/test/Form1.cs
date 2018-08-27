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

namespace test
{
    public partial class Form1 : Form
    {
        SqlConnectionStringBuilder scsb;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = ".";
            scsb.InitialCatalog = "mybot";
            scsb.IntegratedSecurity = true;
            lblmessage.Visible = false;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
           
          
                string user = tbuser.Text;
                string pass = tbpassword.Text;
            if ((user == "") || (pass == ""))
            {

                lblmessage.Text = "請輸入帳號&&密碼";
                lblmessage.Visible = true;
                
            }
            else
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string sqlmsg = "select*from tStudent where tStudent_fUser= @searchuser and tStudent_fPass= @searchpass";

                SqlCommand cmd = new SqlCommand(sqlmsg, con);
                cmd.Parameters.AddWithValue("@searchuser", tbuser.Text);
                cmd.Parameters.AddWithValue("@searchpass", tbpassword.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    Form2 myform2 = new Form2();
                    myform2.formstrmsg = string.Format($"{reader["tStudent_fName"]}");
                    myform2.ShowDialog();
                    tbuser.Clear();
                    tbpassword.Clear();

                }
                else
                {
                    lblmessage.Text = "無此帳號，請確認是否輸入正確";
                    lblmessage.Visible = true;
                    tbpassword.Text = "";


                }
                reader.Close();
                con.Close();





            }
        }
    }
}
