using System.Security.Cryptography;
using PSS.objects;
using PSS.objects.Items;
using PSS.scripts;
using PSS.services;

namespace __tests__;

[TestClass]
public class main
{
    [TestMethod]
    public void ArtikelListItemTest()
    {
        var expected = "TestArtikel";

        var item = new ArtikelListItem("TestArtikel");

        Assert.IsNotNull(item);
        Assert.AreSame(item.title, expected);
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

    [TestMethod]
    public void Artikel_test()
    {
        var a = new Artikel("Stift", "blau", "Stück", 10, false);

        Assert.AreEqual("Stift", a.name);
        Assert.AreEqual("blau", a.description);
        Assert.AreEqual("Stück", a.unit);
        Assert.AreEqual(10, a.StockQuantity);
    }

    [TestMethod]
    public void User_Test()
    {
        var user = new UserDataLogin("Max", "Password", true);

        Assert.AreEqual("Max", user.Email);
        Assert.IsTrue(user.ValidatePassword("Password"));
    }

    // Folgende Tests sollen fehlschlagen
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "stockquantity cannot be below 1")]
    public void Artikel_NegativeStock_Fails()
    {
        var artikel = new Artikel("Tasche", "groß", "Stück", -5, false);
    }
}
