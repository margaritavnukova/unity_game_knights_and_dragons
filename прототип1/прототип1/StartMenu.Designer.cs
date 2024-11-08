namespace прототип1
{
    partial class StartMenu
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
            this.StartButton = new System.Windows.Forms.Button();
            this.SeeRecordButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StartButton.Location = new System.Drawing.Point(146, 76);
            this.StartButton.Margin = new System.Windows.Forms.Padding(17, 15, 17, 15);
            this.StartButton.Name = "StartButton";
            this.StartButton.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.StartButton.Size = new System.Drawing.Size(309, 153);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Начать игру \"Драконы и рыцари\"";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // SeeRecordButton
            // 
            this.SeeRecordButton.ForeColor = System.Drawing.Color.Teal;
            this.SeeRecordButton.Location = new System.Drawing.Point(146, 237);
            this.SeeRecordButton.Name = "SeeRecordButton";
            this.SeeRecordButton.Size = new System.Drawing.Size(309, 81);
            this.SeeRecordButton.TabIndex = 1;
            this.SeeRecordButton.Text = "Посмотреть рекорды";
            this.SeeRecordButton.UseVisualStyleBackColor = true;
            this.SeeRecordButton.Click += new System.EventHandler(this.SeeRecordButton_Click);
            // 
            // StartMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 408);
            this.Controls.Add(this.SeeRecordButton);
            this.Controls.Add(this.StartButton);
            this.Font = new System.Drawing.Font("Bookman Old Style", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.Name = "StartMenu";
            this.Text = "StartMenu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartMenu_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button SeeRecordButton;
    }
}