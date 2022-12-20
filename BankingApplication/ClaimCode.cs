using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BankingApplication
{
    public partial class ClaimCode : Form
    {
        BankApplicationEntities bankdb;
        UserInformation client;
        E_Wallets wallet;
        public ClaimCode(UserInformation client, BankApplicationEntities bankdb, E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.bankdb = bankdb;
            this.wallet = wallet;
        }

        private void btnClaim_Click(object sender, EventArgs e)
        {
            if (txtPin.Text == "" || txtCode.Text == "")
            {
                MessageBox.Show("Please enter the correct pin ", " Fill all information ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.wallet.Pin = Convert.ToInt32(txtPin.Text);
                this.wallet.Code = txtCode.Text;

                var match = bankdb.E_Wallets.Any(x => x.Pin == wallet.Pin && x.Code == wallet.Code);
                var pin = bankdb.E_Wallets.Where(x => x.Pin == wallet.Pin).ToList();

                txtPin.Clear();
                txtCode.Clear();

                if (pin.Count() == 0)
                {
                    MessageBox.Show("Pin does not Match", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.wallet.Amount = pin[0].Amount;
                MessageBox.Show("You have claimed R" + wallet.Amount.ToString() , "Money Claimed"  , MessageBoxButtons.OK );
                
            }

            int moneyToClaim = Convert.ToInt32(wallet.Amount.ToString());

            if (moneyToClaim > 0)
            {
                try
                {
                    this.client.Balance -= moneyToClaim;
                    MessageBox.Show("Successfully Claimed Money", "Success", MessageBoxButtons.OK);
                    
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

                MessageBox.Show("Please Claim Money", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TransactionSelection transaction = new TransactionSelection(this.client, this.bankdb, this.wallet);
            transaction.Show();
        }
    }
}
