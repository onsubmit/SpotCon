namespace SpotCon
{
    partial class UnknownTrack
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
            if (disposing && (components != null))
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
            this.labelDescriptionTrack = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelArtist = new System.Windows.Forms.Label();
            this.buttonLookup = new System.Windows.Forms.Button();
            this.labelNumSuggestions = new System.Windows.Forms.Label();
            this.numericUpDownNumSuggestions = new System.Windows.Forms.NumericUpDown();
            this.textBoxManual = new System.Windows.Forms.TextBox();
            this.listBoxSuggestions = new System.Windows.Forms.ListBox();
            this.buttonSkip = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelDescriptionArtist = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumSuggestions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDescriptionTrack
            // 
            this.labelDescriptionTrack.AutoSize = true;
            this.labelDescriptionTrack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescriptionTrack.ForeColor = System.Drawing.Color.Red;
            this.labelDescriptionTrack.Location = new System.Drawing.Point(12, 9);
            this.labelDescriptionTrack.Name = "labelDescriptionTrack";
            this.labelDescriptionTrack.Size = new System.Drawing.Size(150, 16);
            this.labelDescriptionTrack.TabIndex = 1;
            this.labelDescriptionTrack.Text = "\"No tracks named <{0}>";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelArtist);
            this.groupBox1.Controls.Add(this.buttonLookup);
            this.groupBox1.Controls.Add(this.labelNumSuggestions);
            this.groupBox1.Controls.Add(this.numericUpDownNumSuggestions);
            this.groupBox1.Controls.Add(this.textBoxManual);
            this.groupBox1.Controls.Add(this.listBoxSuggestions);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 258);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose best match";
            // 
            // labelArtist
            // 
            this.labelArtist.AutoSize = true;
            this.labelArtist.Location = new System.Drawing.Point(6, 210);
            this.labelArtist.Name = "labelArtist";
            this.labelArtist.Size = new System.Drawing.Size(100, 13);
            this.labelArtist.TabIndex = 14;
            this.labelArtist.Text = "Track name or URI:";
            // 
            // buttonLookup
            // 
            this.buttonLookup.Enabled = false;
            this.buttonLookup.Location = new System.Drawing.Point(407, 224);
            this.buttonLookup.Name = "buttonLookup";
            this.buttonLookup.Size = new System.Drawing.Size(75, 23);
            this.buttonLookup.TabIndex = 13;
            this.buttonLookup.Text = "Lookup";
            this.buttonLookup.UseVisualStyleBackColor = true;
            this.buttonLookup.Click += new System.EventHandler(this.ButtonLookup_Click);
            // 
            // labelNumSuggestions
            // 
            this.labelNumSuggestions.AutoSize = true;
            this.labelNumSuggestions.Location = new System.Drawing.Point(382, 13);
            this.labelNumSuggestions.Name = "labelNumSuggestions";
            this.labelNumSuggestions.Size = new System.Drawing.Size(54, 13);
            this.labelNumSuggestions.TabIndex = 12;
            this.labelNumSuggestions.Text = "# results:";
            // 
            // numericUpDownNumSuggestions
            // 
            this.numericUpDownNumSuggestions.Location = new System.Drawing.Point(438, 11);
            this.numericUpDownNumSuggestions.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownNumSuggestions.Name = "numericUpDownNumSuggestions";
            this.numericUpDownNumSuggestions.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownNumSuggestions.TabIndex = 11;
            this.numericUpDownNumSuggestions.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // textBoxManual
            // 
            this.textBoxManual.Location = new System.Drawing.Point(6, 226);
            this.textBoxManual.Name = "textBoxManual";
            this.textBoxManual.Size = new System.Drawing.Size(395, 21);
            this.textBoxManual.TabIndex = 10;
            this.textBoxManual.TextChanged += new System.EventHandler(this.TextBoxManual_TextChanged);
            // 
            // listBoxSuggestions
            // 
            this.listBoxSuggestions.FormattingEnabled = true;
            this.listBoxSuggestions.Location = new System.Drawing.Point(6, 34);
            this.listBoxSuggestions.Name = "listBoxSuggestions";
            this.listBoxSuggestions.Size = new System.Drawing.Size(476, 173);
            this.listBoxSuggestions.TabIndex = 9;
            this.listBoxSuggestions.SelectedIndexChanged += new System.EventHandler(this.ListBoxSuggestions_SelectedIndexChanged);
            // 
            // buttonSkip
            // 
            this.buttonSkip.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.buttonSkip.Location = new System.Drawing.Point(21, 327);
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.Size = new System.Drawing.Size(75, 23);
            this.buttonSkip.TabIndex = 14;
            this.buttonSkip.Text = "Skip";
            this.buttonSkip.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(341, 328);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 13;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // labelDescriptionArtist
            // 
            this.labelDescriptionArtist.AutoSize = true;
            this.labelDescriptionArtist.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescriptionArtist.ForeColor = System.Drawing.Color.Red;
            this.labelDescriptionArtist.Location = new System.Drawing.Point(12, 34);
            this.labelDescriptionArtist.Name = "labelDescriptionArtist";
            this.labelDescriptionArtist.Size = new System.Drawing.Size(154, 16);
            this.labelDescriptionArtist.TabIndex = 15;
            this.labelDescriptionArtist.Text = "\"For artist named <{0}>\"";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(422, 327);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // UnknownTrack
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonSkip;
            this.ClientSize = new System.Drawing.Size(515, 363);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelDescriptionArtist);
            this.Controls.Add(this.buttonSkip);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelDescriptionTrack);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnknownTrack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unknown Track";
            this.Load += new System.EventHandler(this.UnknownTrack_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumSuggestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDescriptionTrack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelArtist;
        private System.Windows.Forms.Button buttonLookup;
        private System.Windows.Forms.Label labelNumSuggestions;
        private System.Windows.Forms.NumericUpDown numericUpDownNumSuggestions;
        private System.Windows.Forms.TextBox textBoxManual;
        private System.Windows.Forms.ListBox listBoxSuggestions;
        private System.Windows.Forms.Button buttonSkip;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelDescriptionArtist;
        private System.Windows.Forms.Button buttonCancel;
    }
}