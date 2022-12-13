using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
         
            this.wallet.Code = txtCode.Text;
            var match = dbContext.E_Wallets.Any(x => x.Code == wallet.Code);
            

            MessageBox.Show("You have claimed " + this.wallet.Amount);

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TransactionSelection transaction= new TransactionSelection(this.client, this.dbContext ,this.wallet);
            transaction.Show();
        }
    }
}
