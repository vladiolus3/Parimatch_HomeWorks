using System;

namespace Library
{
    public class PaymentServiceException : Exception
    {
        public PaymentServiceException() : base()
        {

        }
        public PaymentServiceException(string message) : base(message)
        {
            message = "There are problems with the PaymentService";
        }
    }
    public class LimitExceededException : PaymentServiceException
    {
        public LimitExceededException() : base()
        {

        }
        public LimitExceededException(string message) : base(message)
        {
            message = "The transaction amount exceeds the established limits for the  Payment";
        }
    }
    public class InsufficientFundsException : PaymentServiceException
    {
        public InsufficientFundsException() : base()
        {

        }
        public InsufficientFundsException(string message) : base(message)
        {
            message = "The transaction amount exceeds the account balance of the Payment";
        }
    }
}
