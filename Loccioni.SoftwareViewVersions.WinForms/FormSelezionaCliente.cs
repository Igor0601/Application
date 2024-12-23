﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loccioni.SoftwareViewVersions.DataModels;
using Loccioni.SoftwareViewVersions.Db;
using Loccioni.SoftwareViewVersions.Services;

namespace Loccioni.SoftwareViewVersions.WinForms
{
	public partial class FormSelezionaCliente : Form
	{
		int idClient;
		public FormSelezionaCliente()
		{
			InitializeComponent();

			//clientService.AggiungiCliente("Prova", "Prova", new string[] { "Test" });
			//numeroClienti = clientService.GetClientes().Count();
			//Ho un solo oggetto perchè la new è una sola!
			/*ClientService clientService =  new ClientService();
			ClientService clientService1 = clientService;

			Ho 3 oggetti di tipo Cliente
			clientService1.AggiungiCliente("Pippo", "Pippo", new string[] { "tag" });
			clientService1.AggiungiCliente("Pippo", "Pippo", new string[] { "tag" });
			clientService1.AggiungiCliente("Pippo", "Pippo", new string[] { "tag" });

			int numeroClienti = clientService.GetClientes().Count();

			int numeroClienti1 = clientService1.GetClientes().Count();*/
		}
		public void loadListView(ClientService clientService)
		{
			listViewClienti.View = View.Details;
			listViewClienti.FullRowSelect = true;
			listViewClienti.GridLines = true;
			listViewClienti.Columns.Add("ID", 50);
			listViewClienti.Columns.Add("Nome", 150);
			listViewClienti.Items.Clear();

			foreach (Client client in clientService.GetClientes())
			{
				ListViewItem clientItem = new ListViewItem($"{client.Id}")
				{
					Name = client.Id.ToString()
				};
				clientItem.SubItems.Add($"{client.Name}");
				listViewClienti.Items.Add(clientItem);
			}
		}
		public void ButtonIdCliente_Click(object sender, EventArgs e)
		{
			if(listViewClienti.SelectedItems.Count > 0)
			{
				idClient = int.Parse(listViewClienti.SelectedItems[0].Text);
			}
			this.Close();
		}
		public int GetIdClientes() 
		{
			return idClient;
		}
	}
}
