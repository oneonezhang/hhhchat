namespace hhhchatClient
{
    partial class Form2
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
            this.lbMyName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buddylist = new System.Windows.Forms.ListBox();
            this.textAll = new System.Windows.Forms.TextBox();
            this.textFile = new System.Windows.Forms.TextBox();
            this.textSend = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.group = new System.Windows.Forms.CheckBox();
            this.notice = new System.Windows.Forms.TextBox();
            this.chatter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbMyName
            // 
            this.lbMyName.AutoSize = true;
            this.lbMyName.BackColor = System.Drawing.Color.Transparent;
            this.lbMyName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMyName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbMyName.Location = new System.Drawing.Point(12, 9);
            this.lbMyName.Name = "lbMyName";
            this.lbMyName.Size = new System.Drawing.Size(92, 22);
            this.lbMyName.TabIndex = 0;
            this.lbMyName.Text = "userName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Other Buddies:";
            // 
            // buddylist
            // 
            this.buddylist.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buddylist.FormattingEnabled = true;
            this.buddylist.ItemHeight = 17;
            this.buddylist.Location = new System.Drawing.Point(14, 52);
            this.buddylist.Name = "buddylist";
            this.buddylist.Size = new System.Drawing.Size(130, 395);
            this.buddylist.TabIndex = 2;
            this.buddylist.SelectedIndexChanged += new System.EventHandler(this.buddylist_SelectedIndexChanged);
            // 
            // textAll
            // 
            this.textAll.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textAll.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textAll.Location = new System.Drawing.Point(159, 52);
            this.textAll.Multiline = true;
            this.textAll.Name = "textAll";
            this.textAll.ReadOnly = true;
            this.textAll.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textAll.Size = new System.Drawing.Size(425, 268);
            this.textAll.TabIndex = 3;
            this.textAll.TextChanged += new System.EventHandler(this.textAll_TextChanged);
            // 
            // textFile
            // 
            this.textFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textFile.Location = new System.Drawing.Point(159, 328);
            this.textFile.Name = "textFile";
            this.textFile.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textFile.Size = new System.Drawing.Size(348, 23);
            this.textFile.TabIndex = 4;
            // 
            // textSend
            // 
            this.textSend.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textSend.Location = new System.Drawing.Point(159, 364);
            this.textSend.Multiline = true;
            this.textSend.Name = "textSend";
            this.textSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textSend.Size = new System.Drawing.Size(427, 81);
            this.textSend.TabIndex = 5;
            this.textSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textSend_KeyDown);
            this.textSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textSend_KeyPress);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Location = new System.Drawing.Point(506, 326);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(33, 32);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnFile
            // 
            this.btnFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFile.Location = new System.Drawing.Point(545, 326);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(41, 32);
            this.btnFile.TabIndex = 7;
            this.btnFile.Text = "File";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSend.Location = new System.Drawing.Point(450, 451);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(136, 25);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // group
            // 
            this.group.AutoSize = true;
            this.group.BackColor = System.Drawing.Color.Transparent;
            this.group.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.group.Location = new System.Drawing.Point(32, 456);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(95, 21);
            this.group.TabIndex = 9;
            this.group.Text = "GroupSend";
            this.group.UseVisualStyleBackColor = false;
            this.group.CheckedChanged += new System.EventHandler(this.group_CheckedChanged);
            // 
            // notice
            // 
            this.notice.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.notice.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.notice.Location = new System.Drawing.Point(159, 1);
            this.notice.Multiline = true;
            this.notice.Name = "notice";
            this.notice.Size = new System.Drawing.Size(425, 21);
            this.notice.TabIndex = 10;
            // 
            // chatter
            // 
            this.chatter.AutoSize = true;
            this.chatter.BackColor = System.Drawing.Color.Transparent;
            this.chatter.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatter.Location = new System.Drawing.Point(154, 28);
            this.chatter.Name = "chatter";
            this.chatter.Size = new System.Drawing.Size(137, 26);
            this.chatter.TabIndex = 11;
            this.chatter.Text = "chatterName";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::hhhchatClient.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(596, 479);
            this.Controls.Add(this.notice);
            this.Controls.Add(this.group);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.textSend);
            this.Controls.Add(this.textFile);
            this.Controls.Add(this.textAll);
            this.Controls.Add(this.buddylist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbMyName);
            this.Controls.Add(this.chatter);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "hhhchat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMyName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox buddylist;
        private System.Windows.Forms.TextBox textAll;
        private System.Windows.Forms.TextBox textFile;
        private System.Windows.Forms.TextBox textSend;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox group;
        private System.Windows.Forms.TextBox notice;
        private System.Windows.Forms.Label chatter;
    }
}