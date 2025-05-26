using System.Windows;
using System.Windows.Controls;

namespace PSS.objects.Items;

public class ArtikelListItem
{
    public string title { get; set; }
    public string description { get; set; }
    public string unit { get; set; }
    public int stockQuantity { get; set; }
    
    public ArtikelListItem(Artikel art)
    {
        this.title = art.name;
        this.description = art.description;
        this.unit = art.unit;
        this.stockQuantity = art.StockQuantity;
    }
}