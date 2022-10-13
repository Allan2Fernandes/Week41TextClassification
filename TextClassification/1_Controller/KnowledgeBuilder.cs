using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextClassification._1_Controller;
using TextClassification.Business;
using TextClassification.Domain;
using TextClassification.FileIO;

namespace TextClassification.Controller
{
    public class KnowledgeBuilder:AbstractKnowledgeBuilder
    {
        private Knowledge _knowledge; // the composite object

        private FileLists _fileLists;
        private FileLists TestFileLists;
        private BagOfWords _bagOfWords;
        private Vectors _vectors;

        private FileAdapter _fileAdapter;

        public KnowledgeBuilder()
        {
            _fileAdapter = new TextFile("txt");
            _knowledge = new Knowledge();
        }

        public override void BuildFileLists()
        {
            
            FileListBuilder flb = new FileListBuilder();

            flb.GenerateFileNamesInA();
            flb.GenerateFileNamesInTestA();
            flb.GenerateFileNamesInB();
            flb.GenerateFileNamesInTestB();
            _fileLists = flb.GetFileLists();
            TestFileLists = flb.GetTestFileLists();
            List<string> TestFolderA = TestFileLists.GetTestA();
            List<string> TestFolderB = TestFileLists.GetTestB();

            ModelEvaluationhandler.TestFolderA = TestFolderA;
            ModelEvaluationhandler.TestFolderB = TestFolderB;

            //TestFolderA.ForEach(x => Debug.WriteLine(x));
            //TestFolderB.ForEach(x => Debug.WriteLine(x));

            _knowledge.SetFileLists(_fileLists);
        }

        public override void Train()
        {
            // (1) 
            BuildFileLists();
            // (2)
            BuildBagOfWords();
            // (3)
            BuildVectors();
        }

        private void AddToBagOfWords(string folderName)
        {
            List<string> list;
            if (folderName.Equals("ClassA")){
                list = _fileLists.GetA();
            }
            else{
                list = _fileLists.GetB();
            }
            for (int i = 0; i < list.Count; i++)
            {
                string text;
                if (folderName.Equals("ClassA")){
                    text = _fileAdapter.GetAllTextFromFileA(list[i]);
                }
                else{
                    text = _fileAdapter.GetAllTextFromFileB(list[i]);
                }  
                List<string> wordsInFile = Tokenization.Tokenize(text);
                //Words are added to bag of words after tokenization
                //Words from tokenization are returned after cleaning up the tokens
                foreach (string word in wordsInFile)
                {
                    _bagOfWords.InsertEntry(word);
                }
            }
        }
       

        public override void BuildBagOfWords()
        {
            if (_fileLists == null)
            {
                BuildFileLists();
            }
            _bagOfWords = new BagOfWords();

            AddToBagOfWords("ClassA");
            AddToBagOfWords("ClassB");

            _knowledge.SetBagOfWords(_bagOfWords);
        }

        private void AddToVectors(string folderName, VectorsBuilder vb)
        {
            List<string> list;
            

            if (folderName.Equals("ClassA")){
                list = _fileLists.GetA();
            }
            else{
                list = _fileLists.GetB();
            }
            for (int i = 0; i < list.Count; i++)
            {
                List<bool> vector = new List<bool>();
                foreach (string key in _bagOfWords.GetAllWordsInDictionary())
                {
                    string text;
                    if (folderName.Equals("ClassA")){
                        text = _fileAdapter.GetAllTextFromFileA(_fileLists.GetA()[i]);
                    }
                    else{
                        text = _fileAdapter.GetAllTextFromFileB(_fileLists.GetB()[i]);
                    }
                    List<string> wordsInFile = Tokenization.Tokenize(text); //Tokenize the whole list of words in the file
                    
                    

                    //Looping through the whole dictionary, add a boolean value to the the dictionary if it contains it or not. 
                    //if there is a true in the index of the key, it exist in the text
                    if (wordsInFile.Contains(key)){ 
                        vector.Add(true);
                    }
                    else{
                        vector.Add(false);
                    }
                }
                if (folderName.Equals("ClassA"))
                {
                    vb.AddVectorToA(vector);
                }
                else
                {
                    vb.AddVectorToB(vector);
                }
            }
        }

        public override void BuildVectors()
        {
            if (_fileLists == null)
            {
                BuildFileLists();
            }
            if (_bagOfWords == null)
            {
                BuildBagOfWords();
            }
            _vectors = new Vectors();
            VectorsBuilder vb = new VectorsBuilder();
            AddToVectors("ClassA",vb);
            AddToVectors("ClassB",vb);

            _vectors = vb.GetVectors();
            _knowledge.SetVectors(_vectors);
        }

        public override Knowledge GetKnowledge()
        {
            return _knowledge;
        }

        public Vectors GetVectors()
        {
            return _vectors;
        }
    }
}
