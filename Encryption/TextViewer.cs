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
		Form launcher;

		public TextViewer(Form _launcher, string _text)
		{
			InitializeComponent();

			launcher = _launcher;
			textBox.Text = _text;
		}

		private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
	}
}
