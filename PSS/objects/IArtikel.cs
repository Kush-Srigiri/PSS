using System.Data.Entity.Infrastructure.Design;

namespace PSS.objects;

public interface IArtikel
{
    /// <summary>
    /// Atribute for the ID of the Article
    /// </summary>
    public int ID { get; }
    /// <summary>
    /// Atribute for the name of the Article
    /// </summary>
    public string name { get; }
    /// <summary>
    /// Atribute for the description of the Article
    /// </summary>
    public string description { get; }
    /// <summary>
    /// Atribute for the type of unit of the Article
    /// <example>Kilograms / Liters / Meters</example>
    /// </summary>
    public string unit { get; }
    /// <summary>
    /// Atribute for the count of how many Articles of that type you have
    /// </summary>
    public int StockQuantity { get; }
    /// <summary>
    /// Atribute for the Creation time
    /// </summary>
    public DateTime timestamp { get; }

    /// <summary>
    /// Function for Updating the Article
    /// </summary>
    public virtual void Update() {}
}