﻿using AdamOneilSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Zinger.Models;

namespace Zinger.Forms
{
    public partial class frmConnections : Form
    {
        private SavedConnections _data;

        public SavedConnections SavedConnections { get; internal set; }

        public frmConnections()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void frmConnections_Load(object sender, EventArgs e)
        {
            try
            {
                colProvider.FillFromEnum<ProviderType>();                                                
                dataGridView1.DataSource = new BindingList<SavedConnection>(SavedConnections);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var data = dataGridView1.DataSource as BindingList<SavedConnection>;            
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // ignore
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                bool anyErrors = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    row.ErrorText = null;
                    SavedConnection sc = row.DataBoundItem as SavedConnection;
                    var result = await sc.TestAsync();
                    if (!result.OpenedSuccessfully)
                    {
                        anyErrors = true;
                        row.ErrorText = result.ErrorMessage;
                    }
                }

                if (!anyErrors)
                {
                    MessageBox.Show("All connections opened successfully.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}