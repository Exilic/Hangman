using System;

namespace Hangman
{
    class Program
    {

        

        static void Main(string[] args)
        {
            var stayInGame = true;

            while (stayInGame)
            {
                Menu();
                stayInGame = UserMenuChoice(); // Is it okay to run the whole game through this informal looking line?
            }   
        }


        static void Menu()
        {
            Console.WriteLine("- Start a new game (g)");
            Console.WriteLine("- Quit (q)");
            Console.WriteLine("- Change the level of difficulty (1–3)");
        }


        static bool UserMenuChoice()
        {
            var stay = true;
            Console.Write("Choose what to do: ");
            var menuChoice = Convert.ToChar(Console.Read());

            switch (menuChoice)
            {
                case 'g':
                    GameTurn();
                    break;

                case 'q':
                    stay = false;
                    break;

                default:
                    break;
            }
            return stay;
        }


        static bool GameTurn()
        {
            var guessesLeft = 10;
            var gameOutcome = false;

            string hangmanWord = SecretWordPick();
            var hangmanLetters = new char[hangmanWord.Length];
            for (var i = 0; i < hangmanWord.Length; i++)
            {
                hangmanLetters[i] = '_';
            }

            while (guessesLeft > 8)
            {
                Console.WriteLine("You have {0} guesses left. Your guesses so far: ", guessesLeft);
                HangmanDrawing(11 - guessesLeft);
                guessesLeft--;
            }

            Console.ReadKey();

            return gameOutcome;
        }


        static void Subroutine(char[] hangmanLetters, string secretWord)
        {

            for (var j = 0; j < secretWord.Length; j++)
            {
                Console.Write("{0} ", hangmanLetters[j]);
            }

            hangmanLetters[0] = 'x';
        }

        static string SecretWordPick()
        {
            var possibleWords = new string[] { "steam", "identical", "boat", "undermine", "dog", "formal", "sync", "round", "bird", "catch", "truth" };
            var randomNumber = new Random();
            string theHangmanWord = possibleWords[randomNumber.Next(11)];
            return theHangmanWord;
        }

        static void HangmanDrawing(int hangStage)
        {
           
            switch (hangStage)
            {
                case 1:
                    Console.Write("\n\n\n\n\n\n         / \\\n________/   \\________");
                    break;
                case 2:
                    Console.Write("\n\n          |\n          |\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 3:
                    Console.WriteLine("\n          _______\n          |\n          |\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 4:
                    Console.WriteLine("\n          _______\n          | /\n          |/\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 5:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 6:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 7:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |     |\n          |\n         / \\\n________/   \\________");
                    break;
                case 8:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\n          |\n         / \\\n________/   \\________");
                    break;
                case 9:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\\\n          |\n         / \\\n________/   \\________");
                    break;
                case 10:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\\\n          |    /\n         / \\\n________/   \\________");
                    break;
                case 11:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\\\n          |    / \\\n         / \\\n________/   \\________");
                    break;
                default:
                    break;
            }

        }



        /*
        string hangmanWord = "Foreder";
        string guess = Console.ReadLine();

        if (guess.Length == 1){

        } else if (guess.Length == hangmanWord.Length){

        } else { Console.WriteLine("Guess a single letter or a word that is {0} long", hangmanWord.Length); }

        Console.WriteLine();


        Console.Write("Write a number from 1 to 10:");
        string Choice = Console.ReadLine();
        int level = Convert.ToInt32(Choice);

        HangmanBase(level);

        Console.Clear();

        for (int i = 0; i < 12; i++)
        {
            Console.Clear();
            HangmanPresentation(i);
            System.Threading.Thread.Sleep(1500);
        }*/


    }
}
