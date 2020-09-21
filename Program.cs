using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {

        static void Main(string[] args)
        {
            var wordsFromFile = File.ReadAllText(@"/Users/tobiasengberg/Projects/Hangman/SecretWords.txt");
            string[] possibleWords = wordsFromFile.Split(',');
            
            var stayInGame = true;
            while (stayInGame)
            {
                Menu();
                stayInGame = UserMenuChoice(possibleWords);
            }   
        }


        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("HANGMAN");
            Console.WriteLine("- Start a new game (g)");
            Console.WriteLine("- Quit (q)");
        }


        static bool UserMenuChoice(string[] possibelWords)
        {
            var stay = true;
            Console.Write("Choose what to do: ");
            var menuChoice = Convert.ToChar(Console.Read());

            switch (menuChoice)
            {
                case 'g':
                    GameTurn(possibelWords);
                    break;

                case 'q':
                    stay = false;
                    break;

                default:
                    break;
            }
            return stay;
        }

      
        static void GameTurn(string[] possibelWords)
        {
            var guessesLeft = 10;
            var gameOutcome = false;
            StringBuilder incorrectLettersGuessed = new StringBuilder(0);

            string hangmanWord = SecretWordPick(possibelWords);
            var hangmanLetters = new char[hangmanWord.Length];
            for (var i = 0; i < hangmanWord.Length; i++)
            {
                hangmanLetters[i] = '_';
            }

            while (guessesLeft > 0)
            {
                Console.Clear();
                Console.Write("\nThe word to figure out:        ");
                for (var j = 0; j < hangmanWord.Length; j++)
                {
                    Console.Write("{0} ", hangmanLetters[j]);
                }
                Console.WriteLine("\n\nYou have {0} guesses left. Your faulty guesses so far: {1}", guessesLeft, incorrectLettersGuessed);

                HangmanDrawing(guessesLeft);

                var guessResult = UserGuessing(hangmanWord, hangmanLetters, incorrectLettersGuessed);
                if (guessResult.Item1){
                    guessesLeft--;
                }
                if (guessResult.Item2)
                {
                    Console.Write("Good guess!");
                    if (CheckUnderscore(hangmanLetters))
                    {
                        Console.WriteLine("You won!");
                        System.Threading.Thread.Sleep(1800);
                        gameOutcome = true;
                        guessesLeft = 0;
                    }
                    else if (guessesLeft == 0)
                    {
                        Console.WriteLine("But you still lost!!");
                        System.Threading.Thread.Sleep(1800);
                    }
                    else
                    {
                    }
                }
                if (guessResult.Item3) {
                    Console.WriteLine("You won!!!");
                    System.Threading.Thread.Sleep(1800);
                    gameOutcome = true;
                    guessesLeft = 0;
                }
            }
            if (gameOutcome){
                Console.Clear();
                HangmanSaved();
            } else {
                Console.Clear();
                HangmanDrawing(guessesLeft);
            }
        }

       
        static string SecretWordPick(string[] possibleWords)
        {
            var randomNumber = new Random();
            string hangmanWord = possibleWords[randomNumber.Next(possibleWords.Length)];
            return hangmanWord;
        }


        static (bool, bool, bool) UserGuessing(string hangmanWord, char[] hangmanLetters, StringBuilder incorrectLettersGuessed)
        {
            var correctLetter = false;
            var correctWord = false;
            var correctGuessForm = true;
            string userGuess = Console.ReadLine();

            if (ComposedOfLetters(userGuess)){
                if (userGuess.Length == 1){
                    correctLetter = LetterGuess(userGuess, hangmanWord, hangmanLetters);
                    if (incorrectLettersGuessed.ToString().Contains(userGuess))
                    {
                        Console.WriteLine("You have already guessed that letter!");
                        System.Threading.Thread.Sleep(2200);
                        correctGuessForm = false;
                    } else if (correctLetter == false){
                        incorrectLettersGuessed.Append(userGuess);
                    } else {
                    }
                } else if (userGuess.Length == hangmanWord.Length){
                    correctWord = WordGuess(userGuess, hangmanWord);
                } else {
                    Console.WriteLine("Guess a single letter or a word that is {0} letters long", hangmanWord.Length);
                    System.Threading.Thread.Sleep(1800);
                    correctGuessForm = false;
                }
            } else {
                Console.WriteLine("Please, use letters only");
                System.Threading.Thread.Sleep(1800);
                correctGuessForm = false;
            }
            return (correctGuessForm, correctLetter, correctWord);
        }


        static bool LetterGuess(string userGuess, string hangmanWord, char[] hangmanLetters)
        {
            var correctLetter = false;
            for (var i = 0; i < hangmanWord.Length; i++){
                if (userGuess.Equals(char.ToString(hangmanWord[i])))
                {
                    hangmanLetters[i] = char.Parse(userGuess);
                    correctLetter = true;
                }
            }
            return correctLetter;
        }


        static bool WordGuess(string userGuess, string hangmanWord)
        {
            var correctWord = false;
            if (userGuess.Equals(hangmanWord)){
                correctWord = true;
            }
            return correctWord;
        }


        static bool CheckUnderscore(char[] hangmanLetters)
        {
            var noUnderscore = true;
            for (var i = 0; i < hangmanLetters.Length; i++)
            {
                if (hangmanLetters[i].Equals('_'))
                {
                    noUnderscore = false;
                }
            }
            return noUnderscore;
        }


        static bool ComposedOfLetters(string userEntry)
        {
            var justLetters = true;
            for (var i = 0; i < userEntry.Length; i++){
                if (Char.IsLetter(userEntry[i])){
                } else {
                    justLetters = false;
                }
            }
            return justLetters;
        }


        static void HangmanDrawing(int hangStage)
        {
            switch (hangStage)
            {
                case 10:
                    Console.WriteLine("\n\n\n\n\n\n         / \\\n________/   \\________");
                    break;
                case 9:
                    Console.WriteLine("\n\n          |\n          |\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 8:
                    Console.WriteLine("\n          _______\n          |\n          |\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 7:
                    Console.WriteLine("\n          _______\n          | /\n          |/\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 6:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 5:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |\n          |\n         / \\\n________/   \\________");
                    break;
                case 4:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |     |\n          |\n         / \\\n________/   \\________");
                    break;
                case 3:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\n          |\n         / \\\n________/   \\________");
                    break;
                case 2:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\\\n          |\n         / \\\n________/   \\________");
                    break;
                case 1:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\\\n          |    /\n         / \\\n________/   \\________");
                    break;
                case 0:
                    Console.WriteLine("\n          _______\n          | /   |\n          |/    O\n          |    /|\\\n          |    / \\\n         / \\\n________/   \\________");
                    break;
                default:
                    break;
            }
        }


        static void HangmanSaved()
        {
            Console.WriteLine("\n          _______\n          | /   |\n          |/\n          |       \\O/\n          |        |\n         / \\       /\\\n________/   \\________");
            Console.WriteLine("\nHangman saved");
            System.Threading.Thread.Sleep(5300);
        }
    }
}
