using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BankingApplication
{
    public partial class Balance : Form 
    {
        UserInformation client;
        BankApplicationEntities bankdb;
        E_Wallets wallet;
        public Balance(UserInformation client, BankApplicationEntities bankdb , E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.bankdb = bankdb;
            this.wallet = wallet;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TransactionSelection transactionSelection = new TransactionSelection(client,bankdb,wallet);
            transactionSelection.Show();
        }

        private void Balance_Load(object sender, EventArgs e)
        {
            txtAccountNumber.Text = this.client.AccountNumber;
            txtAccountHolder.Text = this.client.FirstName.ToString() + " " + this.client.LastName.ToString() ;
            txtBalance.Text = client.Balance.ToString();
            
        }
    }
}
