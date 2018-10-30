﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Zinger.Models;

namespace Zinger
{
	public partial class frmQuery : Form
	{
		public frmQuery()
		{
			InitializeComponent();
		}

		private void btnConnections_Click(object sender, EventArgs e)
		{
			try
			{
				frmContainer parent = MdiParent as frmContainer;
				if (parent != null)
				{
					parent.ShowConnectionDialog();
					FillConnectionDropdown();
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		public void LoadQuery(string fileName)
		{
			var query = queryEditor1.LoadQuery(fileName);
			cbConnection.SelectedIndex = cbConnection.FindString(query.ConnectionName);
			FindForm().Text = query.Name;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				FillConnectionDropdown();
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private void FillConnectionDropdown()
		{
			frmContainer parent = MdiParent as frmContainer;
			var connections = parent.GetSavedConnections();
			cbConnection.SelectedIndexChanged -= cbConnection_SelectedIndexChanged;
			cbConnection.Items.Clear();
			foreach (SavedConnection sc in connections) cbConnection.Items.Add(sc);
			cbConnection.SelectedIndexChanged += cbConnection_SelectedIndexChanged;
		}

		private void cbConnection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbConnection.SelectedItem != null)
			{
				SavedConnection sc = cbConnection.SelectedItem as SavedConnection;

				Dictionary<ProviderType, QueryProvider> providers = new Dictionary<ProviderType, QueryProvider>()
				{
					{ ProviderType.SqlServer, new SqlServerQueryProvider(sc.ConnectionString) },
					{ ProviderType.MySql, new MySqlQueryProvider(sc.ConnectionString) },
					{ ProviderType.OleDb, new OleDbQueryProvider(sc.ConnectionString) }
				};

				queryEditor1.Enabled = true;
				queryEditor1.Provider = providers[sc.ProviderType];
			}
			else
			{
				queryEditor1.Enabled = false;
			}
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F5:
					queryEditor1.Execute();
					break;
			}
		}

		private void tbQueryName_TextChanged(object sender, EventArgs e)
		{
			queryEditor1.QueryName = resultClassBuilder1.QueryName;
		}

		private void queryEditor1_Executed(object sender, EventArgs e)
		{
			var results = sender as QueryProvider.ExecuteResult;
			resultClassBuilder1.ResultClass = results.ResultClass;
			resultClassBuilder1.QueryClass = results.QueryClass;
		}

		private void btnRunQuery_Click(object sender, EventArgs e)
		{
			try
			{
				queryEditor1.Execute();
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private void resultClassBuilder1_QueryNameChanged(object sender, EventArgs e)
		{
			queryEditor1.QueryName = resultClassBuilder1.QueryName;
			resultClassBuilder1.RenameQuery(queryEditor1.QueryName);
			FindForm().Text = resultClassBuilder1.QueryName;
		}

		public void AutoSave(int index)
		{
			string fileName = Path.Combine(frmContainer.SavedConnectionPath(), $"query{index}.sql");
			queryEditor1.SaveQuery(resultClassBuilder1.QueryName, fileName);
		}

		public void SaveAs()
		{
			queryEditor1.SaveQuery(resultClassBuilder1.QueryName, cbConnection.SelectedText);
		}
	}
}