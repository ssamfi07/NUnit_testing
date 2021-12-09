using System;
using System.Collections.Generic;
using System.Reflection;

namespace bank
{

// overrides Deposit & Withdraw methods of the Account class in order to record the method calls. 
// this is an Internal Spy, not a Dependency Spy, because the SUT call siblings methods of the same class not external classes/methods.
public class InternalAccountSpy : Account
{
    List<String> _actions = new List<string>();
    public ILogger _logger = new Logger();
    private float _balance;
    public InternalAccountSpy(){}
    public InternalAccountSpy(int balance, ILogger logger)
    {
        _balance = balance;
        _logger = logger;
    }
    public new void Deposit(float amount)
    {
        base.Deposit(amount);
        string message = "[" + DateTime.Now + "] " + MethodBase.GetCurrentMethod().Name + ": " + amount;
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

    public List<String> GetActions()
    {
        return _actions;
    }
}

} // namespace bank
