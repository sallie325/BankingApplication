using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingApplication
{
    public partial class TransactionSelection : Form
    {

        BankApplicationEntities dbContext;
        UserInformation client;
        E_Wallets wallet;

        public TransactionSelection(UserInformation client, BankApplicationEntities dbContext , E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.dbContext = dbContext;
            this.wallet = wallet;
        }

        public TransactionSelection()
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Balance balance = new Balance(client,this.dbContext,this.wallet);
            balance.Show();
        }


        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Deposit deposit = new Deposit(this.client,this.dbContext, this.wallet);
            deposit.Show();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Withdraw withdraw = new Withdraw(client, dbContext);
            withdraw.Show();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn(this.client, dbContext, this.wallet);
            logIn.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            E_Transfer transfer = new E_Transfer(this.client, this.dbContext, this.wallet);
            transfer.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClaimCode claim = new ClaimCode(this.client , this.dbContext, this.wallet);
            claim.Show();
        }
    }
}
