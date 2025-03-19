using System.Windows;
using System.Windows.Controls;

namespace Project.objects.Items;

public class ArtikelListItem
{
    public string title { get; set; }

    public ArtikelListItem(string title)
    {
        this.title = title;
    }
}