using System;
using System.Collections.Generic;

class Banque
{
    private List<Client> clients; // List of clients
    private List<CompteBancaire> comptesBancaires; // List of bank accounts

    public Banque()
    {
        clients = new List<Client>();
        comptesBancaires = new List<CompteBancaire>();
    }

    public Client EnregistrerClient(string prenom, string nom)
    {
        var client = new Client(prenom, nom);
        if (!clients.Contains(client))
        {
            clients.Add(client);
        }
        return client;
    }

    public CompteBancaire CreerCompte(string prenom, string nom)
    {
        var client = EnregistrerClient(prenom, nom);
        var compte = new CompteBancaire(client);
        comptesBancaires.Add(compte);
        return compte;
    }

    public void ObtenirResumeClient(Client client)
    {
        var comptesClient = comptesBancaires.FindAll(a => a.Proprietaire.Equals(client));
        Console.WriteLine($"--------------\n{client.Prenom} {client.Nom} :");
        decimal fortuneTotale = 0;
        for (int i = 0; i < comptesClient.Count; i++)
        {
            Console.WriteLine($"- Compte #{comptesClient[i].IdCompte} : {comptesClient[i].Solde}");
            fortuneTotale += comptesClient[i].Solde;
        }
        Console.WriteLine($"Fortune totale : {fortuneTotale}");
    }

    public CompteBancaire ObtenirCompteParId(int idCompte)
    {
        return comptesBancaires.Find(a => a.IdCompte == idCompte);
    }

    public List<CompteBancaire> ObtenirComptes(Client client)
    {
        return comptesBancaires.FindAll(a => a.Proprietaire.Equals(client));
    }
}

class Client
{
    public string Prenom { get; private set; }
    public string Nom { get; private set; }

    public Client(string prenom, string nom)
    {
        Prenom = prenom;
        Nom = nom;
    }

    public override bool Equals(object obj)
    {
        if (obj is Client client)
        {
            return Prenom == client.Prenom && Nom == client.Nom;
        }
        return false;
    }
}

class CompteBancaire
{
    public int IdCompte { get; private set; }
    public Client Proprietaire { get; private set; }
    public decimal Solde { get; private set; }

    private static int prochainIdCompte = 1;

    public CompteBancaire(Client proprietaire)
    {
        IdCompte = prochainIdCompte++;
        Proprietaire = proprietaire;
        Solde = 0;
    }

    public bool Crediter(decimal montant)
    {
        if (montant <= 0) return false;
        Solde += montant;
        Console.WriteLine($"Compte #{IdCompte} de {Proprietaire.Prenom} {Proprietaire.Nom} : +{montant}");
        return true;
    }

    public bool Debiter(decimal montant)
    {
        if (montant <= 0 || montant > Solde)
        {
            Console.WriteLine($"Compte #{IdCompte} de {Proprietaire.Prenom} {Proprietaire.Nom} : solde insuffisant (-{montant})");
            return false;
        }
        Solde -= montant;
        Console.WriteLine($"Compte #{IdCompte} de {Proprietaire.Prenom} {Proprietaire.Nom} : -{montant}");
        return true;
    }

    public bool Transferer(decimal montant, CompteBancaire compteDestination)
    {
        if (Debiter(montant))
        {
            compteDestination.Crediter(montant);
            Console.WriteLine($"Transfert du compte #{IdCompte} de {Proprietaire.Prenom} {Proprietaire.Nom} au compte #{compteDestination.IdCompte} de {compteDestination.Proprietaire.Prenom} {compteDestination.Proprietaire.Nom} réussi : {montant}");
            return true;
        }
        Console.WriteLine($"Transfert du compte #{IdCompte} de {Proprietaire.Prenom} {Proprietaire.Nom} au compte #{compteDestination.IdCompte} de {compteDestination.Proprietaire.Prenom} {compteDestination.Proprietaire.Nom} échoué: solde insuffisant ({montant})");
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Banque banque = new Banque();

        Console.WriteLine("Bienvenue à la banque!");
        Console.Write("Entrez votre prénom: ");
        string prenom = Console.ReadLine();

        Console.Write("Entrez votre nom: ");
        string nom = Console.ReadLine();

        Client client = banque.EnregistrerClient(prenom, nom);

        while (true)
        {
            Console.WriteLine("\nChoisissez une option:");
            Console.WriteLine("1. Créer un nouveau compte");
            Console.WriteLine("2. Voir le solde de tous les comptes");
            Console.WriteLine("3. Créditer un compte");
            Console.WriteLine("4. Débiter un compte");
            Console.WriteLine("5. Transférer de l'argent");
            Console.WriteLine("6. Voir le résumé du client");
            Console.WriteLine("7. Quitter");

            string choix = Console.ReadLine();
            switch (choix)
            {
                case "1":
                    banque.CreerCompte(prenom, nom);
                    Console.WriteLine("Compte créé avec succès.");
                    break;
                case "2":
                    var comptes = banque.ObtenirComptes(client);
                    foreach (var compte in comptes)
                    {
                        Console.WriteLine($"Compte #{compte.IdCompte} : {compte.Solde}");
                    }
                    break;
                case "3":
                    Console.Write("Entrez l'ID du compte à créditer: ");
                    int idCompteCredit = int.Parse(Console.ReadLine());
                    var compteCredit = banque.ObtenirCompteParId(idCompteCredit);
                    if (compteCredit != null)
                    {
                        Console.Write("Entrez le montant à créditer: ");
                        decimal montantCredit = decimal.Parse(Console.ReadLine());
                        compteCredit.Crediter(montantCredit);
                    }
                    else
                    {
                        Console.WriteLine("Compte non trouvé.");
                    }
                    break;
                case "4":
                    Console.Write("Entrez l'ID du compte à débiter: ");
                    int idCompteDebit = int.Parse(Console.ReadLine());
                    var compteDebit = banque.ObtenirCompteParId(idCompteDebit);
                    if (compteDebit != null)
                    {
                        Console.Write("Entrez le montant à débiter: ");
                        decimal montantDebit = decimal.Parse(Console.ReadLine());
                        compteDebit.Debiter(montantDebit);
                    }
                    else
                    {
                        Console.WriteLine("Compte non trouvé.");
                    }
                    break;
                case "5":
                    Console.Write("Entrez l'ID du compte source: ");
                    int idCompteSource = int.Parse(Console.ReadLine());
                    var compteSource = banque.ObtenirCompteParId(idCompteSource);
                    if (compteSource != null)
                    {
                        Console.Write("Entrez l'ID du compte de destination: ");
                        int idCompteDestination = int.Parse(Console.ReadLine());
                        var compteDestination = banque.ObtenirCompteParId(idCompteDestination);
                        if (compteDestination != null)
                        {
                            Console.Write("Entrez le montant à transférer: ");
                            decimal montantTransfert = decimal.Parse(Console.ReadLine());
                            compteSource.Transferer(montantTransfert, compteDestination);
                        }
                        else
                        {
                            Console.WriteLine("Compte de destination non trouvé.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Compte source non trouvé.");
                    }
                    break;
                case "6":
                    banque.ObtenirResumeClient(client);
                    break;
                case "7":
                    Console.WriteLine("Merci d'avoir utilisé nos services. Au revoir!");
                    return;
                default:
                    Console.WriteLine("Option invalide. Veuillez réessayer.");
                    break;
            }
        }
    }
}