using Humanizer;
using System.Globalization;
using System;

namespace ArtWarehouse.Services
{
    public class NumberToWords
    {
        public static string NumbersPriceToWords_Converter(double num)
        {
            double amount = num;

            int rubles = (int)Math.Truncate(amount);
            int kopeks = (int)Math.Round((amount - rubles) * 100);

            string rublesText = rubles.ToWords(new CultureInfo("ru-RU")) + " " + GetRublesWord(rubles);
            string kopeksText = kopeks.ToWords(new CultureInfo("ru-RU")) + " " + GetKopeksWord(kopeks);

            string firstLetter = rublesText.Substring(0, 1).ToUpper();
            string result = firstLetter + rublesText.Substring(1); 

            return $"{result} {kopeksText}";
        }

        static string GetRublesWord(int rubles) =>
            rubles % 10 == 1 && rubles % 100 != 11 ? "рубль" :
            rubles % 10 >= 2 && rubles % 10 <= 4 && (rubles % 100 < 10 || rubles % 100 >= 20) ? "рубля" : "рублей";

        static string GetKopeksWord(int kopeks) =>
            kopeks % 10 == 1 && kopeks % 100 != 11 ? "копейка" :
            kopeks % 10 >= 2 && kopeks % 10 <= 4 && (kopeks % 100 < 10 || kopeks % 100 >= 20) ? "копейки" : "копеек";

    }
}
