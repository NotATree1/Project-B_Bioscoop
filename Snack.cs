using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

class Snack
{
    public string Name { get; }

    public int Price { get; }


    public Snack(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public static string DisplayShoppingCart(List<Snack> winkelWagen)
    {
        string Items = "Je winkelWagen\n";
        System.Console.WriteLine("");
        foreach (Snack snack in winkelWagen)
        {

            Items += $"{snack.Name}\n";
        };
        return Items;
    }

    private static void FoodOptionsToList(Winkelwagen winkelwagen)
    {
        List<Snack> Food = LoadFoodOptions();
        LoopOverSnacks(Food, winkelwagen);


    }

    private static List<Snack> LoadFoodOptions()
    {
        List<Snack> Snacks = new();
        Snack snack = new Snack("bueno", 30);
        Snack snack1 = new Snack("Lays Chips", 20);
        Snacks.Add(snack);
        Snacks.Add(snack1);
        return Snacks;

    }

    private static List<Snack> LoadDrinkOptions()
    {
        List<Snack> Snacks = new();
        Snack snack = new Snack("Fanta 1L", 30);
        Snack snack1 = new Snack("Cola 1L", 20);
        Snacks.Add(snack);
        Snacks.Add(snack1);
        return Snacks;

    }
    private static void DrinksOptionsToList(Winkelwagen winkelwagen)
    {
        List<Snack> Drinks = LoadDrinkOptions();
        LoopOverSnacks(Drinks, winkelwagen);

    }


    public static void ChooseToAddSnackOrNot()
    {
        string choice;
        do

        {
            System.Console.WriteLine("wil jij een snack toevoegen aan je reservering (ja/nee)");
            choice = Console.ReadLine()!;


        } while (choice != "ja" && choice != "nee");
        System.Console.WriteLine("out");

        if (choice == "ja")
        {
            Winkelwagen winkelwagen = new();
            List<string> options = new List<string> { };
            options.Add("eten");
            options.Add("drinken");

            SnackMainMenu(options, winkelwagen);
        }


    }
    private static void SnackMainMenu(List<string> options, Winkelwagen winkelwagen)
    {
        while (true)
        {
            int selectedIndex = 0;
            ConsoleKeyInfo keyInfo;
            string line = new string('=', Console.WindowWidth);

            do
            {
                Console.Clear();
                System.Console.WriteLine(line);

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.WriteLine("--> " + options[i]);
                    }
                    else
                    {
                        Console.WriteLine("    " + options[i]);
                    }
                }
                System.Console.WriteLine(line);
                System.Console.WriteLine(@"gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen
Druk op ESC om te vertrekken.
");
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
                {
                    selectedIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
                {
                    selectedIndex++;
                }
            } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                System.Console.WriteLine(" SSee you!");
                break;
            }

            Console.WriteLine("je hebt dit geselecteerd: " + options[selectedIndex]);
            Console.ReadKey();
            Console.Clear();

            if (options[selectedIndex] == "eten")
            {
                FoodOptionsToList(winkelwagen);
                Console.ReadKey();
            }
            else if (options[selectedIndex] == "drinken")
            {
                DrinksOptionsToList(winkelwagen);
                Console.ReadKey();

            }
        }

    }
    private static void LoopOverSnacks(List<Snack> options, Winkelwagen winkelwagen)
    {
        while (true)
        {
            int selectedIndex = 0;
            ConsoleKeyInfo keyInfo;
            string line = new string('=', Console.WindowWidth);

            do
            {
                Console.Clear();
                System.Console.WriteLine(line);

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.WriteLine("--> " + options[i].Name + " - " + "€" + options[i].Price);
                    }
                    else
                    {
                        Console.WriteLine("    " + options[i].Name);
                    }

                }
                System.Console.WriteLine(DisplayShoppingCart(winkelwagen.winkelWagen));
                System.Console.WriteLine(line);
                System.Console.WriteLine(@"gebruik WASD keys om je optie te selecteren druk daarna op Enter op je keuze te bevestigen
Druk op ESC om te vertrekken.
");
                keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
                {
                    selectedIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
                {
                    selectedIndex++;
                }
            } while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Spacebar);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                System.Console.WriteLine(" SSee you!");
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                Confirmation();
                break;
            }


            Console.WriteLine("je hebt dit geselecteerd: " + options[selectedIndex]);
            Console.ReadKey();
            Console.Clear();

            foreach (Snack snack in options)
            {
                if (options[selectedIndex].Name == snack.Name)
                {
                    System.Console.WriteLine(snack.Name);
                    winkelwagen.AddtoWinkelWagen(snack);
                    System.Console.WriteLine("is toegevoegd aan je winkelwagen");
                    Console.ReadKey();
                }
            }

        }

    }
    private static void Confirmation()
    {
        string choice;
        do

        {
            System.Console.WriteLine("ben je klaar met het kopen van Snacks? (ja/nee)");
            choice = Console.ReadLine()!;


        } while (choice != "ja" && choice != "nee");
        if (choice == "ja")
        {
            ChooseToAddSnackOrNot();
        }
    }
}
