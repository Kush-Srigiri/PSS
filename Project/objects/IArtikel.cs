using System.Data.Entity.Infrastructure.Design;

namespace Project.objects;

public interface IArtikel
{
    public int ID { get; }
    public string name { get; }
    public string description { get; }
    public string unit { get; }
    public int StockQuantity { get; }
    public DateTime timestamp { get; }

    public virtual void Update() {}
}