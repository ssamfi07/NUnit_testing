using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Xml;
using System.Linq;

namespace bank
{

public class AccountForLogSpy
{
    public float Balance
    {
        get { return balance; }
    }

    public float MinBalance
    {
        get { return minBalance; }
    }

    public IClient client;

    public ILogger logger;

    private float balance;
    private float minBalance = 10;
    public AccountForLogSpy(float amount, IClient client_, ILogger logger_)
    {
        balance = amount;
        client = client_;
        logger = logger_;
    }

     public void Deposit(float amount)
    {
        balance += amount;
    }

    public void Withdraw(float amount)
    {
        balance -= amount;
    }

    public void TransferFunds2(AccountForLogSpy destination, float amount)
    {
        destination.Deposit(amount);
        Withdraw(amount);
    }
};

} // namespace bank