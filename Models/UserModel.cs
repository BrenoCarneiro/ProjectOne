using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectOne.Models
{
    public class UserModel
    {
        public string FirstName { get; protected set; } //Utilização de Properties
        public string LastName { get; protected set; }
        public Status Status { get; protected set; } = Status.Inactive; //Utilização de Enum
        public List<string> Role { get; protected set; } = new List<string>(); //Utilização de lista
        public virtual AccessTo SystemsAccess { get; protected set; } = AccessTo.None;

        public UserModel(string firstName, string lastName, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Role.Add(role);
        }
        public UserModel(string firstName, string lastName, string role, Status status)
        {
            FirstName = firstName;
            LastName = lastName;
            Role.Add(role);
            Status = Status.Active;
        }
    }
    public enum Status
    {
        Active = 0,
        Inactive = 1
    }

    [Flags]
    public enum AccessTo //Utilizaçãao de Enum com Flags
    {
        SystemOne = 1,
        SystemTwo = 2,
        SystemThree = 4,
        SystemFour = 8,
        None = 16
    }
}