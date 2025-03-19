using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Project.objects.Items;

namespace Project.MVVM.ViewModel
{
    class HomeViewModel
    {
        public ObservableCollection<ArtikelListItem> Entries { get; set; }

        public HomeViewModel()
        {
            Entries = new ObservableCollection<ArtikelListItem>();

            for (int i = 1; i < 11; i++)
            {
                Entries.Add(new ArtikelListItem($"Artikel {i}"));
            }
        }
        
    }
}