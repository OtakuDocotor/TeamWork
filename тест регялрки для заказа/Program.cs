﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace тест_регялрки_для_заказа
{

    class Program
    {
        
        public static List<string> Work = new List<string>();
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            string pattern2 = @"[0-9]+/[0-9]+/[0-9]+\s[0-9]+:[0-9]+:[0-9]+.[0-9]+\s[0-9]+.[0-9]+\b";
            string pattern3 = @"[0-9].[0-9]+\sm_(TR|TDR)_[0-9]+_[0-9]+_[0-9]+"; // паттерн для имён файлов не проверял (´｡• ω •｡`) G
            string[] Arrr;
            List<string> LS = new List<string>();
            string s;
            double average = 0;
            double sum = 0;
            Console.WriteLine("Найденные данные:");
            using (BinaryReader sr = new BinaryReader(File.Open("input.dat", FileMode.Open)))
            {
                while (sr.PeekChar()>0)
                {
                    s = sr.ReadString();
                    LS.Add(s);
                }
                Arrr = LS.ToArray();
                Find(Arrr, pattern2);
                foreach (string a in Work)
                {
                    string[] b = a.Split(new char[] { ' ' });
                    sum += double.Parse(b[2]);
                    Console.WriteLine(a);
                }
                average = sum / Work.Count;
            }
            //Console.WriteLine();
            Console.WriteLine("Среднее значение температуры:");
            Console.WriteLine(average);
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
