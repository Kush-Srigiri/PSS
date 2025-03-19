using Project.objects.Items;
using Project.services;

namespace __tests__;

[TestClass]
public class main
{
    [TestMethod]
    public void ArtikelListItemTest()
    {
        var item = new ArtikelListItem("TestArtikel");
        
        Assert.IsNotNull(item);
        Assert.AreSame(item.title, "TestArtikel");
    }
}