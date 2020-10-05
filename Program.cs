using System;
using System.Collections.Generic;
using System.Linq;
using UnscrambleProj3.Data;
using UnscrambleProj3.Workers;

namespace UnscrambleProj3
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private static object wordListFileName;

        //   private static string wordListFileName;

        // public const string wordListFileName = "wordList.txt";

        // private const string wordListFileName = "wordList.txt";
        static void Main(string[] args)
        {
            try
            {
                bool continueWordUnscramble = true;
                do
                {

                    Console.WriteLine(Constants.optionOnHowToEnterScrambleWords);
                    var option = Console.ReadLine() ?? string.Empty;

                    switch (option.ToUpper())
                    {
                        case Constants.File:
                            Console.WriteLine(Constants.EnterScrambledWordsViaFile);
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        case Constants.Manual:
                            Console.WriteLine(Constants.EnterScrambledWordsManually);
                            ExecuteScrambleWordsManualEntryScenario();
                            break;
                        default:
                            Console.WriteLine(Constants.EnterScrambledWordOptionNotRecognized);
                            break;
                    }

                    var continueDecision = string.Empty;
                    do
                    {
                        Console.WriteLine(Constants.OptionsOnContinuingTheProgram);
                        continueDecision = (Console.ReadLine() ?? string.Empty);
                    } while (!continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                                 !continueDecision.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

                    continueWordUnscramble = continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordUnscramble);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorProgramWillBeTerminated + ex.Message);
            }
        }
        private static void ExecuteScrambleWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            try
            {
                var filename = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read((string)filename);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambleWordsCannotBeLoaded + ex.Message);
            }
            ////file reading  previous code AND  top using constants
            //var filename= Console.ReadLine() ?? string.Empty;
            //string[ ]  scrambledWords  = _fileReader.Read((string)filename);
            //DisplayMatchedUnscrambledWords(scrambledWords);

        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string wordList = _fileReader.Read(wordListFileName);

            List<matchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);
            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine(Constants.MatchFound, matchedWord.scrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.MatchNotFound);

            }
        }



    }

    internal class matchedWord
    {
        internal readonly int scrambledWord;

        public int Word { get; internal set; }
    }
}
