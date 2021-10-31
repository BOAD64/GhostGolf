using System;
using System.Collections.Generic;
using System.IO;

namespace GhostGolfServer
{
    public class FileIO
    {
        private static readonly string filePath = $"{Environment.CurrentDirectory}/GhostGolf.txt";
        private ServerUtil util;

        public FileIO()
        {
            this.util = new ServerUtil();

            if (!File.Exists(filePath))
            {
                StreamWriter writer = File.AppendText(filePath);
                writer.WriteLine("0 - 0");
                writer.WriteLine();
                writer.Close();
            }
        }

        public void UpdatePar(int score)
        {
            string[] content = File.ReadAllLines(filePath);

            try {
                Tuple<double, int> newPar = this.util.calculatePar(Double.Parse(content[0].Substring(0, content[0].IndexOf(" "))),
                    Int32.Parse(content[0].Substring(content[0].LastIndexOf(" "))), score);
                content[0] = $"{newPar.Item1} - {newPar.Item2}";
            } catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }

            File.WriteAllLines(filePath, content);
        }

        public bool TryUpdateHighscore(string userName, int score)
        {
            string[] content = File.ReadAllLines(filePath);
            for (int i = 0; i < content.Length; i++)
            {
                if (i > 1)
                {
                    if (content[i].Substring(0, content[i].IndexOf(" ")) == userName)
                    {
                        content[i] = $"{userName} {score}";
                        File.WriteAllLines(filePath, content);
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddHighscore(string userName, int score)
        {
            StreamWriter writer = File.AppendText(filePath);
            writer.WriteLine($"{userName} {score}");
            writer.Close();
        }

        public int GetPlacement(string userName)
        {
            string[] content = File.ReadAllLines(filePath);
            List<int> scores = new List<int>(content.Length - 2);
            int highScore = 0;
            try
            {
                for (int i = 0; i < content.Length; i++)
                {
                    if (i > 1)
                    {
                        scores.Add(Int32.Parse(content[i].Substring(content[i].LastIndexOf(" "))));
                        if (content[i].Substring(0, content[i].IndexOf(" ")) == userName)
                        {
                            highScore = Int32.Parse(content[i].Substring(content[i].LastIndexOf(" ")));
                        }
                    }
                    
                }
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            scores.Sort();
            return scores.IndexOf(highScore) + 1;
        }

        public int GetPar()
        {
            string[] content = File.ReadAllLines(filePath);
            double par = Double.Parse(content[0].Substring(0, content[0].IndexOf(" ")));

            return (int)par;
        }

        public int GetHighScore(string userName)
        {
            string[] content = File.ReadAllLines(filePath);
            int highScore = 0;
            for (int i = 0; i < content.Length; i++)
            {
                if (i > 1)
                {
                    if (content[i].Substring(0, content[i].IndexOf(" ")) == userName)
                    {
                        try
                        {
                            highScore = Int32.Parse(content[i].Substring(content[i].LastIndexOf(" ")));
                            break;
                        }
                        catch (InvalidCastException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            return highScore;
        }
    }
}
