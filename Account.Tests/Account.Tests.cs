using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace bank
{

    [TestFixture]
    public class AccountTest
    {
        Account _source;
        Account _destination;

        [SetUp]
        public void InitAccount()
        {
            // arrange -- set up the initial values
            _source = new Account();
            _source.Deposit(200.00F);
            _destination = new Account();
            _destination.Deposit(150.00F);
        }
    
        [Test]
        [Category("pass")]
        public void TransferFunds()
        {
            // act -- call method
            _source.TransferFunds(_destination, 100.00F);
            
            // assert -- expectations
            Assert.AreEqual(250.00F, _destination.Balance);
            Assert.AreEqual(100.00F, _source.Balance);

        }

        [Test, Category("pass")]
        [TestCase(200, 0, 78)]
        [TestCase(200, 0, 189)]
        [TestCase(200, 0, 1)]
        public void TransferMinFunds(float a, float b, float c)
        {
            // arrange
            Account source = new Account();
            source.Deposit(a);
            Account destination = new Account();
            destination.Deposit(b);

            // act
            source.TransferMinFunds(destination, c);

            // assert
            Assert.AreEqual(c, destination.Balance);
        }

        [Test]
        [Category("fail")]
        [TestCase(200, 150, 190)]
        [TestCase(200, 150, 345)]
        [TestCase(200, 150, 0)]
        [TestCase(200, 150, -54.5F)]
        public void TransferMinFundsFail(float a, float b, float c)
        {
            Account source = new Account();
            source.Deposit(a);
            Account destination = new Account();
            destination.Deposit(b);

            Assert.That(() => source.TransferMinFunds(destination, c), 
                Throws.TypeOf<NotEnoughFundsException>());
        }


        [Test]
        [Category("fail")]
        [Combinatorial]
        public void TransferMinFundsFailAll([Values (700, 500)] float a, [Values (0,20)] float b, [Values (690, -345.54F) ] float c)
        {
            // arrange
            Account source = new Account();
            source.Deposit(a);
            Account destination = new Account();
            destination.Deposit(b);

            // act and assert
            Assert.That(() => source.TransferMinFunds(destination, c), 
                Throws.TypeOf<NotEnoughFundsException>());
        }
    }
}