﻿namespace SoftDev.Forms
{
    partial class AdminForm
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
            this.RequestsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ClientsButton = new System.Windows.Forms.Button();
            this.ReferenceButton = new System.Windows.Forms.Button();
            this.DevelopersButton = new System.Windows.Forms.Button();
            this.ProjectsButton = new System.Windows.Forms.Button();
            this.TaskButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RequestsButton
            // 
            this.RequestsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RequestsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RequestsButton.Location = new System.Drawing.Point(148, 89);
            this.RequestsButton.Name = "RequestsButton";
            this.RequestsButton.Size = new System.Drawing.Size(143, 44);
            this.RequestsButton.TabIndex = 0;
            this.RequestsButton.Text = "Заявки";
            this.RequestsButton.UseVisualStyleBackColor = true;
            this.RequestsButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(130, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "Админ форма";
            // 
            // ClientsButton
            // 
            this.ClientsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ClientsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClientsButton.Location = new System.Drawing.Point(151, 174);
            this.ClientsButton.Name = "ClientsButton";
            this.ClientsButton.Size = new System.Drawing.Size(140, 44);
            this.ClientsButton.TabIndex = 11;
            this.ClientsButton.Text = "Клиенты";
            this.ClientsButton.UseVisualStyleBackColor = true;
            this.ClientsButton.Click += new System.EventHandler(this.ClientsButton_Click);
            // 
            // ReferenceButton
            // 
            this.ReferenceButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ReferenceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReferenceButton.Location = new System.Drawing.Point(151, 587);
            this.ReferenceButton.Name = "ReferenceButton";
            this.ReferenceButton.Size = new System.Drawing.Size(140, 44);
            this.ReferenceButton.TabIndex = 12;
            this.ReferenceButton.Text = "Справочники";
            this.ReferenceButton.UseVisualStyleBackColor = true;
            this.ReferenceButton.Click += new System.EventHandler(this.ReferenceButton_Click);
            // 
            // DevelopersButton
            // 
            this.DevelopersButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DevelopersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DevelopersButton.Location = new System.Drawing.Point(151, 253);
            this.DevelopersButton.Name = "DevelopersButton";
            this.DevelopersButton.Size = new System.Drawing.Size(143, 44);
            this.DevelopersButton.TabIndex = 13;
            this.DevelopersButton.Text = "Разработчики";
            this.DevelopersButton.UseVisualStyleBackColor = true;
            this.DevelopersButton.Click += new System.EventHandler(this.DevelopersButton_Click);
            // 
            // ProjectsButton
            // 
            this.ProjectsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ProjectsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProjectsButton.Location = new System.Drawing.Point(151, 335);
            this.ProjectsButton.Name = "ProjectsButton";
            this.ProjectsButton.Size = new System.Drawing.Size(143, 44);
            this.ProjectsButton.TabIndex = 14;
            this.ProjectsButton.Text = "Проекты";
            this.ProjectsButton.UseVisualStyleBackColor = true;
            this.ProjectsButton.Click += new System.EventHandler(this.ProjectsButton_Click);
            // 
            // TaskButton
            // 
            this.TaskButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TaskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TaskButton.Location = new System.Drawing.Point(151, 414);
            this.TaskButton.Name = "TaskButton";
            this.TaskButton.Size = new System.Drawing.Size(143, 44);
            this.TaskButton.TabIndex = 15;
            this.TaskButton.Text = "Задачи";
            this.TaskButton.UseVisualStyleBackColor = true;
            this.TaskButton.Click += new System.EventHandler(this.TaskButton_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 643);
            this.Controls.Add(this.TaskButton);
            this.Controls.Add(this.ProjectsButton);
            this.Controls.Add(this.DevelopersButton);
            this.Controls.Add(this.ReferenceButton);
            this.Controls.Add(this.ClientsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RequestsButton);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Админ форма";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RequestsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ClientsButton;
        private System.Windows.Forms.Button ReferenceButton;
        private System.Windows.Forms.Button DevelopersButton;
        private System.Windows.Forms.Button ProjectsButton;
        private System.Windows.Forms.Button TaskButton;
    }
}