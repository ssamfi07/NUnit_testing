namespace bank
{
public class CurrencyConvertor: ICurrencyConvertor
{
    float rateEurRon;
    public CurrencyConvertor(float _rateEurRon)
    {
        rateEurRon = _rateEurRon;
    }

    public float EurToRon(float valueInEur)
    {
        return valueInEur * rateEurRon;
    }

    public float RonToEur(float valueInRon)
    {
        return valueInRon / rateEurRon;
    }
}
} // namespace bank