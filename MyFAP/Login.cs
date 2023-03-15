using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
namespace MyFAP
{
    public partial class Login : Form
    {
        MyFapContext myFapContext;
        public Login()
        {
            InitializeComponent();
            myFapContext = new MyFapContext();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;
            var account = from a in myFapContext.Accounts
                          where a.UserName == user && a.Password == pass
                          select a;
            try
            {
                Account a = account.FirstOrDefault();
                if (a != null)
                {
                    Form1 f = new Form1(a.AccountId, this);
                    f.Show();
                }
                else
                {
                    label4.Text = "Login failed!";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}



