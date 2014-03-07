namespace SpotCon
{
    partial class UnknownArtist
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLookup = new System.Windows.Forms.Button();
            this.labelNumSuggestions = new System.Windows.Forms.Label();
            this.numericUpDownNumSuggestions = new System.Windows.Forms.NumericUpDown();
            this.textBoxManual = new System.Windows.Forms.TextBox();
            this.listBoxSuggestions = new System.Windows.Forms.ListBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonSkip = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumSuggestions)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonLookup);
            this.groupBox1.Controls.Add(this.labelNumSuggestions);
            this.groupBox1.Controls.Add(this.numericUpDownNumSuggestions);
            this.groupBox1.Controls.Add(this.textBoxManual);
            this.groupBox1.Controls.Add(this.listBoxSuggestions);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 278);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose best match";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Artist name or URI:";
            // 
            // buttonLookup
            // 
            this.buttonLookup.Enabled = false;
            this.buttonLookup.Location = new System.Drawing.Point(407, 243);
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
            this.textBoxManual.Location = new System.Drawing.Point(6, 245);
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
            this.listBoxSuggestions.Size = new System.Drawing.Size(476, 186);
            this.listBoxSuggestions.TabIndex = 9;
            this.listBoxSuggestions.SelectedIndexChanged += new System.EventHandler(this.ListBoxSuggestions_SelectedIndexChanged);
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.ForeColor = System.Drawing.Color.Red;
            this.labelDescription.Location = new System.Drawing.Point(12, 9);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(208, 16);
            this.labelDescription.TabIndex = 0;
            this.labelDescription.Text = "No results for artists named <{0}>";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.Location = new System.Drawing.Point(341, 328);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 11;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // buttonSkip
            // 
            this.buttonSkip.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.buttonSkip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSkip.Location = new System.Drawing.Point(15, 328);
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.Size = new System.Drawing.Size(75, 23);
            this.buttonSkip.TabIndex = 12;
            this.buttonSkip.Text = "Skip";
            this.buttonSkip.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(422, 327);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // UnknownArtist
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonSkip;
            this.ClientSize = new System.Drawing.Size(515, 363);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSkip);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelDescription);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnknownArtist";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unknown Artist";
            this.Load += new System.EventHandler(this.UnknownArtist_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumSuggestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLookup;
        private System.Windows.Forms.Label labelNumSuggestions;
        private System.Windows.Forms.NumericUpDown numericUpDownNumSuggestions;
        private System.Windows.Forms.TextBox textBoxManual;
        private System.Windows.Forms.ListBox listBoxSuggestions;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonSkip;
        private System.Windows.Forms.Button buttonCancel;



    }
}