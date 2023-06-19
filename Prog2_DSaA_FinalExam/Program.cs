using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prog2_DSaA_FinalExam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] windowSize = { 120, 30 };
            string word = "";
            int[] letterLoc = new int[] { };
            bool doneAnim = false;
            int stagCount = 0;

            Console.SetWindowSize(windowSize[0], windowSize[1]);
            word = takeUserInput(windowSize[0]);
            letterLoc = new int[word.Length];
            letterLoc = initializeLetterLocations(letterLoc);
            displayLetters(word, letterLoc);

            while (!doneAnim)
            {
                Console.Clear();
                if (stagCount < word.Length)
                    stagCount++;
                letterLoc = updateLetterLocation(stagCount, letterLoc, windowSize[1] - 1);
                displayLetters(word, letterLoc);
                Thread.Sleep(100);
                doneAnim = animationFinished(letterLoc, windowSize[1] - 1);
            }

            Console.SetCursorPosition(0, windowSize[1] - 1);
            Console.Write("Done");
            Console.ReadKey();
        }

        static string takeUserInput(int maxLen)
        {
            string uInput = "";

            Console.Clear();
            Console.WriteLine("Please input a string that is 1 to {0} characters long: ", maxLen);
            uInput = Console.ReadLine();
            if (uInput.Length < 1 || uInput.Length > maxLen)
            {
                Console.WriteLine("Input string is longer than instructed. Press any key to try again...");
                Console.ReadKey();
                uInput = takeUserInput(maxLen);
            }

            return uInput;
        }

        static int[] initializeLetterLocations(int[] loc)
        {
            for (int x = 0; x < loc.Length; x++)
                loc[x] = 0;

            return loc;
        }

        static int[] updateLetterLocation(int sequenceNumber, int[] letterLoc, int windowHeight)
        {
            for(int x = 0; x < sequenceNumber; x++)
            {
                if (letterLoc[x] != windowHeight - 1)
                    letterLoc[x] = letterLoc[x] + 1;
            }

            return letterLoc;
        }

        static void displayLetters(string word, int[] letterLoc) 
        {
            for(int x = 0; x < letterLoc.Length;x++)
            {
                Console.SetCursorPosition(x, letterLoc[x]);
                Console.Write(word[x]);
            }
        }

        static bool animationFinished(int[] letterLoc, int windowHeight)
        {
            for(int x =0; x < letterLoc.Length; x++)
            {
                if (letterLoc[x] != windowHeight - 1)
                    return false;
            }

            return true;
        }
    }
}
