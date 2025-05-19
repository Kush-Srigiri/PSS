using Project.objects;
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
    public void Artikel_NegativeStock_Fails()
    {
        var artikel = new Artikel("Tasche", "groß", "Stück", -5, false);
        Assert.IsTrue(artikel.StockQuantity >= 0, "Artikel darf keinen negativen Lagerstand haben.");
    }

    [TestMethod]
    public void Crypt_WrongPassword_ShouldFail()
    {
        string password1 = "abc123";
        string password2 = "wrongpass";
        string data = "SecretText";

        var crypt1 = new Crypt(password1);
        var crypt2 = new Crypt(password2);

        var encrypted = crypt1.Encrypt(data);
        var decrypted = crypt2.Decrypt(encrypted);

        Assert.AreNotEqual(data, decrypted);
    }
}
