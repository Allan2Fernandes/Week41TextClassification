using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextClassification.Business;
using TextClassification.Domain;
using TextClassification.FileIO;

namespace TextClassification._1_Controller
{
    public class ModelEvaluationhandler
    {
        public static List<string> TestFolderA;
        public static List<string> TestFolderB;

        public static List<string> ClassATextCorpus = new List<string>();
        public static List<string> ClassBTextCorpus = new List<string>();

        public static List<List<bool>> ClassATextVectors = new List<List<bool>>();
        public static List<List<bool>> ClassBTextVectors = new List<List<bool>>();

        
        public static BagOfWords bof;




        public static void CreateTextVectors()
        {
            //First get the text from file paths
            SetTextCorpus();


            //Second conver them into lists of booleans
            foreach (string TextCorpus in ClassATextCorpus)
            {
                List<bool> TextVector = GetPredictionVectorForText(TextCorpus);
                ClassATextVectors.Add(TextVector);
            }

            foreach(string TextCorpus in ClassBTextCorpus)
            {
                List<bool> TextVector = GetPredictionVectorForText(TextCorpus);
                ClassBTextVectors.Add(TextVector);
            }
        }

        public static List<bool> GetPredictionVectorForText(string InputText)
        {
            List<bool> PredictionVector = new List<bool>();
            foreach (string key in bof.GetAllWordsInDictionary())
            {
                List<string> wordsInFile = Tokenization.Tokenize(InputText); //Tokenize the whole list of words in the file

                //Looping through the whole dictionary, add a boolean value to the the dictionary if it contains it or not. 
                //if there is a true in the index of the key, it exist in the text
                if (wordsInFile.Contains(key))
                {
                    PredictionVector.Add(true);
                }
                else
                {
                    PredictionVector.Add(false);
                }
            }

            return PredictionVector;
        }





        public static void SetTextCorpus()
        {
            foreach (string path in TestFolderA)
            {
                string TextFromFile = TextFile.GetTextFromFile(path);
                ClassATextCorpus.Add(TextFromFile);
            }

            foreach (string path in TestFolderB)
            {
                string TextFromFile = TextFile.GetTextFromFile(path);
                ClassBTextCorpus.Add(TextFromFile);
            }
        }

    }
}
