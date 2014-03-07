namespace SpotCon
{
    partial class SpotConForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpotConForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.labelTime = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelShuffle = new System.Windows.Forms.Panel();
            this.panelSpacer = new System.Windows.Forms.Panel();
            this.panelRepeat = new System.Windows.Forms.Panel();
            this.panelVolume = new System.Windows.Forms.Panel();
            this.panelVolumeKnob = new System.Windows.Forms.Panel();
            this.panelNext = new System.Windows.Forms.Panel();
            this.panelPlayPause = new System.Windows.Forms.Panel();
            this.panelPrevious = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panelForward = new System.Windows.Forms.Panel();
            this.panelBack = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dataGridViewTracks = new System.Windows.Forms.DataGridView();
            this.ColumnEmpty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTrack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnArtist = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPopularity = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnAlbum = new System.Windows.Forms.DataGridViewLinkColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemImportPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFindDuplicates = new System.Windows.Forms.ToolStripMenuItem();
            this.splitterMain = new System.Windows.Forms.Splitter();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelPicture = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panelTrack = new System.Windows.Forms.Panel();
            this.linkLabelArtist = new System.Windows.Forms.LinkLabel();
            this.linkLabelTrack = new System.Windows.Forms.LinkLabel();
            this.panelComputers = new System.Windows.Forms.Panel();
            this.dataGridViewComputers = new System.Windows.Forms.DataGridView();
            this.HeaderConnected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HeaderConnectionStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.HeaderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelVolume.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panelTrack.SuspendLayout();
            this.panelComputers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComputers)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelFooter, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelMain, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(984, 546);
            this.tableLayoutPanelMain.TabIndex = 7;
            // 
            // panelFooter
            // 
            this.panelFooter.BackgroundImage = global::SpotCon.Properties.Resources.FooterBackground;
            this.panelFooter.Controls.Add(this.labelTime);
            this.panelFooter.Controls.Add(this.panel2);
            this.panelFooter.Controls.Add(this.panelShuffle);
            this.panelFooter.Controls.Add(this.panelSpacer);
            this.panelFooter.Controls.Add(this.panelRepeat);
            this.panelFooter.Controls.Add(this.panelVolume);
            this.panelFooter.Controls.Add(this.panelNext);
            this.panelFooter.Controls.Add(this.panelPlayPause);
            this.panelFooter.Controls.Add(this.panelPrevious);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFooter.Location = new System.Drawing.Point(0, 506);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panelFooter.Size = new System.Drawing.Size(984, 40);
            this.panelFooter.TabIndex = 0;
            // 
            // labelTime
            // 
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelTime.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Location = new System.Drawing.Point(837, 0);
            this.labelTime.Margin = new System.Windows.Forms.Padding(0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.labelTime.Size = new System.Drawing.Size(44, 40);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "0:00/0:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(881, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(37, 40);
            this.panel2.TabIndex = 9;
            // 
            // panelShuffle
            // 
            this.panelShuffle.BackColor = System.Drawing.Color.Transparent;
            this.panelShuffle.BackgroundImage = global::SpotCon.Properties.Resources.Shuffle;
            this.panelShuffle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelShuffle.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelShuffle.Location = new System.Drawing.Point(918, 0);
            this.panelShuffle.Margin = new System.Windows.Forms.Padding(0);
            this.panelShuffle.Name = "panelShuffle";
            this.panelShuffle.Size = new System.Drawing.Size(19, 40);
            this.panelShuffle.TabIndex = 7;
            // 
            // panelSpacer
            // 
            this.panelSpacer.BackColor = System.Drawing.Color.Transparent;
            this.panelSpacer.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSpacer.Location = new System.Drawing.Point(937, 0);
            this.panelSpacer.Margin = new System.Windows.Forms.Padding(0);
            this.panelSpacer.Name = "panelSpacer";
            this.panelSpacer.Size = new System.Drawing.Size(11, 40);
            this.panelSpacer.TabIndex = 8;
            // 
            // panelRepeat
            // 
            this.panelRepeat.BackColor = System.Drawing.Color.Transparent;
            this.panelRepeat.BackgroundImage = global::SpotCon.Properties.Resources.Repeat;
            this.panelRepeat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelRepeat.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRepeat.Location = new System.Drawing.Point(948, 0);
            this.panelRepeat.Margin = new System.Windows.Forms.Padding(0);
            this.panelRepeat.Name = "panelRepeat";
            this.panelRepeat.Size = new System.Drawing.Size(16, 40);
            this.panelRepeat.TabIndex = 6;
            // 
            // panelVolume
            // 
            this.panelVolume.BackColor = System.Drawing.Color.Transparent;
            this.panelVolume.BackgroundImage = global::SpotCon.Properties.Resources.VolumeLeft;
            this.panelVolume.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelVolume.Controls.Add(this.panelVolumeKnob);
            this.panelVolume.Location = new System.Drawing.Point(111, 7);
            this.panelVolume.Name = "panelVolume";
            this.panelVolume.Size = new System.Drawing.Size(91, 26);
            this.panelVolume.TabIndex = 4;
            this.panelVolume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelVolume_MouseDown);
            this.panelVolume.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelVolume_MouseUp);
            // 
            // panelVolumeKnob
            // 
            this.panelVolumeKnob.BackgroundImage = global::SpotCon.Properties.Resources.VolumeKnob;
            this.panelVolumeKnob.Location = new System.Drawing.Point(56, 0);
            this.panelVolumeKnob.Name = "panelVolumeKnob";
            this.panelVolumeKnob.Size = new System.Drawing.Size(9, 26);
            this.panelVolumeKnob.TabIndex = 0;
            this.panelVolumeKnob.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelVolumeKnob_MouseDown);
            this.panelVolumeKnob.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelVolumeKnob_MouseMove);
            this.panelVolumeKnob.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelVolumeKnob_MouseUp);
            // 
            // panelNext
            // 
            this.panelNext.BackColor = System.Drawing.Color.Transparent;
            this.panelNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelNext.BackgroundImage")));
            this.panelNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelNext.Enabled = false;
            this.panelNext.Location = new System.Drawing.Point(74, 3);
            this.panelNext.Name = "panelNext";
            this.panelNext.Size = new System.Drawing.Size(25, 35);
            this.panelNext.TabIndex = 1;
            this.panelNext.Click += new System.EventHandler(this.panelNext_Click);
            this.panelNext.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelNext_MouseDown);
            this.panelNext.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelNext_MouseUp);
            // 
            // panelPlayPause
            // 
            this.panelPlayPause.BackColor = System.Drawing.Color.Transparent;
            this.panelPlayPause.BackgroundImage = global::SpotCon.Properties.Resources.Play;
            this.panelPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelPlayPause.Enabled = false;
            this.panelPlayPause.Location = new System.Drawing.Point(38, 3);
            this.panelPlayPause.Name = "panelPlayPause";
            this.panelPlayPause.Size = new System.Drawing.Size(35, 35);
            this.panelPlayPause.TabIndex = 1;
            this.panelPlayPause.Click += new System.EventHandler(this.panelPlayPause_Click);
            this.panelPlayPause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPlayPause_MouseDown);
            this.panelPlayPause.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelPlayPause_MouseUp);
            // 
            // panelPrevious
            // 
            this.panelPrevious.BackColor = System.Drawing.Color.Transparent;
            this.panelPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelPrevious.BackgroundImage")));
            this.panelPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelPrevious.Enabled = false;
            this.panelPrevious.Location = new System.Drawing.Point(10, 3);
            this.panelPrevious.Name = "panelPrevious";
            this.panelPrevious.Size = new System.Drawing.Size(25, 35);
            this.panelPrevious.TabIndex = 0;
            this.panelPrevious.Click += new System.EventHandler(this.panelPrevious_Click);
            this.panelPrevious.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPrevious_MouseDown);
            this.panelPrevious.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelPrevious_MouseUp);
            // 
            // panelHeader
            // 
            this.panelHeader.BackgroundImage = global::SpotCon.Properties.Resources.HeaderBackground;
            this.panelHeader.Controls.Add(this.progressBar);
            this.panelHeader.Controls.Add(this.panelSearch);
            this.panelHeader.Controls.Add(this.panelForward);
            this.panelHeader.Controls.Add(this.panelBack);
            this.panelHeader.Controls.Add(this.labelStatus);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(7, 0, 5, 0);
            this.panelHeader.Size = new System.Drawing.Size(984, 40);
            this.panelHeader.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(303, 8);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 23);
            this.progressBar.TabIndex = 10;
            this.progressBar.Visible = false;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.Transparent;
            this.panelSearch.BackgroundImage = global::SpotCon.Properties.Resources.Search;
            this.panelSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSearch.Location = new System.Drawing.Point(61, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(239, 40);
            this.panelSearch.TabIndex = 13;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BackColor = System.Drawing.Color.White;
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxSearch.Location = new System.Drawing.Point(29, 13);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(193, 14);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.Text = "Search";
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            this.textBoxSearch.Leave += new System.EventHandler(this.textBoxSearch_Leave);
            // 
            // panelForward
            // 
            this.panelForward.BackColor = System.Drawing.Color.Transparent;
            this.panelForward.BackgroundImage = global::SpotCon.Properties.Resources.ForwardDisabled;
            this.panelForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelForward.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelForward.Location = new System.Drawing.Point(34, 0);
            this.panelForward.Name = "panelForward";
            this.panelForward.Size = new System.Drawing.Size(27, 40);
            this.panelForward.TabIndex = 17;
            this.panelForward.Click += new System.EventHandler(this.panelForward_Click);
            this.panelForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelForward_MouseDown);
            this.panelForward.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelForward_MouseMove);
            this.panelForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelForward_MouseUp);
            // 
            // panelBack
            // 
            this.panelBack.BackColor = System.Drawing.Color.Transparent;
            this.panelBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelBack.BackgroundImage")));
            this.panelBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBack.Location = new System.Drawing.Point(7, 0);
            this.panelBack.Name = "panelBack";
            this.panelBack.Size = new System.Drawing.Size(27, 40);
            this.panelBack.TabIndex = 15;
            this.panelBack.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelBack_MouseClick);
            this.panelBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBack_MouseDown);
            this.panelBack.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelBack_MouseMove);
            this.panelBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelBack_MouseUp);
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(579, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(400, 40);
            this.labelStatus.TabIndex = 14;
            this.labelStatus.Text = "Status";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Black;
            this.panelMain.Controls.Add(this.dataGridViewTracks);
            this.panelMain.Controls.Add(this.splitterMain);
            this.panelMain.Controls.Add(this.panelLeft);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 41);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(984, 464);
            this.panelMain.TabIndex = 3;
            // 
            // dataGridViewTracks
            // 
            this.dataGridViewTracks.AllowDrop = true;
            this.dataGridViewTracks.AllowUserToAddRows = false;
            this.dataGridViewTracks.AllowUserToDeleteRows = false;
            this.dataGridViewTracks.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(230)))), ((int)(((byte)(226)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTracks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTracks.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewTracks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTracks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewTracks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTracks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTracks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnEmpty,
            this.ColumnTrack,
            this.ColumnArtist,
            this.ColumnTime,
            this.ColumnPopularity,
            this.ColumnAlbum});
            this.dataGridViewTracks.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(184)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTracks.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTracks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTracks.Location = new System.Drawing.Point(152, 0);
            this.dataGridViewTracks.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewTracks.Name = "dataGridViewTracks";
            this.dataGridViewTracks.ReadOnly = true;
            this.dataGridViewTracks.RowHeadersVisible = false;
            this.dataGridViewTracks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTracks.ShowEditingIcon = false;
            this.dataGridViewTracks.Size = new System.Drawing.Size(832, 464);
            this.dataGridViewTracks.TabIndex = 6;
            this.dataGridViewTracks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTracks_CellContentClick);
            this.dataGridViewTracks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTracks_CellDoubleClick);
            this.dataGridViewTracks.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewTracks_ColumnHeaderMouseClick);
            this.dataGridViewTracks.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTracks_RowLeave);
            this.dataGridViewTracks.SelectionChanged += new System.EventHandler(this.dataGridViewTracks_SelectionChanged);
            this.dataGridViewTracks.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridViewTracks_SortCompare);
            this.dataGridViewTracks.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridViewTracks_DragDrop);
            this.dataGridViewTracks.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridViewTracks_DragEnter);
            // 
            // ColumnEmpty
            // 
            this.ColumnEmpty.HeaderText = "";
            this.ColumnEmpty.MinimumWidth = 20;
            this.ColumnEmpty.Name = "ColumnEmpty";
            this.ColumnEmpty.ReadOnly = true;
            this.ColumnEmpty.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnEmpty.Width = 20;
            // 
            // ColumnTrack
            // 
            this.ColumnTrack.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnTrack.FillWeight = 25F;
            this.ColumnTrack.HeaderText = "Track";
            this.ColumnTrack.MinimumWidth = 50;
            this.ColumnTrack.Name = "ColumnTrack";
            this.ColumnTrack.ReadOnly = true;
            this.ColumnTrack.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ColumnArtist
            // 
            this.ColumnArtist.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            this.ColumnArtist.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnArtist.FillWeight = 25F;
            this.ColumnArtist.HeaderText = "Artist";
            this.ColumnArtist.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.ColumnArtist.LinkColor = System.Drawing.SystemColors.ControlText;
            this.ColumnArtist.MinimumWidth = 50;
            this.ColumnArtist.Name = "ColumnArtist";
            this.ColumnArtist.ReadOnly = true;
            this.ColumnArtist.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnArtist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ColumnArtist.TrackVisitedState = false;
            // 
            // ColumnTime
            // 
            this.ColumnTime.HeaderText = "Time";
            this.ColumnTime.MinimumWidth = 50;
            this.ColumnTime.Name = "ColumnTime";
            this.ColumnTime.ReadOnly = true;
            this.ColumnTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ColumnTime.Width = 60;
            // 
            // ColumnPopularity
            // 
            this.ColumnPopularity.HeaderText = "Popularity";
            this.ColumnPopularity.MinimumWidth = 90;
            this.ColumnPopularity.Name = "ColumnPopularity";
            this.ColumnPopularity.ReadOnly = true;
            this.ColumnPopularity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnPopularity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ColumnPopularity.Width = 90;
            // 
            // ColumnAlbum
            // 
            this.ColumnAlbum.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
            this.ColumnAlbum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnAlbum.FillWeight = 25F;
            this.ColumnAlbum.HeaderText = "Album";
            this.ColumnAlbum.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.ColumnAlbum.LinkColor = System.Drawing.SystemColors.ControlText;
            this.ColumnAlbum.MinimumWidth = 50;
            this.ColumnAlbum.Name = "ColumnAlbum";
            this.ColumnAlbum.ReadOnly = true;
            this.ColumnAlbum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnAlbum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ColumnAlbum.TrackVisitedState = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPlay,
            this.toolStripSeparator1,
            this.toolStripMenuItemImportPlaylist,
            this.toolStripMenuItemFindDuplicates});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(156, 76);
            // 
            // toolStripMenuItemPlay
            // 
            this.toolStripMenuItemPlay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItemPlay.Name = "toolStripMenuItemPlay";
            this.toolStripMenuItemPlay.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItemPlay.Text = "Play";
            this.toolStripMenuItemPlay.Click += new System.EventHandler(this.toolStripMenuItemPlay_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // toolStripMenuItemImportPlaylist
            // 
            this.toolStripMenuItemImportPlaylist.Image = global::SpotCon.Properties.Resources.ImportPlaylist;
            this.toolStripMenuItemImportPlaylist.Name = "toolStripMenuItemImportPlaylist";
            this.toolStripMenuItemImportPlaylist.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItemImportPlaylist.Text = "Import Playlist";
            // 
            // toolStripMenuItemFindDuplicates
            // 
            this.toolStripMenuItemFindDuplicates.Image = global::SpotCon.Properties.Resources.FindDuplicates;
            this.toolStripMenuItemFindDuplicates.Name = "toolStripMenuItemFindDuplicates";
            this.toolStripMenuItemFindDuplicates.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItemFindDuplicates.Text = "Find Duplicates";
            this.toolStripMenuItemFindDuplicates.Click += new System.EventHandler(this.toolStripMenuItemFindDuplicates_Click);
            // 
            // splitterMain
            // 
            this.splitterMain.Location = new System.Drawing.Point(151, 0);
            this.splitterMain.Name = "splitterMain";
            this.splitterMain.Size = new System.Drawing.Size(1, 464);
            this.splitterMain.TabIndex = 14;
            this.splitterMain.TabStop = false;
            this.splitterMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitterMain_SplitterMoved);
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.panelLeft.Controls.Add(this.panelPicture);
            this.panelLeft.Controls.Add(this.panelTrack);
            this.panelLeft.Controls.Add(this.panelComputers);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.MaximumSize = new System.Drawing.Size(640, 0);
            this.panelLeft.MinimumSize = new System.Drawing.Size(150, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(151, 464);
            this.panelLeft.TabIndex = 13;
            // 
            // panelPicture
            // 
            this.panelPicture.BackColor = System.Drawing.Color.Black;
            this.panelPicture.Controls.Add(this.pictureBox);
            this.panelPicture.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPicture.Location = new System.Drawing.Point(0, 262);
            this.panelPicture.Margin = new System.Windows.Forms.Padding(0);
            this.panelPicture.Name = "panelPicture";
            this.panelPicture.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.panelPicture.Size = new System.Drawing.Size(151, 152);
            this.panelPicture.TabIndex = 12;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 1);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(151, 150);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 10;
            this.pictureBox.TabStop = false;
            this.pictureBox.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // panelTrack
            // 
            this.panelTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.panelTrack.BackgroundImage = global::SpotCon.Properties.Resources.TrackBackground;
            this.panelTrack.Controls.Add(this.linkLabelArtist);
            this.panelTrack.Controls.Add(this.linkLabelTrack);
            this.panelTrack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTrack.Location = new System.Drawing.Point(0, 414);
            this.panelTrack.Margin = new System.Windows.Forms.Padding(0);
            this.panelTrack.Name = "panelTrack";
            this.panelTrack.Size = new System.Drawing.Size(151, 50);
            this.panelTrack.TabIndex = 11;
            // 
            // linkLabelArtist
            // 
            this.linkLabelArtist.ActiveLinkColor = System.Drawing.Color.White;
            this.linkLabelArtist.AutoEllipsis = true;
            this.linkLabelArtist.AutoSize = true;
            this.linkLabelArtist.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelArtist.Enabled = false;
            this.linkLabelArtist.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelArtist.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelArtist.LinkColor = System.Drawing.Color.White;
            this.linkLabelArtist.Location = new System.Drawing.Point(7, 27);
            this.linkLabelArtist.MaximumSize = new System.Drawing.Size(150, 16);
            this.linkLabelArtist.Name = "linkLabelArtist";
            this.linkLabelArtist.Size = new System.Drawing.Size(92, 13);
            this.linkLabelArtist.TabIndex = 1;
            this.linkLabelArtist.TabStop = true;
            this.linkLabelArtist.Text = "Artist loading...";
            this.linkLabelArtist.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelArtist_LinkClicked);
            this.linkLabelArtist.MouseHover += new System.EventHandler(this.linkLabelArtist_MouseHover);
            // 
            // linkLabelTrack
            // 
            this.linkLabelTrack.ActiveLinkColor = System.Drawing.Color.White;
            this.linkLabelTrack.AutoEllipsis = true;
            this.linkLabelTrack.AutoSize = true;
            this.linkLabelTrack.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelTrack.Enabled = false;
            this.linkLabelTrack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelTrack.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelTrack.LinkColor = System.Drawing.Color.White;
            this.linkLabelTrack.Location = new System.Drawing.Point(7, 8);
            this.linkLabelTrack.MaximumSize = new System.Drawing.Size(150, 16);
            this.linkLabelTrack.Name = "linkLabelTrack";
            this.linkLabelTrack.Size = new System.Drawing.Size(105, 16);
            this.linkLabelTrack.TabIndex = 0;
            this.linkLabelTrack.TabStop = true;
            this.linkLabelTrack.Text = "Track loading...";
            this.linkLabelTrack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTrack_LinkClicked);
            this.linkLabelTrack.MouseHover += new System.EventHandler(this.linkLabelTrack_MouseHover);
            // 
            // panelComputers
            // 
            this.panelComputers.Controls.Add(this.dataGridViewComputers);
            this.panelComputers.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelComputers.Location = new System.Drawing.Point(0, 0);
            this.panelComputers.MinimumSize = new System.Drawing.Size(0, 80);
            this.panelComputers.Name = "panelComputers";
            this.panelComputers.Size = new System.Drawing.Size(151, 240);
            this.panelComputers.TabIndex = 13;
            // 
            // dataGridViewComputers
            // 
            this.dataGridViewComputers.AllowUserToAddRows = false;
            this.dataGridViewComputers.AllowUserToDeleteRows = false;
            this.dataGridViewComputers.AllowUserToResizeColumns = false;
            this.dataGridViewComputers.AllowUserToResizeRows = false;
            this.dataGridViewComputers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewComputers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.dataGridViewComputers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewComputers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewComputers.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewComputers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComputers.ColumnHeadersVisible = false;
            this.dataGridViewComputers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HeaderConnected,
            this.HeaderConnectionStatus,
            this.HeaderName});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(56)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewComputers.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewComputers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewComputers.GridColor = System.Drawing.Color.White;
            this.dataGridViewComputers.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewComputers.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewComputers.MinimumSize = new System.Drawing.Size(60, 60);
            this.dataGridViewComputers.MultiSelect = false;
            this.dataGridViewComputers.Name = "dataGridViewComputers";
            this.dataGridViewComputers.RowHeadersVisible = false;
            this.dataGridViewComputers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComputers.Size = new System.Drawing.Size(151, 240);
            this.dataGridViewComputers.TabIndex = 9;
            this.dataGridViewComputers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComputers_CellValueChanged);
            this.dataGridViewComputers.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewComputers_CurrentCellDirtyStateChanged);
            this.dataGridViewComputers.SelectionChanged += new System.EventHandler(this.dataGridViewComputers_SelectionChanged);
            // 
            // HeaderConnected
            // 
            this.HeaderConnected.HeaderText = "Connected";
            this.HeaderConnected.Name = "HeaderConnected";
            this.HeaderConnected.Width = 5;
            // 
            // HeaderConnectionStatus
            // 
            this.HeaderConnectionStatus.HeaderText = "Connection Status";
            this.HeaderConnectionStatus.Name = "HeaderConnectionStatus";
            this.HeaderConnectionStatus.ReadOnly = true;
            this.HeaderConnectionStatus.Width = 5;
            // 
            // HeaderName
            // 
            this.HeaderName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HeaderName.HeaderText = "Name";
            this.HeaderName.Name = "HeaderName";
            this.HeaderName.ReadOnly = true;
            // 
            // SpotConForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 546);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 410);
            this.Name = "SpotConForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SpotCon";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SpotConForm_FormClosed);
            this.Load += new System.EventHandler(this.SpotConForm_Load);
            this.Resize += new System.EventHandler(this.SpotConForm_Resize);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelVolume.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTracks)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panelTrack.ResumeLayout(false);
            this.panelTrack.PerformLayout();
            this.panelComputers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComputers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewComputers;
        private System.Windows.Forms.DataGridView dataGridViewTracks;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panelTrack;
        private System.Windows.Forms.LinkLabel linkLabelArtist;
        private System.Windows.Forms.LinkLabel linkLabelTrack;
        private System.Windows.Forms.DataGridViewCheckBoxColumn HeaderConnected;
        private System.Windows.Forms.DataGridViewImageColumn HeaderConnectionStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeaderName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panelNext;
        private System.Windows.Forms.Panel panelPlayPause;
        private System.Windows.Forms.Panel panelPrevious;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTrack;
        private System.Windows.Forms.DataGridViewLinkColumn ColumnArtist;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTime;
        private System.Windows.Forms.DataGridViewImageColumn ColumnPopularity;
        private System.Windows.Forms.DataGridViewLinkColumn ColumnAlbum;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelPicture;
        private System.Windows.Forms.Splitter splitterMain;
        private System.Windows.Forms.Panel panelComputers;
        private System.Windows.Forms.Panel panelVolume;
        private System.Windows.Forms.Panel panelVolumeKnob;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Panel panelRepeat;
        private System.Windows.Forms.Panel panelShuffle;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelSpacer;
        private System.Windows.Forms.Panel panelForward;
        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFindDuplicates;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemImportPlaylist;

    }
}

