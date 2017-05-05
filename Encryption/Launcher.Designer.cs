﻿namespace Encryption
{
    partial class Launcher
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
			this.fileOpenButton = new System.Windows.Forms.Button();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.fileName = new System.Windows.Forms.Label();
			this.encryptionButton = new System.Windows.Forms.Button();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.decryptionButton = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// fileOpenButton
			// 
			this.fileOpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.fileOpenButton.Location = new System.Drawing.Point(163, 13);
			this.fileOpenButton.Name = "fileOpenButton";
			this.fileOpenButton.Size = new System.Drawing.Size(75, 23);
			this.fileOpenButton.TabIndex = 0;
			this.fileOpenButton.Text = "開く";
			this.fileOpenButton.UseVisualStyleBackColor = true;
			this.fileOpenButton.Click += new System.EventHandler(this.fileOpenButton_Click);
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fileNameTextBox.Location = new System.Drawing.Point(12, 41);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(226, 19);
			this.fileNameTextBox.TabIndex = 1;
			// 
			// fileName
			// 
			this.fileName.AutoSize = true;
			this.fileName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.fileName.Location = new System.Drawing.Point(12, 13);
			this.fileName.Name = "fileName";
			this.fileName.Size = new System.Drawing.Size(77, 16);
			this.fileName.TabIndex = 2;
			this.fileName.Text = "ファイル名：";
			// 
			// encryptionButton
			// 
			this.encryptionButton.Location = new System.Drawing.Point(12, 107);
			this.encryptionButton.Name = "encryptionButton";
			this.encryptionButton.Size = new System.Drawing.Size(75, 23);
			this.encryptionButton.TabIndex = 3;
			this.encryptionButton.Text = "暗号化";
			this.encryptionButton.UseVisualStyleBackColor = true;
			this.encryptionButton.Click += new System.EventHandler(this.encryptionButton_Click);
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.passwordTextBox.Location = new System.Drawing.Point(12, 82);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(226, 19);
			this.passwordTextBox.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(12, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "パスワード：";
			// 
			// decryptionButton
			// 
			this.decryptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.decryptionButton.Location = new System.Drawing.Point(163, 107);
			this.decryptionButton.Name = "decryptionButton";
			this.decryptionButton.Size = new System.Drawing.Size(75, 23);
			this.decryptionButton.TabIndex = 6;
			this.decryptionButton.Text = "復号化";
			this.decryptionButton.UseVisualStyleBackColor = true;
			this.decryptionButton.Click += new System.EventHandler(this.decryptionButton_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "encpw";
			this.saveFileDialog.Filter = "暗号化ファイル|*.encpw";
			// 
			// Launcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(250, 142);
			this.Controls.Add(this.decryptionButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.encryptionButton);
			this.Controls.Add(this.fileName);
			this.Controls.Add(this.fileNameTextBox);
			this.Controls.Add(this.fileOpenButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Launcher";
			this.Text = "ランチャー";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fileOpenButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label fileName;
        private System.Windows.Forms.Button encryptionButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button decryptionButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

