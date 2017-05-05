using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO.Compression;

namespace Encryption
{
	public partial class Launcher : Form
	{
		public Launcher()
		{
			InitializeComponent();
		}

		private void fileOpenButton_Click(object sender, EventArgs e)
		{

			DialogResult result = openFileDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				fileNameTextBox.Text = openFileDialog.FileName;
			}

		}

		/// <summary>
		/// テキストファイルを暗号化して保存する
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void encryptionButton_Click(object sender, EventArgs e)
		{
			if (!File.Exists(fileNameTextBox.Text))
			{
				MessageBox.Show("入力されたファイルは存在しません", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Form passwordForm = new PasswordInputForm(passwordTextBox.Text);

			if (passwordForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			DialogResult result = saveFileDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				string text = File.ReadAllText(fileNameTextBox.Text, System.Text.Encoding.GetEncoding("shift_jis"));

				byte[] data = StringEncrypt(text, passwordTextBox.Text);
				File.WriteAllBytes(saveFileDialog.FileName, data);

				DialogResult select = MessageBox.Show("ファイルを保存しました\n元ファイルを削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (select == DialogResult.Yes)
				{
					File.Delete(fileNameTextBox.Text);
					MessageBox.Show("元ファイルを削除しました\n" + fileNameTextBox.Text, "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

			}

		}

		/// <summary>
		/// 復号化を行い、ファイルを開く
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void decryptionButton_Click(object sender, EventArgs e)
		{
			if (!File.Exists(fileNameTextBox.Text))
			{
				MessageBox.Show("入力されたファイルは存在しません", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			byte[] data = File.ReadAllBytes(fileNameTextBox.Text);

			try
			{
				string text = BinaryDecrypt(data, passwordTextBox.Text);

				TextViewer textViewer = new TextViewer(this, Path.GetFileName(fileNameTextBox.Text), text);
				textViewer.ShowDialog();
			}
			catch (CryptographicException)
			{
				MessageBox.Show("パスワードが間違っています", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

		}

		public void saveString(string text)
		{
			byte[] data = StringEncrypt(text, passwordTextBox.Text);
			File.WriteAllBytes(fileNameTextBox.Text, data);
		}

		private static readonly int BlockSize = 128;
		private static readonly int KeySize = 256;
		private static readonly int ByteSize = 16;

		private byte[] StringEncrypt(string str, string password)
		{
			byte[] binary;
			using (AesManaged aes = new AesManaged())
			{
				aes.BlockSize = BlockSize;
				aes.KeySize = KeySize;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;

				Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, ByteSize);
				byte[] salt = new byte[ByteSize];
				salt = deriveBytes.Salt;
				byte[] bufferKey = deriveBytes.GetBytes(ByteSize);

				aes.Key = bufferKey;
				aes.GenerateIV();

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
				using (MemoryStream data = new MemoryStream(Encoding.GetEncoding("shift_jis").GetBytes(str)))
				{
					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cse = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
						{
							ms.Write(salt, 0, ByteSize);
							ms.Write(aes.IV, 0, ByteSize);

							using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Compress))
							{
								int len;
								byte[] buffer = new byte[4096];

								while ((len = data.Read(buffer, 0, 4096)) > 0)
								{
									ds.Write(buffer, 0, len);
								}
							}
						}
						binary = ms.ToArray();
					}
				}
			}

			return binary;
		}

		private string BinaryDecrypt(byte[] data, string password)
		{
			string str = "";

			using (MemoryStream ms = new MemoryStream(data))
			{
				using (AesManaged aes = new AesManaged())
				{
					aes.BlockSize = BlockSize;
					aes.KeySize = KeySize;
					aes.Mode = CipherMode.CBC;
					aes.Padding = PaddingMode.PKCS7;

					byte[] salt = new byte[ByteSize];
					ms.Read(salt, 0, ByteSize);

					byte[] iv = new byte[ByteSize];
					ms.Read(iv, 0, ByteSize);
					aes.IV = iv;

					Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt);
					byte[] bufferKey = deriveBytes.GetBytes(ByteSize);
					aes.Key = bufferKey;

					ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

					using (MemoryStream text = new MemoryStream())
					{
						using (CryptoStream cse = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
						{
							using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Decompress))   //解凍
							{
								int len;
								byte[] buffer = new byte[4096];

								while ((len = ds.Read(buffer, 0, 4096)) > 0)
								{
									text.Write(buffer, 0, len);
								}
							}
						}
						str = Encoding.GetEncoding("shift_jis").GetString(text.ToArray());
					}
				}
			}
			return str;
		}

	}
}
