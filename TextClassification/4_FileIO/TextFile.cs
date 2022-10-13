using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextClassification.Foundation;

namespace TextClassification.FileIO
{
    public class TextFile:FileAdapter
    {
        const string PROJECTPATH = "C:\\Users\\allan\\OneDrive\\Desktop\\Week41Project\\TextClassification\\TextClassification\\bin\\Debug\\";

        const string FOLDERA = "ClassA";
        const string FOLDERB = "ClassB";
        public TextFile(string fileType):base(fileType)
        {
            
        }
        public override List<string> GetAllTrianingFileNames(string folderName)
        {
            List<string> fileNames = new List<string>();
            string FolderPath = PROJECTPATH + folderName;
            Console.WriteLine(FolderPath);
            string[] paths = Directory.GetFiles(FolderPath, "*." + GetFileType());
           
            foreach (string path in paths)
            {
                fileNames.Add(path);
            }
            return fileNames;
        }

        public override List<string> GetAllTestFileNames(string folderName)
        {
            string EvaluationFolderPath = "C:\\Users\\allan\\OneDrive\\Desktop\\Week41Project\\TextClassification\\TextClassification\\bin\\Debug\\Evaluation\\";
            List<string> fileNames = new List<string>();
            string FolderPath = EvaluationFolderPath + folderName;
            Console.WriteLine(FolderPath);
            string[] paths = Directory.GetFiles(FolderPath, "*." + GetFileType());

            foreach (string path in paths)
            {
                fileNames.Add(path);
            }
            return fileNames;
        }

        public string GetFilePathA(string fileName)
        {
            return @PROJECTPATH + FOLDERA + "\\" + fileName + "."+base.GetFileType();
        }

        public override string GetAllTextFromFileA(string path)
        {
            string text = File.ReadAllText(path);

            return text;
        }

        public override string GetAllTextFromFileB(string path)
        {
            string text = File.ReadAllText(path);

            return text;
        }

        public static string GetTextFromFile(string Path)
        {
            return File.ReadAllText(Path);
        }
    }
}
