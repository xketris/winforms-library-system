using LibraryApp.Models;
using LibraryApp.Presenters;
using LibraryApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace LibraryApp.View
{
    public partial class ExemplarsForm : Form
    {
        public event Action<Exemplar> OnBorrow;
        public event Action<Exemplar> OnReturn;
        // public event Action<Exemplar, int> OnExtend; // Opcjonalne, jeśli używasz przedłużania

        // Usunięto: private DataGridView dgvExemplars; (jest już w Designer.cs)
        private DataGridView dgvBooks; // Referencja do odświeżania rodzica
        private BindingList<Exemplar> exemplars;

        public ExemplarsForm(string bookTitle, List<Exemplar> exemplarList, DataGridView parentDgv)
        {
            this.Text = $"Copies - {bookTitle}";
            this.Size = new Size(700, 400);
            InitializeComponent(); // Tutaj dgvExemplars jest tworzone przez Designer

            exemplars = new BindingList<Exemplar>(exemplarList);
            dgvBooks = parentDgv;

            // Usunięto ręczne tworzenie dgvExemplars, polegamy na Designerze

            dgvExemplars.DataSource = exemplars;
            AddActionButtons();

            dgvExemplars.CellClick += DgvExemplars_CellClick;
            dgvExemplars.RowPrePaint += DgvExemplars_RowPrePaint;
        }

        private void AddActionButtons()
        {
            // Sprawdź czy kolumny już istnieją (zabezpieczenie)
            if (dgvExemplars.Columns["Borrow"] != null) return;

            var borrowBtn = new DataGridViewButtonColumn
            {
                Name = "Borrow",
                HeaderText = "Borrow",
                Text = "Borrow",
                UseColumnTextForButtonValue = true
            };

            var returnBtn = new DataGridViewButtonColumn
            {
                Name = "Return",
                HeaderText = "Return",
                Text = "Return",
                UseColumnTextForButtonValue = true
            };

            dgvExemplars.Columns.Add(borrowBtn);
            dgvExemplars.Columns.Add(returnBtn);
        }

        private void DgvExemplars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var exemplar = dgvExemplars.Rows[e.RowIndex].DataBoundItem as Exemplar;
            if (exemplar == null) return;

            string colName = dgvExemplars.Columns[e.ColumnIndex].Name;

            if (colName == "Borrow" && exemplar.IsAvailable)
            {
                exemplar.BorrowDate = DateTime.Today;
                exemplar.ReturnDate = DateTime.Today.AddDays(14);
                OnBorrow?.Invoke(exemplar);
            }
            else if (colName == "Return" && !exemplar.IsAvailable)
            {
                exemplar.BorrowDate = null;
                exemplar.ReturnDate = null;
                OnReturn?.Invoke(exemplar);
            }

            dgvExemplars.Refresh();
            dgvBooks.Refresh(); // Odśwież listę główną (np. licznik dostępnych egzemplarzy)
        }

        private void DgvExemplars_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvExemplars.Rows.Count)
            {
                var exemplar = dgvExemplars.Rows[e.RowIndex].DataBoundItem as Exemplar;
                if (exemplar != null && exemplar.HasReturnDatePassed)
                {
                    dgvExemplars.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                }
                else
                {
                    dgvExemplars.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}