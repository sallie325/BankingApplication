using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingApplication
{
    public partial class CreateAccount : Form
    {
        UserInformation client ;
        BankApplicationEntities bankdb;
        E_Wallets wallet;
        public CreateAccount(UserInformation client, BankApplicationEntities bankdb, E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.bankdb = bankdb;   
            this.wallet = wallet;
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {

            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtAccountNumber.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please Enter in Your Information", "Fill all information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                BankApplicationEntities bankdb = new BankApplicationEntities();
                client.FirstName = txtFirstName.Text;
                client.LastName = txtLastName.Text;
                client.AccountNumber = txtAccountNumber.Text;
                client.UserPassword = txtPassword.Text;

                bankdb.UserInformations.Add(client);
                bankdb.SaveChanges();
                MessageBox.Show("Account Created Successfully", "Account Created", MessageBoxButtons.OK);

                this.Hide();
                LogIn logIn = new LogIn(this.client,this.bankdb,this.wallet);
                logIn.Show();
            }
        }
    }
}
