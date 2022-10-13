using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextClassification.Domain;
using TextClassification.FileIO;

namespace TextClassification.Controller
{
    public class FileListBuilder:AbstractFileListsBuilder
    {
        const string AFOLDERNAME = "ClassA";
        const string BFOLDERNAME = "ClassB";

        private FileLists _fileLists;
        private FileLists TestFileLists;
        private FileAdapter _fileAdapter;

        public FileListBuilder()
        {
            _fileLists = new FileLists();
            TestFileLists = new FileLists();
            _fileAdapter = new TextFile("txt");
        }

        public override FileLists GetFileLists()
        {
            return _fileLists;
        }

        public override FileLists GetTestFileLists()
        {
            return TestFileLists;
        }

        public override void GenerateFileNamesInA()
        { 
            List<string> fileNames = _fileAdapter.GetAllTrianingFileNames(AFOLDERNAME);
            _fileLists.SetA(fileNames);
        }

        public override void GenerateFileNamesInB()
        {
            List<string> fileNames = _fileAdapter.GetAllTrianingFileNames(BFOLDERNAME);
            _fileLists.SetB(fileNames);
        }

        public void GenerateFileNamesInTestA()
        {
            List<string> fileNames = _fileAdapter.GetAllTestFileNames(AFOLDERNAME);
            TestFileLists.SetTestA(fileNames);
        }

        public void GenerateFileNamesInTestB()
        {
            List<string> fileNames = _fileAdapter.GetAllTestFileNames(BFOLDERNAME);
            TestFileLists.SetTestB(fileNames);
        }
    }
}
