using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Project.objects;
using Project.objects.Items;
using Project.services;

namespace Project.MVVM.ViewModel
{
    class HomeViewModel
    {
        public ObservableCollection<ArtikelListItem> Entries { get; set; }

        public HomeViewModel()
        {
            Entries = new ObservableCollection<ArtikelListItem>();
            if (DB.Instance.TableExists("Artikel"))
            {
                string[,] entries = DB.Instance.Get("SELECT * FROM Artikel");

                int rows = entries.GetLength(0);
                int cols = entries.GetLength(1);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.WriteLine($"Row {i}, Column {j}: {entries[i, j]}");
                    }
                }
            }
        }
    }
}