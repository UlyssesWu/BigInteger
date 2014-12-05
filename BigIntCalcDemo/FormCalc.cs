using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Uly.Numerics;

namespace BigIntCalcDemo
{
    public partial class FormCalc : Form
    {
        public FormCalc()
        {
            InitializeComponent();
        }

        private bool CheckInput()
        {
            try
            {
                BigInteger a = new BigInteger(txtOp1.Text);
                a = new BigInteger(txtOp2.Text);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                BigInteger op1 = new BigInteger(txtOp1.Text);
                BigInteger op2 = new BigInteger(txtOp2.Text);
                txtAns.Text = (op1 + op2).ToString();
            }
            catch (Exception)
            {
                txtAns.Text = "ERROR";
                //throw;
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            try
            {
                BigInteger op1 = new BigInteger(txtOp1.Text);
                BigInteger op2 = new BigInteger(txtOp2.Text);
                txtAns.Text = (op1 - op2).ToString();
            }
            catch (Exception)
            {
                txtAns.Text = "ERROR";
                //throw;
            }
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            try
            {
                BigInteger op1 = new BigInteger(txtOp1.Text);
                BigInteger op2 = new BigInteger(txtOp2.Text);
                txtAns.Text = (op1 * op2).ToString();
            }
            catch (Exception)
            {
                txtAns.Text = "ERROR";
                //throw;
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            try
            {
                BigInteger op1 = new BigInteger(txtOp1.Text);
                BigInteger op2 = new BigInteger(txtOp2.Text);
                txtAns.Text = (op1 / op2).ToString();
            }
            catch (Exception)
            {
                txtAns.Text = "ERROR";
                //throw;
            }
        }

        private void btnTab_Click(object sender, EventArgs e)
        {
            if (!txtAns.Text.Contains("ERROR"))
            {
                txtOp1.Text = txtAns.Text;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtAns.Text);
        }

    }
}
