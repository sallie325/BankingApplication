using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BankingApplication
{
    public partial class LogIn : Form
    {
        UserInformation client;
        BankApplicationEntities bankdb;
        E_Wallets wallet;
        public LogIn(UserInformation client, BankApplicationEntities bankdb, E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.bankdb = bankdb;   
            this.wallet = wallet;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CreateAccount createAccount = new CreateAccount(this.client, this.bankdb, this.wallet); 
            createAccount.Show();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {


            if (txtAccountNumber.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please Enter in Your Information", "Fill all information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                BankApplicationEntities bankdb = new BankApplicationEntities();
                this.client.AccountNumber = txtAccountNumber.Text;
                this.client.UserPassword = txtPassword.Text;

                var match = bankdb.UserInformations.Any(x => x.AccountNumber == client.AccountNumber && x.UserPassword == client.UserPassword);

                var user = bankdb.UserInformations.Where(x => x.AccountNumber == client.AccountNumber).ToList();

                txtAccountNumber.Clear();
                txtPassword.Clear();

                if (user.Count() == 0)
                {
                    MessageBox.Show("Account Number Does Not Match", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.client.Balance = user[0].Balance;
                this.client.Withdraw = user[0].Withdraw;
                this.client.Deposit = user[0].Deposit;
                this.client.FirstName = user[0].FirstName;
                this.client.LastName = user[0].LastName;


                MessageBox.Show("Logged in Successfully", "Successfully Logged In", MessageBoxButtons.OK);

                this.Hide();
                TransactionSelection transactionSelection = new TransactionSelection(this.client,this.bankdb,this.wallet);
                transactionSelection.Show();


            }

        }
    }
}
