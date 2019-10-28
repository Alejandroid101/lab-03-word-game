using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to the Guessing game!");
                string[] words = FileSetup();
                UserInterface(words);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.Write("Play again soon!");
            }
        }
        public static void UserInterface(string[] words)
        {
            Console.WriteLine("1.)View all available words.");
            Console.WriteLine("2.)Add a word to the list.");
            Console.WriteLine("3.)Remove a word from the list.");
            Console.WriteLine("4.)PLAY THE GAME!");
            Console.WriteLine("5.)Exit");

            int userChoice;

            while(true)
            {
                string input = Console.ReadLine();
                userChoice = Convert.ToInt32(input);
                if (userChoice >= 1 && userChoice <= 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }
            }

            switch(userChoice)
            {
                case 1:
                    ViewWords(words);
                    break;
                case 2:
                    AddWord();
                    break;
                case 3:
                    RemoveWord();
                    break;
                case 4:
                    StartGame(words);
                    break;
                default:
                    Console.WriteLine();
                    break;
            }


        }

        //Done
        static string[] FileSetup()
        {
            string filePath = "../../../wordsFile.txt";
            string[] starterFile = new string[1] { "papaya" };
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    for (int i = 0; i < starterFile.Length; i++)
                    {
                        sw.WriteLine(starterFile[i]);
                    }
                }
            }
            string[] availableWords = File.ReadAllLines(filePath);
            return availableWords;

        }

        public static void ViewWords(string[] words)
        {
            Console.WriteLine("View Words!");
            string filePath = "../../../wordsFile.txt";
            using (StreamReader stringreader = File.OpenText(filePath))
            {
                string readingWords;
                while ((readingWords = stringreader.ReadLine()) != null)
                {
                    Console.WriteLine(readingWords);
                }
            }
            UserInterface(words);
        }

        public static void RemoveWord()
        {
            Console.WriteLine("Remove words!");
            string filePath = "../../../wordsFile.txt";
            using (StreamReader stringreader = File.OpenText(filePath))
            {
                string readingWords;
                while ((readingWords = stringreader.ReadLine()) != null)
                {
                    Console.WriteLine(readingWords);
                }
            }

            while(true)
            {
                Console.WriteLine("Write 'done' to finish removing words");
                Console.Write("Enter word to remove:");

                string remove = Console.ReadLine();

                if(remove == "done")
                {
                    break;
                }
                else
                {
                    string[] oldList = File.ReadAllLines(filePath);
                    string[] newList = new string[oldList.Length - 1];

                    int j = 0;
                    for (int i = 0; i < oldList.Length; i++)
                    {
                        if (remove != oldList[i]){
                            newList[j] = oldList[i];
                            j++;
                        }
                    }
                    File.Delete(filePath);

                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        for (int i = 0; i < newList.Length; i++)
                        {
                            sw.WriteLine(newList[i]);
                        }
                    }

                    Console.WriteLine("Updated list!");
                    using(StreamReader sr = File.OpenText(filePath))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }
                }
            }
            string[] words = FileSetup();
            UserInterface(words);
        }
        public static void AddWord()
        {
            Console.WriteLine("Add word!");

            string filePath = "../../../wordsFile.txt";
            using (StreamReader stringreader = File.OpenText(filePath))
            {
                string readingWords;
                while ((readingWords = stringreader.ReadLine()) != null)
                {
                    Console.WriteLine(readingWords);
                }
            }
            while (true)
            {
                Console.WriteLine("Write 'done' to finish adding words");
                Console.Write("Enter word to add to list:");

                string addWord = Console.ReadLine();

                if (addWord == "done")
                {
                    break;
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(addWord);
                    }

                    Console.WriteLine();
                }

            }
            string[] words = FileSetup();
            UserInterface(words);
        }
        public static void StartGame(string[] words)
        {
            Console.WriteLine("Guess the word!!");

            Random rdmWord = new Random();
            int rdmIndex = rdmWord.Next(0, words.Length - 1);
            string guessing = words[rdmIndex];

            //making List to hold guesses, then arrays for the letters in word and then one for the underscores.
            List<string> guesses = new List<string>();
            char[] letters = guessing.ToCharArray();
            char[] underScore = new char[letters.Length];

            //populate array with _ _ _ _.
            for (int i = 0; i < underScore.Length; i++)
            {
                underScore[i] = '_';
            }

            bool playing = true;
            while(playing)
            {
                Console.Clear();
                for (int i = 0; i < underScore.Length; i++)
                {
                    Console.Write($"{underScore[i]} ");
                }
                Console.WriteLine();
                Console.Write("Guesses: [");
                foreach(var letter in guesses)
                {
                    Console.Write($"{letter} ");
                }
                Console.Write("]");
                Console.WriteLine();

                Console.WriteLine("Guess a letter. One at a time!!");
                string inputLetter = Console.ReadLine();

                if(inputLetter.Length > 1)
                {
                    Console.WriteLine("One letter at a time!");
                }
                else if(inputLetter.Length == 1)
                {
                    //all of this happens if the input is only ome character (one letter at a time).
                    guesses.Add(inputLetter);
                    char character = char.Parse(inputLetter);

                    for (int i = 0; i < underScore.Length; i++)
                    {
                        if(letters[i] == character)
                        {
                            underScore[i] = character;
                        }
                    }
                    for (int i = 0; i < underScore.Length; i++)
                    {
                        string s = new string(underScore);
                        if (!s.Contains('_'))
                        {
                            playing = false;

                        }
                    }
                    if(!playing)
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Awesome, you guessed the word!!");
                    }
                }


            }
            UserInterface(words);

        }
    }
}
