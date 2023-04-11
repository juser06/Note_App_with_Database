﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using Note_App_with_Database.forpracticeDataSetTableAdapters;
using System.Windows.Threading;

namespace Note_App_with_Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LinqToSqlDataClassesDataContext dataContext;
        string connectionString;



        public MainWindow()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["Note_App_with_Database.Properties.Settings.forpracticeConnectionString"].ConnectionString;
            dataContext = new LinqToSqlDataClassesDataContext(connectionString);
            reloj();
            MostrarNotas();
        }

        private void MostrarNotas()
        {
            dataContext = new LinqToSqlDataClassesDataContext(connectionString);
            ListaDeTitulosPendientes.ItemsSource = dataContext.Notas;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (Titulo_Box.Text == null || Titulo_Box.Text.Trim() == "")
            {
                return;
            }
            Nota nota = new Nota();
            nota.Titulo = Titulo_Box.Text;
            nota.Nota1 = Nota_box.Text;
            nota.Hora = DateTime.Now.ToString();
            nota.Pendiente_Archivado = true;
            dataContext.Notas.InsertOnSubmit(nota);
            dataContext.SubmitChanges();
            ListaDeTitulosPendientes.Items.Refresh();
            Titulo_Box.Clear();
            Nota_box.Clear();
            ListaDeTitulosPendientes.Items.Refresh();
            MostrarNotas();
        }

        private void reloj()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            Clock_box.Text = DateTime.Now.ToString();
            Clock_box2.Text = DateTime.Now.ToString();
        }

        private void ListaDeTitulosPendientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            MostrarNotas();
        }

        private void Archivar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
