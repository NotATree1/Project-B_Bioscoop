public class ChooseMovie
{
    public static void Films_kiezen(Customer? currentCustomer)
    {
        MovieWriteAndLoad film_menu = new("Movies.json");

        List<string> options = new() { "Sorteer en filter opties\n" };
        List<Film> Movies = film_menu.ReadFilms();
        TimeSpan timeGap = TimeSpan.FromHours(0);

        // check hier maken voor films die later dan vandaag te zien is op de bioscoop zodat je niet naar oude films gaat
        // die niet meer te zien is

        // en confirmatie code - film tijd wanneer het te zien is als het meer dan 2 uur is kan je het annuleren minder dan 2 weer niet
        List<Film> MoviesAfterFilter = FilterMovies.MoviesInTheFuture(Movies, timeGap, DateTime.Now);



        foreach (var movie in MoviesAfterFilter)
        {
            options.Add(movie.Title);
        }

        ConsoleKeyInfo keyInfo;
        int selectedIndex = 0;

        do
        {
            Display(options, selectedIndex);
            keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape) return;
            HandleUserInput(keyInfo, options, ref selectedIndex, currentCustomer);
        } while (keyInfo.Key != ConsoleKey.Enter);

        HandleSelecedOption(currentCustomer, MoviesAfterFilter, options, selectedIndex);
    }

    public static void Display(List<string> options, int selectedIndex)
    {
        string line = new string('=', Console.WindowWidth);
        Console.Clear();
        Console.WriteLine(line);
        BasicMenu.CreateTitleASCII();
        Console.WriteLine(line);
        Console.WriteLine("Film kiezen om te bekijken:\n");

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
        Console.WriteLine(line);
    }


    public static void HandleUserInput(ConsoleKeyInfo keyInfo, List<string> options, ref int selectedIndex, Customer? currenCustomer)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.W or ConsoleKey.UpArrow:
                if (selectedIndex > 0) selectedIndex--;
                break;
            case ConsoleKey.S or ConsoleKey.DownArrow:
                if (selectedIndex < options.Count - 1) selectedIndex++;
                break;
            case ConsoleKey.Escape:
                return;
        }
    }

    public static void HandleSelecedOption(Customer? currentCustomer, List<Film> movies, List<string> options, int selectedIndex)
    {
        if (selectedIndex >= 1 && selectedIndex < options.Count)
        {
            MovieWriteAndLoad.printfilmInfo(movies[selectedIndex - 1]);
            System.Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
            ConfirmMovieSelection(currentCustomer, options[selectedIndex]);
        }
        else if (selectedIndex == 0)
        {
            SortedMovies.ViewSortOptions(currentCustomer, movies);
        }
    }

    public static void InputTitleToSearch(Customer currentCustomer, List<Film> movies)
    {
        Console.WriteLine("Druk Enter en laat het veld leeg om terug te gaan \nType de titel van de film om op te zoeken:");
        string title = Console.ReadLine()!;
        if (!IsMovieFound(title, movies, currentCustomer)) Films_kiezen(currentCustomer);

    }

    public static bool IsMovieFound(string title, List<Film> movies, Customer currentCustomer)
    {
        if (string.IsNullOrEmpty(title)) return false;

        string foundMovie = SearchTitleThroughMovies(title, movies);

        if (foundMovie != null && foundMovie.ToLower() == title.ToLower())
        {
            ConfirmMovieSelection(currentCustomer, foundMovie);
            return true;
        }
        else
        {
            if (ChooseToKeepSearching()) InputTitleToSearch(currentCustomer, movies);
            else Films_kiezen(currentCustomer);
            return false;
        }
    }

    public static bool ChooseToKeepSearching()
    {
        string choice;
        do
        {
            Console.WriteLine("Titel niet gevonden");
            Console.WriteLine("Wil je verder zoeken? \n\nType 'j' voor ja en 'n' voor nee");
            choice = Console.ReadLine()!;
            if (choice.ToLower() == "j")
            {
                return true;
            }
            else if (choice.ToLower() == "n")
            {
                return false;
            }
        }
        while (choice.ToLower() != "j" && choice.ToLower() != "n");
        return false;
    }

    public static string SearchTitleThroughMovies(string title, List<Film> Movies)
    {
        foreach (var movie in Movies)
        {
            if (title.ToLower() == movie.Title.ToLower())
            {
                MovieWriteAndLoad.printfilmInfo(movie);
                Console.WriteLine("Druk op Enter om stoelen te reserveren voor deze film \nDruk een ander willekeurige toets om terug te gaan naar de vorige pagina");
                return movie.Title;
            }
        }
        return null!;
    }
    public static void ConfirmMovieSelection(Customer currentCustomer, string MovieTitle)
    {
        ConsoleKeyInfo keyInfo;
        keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            AuditoriumMap150 map500 = new AuditoriumMap150();
            map500.TakeSeats(MovieTitle, currentCustomer, false);
        }
        else if (keyInfo.Key != ConsoleKey.Enter)
        {
            // Films_kiezen(currentCustomer);
            // infinite ?
            // ja was infinite 
        }
    }


}