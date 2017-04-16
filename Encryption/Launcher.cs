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

			DialogResult result = saveFileDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				FileEncrypt(fileNameTextBox.Text, saveFileDialog.FileName, passwordTextBox.Text);

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

			string text = FileDecrypt(fileNameTextBox.Text, passwordTextBox.Text);

			TextViewer textViewer = new TextViewer(this, text);

			textViewer.ShowDialog();

		}

		private static readonly int BlockSize = 128;
		private static readonly int KeySize = 256;

		private bool FileEncrypt(string inFilePath, string outFilePath, string password)
		{
			using (FileStream outfs = new FileStream(outFilePath, FileMode.Create, FileAccess.Write))
			{
				using (AesManaged aes = new AesManaged())
				{
					aes.BlockSize = BlockSize;              // BlockSize = 16bytes
					aes.KeySize = KeySize;                // KeySize = 16bytes
					aes.Mode = CipherMode.CBC;        // CBC mode
					aes.Padding = PaddingMode.PKCS7;    // Padding mode is "PKCS7".

					//入力されたパスワードをベースに擬似乱数を新たに生成
					Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, 16);
					byte[] salt = new byte[16]; // Rfc2898DeriveBytesが内部生成したなソルトを取得
					salt = deriveBytes.Salt;
					// 生成した擬似乱数から16バイト切り出したデータをパスワードにする
					byte[] bufferKey = deriveBytes.GetBytes(16);

					aes.Key = bufferKey;
					// IV ( Initilization Vector ) は、AesManagedにつくらせる
					aes.GenerateIV();

					//Encryption interface.
					ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

					using (CryptoStream cse = new CryptoStream(outfs, encryptor, CryptoStreamMode.Write))
					{
						outfs.Write(salt, 0, 16);     // salt をファイル先頭に埋め込む
						outfs.Write(aes.IV, 0, 16); // 次にIVもファイルに埋め込む
						using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Compress)) //圧縮
						{
							using (FileStream fs = new FileStream(inFilePath, FileMode.Open, FileAccess.Read))
							{
								int len;
								byte[] buffer = new byte[4096];

								while ((len = fs.Read(buffer, 0, 4096)) > 0)
								{
									ds.Write(buffer, 0, len);
								}
							}
						}

					}

				}
			}

			return true;
		}

		private string FileDecrypt(string filePath, string password)
		{
			string text = "";

			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				using (AesManaged aes = new AesManaged())
				{
					aes.BlockSize = BlockSize;              // BlockSize = 16bytes
					aes.KeySize = KeySize;                // KeySize = 16bytes
					aes.Mode = CipherMode.CBC;        // CBC mode
					aes.Padding = PaddingMode.PKCS7;    // Padding mode is "PKCS7".

					// salt
					byte[] salt = new byte[16];
					fs.Read(salt, 0, 16);

					// Initilization Vector
					byte[] iv = new byte[16];
					fs.Read(iv, 0, 16);
					aes.IV = iv;

					// ivをsaltにしてパスワードを擬似乱数に変換
					Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt);
					byte[] bufferKey = deriveBytes.GetBytes(16);    // 16バイトのsaltを切り出してパスワードに変換
					aes.Key = bufferKey;

					//Decryption interface.
					ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

					using (CryptoStream cse = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
					{
						using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Decompress))   //解凍
						{
							int len;
							byte[] buffer = new byte[4096];

							while ((len = ds.Read(buffer, 0, 4096)) > 0)
							{
								text += Encoding.GetEncoding("shift_jis").GetString(buffer, 0, len);
							}
						}
					}
				}
			}

			return text;
		}


	}
}
