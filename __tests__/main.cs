using Project.objects.Items;
using Project.scripts;
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

    [TestMethod]
    public void cryptoTest()
    {
        string password = "123456";
        string testData = "Hello World!";
        Crypt crt = new Crypt(password);


        var encrypted = crt.Encrypt(testData);
        var decrypted = crt.Decrypt(encrypted);
        
        Assert.AreEqual(testData, decrypted);
    }
}