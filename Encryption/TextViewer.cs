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
	public partial class TextViewer : Form
	{
		Launcher launcher;

		private bool change = false;
		private readonly string FileName;

		public TextViewer(Form _launcher, string fileName, string _text)
		{
			InitializeComponent();

			launcher = (Launcher)_launcher;
			FileName = fileName;
			textBox.Text = _text;
			textBox.SelectionStart = 0;
			change = false;
			Text = FileName;
		}

		private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("上書き保存しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				launcher.saveString(textBox.Text);
				Text = FileName;
				change = false;
			}
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			Text = FileName + "*";
			change = true;
		}

		private void TextViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (change)
			{
				DialogResult result = MessageBox.Show("ファイルの内容が変更されています\n保存しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				switch (result)
				{
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
					case DialogResult.Yes:
						保存SToolStripMenuItem_Click(null, null);
						break;
					case DialogResult.No:
						break;
					default:
						break;
				}

			}
		}
	}
}
