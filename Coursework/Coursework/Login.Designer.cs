namespace Coursework
{
    partial class Login
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
            this.lb_welcome = new System.Windows.Forms.Label();
            this.cb_role = new System.Windows.Forms.ComboBox();
            this.lb_choose_role = new System.Windows.Forms.Label();
            this.btn_access = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_welcome
            // 
            this.lb_welcome.AutoSize = true;
            this.lb_welcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_welcome.Location = new System.Drawing.Point(36, 28);
            this.lb_welcome.Name = "lb_welcome";
            this.lb_welcome.Size = new System.Drawing.Size(275, 39);
            this.lb_welcome.TabIndex = 0;
            this.lb_welcome.Text = "Welcome to UoG";
            // 
            // cb_role
            // 
            this.cb_role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_role.FormattingEnabled = true;
            this.cb_role.Items.AddRange(new object[] {
            "Teacher",
            "Student",
            "Admin"});
            this.cb_role.Location = new System.Drawing.Point(43, 129);
            this.cb_role.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_role.Name = "cb_role";
            this.cb_role.Size = new System.Drawing.Size(268, 24);
            this.cb_role.TabIndex = 3;
            // 
            // lb_choose_role
            // 
            this.lb_choose_role.AutoSize = true;
            this.lb_choose_role.Location = new System.Drawing.Point(109, 92);
            this.lb_choose_role.Name = "lb_choose_role";
            this.lb_choose_role.Size = new System.Drawing.Size(109, 16);
            this.lb_choose_role.TabIndex = 4;
            this.lb_choose_role.Text = "Choose your role";
            // 
            // btn_access
            // 
            this.btn_access.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_access.Location = new System.Drawing.Point(43, 184);
            this.btn_access.Margin = new System.Windows.Forms.Padding(4);
            this.btn_access.Name = "btn_access";
            this.btn_access.Size = new System.Drawing.Size(268, 38);
            this.btn_access.TabIndex = 7;
            this.btn_access.Text = "Access";
            this.btn_access.UseVisualStyleBackColor = false;
            this.btn_access.Click += new System.EventHandler(this.btn_access_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 317);
            this.Controls.Add(this.btn_access);
            this.Controls.Add(this.lb_choose_role);
            this.Controls.Add(this.cb_role);
            this.Controls.Add(this.lb_welcome);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_welcome;
        private System.Windows.Forms.ComboBox cb_role;
        private System.Windows.Forms.Label lb_choose_role;
        private System.Windows.Forms.Button btn_access;
    }
}

