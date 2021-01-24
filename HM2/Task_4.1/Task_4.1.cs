using System;
using Library;

namespace Task_4._1
{
    class Task_4_1
    {
        static void Main()
        {
            try
            {
                throw new LimitExceededException();
            }
            catch (InsufficientFundsException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (LimitExceededException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (PaymentServiceException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Console.ReadKey();
        }
    }
}
