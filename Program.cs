using System;
using System.Collections.Generic;

class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<User> users = new List<User>
        {
            new User("Jose", "Gomes"),
            new User("Tatiana", "Isabel"),
            new User("Paulo", "Cunha")
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