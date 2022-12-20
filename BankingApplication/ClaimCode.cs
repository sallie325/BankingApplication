using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        BankApplicationEntities dbContext;
        UserInformation client;
        E_Wallets wallet;
        public ClaimCode(UserInformation client, BankApplicationEntities dbContext, E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.dbContext = dbContext;
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

                var match = dbContext.E_Wallets.Any(x => x.Pin == wallet.Pin && x.Code == wallet.Code);
                var pin = dbContext.E_Wallets.Where(x => x.Pin == wallet.Pin).ToList();

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
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TransactionSelection transaction = new TransactionSelection(this.client, this.dbContext, this.wallet);
            transaction.Show();
        }
    }
}
