namespace JWMSU
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && ( components != null ))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.gameLocField = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.wadOutputField = new System.Windows.Forms.TextBox();
			this.gameLocBrowseButton = new System.Windows.Forms.Button();
			this.wadOutputBrowseButton = new System.Windows.Forms.Button();
			this.wmsuButton = new System.Windows.Forms.Button();
			this.wmsuProgressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "LSD Revamped location:";
			// 
			// gameLocField
			// 
			this.gameLocField.Location = new System.Drawing.Point(16, 30);
			this.gameLocField.Name = "gameLocField";
			this.gameLocField.Size = new System.Drawing.Size(267, 20);
			this.gameLocField.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "WAD output:";
			// 
			// wadOutputField
			// 
			this.wadOutputField.Location = new System.Drawing.Point(16, 74);
			this.wadOutputField.Name = "wadOutputField";
			this.wadOutputField.Size = new System.Drawing.Size(267, 20);
			this.wadOutputField.TabIndex = 3;
			// 
			// gameLocBrowseButton
			// 
			this.gameLocBrowseButton.Location = new System.Drawing.Point(289, 28);
			this.gameLocBrowseButton.Name = "gameLocBrowseButton";
			this.gameLocBrowseButton.Size = new System.Drawing.Size(63, 23);
			this.gameLocBrowseButton.TabIndex = 4;
			this.gameLocBrowseButton.Text = "Browse";
			this.gameLocBrowseButton.UseVisualStyleBackColor = true;
			// 
			// wadOutputBrowseButton
			// 
			this.wadOutputBrowseButton.Location = new System.Drawing.Point(289, 72);
			this.wadOutputBrowseButton.Name = "wadOutputBrowseButton";
			this.wadOutputBrowseButton.Size = new System.Drawing.Size(63, 23);
			this.wadOutputBrowseButton.TabIndex = 5;
			this.wadOutputBrowseButton.Text = "Browse";
			this.wadOutputBrowseButton.UseVisualStyleBackColor = true;
			// 
			// wmsuButton
			// 
			this.wmsuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.wmsuButton.Location = new System.Drawing.Point(16, 101);
			this.wmsuButton.Name = "wmsuButton";
			this.wmsuButton.Size = new System.Drawing.Size(336, 55);
			this.wmsuButton.TabIndex = 6;
			this.wmsuButton.Text = "Just WAD my shit up™";
			this.wmsuButton.UseVisualStyleBackColor = true;
			// 
			// wmsuProgressBar
			// 
			this.wmsuProgressBar.Location = new System.Drawing.Point(16, 159);
			this.wmsuProgressBar.Name = "wmsuProgressBar";
			this.wmsuProgressBar.Size = new System.Drawing.Size(336, 14);
			this.wmsuProgressBar.TabIndex = 7;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 185);
			this.Controls.Add(this.wmsuProgressBar);
			this.Controls.Add(this.wmsuButton);
			this.Controls.Add(this.wadOutputBrowseButton);
			this.Controls.Add(this.gameLocBrowseButton);
			this.Controls.Add(this.wadOutputField);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gameLocField);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "JWMSU";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox gameLocField;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox wadOutputField;
		private System.Windows.Forms.Button gameLocBrowseButton;
		private System.Windows.Forms.Button wadOutputBrowseButton;
		private System.Windows.Forms.Button wmsuButton;
		private System.Windows.Forms.ProgressBar wmsuProgressBar;
	}
}

