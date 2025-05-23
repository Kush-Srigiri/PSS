using System.Windows;
using System.Windows.Controls;

namespace PSS.objects.Items;

public class ArtikelListItem
{
    public string title { get; set; }

    public ArtikelListItem(string title)
    {
        this.title = title;
    }
}