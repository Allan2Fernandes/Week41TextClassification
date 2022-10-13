using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TextClassification.Business
{

    public class Tokenization
    {
        private const int SMALLESTWORDLENGTH = 3;

        public static List<string> Tokenize(string originalText)
        {
            List<string> words = new List<string>();
            String [] tokens = originalText.Split(' ');

            
            foreach (string token in tokens)
            {
                
                if (IsAShortWord(token)){
                    // skip
                }
                else
                {
                    string cleanWord = RemovePunctuation(token); //remove punctuation here
                    cleanWord = cleanWord.ToLower();
                    words.Add(cleanWord);
                }
            }
            return words;
        }
        public static bool IsAShortWord(string token)
        {
            if (token.Length < SMALLESTWORDLENGTH)
            {
                return true;
            }
            return false;
        }

        public static string RemovePunctuation(string token)
        {
            token = token.Trim();
            char[] punctuations = {'.', ',', '"', '?','\n', '!', '\''};
            string CleanedToken = token;
            bool MoreToRemove = true;

            for (int i = 0; i < punctuations.Length; i++)
            {
                while (MoreToRemove)
                {
                    MoreToRemove = false;
                    string ch = punctuations[i].ToString();
                    if (CleanedToken.StartsWith(ch))
                    {
                        CleanedToken = CleanedToken.Substring(1, token.Length);
                        MoreToRemove = true;
                        
                    }
                    if (CleanedToken.EndsWith(ch))
                    {
                        CleanedToken = CleanedToken.Substring(0, token.Length - 1);
                        MoreToRemove = true;

                    }
                }
            }
            return CleanedToken;
        }
    }
}
