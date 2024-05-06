using System;
using System.Collections.Generic;

namespace WordToNumberConverter
{
    internal class WordToNumberConverter
    {
        static Dictionary<string, int> getNumberValue = new Dictionary<string, int>()
        {
            { "null", 0 }, { "ein", 1 }, { "eins", 1 }, { "eine", 1 }, { "zwei", 2 }, { "drei", 3 }, { "vier", 4 }, { "fünf", 5 }, { "sechs", 6 },
            { "sieben", 7 }, { "acht", 8 }, { "neun", 9 }, { "zehn", 10 }, { "elf", 11 }, { "zwölf", 12 }, { "dreizehn", 13 },
            { "zwanzig", 20 }, { "dreißig", 30 }, { "vierzig", 40 }, { "fünfzig", 50 }, { "sechzig", 60 }, { "siebzig", 70 }, { "achtzig", 80 }, { "neunzig", 90 },
            { "hundert", 100 }, { "tausend", 1000 }, { "million", 1000000 }, { "millionen", 1000000}
};


        static void Main(string[] args)
        {
            List<string> inputs = new List<string>()
            {
                "eins",
                "dreizehn",
                "zwanzig",
                "vierunddreißig",
                "fünfhundert",
                "sechshunderteins",
                "eintausendsechshundertvier",
                "sechsunddreißigtausendvierundzwanzig",
                "fünfhundertfünfundfünfzigtausendfünfhundertfünfundfünfzig",
                "einemillion",
                "dreihunderteinundzwanzigmillionenfünfhundertachtundvierzigtausenddreihundertzwölf",
                "neunhundertmillionenzwei"
            };

            foreach (string input in inputs)
            {
                Console.WriteLine(input + " = " + ConvertToNumber(input));
            }
        }

        static long ConvertToNumber(string input)
        {
            long result = 0;
            string word = "";
            bool hundertFound = false;
            bool tausendFound = false;
            bool millionFound = false;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                word = input[i] + word;
                if (getNumberValue.TryGetValue(word, out int value)){
                    word = "";
                    if (value == 100)
                    {
                        hundertFound = true;
                        continue;
                    }
                    else if (value == 1_000)
                    {
                        tausendFound= true;
                        continue;
                    }
                    else if (value == 1_000_000)
                    {
                        millionFound = true;
                        continue;
                    }
                    if (hundertFound)
                    {
                        if (tausendFound)
                        {
                            result += value * 100_000;
                            tausendFound = false;
                            hundertFound = false;
                        }
                        else if (millionFound)
                        {
                            result += value * 100_000_000;
                            millionFound = false;
                            hundertFound = false;
                        }
                        else
                        {
                            result += value * 100;
                            hundertFound = false;
                        }
                    }
                    else if (tausendFound)
                    {
                        result += value * 1_000;
                    }
                    else if (millionFound)
                    {
                        result += value * 1_000_000;
                    }
                    else
                    {
                        result += value;
                    }
                }
                if (word.Length == 3)
                {
                    if (word.Equals("und"))
                    {
                        word = "";
                        continue;
                    }
                }
            }
            return result;
        }
    }
}
