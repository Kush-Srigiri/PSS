namespace __tests__;

[TestClass]
public class DB
{
    [TestMethod]
    public void DB_Test()
    {
        var db = Project.services.DB.Instance;
        
        
        db.Execute(@"CREATE TABLE Test(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            test INTEGER
        );");
        
        
        Assert.IsNotNull(db);
        Assert.IsTrue(db.TableExists("Test"));
        
        db.Execute("INSERT INTO Test(test) VALUES (1);");
        
        var res = db.Get("SELECT test FROM Test");

        Assert.IsNotNull(res);
        Console.WriteLine(res);
        
        db.Execute("DROP TABLE Test;");
        
        db.Dispose();
    }
}