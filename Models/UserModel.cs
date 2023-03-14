using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectOne.Models
{
    public class UserModel
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public Status Status { get; protected set; } = Status.Inactive;
        public List<string> Role { get; protected set; } = new List<string>();

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
    [Flags]
    public enum Status
    {
        Active = 0,
        Inactive = 1
    }
}