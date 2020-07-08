using System.Collections.Generic;

namespace ShitShooter
{
    partial class SelectLevel
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
            
            this.level1 = new System.Windows.Forms.Button();
            this.level2 = new System.Windows.Forms.Button();
            this.level3 = new System.Windows.Forms.Button();
            this.level4 = new System.Windows.Forms.Button();
            this.level5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // level1
            // 
            this.level1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.level1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.level1.Font = new System.Drawing.Font("Segoe UI Historic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.level1.ForeColor = System.Drawing.Color.Navy;
            this.level1.Location = new System.Drawing.Point(77, 245);
            this.level1.Name = "level1";
            this.level1.Size = new System.Drawing.Size(40, 40);
            this.level1.TabIndex = 0;
            this.level1.Text = "1";
            this.level1.UseVisualStyleBackColor = false;
            // 
            // level2
            // 
            this.level2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.level2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.level2.Font = new System.Drawing.Font("Segoe UI Historic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.level2.ForeColor = System.Drawing.Color.Navy;
            this.level2.Location = new System.Drawing.Point(150, 245);
            this.level2.Name = "level2";
            this.level2.Size = new System.Drawing.Size(40, 40);
            this.level2.TabIndex = 1;
            this.level2.Text = "2";
            this.level2.UseVisualStyleBackColor = false;
            // 
            // level3
            // 
            this.level3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.level3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.level3.Font = new System.Drawing.Font("Segoe UI Historic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.level3.ForeColor = System.Drawing.Color.Navy;
            this.level3.Location = new System.Drawing.Point(218, 245);
            this.level3.Name = "level3";
            this.level3.Size = new System.Drawing.Size(40, 40);
            this.level3.TabIndex = 2;
            this.level3.Text = "3";
            this.level3.UseVisualStyleBackColor = false;
            // 
            // level4
            // 
            this.level4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.level4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.level4.Font = new System.Drawing.Font("Segoe UI Historic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.level4.ForeColor = System.Drawing.Color.Navy;
            this.level4.Location = new System.Drawing.Point(287, 245);
            this.level4.Name = "level4";
            this.level4.Size = new System.Drawing.Size(40, 40);
            this.level4.TabIndex = 3;
            this.level4.Text = "4";
            this.level4.UseVisualStyleBackColor = false;
            // 
            // level5
            // 
            this.level5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.level5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.level5.Font = new System.Drawing.Font("Segoe UI Historic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.level5.ForeColor = System.Drawing.Color.Navy;
            this.level5.Location = new System.Drawing.Point(363, 245);
            this.level5.Name = "level5";
            this.level5.Size = new System.Drawing.Size(40, 40);
            this.level5.TabIndex = 4;
            this.level5.Text = "5";
            this.level5.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Historic", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Bisque;
            this.label1.Location = new System.Drawing.Point(142, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 47);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select Level";
            // 
            // SelectLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ShitShooter.Resources.sky;
            this.ClientSize = new System.Drawing.Size(504, 541);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.level5);
            this.Controls.Add(this.level4);
            this.Controls.Add(this.level3);
            this.Controls.Add(this.level2);
            this.Controls.Add(this.level1);
            this.Name = "SelectLevel";
            this.Text = "SelectLevel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button level1;
        private System.Windows.Forms.Button level2;
        private System.Windows.Forms.Button level3;
        private System.Windows.Forms.Button level4;
        private System.Windows.Forms.Button level5;
        private System.Windows.Forms.Label label1;
    }
}