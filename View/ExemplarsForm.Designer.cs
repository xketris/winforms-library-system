namespace LibraryApp.View
{
    partial class ExemplarsForm
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
            this.dgvExemplars = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemplars)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvExemplars
            // 
            this.dgvExemplars.AllowUserToAddRows = false;
            this.dgvExemplars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExemplars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExemplars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExemplars.Location = new System.Drawing.Point(0, 0);
            this.dgvExemplars.Name = "dgvExemplars";
            this.dgvExemplars.ReadOnly = true;
            this.dgvExemplars.RowHeadersWidth = 51;
            this.dgvExemplars.RowTemplate.Height = 29;
            this.dgvExemplars.Size = new System.Drawing.Size(700, 400);
            this.dgvExemplars.TabIndex = 0;
            // 
            // ExemplarsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.dgvExemplars);
            this.Name = "ExemplarsForm";
            this.Text = "Exemplars";
            ((System.ComponentModel.ISupportInitialize)(this.dgvExemplars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExemplars;
    }
}