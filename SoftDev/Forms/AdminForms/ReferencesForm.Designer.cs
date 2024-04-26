namespace SoftDev.Forms.AdminForms
{
    partial class ReferencesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.RequestsButton = new System.Windows.Forms.Button();
            this.TechnologiesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(162, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 29);
            this.label1.TabIndex = 11;
            this.label1.Text = "Справочники";
            // 
            // RequestsButton
            // 
            this.RequestsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RequestsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RequestsButton.Location = new System.Drawing.Point(182, 99);
            this.RequestsButton.Name = "RequestsButton";
            this.RequestsButton.Size = new System.Drawing.Size(133, 44);
            this.RequestsButton.TabIndex = 12;
            this.RequestsButton.Text = "Адрес";
            this.RequestsButton.UseVisualStyleBackColor = true;
            this.RequestsButton.Click += new System.EventHandler(this.RequestsButton_Click);
            // 
            // TechnologiesButton
            // 
            this.TechnologiesButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TechnologiesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TechnologiesButton.Location = new System.Drawing.Point(174, 191);
            this.TechnologiesButton.Name = "TechnologiesButton";
            this.TechnologiesButton.Size = new System.Drawing.Size(150, 44);
            this.TechnologiesButton.TabIndex = 13;
            this.TechnologiesButton.Text = "Технологии";
            this.TechnologiesButton.UseVisualStyleBackColor = true;
            this.TechnologiesButton.Click += new System.EventHandler(this.TechnologiesButton_Click);
            // 
            // ReferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 552);
            this.Controls.Add(this.TechnologiesButton);
            this.Controls.Add(this.RequestsButton);
            this.Controls.Add(this.label1);
            this.Name = "ReferencesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочники";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RequestsButton;
        private System.Windows.Forms.Button TechnologiesButton;
    }
}