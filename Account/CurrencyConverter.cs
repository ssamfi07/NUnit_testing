using System;
using System.Collections.Generic;
using System.Text;

namespace bank
{
public interface ICurrencyConvertor 
{
    public float EurToRon(float valueInEur);
    public float RonToEur(float valueInRon);
}

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