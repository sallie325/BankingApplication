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
    public partial class E_Transfer : Form
    {

        BankApplicationEntities dbContext;
        UserInformation client;
        E_Wallets wallet;

        public E_Transfer(UserInformation client, BankApplicationEntities dbContext, E_Wallets wallet)
        {
            InitializeComponent();
            this.client = client;
            this.dbContext = dbContext;
            this.wallet = wallet;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (txtCellNumber.Text.Length == 10)
            {
                Random ran = new Random();
                String b = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int length = 6;
                String random = "";

                for (int i = 0; i < length; i++)
                {
                    int a = ran.Next(26);
                    random = random + b.ElementAt(a);
                }

                txtCode.Text = random;
                MessageBox.Show("Phone Number is Valid", "Success", MessageBoxButtons.OK);
                MessageBox.Show("R" + txtAmount.Text + " Was sent to " + txtCellNumber.Text + " Code = " + txtCode.Text);
            }
            else
            {
                MessageBox.Show("Phone Number provided is not Valid", "Incorrect Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            BankApplicationEntities bankdb = new BankApplicationEntities();
            wallet.CellNumber = Convert.ToInt32(txtCellNumber.Text.ToString());
            wallet.Code = txtCode.Text;
            wallet.Amount = Convert.ToInt32(txtAmount.Text.ToString());
            wallet.Pin = Convert.ToInt32(txtPin.Text.ToString());

            bankdb.E_Wallets.Add(wallet);
            bankdb.SaveChanges();
            MessageBox.Show("Money has been transfered to " + txtCode.Text + MessageBoxButtons.OK);

        }

            private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            TransactionSelection transactionSelection = new TransactionSelection(this.client , this.dbContext, this.wallet);
            transactionSelection.Show();
        }

    }
}
