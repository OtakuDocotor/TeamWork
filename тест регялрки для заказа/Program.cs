﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using TempLib_V2;

namespace тест_регялрки_для_заказа
{

    class Program
    {

        public static List<string> Work = new List<string>();
        static void Main(string[] args)
        {
            //System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            //customCulture.NumberFormat.NumberDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            string pattern2 = @"[0-9]+/[0-9]+/[0-9]+\s[0-9]+:[0-9]+:[0-9]+.[0-9]+[;]+[0-9]+.[0-9]+\b";
            string pattern3 = @"[0-9].[0-9]+\sm_(TR|TDR)_[0-9]+_[0-9]+_[0-9]+"; // паттерн для имён файлов не проверял (´｡• ω •｡`) G
            string[] Arrr;
            List<string> LS = new List<string>();
            string[] s;
            double average = 0;
            double sum = 0;
            List<TRMesure> All_Mesures = new List<TRMesure>();
            List<TRMesure> Cutted_Mesures = new List<TRMesure>();
            Console.WriteLine("Найденные данные:");
            using (StreamReader sr = new StreamReader("0.60 m_TR_077222_20230419_1447.csv"))
            {

                string[] S = sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                MatchCollection Matches = null;
                Regex r = new Regex(@"[0-9]+/[0-9]+/[0-9]+\s[0-9]+:[0-9]+:[0-9]+.[0-9]+[;]+[0-9]+.[0-9]+\b");
                foreach (string q in S)
                {
                    Matches = r.Matches(q);
                    foreach (Match m in Matches)
                    {
                        string[] SubS = m.Value.Split(new char[] { ' ', ';', '/', ':', '.' }, StringSplitOptions.RemoveEmptyEntries);
                        All_Mesures.Add(new TRMesure(new DateTime(int.Parse(SubS[2]), int.Parse(SubS[1]), int.Parse(SubS[0]), int.Parse(SubS[3]), int.Parse(SubS[4]), int.Parse(SubS[5])), double.Parse(SubS[7])));
                        sum += All_Mesures.Last()._Temperature;
                    }
                }

            }
            Console.WriteLine("AVG: {0}", sum / All_Mesures.Count);
            average = sum / All_Mesures.Count;
            int n = All_Mesures.Count();
            Console.WriteLine("n={0}", n);
            foreach (TRMesure a in All_Mesures)
            {
                if (a._Temperature < average)
                {
                    Cutted_Mesures.Add(a);
                }
            }
            Cutted_Mesures.RemoveRange(0, 250);
            n = Cutted_Mesures.Count();
            Cutted_Mesures.RemoveRange(n - 1200, 1200);
            sum = 0;
            foreach (TRMesure a in Cutted_Mesures)
            {
                Console.WriteLine(a);
                sum += a._Temperature;
            }
            Console.WriteLine(sum / Cutted_Mesures.Count);
            Console.ReadKey();

        }
        public static void Find(string[] s, string pattern)
        {
            Work.Clear();
            MatchCollection Matches = null;
            Regex r = new Regex(pattern);
            foreach (string q in s)
            {
                Matches = r.Matches(q);
                foreach (Match m in Matches)
                {
                    Work.Add(m.Value);
                }
            }
        }
    }
}