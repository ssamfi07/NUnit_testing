namespace bank
{
public interface IAccount
{
    void Deposit(float amount);

    void Withdraw(float amount);

    void TransferFunds(Account destination, float amount);

    Account TransferMinFunds(Account destination, float amount);

    void TransferFundsFromEurAccount(Account destination, float amountInEur, ICurrencyConvertor convertor);
}
} // namespace bank
