using System;
using NUnit.Framework;
using Moq;

namespace bank
{
[TestFixture]
public class AccountTest
{
    Account _source = new Account();
    Account _destination = new Account();
    float _amount;
    float _rateEurRon;
     Moq.Mock<ILogger> loggerMock = new Moq.Mock<ILogger>(MockBehavior.Strict);

    [SetUp]
    public void Init()
    {
        // arrange -- set up the initial values
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

    // [Test, Category("pass")]
    // [TestCase(200, 0, 78)]
    // [TestCase(200, 0, 189)]
    // [TestCase(200, 0, 1)]
    // public void TransferMinFundsTest(float a, float b, float c)
    // {
    //     // arrange
    //     Account source = new Account();
    //     source.Deposit(a);
    //     Account destination = new Account();
    //     destination.Deposit(b);

    //     // act
    //     source.TransferMinFunds(destination, c);

    //     // assert
    //     Assert.AreEqual(c, destination.Balance);
    // }

    // [Test]
    // [Category("fail")]
    // [TestCase(200, 150, 190)]
    // [TestCase(200, 150, 345)]
    // [TestCase(200, 150, 0)]
    // [TestCase(200, 150, -54.5F)]
    // public void TransferMinFundsFailTest(float a, float b, float c)
    // {
    //     Account source = new Account();
    //     source.Deposit(a);
    //     Account destination = new Account();
    //     destination.Deposit(b);

    //     Assert.That(() => source.TransferMinFunds(destination, c), 
    //         Throws.TypeOf<NotEnoughFundsException>());
    // }


    // [Test]
    // [Category("fail")]
    // [Combinatorial]
    // public void TransferMinFundsFailAllTest([Values (700, 500)] int a, [Values (0,20)] int b, [Values (690, -345.54F) ] float c)
    // {
    //     // arrange
    //     Account source = new Account(a);
    //     Account destination = new Account(b);

    //     // act and assert
    //     Assert.That(() => source.TransferMinFunds(destination, c), 
    //         Throws.TypeOf<NotEnoughFundsException>());
    // }

    // [Test]
    // [Category("pass")]
    // public void TransferFundsFromEurAmountTestMock()
    // {
    //     // arrange
    //     var convertorMock = new Mock<ICurrencyConvertor>();
    //     convertorMock.Setup(_ => _.EurToRon(_amount)).Returns( _amount * _rateEurRon);  // set mock -- acts as a stub

    //     // act
    //     _source.TransferFundsFromEurAccount(_destination, _amount, convertorMock.Object);

    //     // assert
    //     Assert.AreEqual(150.00F + (_amount * _rateEurRon), _destination.Balance);
    //     Assert.AreEqual(200.00F - _amount, _source.Balance);

    //     convertorMock.Verify(_ => _.EurToRon(_amount), Times.Once());  // verify behavior -- mock should only call EurToRon(_amount) once
    // }

    // [Test]
    // [Category("pass")]
    // public void SpyTest()
    // {
    //     // arrange
    //     InternalAccountSpy spy = new InternalAccountSpy();

    //     // act
    //     spy.Deposit(200);
    //     spy.Withdraw(100);

    //     // var actions = spy.GetActions();
    //     // for(int i = 0; i < actions.Count; ++i)
    //     // {
    //     //     Console.WriteLine(actions[i]);
    //     // }

    //     // assert
    //     Assert.AreEqual(2, spy.GetActions().Count);
    // }

    [Test]
    public void LoggerMockTest()
    {
        // arrange
        InternalAccountSpy sourceSpy = new InternalAccountSpy(200, loggerMock.Object);
        InternalAccountSpy destinationSpy = new InternalAccountSpy(150, loggerMock.Object);

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

        // display logs
        var actions = sourceSpy.GetActions();
        for(int i = 0; i < actions.Count; ++i)
        {
            Console.WriteLine(actions[i]);
        }
    }
}
} // namespace bank