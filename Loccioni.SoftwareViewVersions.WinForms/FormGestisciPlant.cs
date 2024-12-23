﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loccioni.SoftwareViewVersions.DataModels;
using Loccioni.SoftwareViewVersions.Db;
using Loccioni.SoftwareViewVersions.Services;

namespace Loccioni.SoftwareViewVersions.WinForms
{
	public partial class FormGestisciPlant : Form
	{
		int id;
		string nome;
		string nazione;
		string citta;
		string indirizzo;
		string[] tag;
		FormModificaPlant formModificaPlant;
		public FormGestisciPlant(bool visualizza)
		{
			InitializeComponent();
			formModificaPlant = new FormModificaPlant();
			if(visualizza == true )
				ButtonDeletePlant.Visible = false;
			else
				ButtonModificaPlant.Visible = false;
		}
		public void loadListView(PlantService plantService, ClientService clientService)
		{
			listViewGestisciPlant.View = View.Details;
			listViewGestisciPlant.FullRowSelect = true;
			listViewGestisciPlant.GridLines = true;
			listViewGestisciPlant.Columns.Add($"ID: ", 50);
			listViewGestisciPlant.Columns.Add($"Nome: ", 150);
			listViewGestisciPlant.Columns.Add($"Nazione: ", 150);
			listViewGestisciPlant.Columns.Add($"Citta: ", 150);
			listViewGestisciPlant.Columns.Add($"Indirizzo: ", 150);
			listViewGestisciPlant.Columns.Add($"Tag: ", 50);
			listViewGestisciPlant.Columns.Add($"Cliente: ", 150);
			listViewGestisciPlant.Items.Clear();

			foreach (Plant plant in plantService.GetPlants())
			{
				ListViewItem plantItem = new ListViewItem($"{plant.Id}")
				{
					Name = plant.Id.ToString()
				};
				plantItem.SubItems.Add($"{plant.Name}");
				plantItem.SubItems.Add($"{plant.State}");
				plantItem.SubItems.Add($"{plant.City}");
				plantItem.SubItems.Add($"{plant.Address}");
				plantItem.SubItems.Add($"{plant.Tags[0]}");
				foreach (Client client in clientService.GetClientes()) 
				{
					if (client.Id == plant.IdClient)
						plantItem.SubItems.Add($"{client.Name}");
				}
				listViewGestisciPlant.Items.Add(plantItem);
			}
		}
		public void ButtonModificaPlant_Click(object sender, EventArgs e)
		{
			formModificaPlant = new FormModificaPlant();
			if (listViewGestisciPlant.SelectedItems.Count > 0)
			{
				ListView.SelectedListViewItemCollection selectedItems = listViewGestisciPlant.SelectedItems;
				formModificaPlant.loadTextBoxes(selectedItems);
				formModificaPlant.ShowDialog();
			}
			id = formModificaPlant.GetId();
			nome = formModificaPlant.GetNome();
			nazione = formModificaPlant.GetNazione();
			citta = formModificaPlant.GetCitta();
			indirizzo = formModificaPlant.GetIndirizzo();
			tag = formModificaPlant.GetTag();
			this.Close();
		}
		public int Id 
		{ 
			get { return id; } 
		}

		public int GetId() { return id; }
		public string GetNome() { return nome; }
		public string GetNazione() { return nazione; }
		public string GetCitta() { return citta; }
		public string GetIndirizzo() { return indirizzo; }
		public string[] GetTag() { return tag; }
		public void ButtonDeletePlant_Click(object sender, EventArgs e)
		{
			if (listViewGestisciPlant.SelectedItems.Count > 0) 
			{ 
				id = int.Parse(listViewGestisciPlant.SelectedItems[0].Text);
				nome = listViewGestisciPlant.SelectedItems[0].SubItems[1].Text;
				nazione = listViewGestisciPlant.SelectedItems[0].SubItems[2].Text;
				citta = listViewGestisciPlant.SelectedItems[0].SubItems[3].Text;
				indirizzo = listViewGestisciPlant.SelectedItems[0].SubItems[4].Text;
				tag = new string[1];
				tag[0] = listViewGestisciPlant.SelectedItems[0].SubItems[5].Text;
			}
			this.Close();
		}
	}
}
