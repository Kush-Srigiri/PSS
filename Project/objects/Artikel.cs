using Project.services;

namespace Project.objects;

public class Artikel : IArtikel
{
    private DB _db;

    public int ID { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string unit { get; private set; }
    public int StockQuantity { get; private set; }
    
    public DateTime timestamp { get; private set; }


    public Artikel(string name, string description, string unit, int StockQuantity, bool insert)
    {
        this.name = name;
        this.description = description;
        this.unit = unit;
        this.StockQuantity = StockQuantity;


		if (insert == true) {	
	        _db = DB.Instance;
	        if (!_db.TableExists("Artikel"))
	        {
	            _db.Execute(@"
	            Create table Artikel
	                (
	                    id            INTEGER PRIMARY KEY AUTOINCREMENT,
	                    name          VARCHAR(120),
	                    description   TEXT,
	                    unit          VARCHAR(30) NOT NULL, 
	                    StockQuantity INT NOT NULL, 
	                    CreationDate  DATETIME DEFAULT CURRENT_TIMESTAMP
	                )
	            ");
	        }
		}
    }


    public void Update(Artikel artikel)
    {
        this.name = artikel.name;
        this.description = artikel.description;
        this.unit = artikel.unit;
        this.StockQuantity = artikel.StockQuantity;

        UpdateArtikel(name, description, StockQuantity, unit);
    }

    public void Update(string name, string description, int StockQuantity, string unit)
    {
        this.name = name;
        this.description = description;
        this.unit = unit;
        this.StockQuantity = StockQuantity;

        UpdateArtikel(name, description, StockQuantity, unit);        
    }

    private void UpdateArtikel(string name, string descriptions, int StockQuantity, string unit)
    {
    	_db = DB.Instance;
    	_db.Execute(@"UPDATE Artikel SET name = @name, description = @description, unit = @unit WHERE id = @id",
    		@name = name, @description = description, @unit = unit);
    }
}
