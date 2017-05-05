using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption
{
	public partial class PasswordInputForm : Form
	{
		string password;
		bool isInput = false;

		public PasswordInputForm(string pass)
		{
			InitializeComponent();

			password = pass;

		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (password == passwordTextBox.Text)
			{
				isInput = true;
				Close();
				return;
			}
			else
			{
				MessageBox.Show("パスワードが違います", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

		}

		private void PasswordInputForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (isInput)
				DialogResult = DialogResult.OK;
			else
				DialogResult = DialogResult.Cancel;
		}
	}
}
