namespace SpotCon.PlaylistImporter
{
    partial class SimpleTextPlaylistImporter
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxInput = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxFormat = new System.Windows.Forms.TextBox();
            this.labelFormat = new System.Windows.Forms.Label();
            this.buttonImport = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxInput
            // 
            this.richTextBoxInput.DetectUrls = false;
            this.richTextBoxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxInput.EnableAutoDragDrop = true;
            this.richTextBoxInput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxInput.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxInput.Name = "richTextBoxInput";
            this.richTextBoxInput.Size = new System.Drawing.Size(420, 390);
            this.richTextBoxInput.TabIndex = 1;
            this.richTextBoxInput.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonImport);
            this.panel1.Controls.Add(this.textBoxFormat);
            this.panel1.Controls.Add(this.labelFormat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 390);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 40);
            this.panel1.TabIndex = 2;
            // 
            // textBoxFormat
            // 
            this.textBoxFormat.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFormat.Location = new System.Drawing.Point(84, 12);
            this.textBoxFormat.Name = "textBoxFormat";
            this.textBoxFormat.Size = new System.Drawing.Size(247, 20);
            this.textBoxFormat.TabIndex = 8;
            this.textBoxFormat.Text = "^(?<ARTIST>[^\\|]+)\\|(?<TRACK>(.+))$";
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(12, 15);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(66, 13);
            this.labelFormat.TabIndex = 7;
            this.labelFormat.Text = "Input format:";
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(337, 10);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 9;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // SimpleTextPlaylistImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 430);
            this.Controls.Add(this.richTextBoxInput);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SimpleTextPlaylistImporter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "SimpleTextPlaylistImporter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.TextBox textBoxFormat;
        private System.Windows.Forms.Label labelFormat;
    }
}