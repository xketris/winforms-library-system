namespace LibraryApp.View
{
    partial class MainForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            txtTitle = new TextBox();
            txtGenre = new TextBox();
            txtISBN = new TextBox();
            txtAuthor = new TextBox();
            dgvBooks = new DataGridView();
            txtReleaseYear = new TextBox();
            cmbSort = new ComboBox();
            button2 = new Button();
            button1 = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();

            // Inicjalizacja kontrolek
            adminpanelksiazki1 = new AdminBooksControl();
            adminpanelogloszenia1 = new AdminAnnouncementsControl();
            adminpaneluzytkownicy1 = new AdminUsersControl();
            logowanie1 = new LoginControl();
            paneluzytkownika1 = new UserDashboardControl();
            rejestracja1 = new RegisterControl();

            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 10;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.6916523F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.2657585F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 117F));
            tableLayoutPanel1.Controls.Add(txtTitle, 0, 0);
            tableLayoutPanel1.Controls.Add(txtGenre, 1, 0);
            tableLayoutPanel1.Controls.Add(txtISBN, 2, 0);
            tableLayoutPanel1.Controls.Add(txtAuthor, 3, 0);
            tableLayoutPanel1.Controls.Add(dgvBooks, 0, 1);
            tableLayoutPanel1.Controls.Add(txtReleaseYear, 4, 0);
            tableLayoutPanel1.Controls.Add(cmbSort, 5, 0);
            tableLayoutPanel1.Controls.Add(button1, 8, 0);
            tableLayoutPanel1.Controls.Add(comboBox1, 6, 0);
            tableLayoutPanel1.Controls.Add(comboBox2, 7, 0);
            tableLayoutPanel1.Controls.Add(button2, 9, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1339, 540);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // txtTitle
            // 
            txtTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtTitle.Location = new Point(3, 5);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Title";
            txtTitle.Size = new Size(187, 27);
            txtTitle.TabIndex = 0;
            // 
            // txtGenre
            // 
            txtGenre.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtGenre.Location = new Point(204, 5);
            txtGenre.Name = "txtGenre";
            txtGenre.PlaceholderText = "Genre";
            txtGenre.Size = new Size(110, 27);
            txtGenre.TabIndex = 1;
            // 
            // txtISBN
            // 
            txtISBN.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtISBN.Location = new Point(324, 5);
            txtISBN.Name = "txtISBN";
            txtISBN.PlaceholderText = "ISBN";
            txtISBN.Size = new Size(110, 27);
            txtISBN.TabIndex = 2;
            // 
            // txtAuthor
            // 
            txtAuthor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtAuthor.Location = new Point(444, 5);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.PlaceholderText = "Author";
            txtAuthor.Size = new Size(148, 27);
            txtAuthor.TabIndex = 3;
            // 
            // dgvBooks
            // 
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.ColumnHeadersHeight = 29;
            tableLayoutPanel1.SetColumnSpan(dgvBooks, 10);
            dgvBooks.Dock = DockStyle.Fill;
            dgvBooks.Location = new Point(3, 38);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.ReadOnly = true;
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.Size = new Size(1333, 499);
            dgvBooks.TabIndex = 6;
            // 
            // txtReleaseYear
            // 
            txtReleaseYear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtReleaseYear.Location = new Point(605, 5);
            txtReleaseYear.Name = "txtReleaseYear";
            txtReleaseYear.PlaceholderText = "Year";
            txtReleaseYear.Size = new Size(116, 27);
            txtReleaseYear.TabIndex = 4;
            // 
            // cmbSort
            // 
            cmbSort.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbSort.Location = new Point(755, 4);
            cmbSort.Name = "cmbSort";
            cmbSort.Size = new Size(116, 28);
            cmbSort.TabIndex = 5;
            // 
            // button2
            // 
            button2.Location = new Point(1221, 3);
            button2.Name = "button2";
            button2.Size = new Size(98, 29);
            button2.TabIndex = 8;
            button2.Text = "Register";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(1121, 3);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 7;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(882, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(112, 28);
            comboBox1.TabIndex = 9;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(1005, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(110, 28);
            comboBox2.TabIndex = 10;
            // 
            // adminpanelksiazki1
            // 
            adminpanelksiazki1.AuthorsLabel = "";
            adminpanelksiazki1.ReleaseDate = DateTime.Now;
            adminpanelksiazki1.GenreLabel = "";

            // --- POPRAWKA: Zmiana z 0 na 1 ---
            adminpanelksiazki1.Quantity = 1;
            // ---------------------------------

            adminpanelksiazki1.ISBN = "";
            adminpanelksiazki1.Location = new Point(0, 0);
            adminpanelksiazki1.Name = "adminpanelksiazki1";
            adminpanelksiazki1.Size = new Size(1298, 669);
            adminpanelksiazki1.TabIndex = 1;
            adminpanelksiazki1.TitleText = "";
            // 
            // adminpanelogloszenia1
            // 
            adminpanelogloszenia1.Location = new Point(0, 0);
            adminpanelogloszenia1.Name = "adminpanelogloszenia1";
            adminpanelogloszenia1.DescriptionText = "";
            adminpanelogloszenia1.Size = new Size(1578, 684);
            adminpanelogloszenia1.TabIndex = 2;
            adminpanelogloszenia1.TitleText = "";
            // 
            // adminpaneluzytkownicy1
            // 
            adminpaneluzytkownicy1.Location = new Point(0, 0);
            adminpaneluzytkownicy1.Name = "adminpaneluzytkownicy1";
            adminpaneluzytkownicy1.Size = new Size(1301, 537);
            adminpaneluzytkownicy1.TabIndex = 3;
            // 
            // logowanie1
            // 
            logowanie1.Location = new Point(3, 1);
            logowanie1.Name = "logowanie1";
            logowanie1.Size = new Size(1298, 634);
            logowanie1.TabIndex = 4;
            // 
            // paneluzytkownika1
            // 
            paneluzytkownika1.Email = "";
            paneluzytkownika1.ZipCode1 = "";
            paneluzytkownika1.ZipCode2 = "";
            paneluzytkownika1.Location = new Point(1, 1);
            paneluzytkownika1.City = "";
            paneluzytkownika1.Name = "paneluzytkownika1";
            paneluzytkownika1.HouseNumber = "";
            paneluzytkownika1.ApartmentNumber = "";
            paneluzytkownika1.Phone = "";
            paneluzytkownika1.Size = new Size(1355, 674);
            paneluzytkownika1.TabIndex = 5;
            paneluzytkownika1.Street = "";
            paneluzytkownika1.Province = "";
            // 
            // rejestracja1
            // 
            rejestracja1.Location = new Point(2, 0);
            rejestracja1.Name = "rejestracja1";
            rejestracja1.Size = new Size(1332, 699);
            rejestracja1.TabIndex = 6;
            // 
            // MainForm
            // 
            ClientSize = new Size(1339, 540);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(rejestracja1);
            Controls.Add(paneluzytkownika1);
            Controls.Add(logowanie1);
            Controls.Add(adminpaneluzytkownicy1);
            Controls.Add(adminpanelogloszenia1);
            Controls.Add(adminpanelksiazki1);
            Name = "MainForm";
            Text = "Library";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ResumeLayout(false);
        }

        #endregion 

        private Button button1;
        private Button button2;
        private TextBox txtTitle;
        private TextBox txtGenre;
        private TextBox txtISBN;
        private TextBox txtAuthor;
        private TextBox txtReleaseYear;
        private ComboBox cmbSort;
        private DataGridView dgvBooks;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;

        private LibraryApp.View.AdminBooksControl adminpanelksiazki1;
        private LibraryApp.View.AdminAnnouncementsControl adminpanelogloszenia1;
        private LibraryApp.View.AdminUsersControl adminpaneluzytkownicy1;
        private LibraryApp.View.LoginControl logowanie1;
        private LibraryApp.View.UserDashboardControl paneluzytkownika1;
        private LibraryApp.View.RegisterControl rejestracja1;
    }
}