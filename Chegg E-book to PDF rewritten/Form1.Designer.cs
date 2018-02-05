namespace Chegg_E_book_to_PDF
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mainTabPage = new System.Windows.Forms.TabPage();
            this.loadingIcon = new System.Windows.Forms.PictureBox();
            this.booksListView = new System.Windows.Forms.ListView();
            this.browserTabPage = new System.Windows.Forms.TabPage();
            this.logTabPage = new System.Windows.Forms.TabPage();
            this.logsTextbox = new System.Windows.Forms.TextBox();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.aliveCheckTextbox = new System.Windows.Forms.TextBox();
            this.exitSettingsButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.qualityTextbox = new System.Windows.Forms.TextBox();
            this.revertSettingsButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.delay1Textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.delay0Textbox = new System.Windows.Forms.TextBox();
            this.applySettingsButton = new System.Windows.Forms.Button();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.debugBrowser = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.emailTextbox = new System.Windows.Forms.TextBox();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.fetchBooksButton = new System.Windows.Forms.Button();
            this.bookDetailsLabel = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.settingsButton = new System.Windows.Forms.Button();
            this.coverPreview = new System.Windows.Forms.PictureBox();
            this.hangChecker = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.mainTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIcon)).BeginInit();
            this.logTabPage.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coverPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mainTabPage);
            this.tabControl1.Controls.Add(this.browserTabPage);
            this.tabControl1.Controls.Add(this.logTabPage);
            this.tabControl1.Controls.Add(this.settingsTabPage);
            this.tabControl1.Location = new System.Drawing.Point(170, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(504, 436);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // mainTabPage
            // 
            this.mainTabPage.Controls.Add(this.loadingIcon);
            this.mainTabPage.Controls.Add(this.booksListView);
            this.mainTabPage.Location = new System.Drawing.Point(4, 22);
            this.mainTabPage.Name = "mainTabPage";
            this.mainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mainTabPage.Size = new System.Drawing.Size(496, 410);
            this.mainTabPage.TabIndex = 1;
            this.mainTabPage.Text = "Main";
            this.mainTabPage.UseVisualStyleBackColor = true;
            // 
            // loadingIcon
            // 
            this.loadingIcon.BackColor = System.Drawing.Color.White;
            this.loadingIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loadingIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingIcon.Image = global::Chegg_E_book_to_PDF.Properties.Resources.Kuriyama_Mirai_reading;
            this.loadingIcon.Location = new System.Drawing.Point(3, 3);
            this.loadingIcon.Name = "loadingIcon";
            this.loadingIcon.Size = new System.Drawing.Size(490, 404);
            this.loadingIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingIcon.TabIndex = 1;
            this.loadingIcon.TabStop = false;
            // 
            // booksListView
            // 
            this.booksListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.booksListView.BackColor = System.Drawing.Color.White;
            this.booksListView.Location = new System.Drawing.Point(6, 6);
            this.booksListView.MultiSelect = false;
            this.booksListView.Name = "booksListView";
            this.booksListView.Size = new System.Drawing.Size(484, 396);
            this.booksListView.TabIndex = 0;
            this.booksListView.TabStop = false;
            this.booksListView.UseCompatibleStateImageBehavior = false;
            this.booksListView.SelectedIndexChanged += new System.EventHandler(this.bookListView_SelectedIndexChanged);
            // 
            // browserTabPage
            // 
            this.browserTabPage.Location = new System.Drawing.Point(4, 22);
            this.browserTabPage.Name = "browserTabPage";
            this.browserTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.browserTabPage.Size = new System.Drawing.Size(496, 410);
            this.browserTabPage.TabIndex = 0;
            this.browserTabPage.Text = "Browser";
            this.browserTabPage.UseVisualStyleBackColor = true;
            // 
            // logTabPage
            // 
            this.logTabPage.Controls.Add(this.logsTextbox);
            this.logTabPage.Location = new System.Drawing.Point(4, 22);
            this.logTabPage.Name = "logTabPage";
            this.logTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.logTabPage.Size = new System.Drawing.Size(496, 410);
            this.logTabPage.TabIndex = 2;
            this.logTabPage.Text = "Log";
            this.logTabPage.UseVisualStyleBackColor = true;
            // 
            // logsTextbox
            // 
            this.logsTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logsTextbox.Location = new System.Drawing.Point(6, 3);
            this.logsTextbox.Multiline = true;
            this.logsTextbox.Name = "logsTextbox";
            this.logsTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logsTextbox.Size = new System.Drawing.Size(484, 403);
            this.logsTextbox.TabIndex = 0;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.label6);
            this.settingsTabPage.Controls.Add(this.aliveCheckTextbox);
            this.settingsTabPage.Controls.Add(this.exitSettingsButton);
            this.settingsTabPage.Controls.Add(this.label5);
            this.settingsTabPage.Controls.Add(this.qualityTextbox);
            this.settingsTabPage.Controls.Add(this.revertSettingsButton);
            this.settingsTabPage.Controls.Add(this.label4);
            this.settingsTabPage.Controls.Add(this.delay1Textbox);
            this.settingsTabPage.Controls.Add(this.label3);
            this.settingsTabPage.Controls.Add(this.delay0Textbox);
            this.settingsTabPage.Controls.Add(this.applySettingsButton);
            this.settingsTabPage.Controls.Add(this.debugCheckBox);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Size = new System.Drawing.Size(496, 410);
            this.settingsTabPage.TabIndex = 3;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Delay: Alive Check (ms)";
            // 
            // aliveCheckTextbox
            // 
            this.aliveCheckTextbox.Location = new System.Drawing.Point(12, 100);
            this.aliveCheckTextbox.Name = "aliveCheckTextbox";
            this.aliveCheckTextbox.Size = new System.Drawing.Size(100, 20);
            this.aliveCheckTextbox.TabIndex = 11;
            this.aliveCheckTextbox.Text = "30000";
            // 
            // exitSettingsButton
            // 
            this.exitSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitSettingsButton.Location = new System.Drawing.Point(374, 379);
            this.exitSettingsButton.Name = "exitSettingsButton";
            this.exitSettingsButton.Size = new System.Drawing.Size(113, 23);
            this.exitSettingsButton.TabIndex = 10;
            this.exitSettingsButton.Text = "Exit Settings";
            this.exitSettingsButton.UseVisualStyleBackColor = true;
            this.exitSettingsButton.Click += new System.EventHandler(this.exitSettingsButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Page Quality Multiplier\r\n";
            // 
            // qualityTextbox
            // 
            this.qualityTextbox.Location = new System.Drawing.Point(12, 139);
            this.qualityTextbox.Name = "qualityTextbox";
            this.qualityTextbox.Size = new System.Drawing.Size(100, 20);
            this.qualityTextbox.TabIndex = 8;
            this.qualityTextbox.Text = "2.0";
            // 
            // revertSettingsButton
            // 
            this.revertSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.revertSettingsButton.Location = new System.Drawing.Point(255, 379);
            this.revertSettingsButton.Name = "revertSettingsButton";
            this.revertSettingsButton.Size = new System.Drawing.Size(113, 23);
            this.revertSettingsButton.TabIndex = 7;
            this.revertSettingsButton.Text = "Revert To Default";
            this.revertSettingsButton.UseVisualStyleBackColor = true;
            this.revertSettingsButton.Click += new System.EventHandler(this.revertSettingsButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Delay: Loading (ms)";
            // 
            // delay1Textbox
            // 
            this.delay1Textbox.Location = new System.Drawing.Point(12, 59);
            this.delay1Textbox.Name = "delay1Textbox";
            this.delay1Textbox.Size = new System.Drawing.Size(100, 20);
            this.delay1Textbox.TabIndex = 5;
            this.delay1Textbox.Text = "3000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Delay: Snapshot (ms)";
            // 
            // delay0Textbox
            // 
            this.delay0Textbox.Location = new System.Drawing.Point(12, 20);
            this.delay0Textbox.Name = "delay0Textbox";
            this.delay0Textbox.Size = new System.Drawing.Size(100, 20);
            this.delay0Textbox.TabIndex = 2;
            this.delay0Textbox.Text = "250";
            // 
            // applySettingsButton
            // 
            this.applySettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applySettingsButton.Location = new System.Drawing.Point(136, 379);
            this.applySettingsButton.Name = "applySettingsButton";
            this.applySettingsButton.Size = new System.Drawing.Size(113, 23);
            this.applySettingsButton.TabIndex = 1;
            this.applySettingsButton.Text = "Apply";
            this.applySettingsButton.UseVisualStyleBackColor = true;
            this.applySettingsButton.Click += new System.EventHandler(this.applySettingsButton_Click);
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Location = new System.Drawing.Point(12, 210);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(213, 17);
            this.debugCheckBox.TabIndex = 0;
            this.debugCheckBox.Text = "Debug Mode (Only enable if advised to)";
            this.debugCheckBox.UseVisualStyleBackColor = true;
            // 
            // debugBrowser
            // 
            this.debugBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugBrowser.Location = new System.Drawing.Point(180, 27);
            this.debugBrowser.Name = "debugBrowser";
            this.debugBrowser.Size = new System.Drawing.Size(484, 397);
            this.debugBrowser.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // loginButton
            // 
            this.loginButton.Enabled = false;
            this.loginButton.Location = new System.Drawing.Point(11, 100);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(150, 23);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // emailTextbox
            // 
            this.emailTextbox.Enabled = false;
            this.emailTextbox.Location = new System.Drawing.Point(11, 28);
            this.emailTextbox.Name = "emailTextbox";
            this.emailTextbox.Size = new System.Drawing.Size(150, 20);
            this.emailTextbox.TabIndex = 0;
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Enabled = false;
            this.passwordTextbox.Location = new System.Drawing.Point(11, 74);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(150, 20);
            this.passwordTextbox.TabIndex = 1;
            this.passwordTextbox.UseSystemPasswordChar = true;
            // 
            // fetchBooksButton
            // 
            this.fetchBooksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fetchBooksButton.Enabled = false;
            this.fetchBooksButton.Location = new System.Drawing.Point(11, 185);
            this.fetchBooksButton.Name = "fetchBooksButton";
            this.fetchBooksButton.Size = new System.Drawing.Size(150, 23);
            this.fetchBooksButton.TabIndex = 4;
            this.fetchBooksButton.Text = "Fetch Books";
            this.fetchBooksButton.UseVisualStyleBackColor = true;
            this.fetchBooksButton.Click += new System.EventHandler(this.fetchBooksButton_Click);
            // 
            // bookDetailsLabel
            // 
            this.bookDetailsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bookDetailsLabel.AutoSize = true;
            this.bookDetailsLabel.BackColor = System.Drawing.Color.White;
            this.bookDetailsLabel.ForeColor = System.Drawing.Color.Black;
            this.bookDetailsLabel.Location = new System.Drawing.Point(12, 213);
            this.bookDetailsLabel.MaximumSize = new System.Drawing.Size(150, 0);
            this.bookDetailsLabel.Name = "bookDetailsLabel";
            this.bookDetailsLabel.Size = new System.Drawing.Size(0, 13);
            this.bookDetailsLabel.TabIndex = 1;
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.downloadButton.Enabled = false;
            this.downloadButton.Location = new System.Drawing.Point(11, 400);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(150, 23);
            this.downloadButton.TabIndex = 5;
            this.downloadButton.Text = "Download Selected Book";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(13, 379);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(146, 14);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // progressLabel
            // 
            this.progressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressLabel.AutoSize = true;
            this.progressLabel.BackColor = System.Drawing.Color.White;
            this.progressLabel.ForeColor = System.Drawing.Color.Black;
            this.progressLabel.Location = new System.Drawing.Point(12, 352);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(136, 26);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "Progress: Calculating...\r\nTime Remain: Calculating...";
            this.progressLabel.Visible = false;
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(11, 129);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(150, 23);
            this.settingsButton.TabIndex = 3;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // coverPreview
            // 
            this.coverPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.coverPreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.coverPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coverPreview.Location = new System.Drawing.Point(11, 212);
            this.coverPreview.Name = "coverPreview";
            this.coverPreview.Size = new System.Drawing.Size(150, 183);
            this.coverPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coverPreview.TabIndex = 1;
            this.coverPreview.TabStop = false;
            // 
            // hangChecker
            // 
            this.hangChecker.Enabled = true;
            this.hangChecker.Interval = 30000;
            this.hangChecker.Tick += new System.EventHandler(this.hangChecker_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 435);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.bookDetailsLabel);
            this.Controls.Add(this.coverPreview);
            this.Controls.Add(this.fetchBooksButton);
            this.Controls.Add(this.passwordTextbox);
            this.Controls.Add(this.emailTextbox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.debugBrowser);
            this.Name = "Form1";
            this.Text = "Chegg E-book to PDF (Version 2.0.1) by Jimmy Quach";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.mainTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingIcon)).EndInit();
            this.logTabPage.ResumeLayout(false);
            this.logTabPage.PerformLayout();
            this.settingsTabPage.ResumeLayout(false);
            this.settingsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coverPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage browserTabPage;
        private System.Windows.Forms.TabPage mainTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.TextBox emailTextbox;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Panel debugBrowser;
        private System.Windows.Forms.ListView booksListView;
        private System.Windows.Forms.TabPage logTabPage;
        private System.Windows.Forms.TextBox logsTextbox;
        private System.Windows.Forms.Button fetchBooksButton;
        private System.Windows.Forms.Label bookDetailsLabel;
        private System.Windows.Forms.PictureBox coverPreview;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox qualityTextbox;
        private System.Windows.Forms.Button revertSettingsButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox delay1Textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox delay0Textbox;
        private System.Windows.Forms.Button applySettingsButton;
        private System.Windows.Forms.CheckBox debugCheckBox;
        private System.Windows.Forms.Button exitSettingsButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.PictureBox loadingIcon;
        private System.Windows.Forms.Timer hangChecker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox aliveCheckTextbox;
    }
}

