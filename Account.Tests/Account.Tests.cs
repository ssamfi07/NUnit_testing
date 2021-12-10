using System;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;

namespace bank
{
[TestFixture]
public class AccountTest
{
    Account _source;
    Account _destination;
    float _amount;
    float _rateEurRon;
     Moq.Mock<ILogger> loggerMock = new Moq.Mock<ILogger>(MockBehavior.Default);

    [SetUp]
    public void Init()
    {
        // arrange -- set up the initial values
        _source = new Account();
        _destination = new Account();
        _source.Deposit(200.00F);
        _destination.Deposit(150.00F);
        _amount = 50F;
        _rateEurRon = 4.95F;
    }

    [TearDown]
    public void VerifyAndTearDown()
    {
        loggerMock.VerifyAll();
    }

    [Test]
    public void DepositWithdrawMockTest()
    {
        // arrange
        InternalAccountSpy sourceSpy = new InternalAccountSpy(200, loggerMock.Object);

        loggerMock.Setup(cmd => cmd.Log("[" + DateTime.Now + "] Deposit: 10"));
        loggerMock.Setup(cmd => cmd.Log("[" + DateTime.Now + "] Withdraw: 20"));

        // act
        sourceSpy.Deposit(10F);
        sourceSpy.Withdraw(20F);

        // assert
        Assert.AreEqual(2, sourceSpy.GetActions().Count);

        // verify mock behaviour
        loggerMock.Verify((cmd => cmd.Log("[" + DateTime.Now + "] Deposit: 10")), Times.Once());
        loggerMock.Verify((cmd => cmd.Log("[" + DateTime.Now + "] Withdraw: 20")), Times.Once());

        // display logs (same as the ones faked by mock)
        var actions = sourceSpy.GetActions();
        for(int i = 0; i < actions.Count; ++i)
        {
            Console.WriteLine(actions[i]);
        }
    }

    [Test]
    public void TransferFundsMockTest()
    {
        // arrange
        InternalAccountSpy sourceSpy = new InternalAccountSpy(200, loggerMock.Object);
        InternalAccountSpy destinationSpy = new InternalAccountSpy(150, loggerMock.Object);

        loggerMock.Setup(cmd => cmd.Log("[" + DateTime.Now + "] TransferFunds: 10"));

        // act
        sourceSpy.TransferFunds(destinationSpy, 10F);

        // assert
        Assert.AreEqual(1, sourceSpy.GetActions().Count);

        // verify mock behaviour
        loggerMock.Verify((cmd => cmd.Log("[" + DateTime.Now + "] TransferFunds: 10")), Times.Once());

        // display logs (same as the ones faked by mock)
        var actions = sourceSpy.GetActions();
        for(int i = 0; i < actions.Count; ++i)
        {
            Console.WriteLine(actions[i]);
        }
    }

    [Test]
    [Category("pass")]
    public void TransferMinFundsMockTest()
    {
        // arrange
        InternalAccountSpy sourceSpy = new InternalAccountSpy(200, loggerMock.Object);
        InternalAccountSpy destinationSpy = new InternalAccountSpy(150, loggerMock.Object);

        // act
        // initialize deposit (set the sut balance)
        sourceSpy.Deposit(200F);

        sourceSpy.TransferMinFunds(destinationSpy, 180F);

        // assert
        Assert.AreEqual(2, sourceSpy.GetActions().Count);

        // verify mock behaviour
        loggerMock.Verify(cmd => cmd.Log("[" + DateTime.Now + "] Deposit: 200"));
        loggerMock.Verify(cmd => cmd.Log("[" + DateTime.Now + "] TransferMinFunds: 180"));

        // display logs (same as the ones faked by mock)
        var actions = sourceSpy.GetActions();
        for(int i = 0; i < actions.Count; ++i)
        {
            Console.WriteLine(actions[i]);
        }
    }

    [Test]
    [Category("fail")]
    [Combinatorial]
    public void TransferMinFundsFailAllTest([Values (700, 500)] int a, [Values (0,20)] int b, [Values (690, -345.54F) ] float c)
    {
        // arrange
        Account source = new Account(a);
        Account destination = new Account(b);

        // act and assert
        Assert.That(() => source.TransferMinFunds(destination, c), 
            Throws.TypeOf<NotEnoughFundsException>());
    }

    [Test]
    [Category("pass")]
    public void TransferFundsFromEurAmountTestMock()
    {
        // arrange
        var convertorMock = new Mock<ICurrencyConvertor>();
        convertorMock.Setup(_ => _.EurToRon(_amount)).Returns( _amount * _rateEurRon);  // set mock -- acts as a stub

        // act
        _source.TransferFundsFromEurAccount(_destination, _amount, convertorMock.Object);

        // assert
        Assert.AreEqual(150.00F + (_amount * _rateEurRon), _destination.Balance);
        Assert.AreEqual(200.00F - _amount, _source.Balance);

        convertorMock.Verify(_ => _.EurToRon(_amount), Times.Once());  // verify behavior -- mock should only call EurToRon(_amount) once
    }
}
} // namespace bank