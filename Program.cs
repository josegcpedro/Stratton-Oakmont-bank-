using System;
using System.Collections.Generic;

class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Balance { get; set; }

    public User(string firstName, string lastName, decimal initialBalance = 0)
    {
        FirstName = firstName;
        LastName = lastName;
        Balance = initialBalance;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<User> users = new List<User>
        {
            new User("Jose", "Gomes",900),
            new User("Tatiana", "Isabel",1230),
            new User("Paulo", "Cunha",5000)
        };

        Console.WriteLine("Hello, welcome to Statton Oakmont bank!");
        Console.Write("Please, tell me your first name: ");
        string firstName = Console.ReadLine();
        
        Console.Write("Please, tell me your last name: ");
        string lastName = Console.ReadLine();

        bool userExists = false;
        foreach (var user in users)
        {
            if (user.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                user.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
            {
                userExists = true;
                break;
            }
        }

        if (userExists)
        {
            Console.WriteLine($"Welcome back {firstName} {lastName}!");
            Console.WriteLine("Your balance is: {0:C}", users.Find(x => x.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) && x.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)).Balance);
        }
        else
        {
            Console.WriteLine("User not found.");
            Console.WriteLine("Do you want to create a new account? (Y/N)");
            string answer = Console.ReadLine();
            
            if (answer.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                users.Add(new User(firstName, lastName));
                Console.WriteLine("Account created successfully!");
            }
            else
            {
                Console.WriteLine("Goodbye!");
            }
        }
    }
}