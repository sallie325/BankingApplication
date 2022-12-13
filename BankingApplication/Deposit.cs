using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BankingApplication
{
    public partial class Deposit : Form
    {
        BankApplicationEntities bankdb;
        UserInformation client;
        E_Wallets wallet;


        public Deposit(UserInformation userInformation, BankApplicationEntities bankdb , E_Wallets wallet)
        {
            InitializeComponent();
            this.client = userInformation;
            this.bankdb = bankdb;
            this.wallet = wallet;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TransactionSelection transactionSelection = new TransactionSelection(this.client,this.bankdb,this.wallet);
            transactionSelection.Show();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
           //int moneyToDeposit = this.client 

            int moneyToDeposit = Convert.ToInt32(txtAmount.Text.ToString());

            if (moneyToDeposit > 0)
            {
                try
                {
                    this.client.Balance += moneyToDeposit;
                    MessageBox.Show("Successfully Deposited Money","Success",MessageBoxButtons.OK);
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

                MessageBox.Show("Please Deposit Money to Account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
