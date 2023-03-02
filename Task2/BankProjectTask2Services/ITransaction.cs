using BankPjtTask2Enitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProjectTask2Services
{
    public interface ITransaction
    {
        void InsertTransaction(long _userBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc);
        void ViewTransaction();

    }
}
