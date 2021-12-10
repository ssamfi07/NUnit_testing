using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace bank
{

// overrides Deposit & Withdraw methods of the Account class in order to record the method calls. 
// this is an Internal Spy, not a Dependency Spy, because the SUT call siblings methods of the same class not external classes/methods.
public class InternalAccountSpy : Account
{
    List<String> _actions = new List<string>();
    public ILogger _logger;
    private float _balance;
    private MethodBase m;
    public InternalAccountSpy(){}
    public InternalAccountSpy(int balance, ILogger logger)
    {
        _balance = balance;
        _logger = logger;
    }
    public new void Deposit(float amount)
    {
        base.Deposit(amount);
        m = MethodBase.GetCurrentMethod();
        string message = "[" + DateTime.Now + "] " + m.Name + ": " + amount;
        _actions.Add(message);
        _logger.Log(message);
    }

    public new void Withdraw(float amount)
    {
        base.Withdraw(amount);
        string message = "[" + DateTime.Now + "] " + MethodBase.GetCurrentMethod().Name + ": " + amount;
        _actions.Add(message);
        _logger.Log(message);
    }

    public void TransferFunds(InternalAccountSpy destination, float amount)
    {
        base.TransferFunds(destination, amount);
        string message = "[" + DateTime.Now + "] " + MethodBase.GetCurrentMethod().Name + ": " + amount;
        _actions.Add(message);
        _logger.Log(message);
    }

    public void TransferMinFunds(InternalAccountSpy destination, float amount)
    {
        var response = base.TransferMinFunds(destination, amount);
        // TransferMinFunds of Account returns the destination -- transfer successfully
        Console.WriteLine(response.GetType().Name);
        if (response.GetType().Name == "InternalAccountSpy")
        {
            string message = "[" + DateTime.Now + "] " + MethodBase.GetCurrentMethod().Name + ": " + amount;
            _actions.Add(message);
            _logger.Log(message);
        }
        else // exception is thrown -- redundant code
        {
            string message = "[" + DateTime.Now + "] " + MethodBase.GetCurrentMethod().Name + ": NotEnoughFunds";
            _actions.Add(message);
            _logger.Log(message);
        }
    }

    public List<String> GetActions()
    {
        return _actions;
    }
}

} // namespace bank
