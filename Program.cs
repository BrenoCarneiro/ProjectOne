using ProjectOne.Models;

Dictionary<int, dynamic> appData = new Dictionary<int, dynamic>(); //Uso de Dictionary para armazenar os usuários
string[] roles = new string[] { "ADMIN", "INTERN" }; //Uso de array para armazenar os papéis
string firstName = string.Empty; //Declaração de tipo explícito
string lastName = string.Empty;
int userId = 1;

Console.WriteLine("Welcome!", Environment.NewLine);
Console.WriteLine("In this application, we will register users and their respective roles");
Console.WriteLine("First let's register the Admin user");
Console.Write("Please, enter your first name: ");
firstName = Console.ReadLine().ToUpper();
Console.Write("Enter your last name: ");
lastName = Console.ReadLine().ToUpper();

AdminModel adminUser = new(firstName, lastName, roles[0], Status.Active);

appData.Add(userId, adminUser);
Console.Clear();
Console.WriteLine("Select an option, type the respective number: ");
Console.WriteLine("1 - Add a new user");
Console.WriteLine("2 - Add a new role");
Console.WriteLine("3 - Quit");
var selectedOption = Console.ReadLine(); //Declaração de tipo implícito


if (selectedOption.Equals("1") && adminUser.SystemsAccess.HasFlag(AccessTo.SystemOne) ) //Uma forma de comparação entre Strings
{
    userId++;
    appData.Add(userId, adminUser.AddUser(appData, roles));
    Console.WriteLine("User created successfully", Environment.NewLine);
}

else if (selectedOption.Equals("2") && adminUser.SystemsAccess.HasFlag(AccessTo.SystemTwo)) //Utilizaçãao de Enum com Flags
{
    Array.Resize(ref roles, roles.Length + 1);
    roles[roles.Length - 1] = adminUser.AddRole(roles);
    Console.WriteLine("Role created successfully", Environment.NewLine);
}

while (true && (adminUser.Role[0] == "ADMIN"))  //Outra forma de comparação entre Strings
{
    Console.Clear();
    Console.WriteLine("Select an option, type the respective number: ");
    Console.WriteLine("1 - Add a new user");
    Console.WriteLine("2 - Add a new role");
    Console.WriteLine("3 - List of roles");
    Console.WriteLine("4 - List of users");
    Console.WriteLine("5 - Remove roles");
    Console.WriteLine("6 - Remove users");
    Console.WriteLine("7 - Quit");
    Console.Write("-> ");
    selectedOption = Console.ReadLine();
    Console.Clear();
    if (selectedOption.Equals("1"))
    {
        userId++;
        appData.Add(userId, adminUser.AddUser(appData, roles));
    }

    else if (selectedOption.Equals("2"))
    {
        Array.Resize(ref roles, roles.Length + 1);
        roles[roles.Length - 1] = adminUser.AddRole(roles);
    }

    else if (selectedOption.Equals("3"))
    {
        foreach (var role in roles) //Utilização de iteração com Foreach
        {
            Console.WriteLine("- " + role);
        }
        Console.WriteLine("Press enter to return to menu");
        Console.ReadLine();
    }

    else if (selectedOption.Equals("4"))
    {
        foreach (KeyValuePair<int, dynamic> kvp in appData)
        {
            Console.WriteLine("ID: {0}, Name: {1} {2}, Role: {3}, Status: {4}",
            kvp.Key, kvp.Value.FirstName, kvp.Value.LastName, kvp.Value.Role[0], kvp.Value.Status);
        }
        Console.WriteLine("Press enter to return to menu");
        Console.ReadLine();
    }
    else if (selectedOption.Equals("5"))
    {
        for (int i = 0; i < roles.Length; i++) //Utilização de iteração com For
        {
            Console.WriteLine($"{i + 1} - To remove: {roles[i]}");
        }
        Console.WriteLine($"{roles.Length + 1} - Quit");
        Console.Write("-> ");

        selectedOption = Console.ReadLine();

        int selectedRole;
        bool result = int.TryParse(selectedOption, out selectedRole);
        if (result)
        {
            selectedRole -= 1;
        }
        else
        {
                Console.WriteLine("Invalid option");
                Console.WriteLine("Press enter to return to menu");
                Console.ReadLine();
        }
            

        if ((selectedRole < roles.Length) && (selectedRole >= 0))
        {

            foreach (KeyValuePair<int, dynamic> kvp in appData)
            {
                List<string> newRoles = new List<string>(roles);
                newRoles.RemoveAt(selectedRole);

                if (kvp.Value.Role.Contains(roles[selectedRole]))
                {
                    Console.WriteLine($"The user {kvp.Value.FirstName} {kvp.Value.LastName} has that role");
                    Console.WriteLine("1 - Keep that way");
                    Console.WriteLine("2 - Delete the user");
                    Console.WriteLine("3 - Delete the role from the user");
                    Console.Write("-> ");
                    selectedOption = Console.ReadLine();

                    if (selectedOption == "1")
                    {
                        roles = newRoles.ToArray();
                        Console.Clear();
                    }
                    else if (selectedOption == "2")
                    {
                        roles = newRoles.ToArray();
                        appData.Remove(kvp.Key);
                        Console.WriteLine("User deleted successfully", Environment.NewLine);
                    }
                    else if (selectedOption == "3")
                    {
                        kvp.Value.Role[0] = "NONE";
                        appData[kvp.Key] = (dynamic)kvp;
                        roles = newRoles.ToArray();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option");
                        Console.WriteLine("Press enter to return to menu");
                        Console.ReadLine();
                    }
                }
            }

        }
        
    }

    else if (selectedOption.Equals("6"))
    {
        Console.Clear();
        Console.WriteLine("Remove Users");
        Console.WriteLine("Select an option, type the respective number: ");

        foreach (KeyValuePair<int, dynamic> kvp in appData)
        {
            Console.WriteLine("{0} - To remove the user: {1} {2}",
                kvp.Key, kvp.Value.FirstName, kvp.Value.LastName);
        }

        Console.WriteLine($"{appData.Count + 1} - Quit");
        Console.Write("-> ");

        selectedOption = Console.ReadLine();

        if (selectedOption.Equals((appData.Count + 1).ToString()))
            Console.Clear();
        else
        {
            bool keyExists = appData.ContainsKey(Convert.ToInt32(selectedOption));
            if (keyExists)
            {
                if (Convert.ToInt32(selectedOption) == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Are you sure you want to delete your username?");
                    Console.WriteLine("This will erase all data and exit the application");
                    Console.WriteLine("1 - Yes, remove");
                    Console.WriteLine("2 - Return to menu");
                    Console.Write("-> ");
                    selectedOption = Console.ReadLine();

                    if (selectedOption.Equals("1"))
                    {
                        adminUser.Shutdown();
                    }
                    else
                    {
                        Console.Clear();
                    }

                }
                appData.Remove(Convert.ToInt32(selectedOption));
                Console.WriteLine("User deleted successfully", Environment.NewLine);
            }
            else
            {
                Console.WriteLine("Invalid option");
                Console.WriteLine("Press enter to return to menu");
                Console.ReadLine();
            }
        }
    }


    else if (selectedOption.Equals("7"))
    {
        adminUser.Shutdown(out string message);
    }

    else
    {
        Console.WriteLine("Invalid option");
    }

}
Console.Clear();
Console.WriteLine("You no longer have permission");
Console.ReadLine();