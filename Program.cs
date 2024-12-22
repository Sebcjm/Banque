using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO.Compression;
using System.Text.Unicode;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Collections.Generic;

class Compte 
{
    #region Création de classe
    public string? Nom {get; set;} 
    public long? NCompte {get; set;}
    public double? SoldeInitial {get; set;}
    //private static HashSet<long> NCompteExistant = new HashSet<long>();
    List<string> historique = new List<string>();
    public Compte() {}

    public Compte(string? nom, long? ncompte, double? soldeinitial) 
    {
        this.Nom = nom;
        this.NCompte = ncompte;
        this.SoldeInitial = soldeinitial;
    }
        public virtual long GenerationNCompte()
    {
     string valeur = "";
     for (int i = 0; i < 10; i++)
     {
        Random x = new Random();
        int essai = (int)x.NextInt64(0,10);       
        valeur += essai.ToString();
     }
     return long.Parse(valeur);
    }
    #endregion
    public static Compte Creation_compte()
    {
        Console.WriteLine("Nom du compte : ");
        string? nom_compte = Console.ReadLine();

        Console.WriteLine("Solde initial : ");
        string? valeur = Console.ReadLine();
        double solde_compte = double.Parse(valeur);

        long numero_compte = new Compte().GenerationNCompte();

        Console.WriteLine($"Compte créé avec succès !");
        Console.WriteLine($"Nom : {nom_compte}, Numéro : {numero_compte}, Solde initial : {solde_compte}");

        return new Compte(nom_compte, numero_compte, solde_compte);
    }

    public virtual void Dépôt() 
    {
        Console.WriteLine($"Nom du compte : {Nom}"); Console.WriteLine($"Solde actuel : {SoldeInitial}");
        Console.WriteLine("Dépôt de : ");
        string? x = Console.ReadLine(); //input (input tjrs en string)

        double y = Double.Parse(x); //conversion de l'input qui est sous forme de chaîne de caractère en double et stockage dans la variable y
        int z = x.Length;
        SoldeInitial += y;
        Console.WriteLine($"Nouveau solde de {SoldeInitial}");
        var enregistrement = $"dépôt de {y} -> Solde = {SoldeInitial}";

        string affichage_historique = enregistrement.ToString();
        historique.Add(affichage_historique);
    }
    public virtual void Retrait() 
    {
        Console.WriteLine($"Nom du compte : {Nom}"); Console.WriteLine($"Solde actuel : {SoldeInitial}");
        Console.WriteLine("Retrait de :");
        string? x = Console.ReadLine();
        double? y = Double.Parse(x);
        SoldeInitial -= y;
        Console.WriteLine($"Nouveau solde :  {SoldeInitial}");  

        var enregistrement = $"Retrait de {y} -> Solde = {SoldeInitial}";
        historique.Add(enregistrement);   
    }
    /*public virtual void Virement()
    {
        Console.WriteLine(Nom);
    }*/

    public void Historique()
    {
        Console.WriteLine("Historique :");
        foreach (var trans in historique)
        {
            Console.WriteLine(trans);
        } 
        
    }

}
#region essai Sauvegarde
//test de sauvegarde (non fonctionnel)
/*static class Sauvegarde
{
    static private string _dossier = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    static private string _fichierBin = "clients.bin";
    static private string _cheminBin = Path.Combine(_dossier, _fichierBin);
    public static SauvegardeBin(ObservableCollection<Compte> comptes) 
    {
        using (StreamWriter fichier = new StreamWriter(_cheminBin, false, Encoding.UTF8, 1024))
        {
            
        }
    }
}*/
#endregion
[Serializable]
/*class Gérer_Exception : Exception
{
    throw new Gérer_Exception
}*/


class Program
{
        enum Fonctions_Compte
    {
        Creation_compte = 1,
        Dépôt = 2,
        Retrait = 3,
        //Virement = 4,
        Affichage_historique = 5,
    }
    static void Main(string[] args)
    {
        /*using (FileStream fs = new FileStream("compte.bin", FileMode.Create))
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Compte));
            serializer.WriteObject(fs);
        }

        // Désérialisation
        using (FileStream fs = new FileStream("compte.bin", FileMode.Open))
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Compte));
            Compte compteDeserialise = (Compte)serializer.ReadObject(fs);
            Console.WriteLine($"Nom: {compteDeserialise.Nom}, Numéro: {compteDeserialise.NCompte}, Solde: {compteDeserialise.SoldeInitial}");
        }*/
        Compte? compte = null; // Initialisation d'un compte
        bool continuer = true;

        while (continuer)
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1. Créer un compte");
            Console.WriteLine("2. Dépôt");
            Console.WriteLine("3. Retrait");
            Console.WriteLine("5. Historique");
            Console.Write("Choisissez une option : ");

            if (int.TryParse(Console.ReadLine(), out int choix) && Enum.IsDefined(typeof(Fonctions_Compte), choix))
            {
                Fonctions_Compte option = (Fonctions_Compte)choix;
                
                switch (option)
                {
                    case Fonctions_Compte.Creation_compte:
                    compte = Compte.Creation_compte();
                    break;

                    case Fonctions_Compte.Dépôt:
                        if (compte != null) 
                        {
                            compte.Dépôt();
                        }
                        break;
                    case Fonctions_Compte.Retrait:
                        if (compte != null)
                        {
                            compte.Retrait();
                        }
                        break;
                    case Fonctions_Compte.Affichage_historique:
                        if (compte != null)
                        {
                            compte.Historique();
                        }
                        break;
                }
            }
        
        /*{
            
            
            Compte compte1 = new Compte
            {
                Nom = "jean", 
                NCompte = 10000, 
                SoldeInitial = 118.50
            };
            compte1.Dépôt();
            compte1.Retrait();*/
        }
    }
}

//utiliser un enum pour créer un menu : 1. Créer un compte, 2. Mettre à jour mon compte, 3. Virement, 4. Dépôt, 5. Retrait ...