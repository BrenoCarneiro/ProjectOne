using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectOne.Models;

public class AdminModel : UserModel //Representação de herança entre classes
{
    //Utilizaçãao de Enum com Flags
    public override AccessTo SystemsAccess { get; protected set; } = AccessTo.SystemOne | AccessTo.SystemTwo | AccessTo.SystemThree | AccessTo.SystemFour; 
    public AdminModel(string firstName, string lastName, string role) : base(firstName, lastName, role) //Reutilizando o construtor da classe pai
    {

    }
    public AdminModel(string firstName, string lastName, string role, Status status) : base(firstName, lastName, role, status)
    {

    }
    public dynamic AddUser(Dictionary<int, dynamic> appData, string[] roles)
    {
        Console.Clear();
        Console.Write("Please, enter the first name: ");
        var firstName = Console.ReadLine().ToUpper();
        Console.Write("Enter the last name: ");
        var lastName = Console.ReadLine().ToUpper();

        Console.WriteLine("Select an option, type the respective number or name");
        Console.WriteLine("Roles available: ");
        for(int i  = 1; i <= roles.Length; i++)
        {
            Console.WriteLine($"{i} - {roles[i-1]}");
        }
        var role = Console.ReadLine();
        var searchRole = Array.Find(roles, x => x == role.ToUpper());

        if (String.Compare(role, searchRole, true) != 0)
        {
            int roleNumber;
            if (int.TryParse(role, out roleNumber))
                role = roles[roleNumber - 1];
            if(role == null)
            {
                Console.WriteLine("Invalid option: ");
                AddUser(appData, roles);
            }
                
        }
        dynamic newUser;
        if(role.Equals("ADMIN"))
            newUser = new AdminModel(firstName, lastName, role);
        else
            newUser = new UserModel(firstName, lastName, role);


        foreach (var data in appData)
        {
            if (this.FirstName == firstName && this.LastName == lastName && this.Role.Contains(role) )
            {
                Console.WriteLine($"The user {firstName + " " + lastName} with the {role} role already exists");
                Console.WriteLine("Select an option number: ");
                Console.WriteLine("1 - Try again");
                Console.WriteLine("2 - End application");
                var option = Console.ReadLine();
                if (option == "1")
                    AddUser(appData, roles);
                if (option == "2")
                    Environment.Exit(0);

            }
        }

        return newUser;

    }

    public string AddRole(string[] roles)
    {
        Console.Clear();
        Console.Write("Enter the new role name: ");
        string role = Console.ReadLine().ToUpper();
        foreach (string r in roles)
        {
            if(role.Equals(r))
            {
                Console.Write($"The {role} already exists");
                AddRole(roles);
            }

        }
        return role;

    }
    public void Shutdown(out string message) //Função com utilização de parâmetro Named e Out
    {
        message = "Shutting down the application...";
        Console.WriteLine(message);
        Thread.Sleep(5000);
        Environment.Exit(0);
    }

    public void Shutdown() //A mesma função anterior com utilização de parâmetro Optional
    {
        Environment.Exit(0);
    }

}