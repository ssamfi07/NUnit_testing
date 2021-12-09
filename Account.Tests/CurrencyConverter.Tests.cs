using NUnit.Framework;

namespace bank
{
[TestFixture]
public class CurrencyConverterTest
{
    private float rate;

    [SetUp]
    public void Init()
    {
        rate = 4.95F;
    }

    [Test]
    [Category("pass")]
    [Combinatorial]
    public void EurToRonTest([Values (543.54F, -543.65F, 0, 53453)] float valueInEur)
    {
        // arrange
        CurrencyConvertor _convertor = new CurrencyConvertor(rate);

        // act and assert
        Assert.IsTrue(_convertor.EurToRon(valueInEur) == rate * valueInEur);
    }

    [Test]
    [Category("pass")]
    [Combinatorial]
    public void RonToEurTest([Values (543.54F, -543.65F, 0, 53453)] float valueInRon)
    {
        // arrange
        CurrencyConvertor _convertor = new CurrencyConvertor(rate);

        // act and assert
        Assert.IsTrue(_convertor.RonToEur(valueInRon) == valueInRon / rate);
    }
}
} // namespace bank