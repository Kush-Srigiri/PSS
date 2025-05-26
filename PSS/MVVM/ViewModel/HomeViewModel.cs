using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PSS.objects;
using PSS.objects.Items;
using PSS.services;

namespace PSS.MVVM.ViewModel
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
                        Entries.Add(new ArtikelListItem(new Artikel(entries[i, 1], entries[i, 2], entries[i, 3], Convert.ToInt32(entries[i, 4]), false)));
                }
            }
        }
    }
}