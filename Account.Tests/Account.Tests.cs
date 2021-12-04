using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace bank
{

    [TestFixture]
    public class AccountTest
    {
        Account source;
        Account destination;

        [SetUp]
        public void InitAccount()
        {
            //arrange
            source = new Account();
            source.Deposit(200.00F);
            destination = new Account();
            destination.Deposit(150.00F);
        }
    
        [Test]
        [Category("pass")]
        public void TransferFunds()
        {
            //act
            source.TransferFunds(destination, 100.00F);
            
            //assert
            Assert.AreEqual(250.00F, destination.Balance);
            Assert.AreEqual(100.00F, source.Balance);

        }

        [Test, Category("pass")]

        [TestCase(200, 0, 78)]
        [TestCase(200, 0, 189)]
        [TestCase(200, 0, 1)]
        public void TransferMinFunds(int a, int b, int c)
        {

            Account source = new Account();
            source.Deposit(a);
            Account destination = new Account();
            destination.Deposit(b);

            source.TransferMinFunds(destination, c);
            Assert.AreEqual(c, destination.Balance);
        }

        [Test]
        [Category("fail")]
        [TestCase(200, 150, 1900)]
        [TestCase(200, 150, 345)]

        public void TransferMinFundsFail(int a, int b, int c)
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

        public void TransferMinFundsFailAll([Values (700, 500)] int a, [Values (0,20)] int b, [Values (190,345) ] int c)
        {
            Account source = new Account();
            source.Deposit(a);
            Account destination = new Account();
            destination.Deposit(b);

            source.TransferMinFunds(destination, c);
            // Assert.That(() => source.TransferMinFunds(destination, c), 
            //     Throws.TypeOf<NotEnoughFundsException>());
        }
    }
}