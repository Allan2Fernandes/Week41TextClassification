using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextClassification._4_FileIO;
using TextClassification.Business;
using TextClassification.Domain;
using TextClassification.FileIO;

namespace TextClassification._1_Controller
{
    public class CustomPredictionHandler
    {
        string InputText;
        BagOfWords bof;
        List<bool> PredictionVector = new List<bool>();
        public CustomPredictionHandler(string InputText, BagOfWords bof)
        {
            this.InputText = InputText;
            this.bof = bof;
        }

        public void SetPredictionVector()
        {
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
        }

        public List<bool> GetPredictionVector()
        {
            return PredictionVector;
        }

        public static string GetTextFromFile(string FilePath)
        {
            return TextFile.GetTextFromFile(FilePath);
        }

        public static string GetTextFromPDF(string FilePath)
        {
            return PDFReader.ReadTextFromPDF(FilePath);
        }


    }
}
