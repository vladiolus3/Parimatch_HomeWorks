
namespace Library
{
    public interface ISupportDeposit
    {
        void StartDeposit(decimal amount, string currency);
    }
    public interface ISupportWithdrawal
    {
        void StartWithdrawal(decimal amount, string currency);
    }
}
