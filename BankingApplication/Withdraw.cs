using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingApplication
{
    public partial class Withdraw : Form
    {
        UserInformation client;
        BankApplicationEntities bankdb;
        E_Wallets wallet;
        public Withdraw(UserInformation client, BankApplicationEntities bankdb, E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.bankdb = bankdb;
            this.wallet = wallet;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TransactionSelection transactionSelection = new TransactionSelection(this.client,this.bankdb,this.wallet);
            transactionSelection.Show();
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {

            int moneyToWithdraw = Convert.ToInt32(txtAmount.Text.ToString());

            if (moneyToWithdraw > 0)
            {
                try
                {
                    this.client.Balance -= moneyToWithdraw;
                    MessageBox.Show("Successfully Withdrew Money", "Success", MessageBoxButtons.OK);
                    txtAmount.Clear();
                    this.bankdb.UserInformations.AddOrUpdate(this.client);
                    this.bankdb.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }

            }
            else
            {

                MessageBox.Show("Please Withdraw Money From Account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
