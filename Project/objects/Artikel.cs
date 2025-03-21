using Project.services;

namespace Project.objects;

public class Artikel
{
    private DB _db;

    public int ID { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string unit { get; private set; }
    public int StockQuantity { get; private set; }
    
    public DateTime timestamp { get; private set; }


    public Artikel(string name, string description, string unit, int StockQuantity)
    {
        this.name = name;
        this.description = description;
        this.unit = unit;

        _db = DB.Instance;
        if (!_db.TableExists("Artikel"))
        {
            _db.Execute(@"
            Create table Artikel
                (
                    id            INTEGER PRIMARY KEY AUTOINCREMENT,
                    name          VARCHAR(255),
                    description   TEXT,
                    unit          VARCHAR(30) NOT NULL, 
                    StockQuantity INT NOT NULL, 
                    CreationDate  DATETIME DEFAULT CURRENT_TIMESTAMP
                )
            ");
        }
    }


    public void Update(Artikel artikel)
    {
        this.name = artikel.name;
        this.description = artikel.description;
        this.unit = artikel.unit;
        this.StockQuantity = artikel.StockQuantity;
    }

    public void Update(string name, string description, int StockQuantity, string unit)
    {
        this.name = name;
        this.description = description;
        this.unit = unit;
        this.StockQuantity = StockQuantity;

        _db = DB.Instance;
        _db.Execute(@"UPDATE Artikel SET name = @name, description = @description, unit = @unit WHERE id = @id",
            @name = name, @description = description, @unit = unit);
        
    }
}