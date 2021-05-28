using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccountHolder
    {
        public AccountHolder(String name, String cpf, DateTime birthday)
        {
            Name = name;
            Cpf = cpf;
            Birthday = birthday;

            Account = new BankAccount();
        }
        public int Id { get; }
        public string Name { get; }
        public string Cpf { get; }
        public DateTime Birthday { get; }
        public BankAccount Account { get; }
    }
}
