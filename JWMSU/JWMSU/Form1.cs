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
using JWMSU.WAD;

namespace JWMSU
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public string[] textureFiles;

		private void MainForm_Load(object sender, EventArgs e)
		{
			wmsuButton.Enabled = false;
		}

		private void gameLocBrowseButton_Click(object sender, EventArgs e)
		{
			DialogResult result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				gameLocField.Text = folderBrowserDialog.SelectedPath;
				CheckEnableWmsuButton(gameLocField.Text, wadOutputField.Text);
			}
		}

		private void wadOutputBrowseButton_Click(object sender, EventArgs e)
		{
			saveFileDialog.InitialDirectory = gameLocField.Text;
			DialogResult result = saveFileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				wadOutputField.Text = saveFileDialog.FileName;
				CheckEnableWmsuButton(gameLocField.Text, wadOutputField.Text);
			}
		}

		private void wmsuButton_Click(object sender, EventArgs e)
		{
			textureFiles = Directory.GetFiles(gameLocField.Text, "*.png", SearchOption.AllDirectories);
			Application.UseWaitCursor = true;
			wmsuButton.Enabled = false;
			string[] textureFileNames = new string[textureFiles.Length];
			for (int i = 0; i < textureFiles.Length; i++)
			{
				textureFileNames[i] = Path.GetFileNameWithoutExtension(textureFiles[i]);
			}
			bool result = WADLoader.CreateWad(wadOutputField.Text, textureFiles, textureFileNames);
			Application.UseWaitCursor = false;
			if (result)
			{
				MessageBox.Show("WAD file successfully created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			wmsuButton.Enabled = true;
		}

		private void gameLocField_TextChanged(object sender, EventArgs e)
		{
			CheckEnableWmsuButton(gameLocField.Text, wadOutputField.Text);
		}

		private void wadOutputField_TextChanged(object sender, EventArgs e)
		{
			CheckEnableWmsuButton(gameLocField.Text, wadOutputField.Text);
		}

		private void CheckEnableWmsuButton(string gameLocFieldText, string wadOutputFieldText)
		{
			if (string.IsNullOrEmpty(gameLocFieldText) || string.IsNullOrEmpty(wadOutputFieldText))
			{
				wmsuButton.Enabled = false;
				return;
			}
			wmsuButton.Enabled = true;
		}
	}
}
