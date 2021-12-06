using System;
using System.Collections.Generic;
using System.Text;

namespace bank
{
public class NotEnoughFundsException : ApplicationException
{
}

public class Account
{
    public float Balance
    {
        get { return balance; }
    }

    public float MinBalance
    {
        get { return minBalance; }
    }

    private float balance;
    private float minBalance = 10;

    public Account()
    {
        balance = 0;
    }

    public Account(int value)
    {
        balance = value;
    }

    public void Deposit(float amount)
    {
        balance += amount;
    }

    public void Withdraw(float amount)
    {
        balance -= amount;
    }

    public void TransferFunds(Account destination, float amount)
    {
        destination.Deposit(amount);
        Withdraw(amount);
    }

    public Account TransferMinFunds(Account destination, float amount)
    {
        if (Balance - amount > MinBalance && amount > 00F)
        {
            destination.Deposit(amount);
            Withdraw(amount);
        }
        else throw new NotEnoughFundsException();
        return destination;
    }

    public void TransferFundsFromEurAccount_version1(Account destination, float amountinEur, float rate)
    {
        float amountInRon = amountinEur * rate;

        destination.Deposit(amountInRon);
        Withdraw(amountInRon);
    }

    public void TransferFundsFromEurAccount_version2(Account destination, float amountInEur, ICurrencyConvertor convertor)
    {
        float amountInRon = convertor.EurToRon(amountInEur);

        destination.Deposit(amountInRon);
        Withdraw(amountInRon);
    }
}

} // namespace bank
