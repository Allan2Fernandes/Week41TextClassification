using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TextClassification._4_FileIO;
using TextClassification.Controller;
using TextClassification.Domain;
using TextClassification.FileIO;
using TextClassification.Foundation;
using WPFApplication.Model;

namespace Test
{
    [TestClass]
    public class Testing
    {
        string DebugPath = "C:\\Users\\allan\\OneDrive\\Desktop\\Week41Project\\TextClassification\\TextClassification\\bin\\Debug\\";

        [TestMethod]
        public void TestWordItemGetWord()
        {
            // arrange
            string expected = "nice";
            WordItem wI = new WordItem("nice", 0);

            // act
            string actual = wI.GetWord();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestStringOperationsGetFileName()
        {
            // deprecated - use 
            // arrange
            string expected = "Suduko";
            string path = "c:\\users\\tha\\documents\\Suduko.txt";

            // act
            string actual = StringOperations.getFileName(path);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFileGetAllFileNames()
        {
            // arrange
            string folderA = "ClassA";
            string fileType = "txt";
            List<string> expected = new List<string>();
            expected.Add(DebugPath + folderA + "\\bbcsportsfootball." + fileType);
            expected.Add(DebugPath + folderA + "\\City Copenhagen UCl." + fileType);
            expected.Add(DebugPath + folderA + "\\dailymirrornfl." + fileType);
            expected.Add(DebugPath + folderA + "\\Kante Left out." + fileType);
            expected.Add(DebugPath + folderA + "\\Luis Diaz football." + fileType);

            // act
            FileAdapter fa = new TextFile(fileType);
            List<string> actual = fa.GetAllTrianingFileNames(folderA);

            // assert
            Assert.AreEqual(expected.Count, actual.Count);
       
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
         
        }
        [TestMethod]
        public void TestGetFilePathA()
        {
            // arrange
            string folderA = "ClassA";
            string fileType = "txt";
            string fileName = "filnavn";
            string expected = DebugPath + folderA + "\\filnavn." + fileType;

            // act
            TextFile tf = new TextFile(fileType);
            string actual = tf.GetFilePathA(fileName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestConvertVectorToInt()
        {
            List<bool> vector = new List<bool>();
            vector.Add(true);
            vector.Add(false);
            vector.Add(true);
            List<int> actualVector = VectorCalculations.ConvertVectorToInt(vector);
            List<int> expectedVector = new List<int>();
            expectedVector.Add(1);
            expectedVector.Add(0);
            expectedVector.Add(1);

            CollectionAssert.AreEqual(expectedVector, actualVector);    
        }

        [TestMethod]
        public void TestDistanceBetweenVectorsIntType1()
        {
            List<int> vector1 = new List<int> { 0, 0, 1, 1, 0 };
            List<int> vector2 = new List<int> { 0, 0, 1, 1, 0 };

            double ActualDistance = 0;
            double ExpectedDistance = VectorCalculations.DistanceBetweenVectorsIntType(vector1, vector2);
            Assert.AreEqual(ExpectedDistance, ActualDistance);

        }

        [TestMethod]
        public void TestDistanceBetweenVectorsIntType2()
        {
            List<int> vector1 = new List<int> { 0, 0, 1, 1, 0 };
            List<int> vector2 = new List<int> { 1, 0, 0, 1, 0 };

            double ActualDistance = Math.Sqrt(1);
            double ExpectedDistance = VectorCalculations.DistanceBetweenVectorsIntType(vector1, vector2);
            Assert.AreEqual(ActualDistance, ExpectedDistance, 0.0000001);

        }
        [TestMethod]
        public void TestDistanceBetweenVectorsIntType3()
        {
            List<int> vector1 = new List<int> { 0, 1, 1, 1, 0, 1, 0, 1, 1 };
            List<int> vector2 = new List<int> { 1, 1, 0, 1, 0, 0, 1, 1, 0 };

            double ActualDistance = Math.Sqrt((10-Math.Sqrt(30))/5);
            double ExpectedDistance = VectorCalculations.DistanceBetweenVectorsIntType(vector1, vector2);
            Assert.AreEqual(ActualDistance, ExpectedDistance, 0.0000001);

        }

        [TestMethod]
        public void TestMakeKNNPrediction()
        {
            //int MakeKNNPrediction(int NumOfNeighbours, List<List<int>> Class1Vectors, List<List<int>> Class2Vectors, List<int> InputTextIntVector)
            List<int> vecA1 = new List<int> {-1, -1, -1 };
            List<int> vecA2 = new List<int> { 0, 1, 0 };
            List<int> vecA3 = new List<int> { 1, 1, 1 };
            List<List<int>> ClassAVectors = new List<List<int>> { vecA1, vecA2, vecA3};

            List<int> vecB1 = new List<int> { 1, 0, 0 };
            List<int> vecB2 = new List<int> { 0, 1, 0 };
            List<int> vecB3 = new List<int> { 0, 0, 1 };

            List<List<int>> ClassBVectors = new List<List<int>> { vecB1, vecB2, vecB3 };

            List<int> VectorToClassify = new List<int> { 1, 1, 1 };

            int prediction = VectorCalculations.MakeKNNPrediction(3, ClassAVectors, ClassBVectors, VectorToClassify);

            int actual = 1;

            Assert.AreEqual(prediction, actual);
        }

        [TestMethod]
        public void TestFileReader()
        {
            string FilePath = "C:\\Users\\allan\\OneDrive\\Desktop\\Week41Project\\TextClassification\\TextClassification\\bin\\Debug\\TestFiles\\TextFileOpenerTester.txt";
            string ExpectedText = "You have opened a textfile.";
            string ActualText = TextFile.GetTextFromFile(FilePath);
            Assert.AreEqual(ExpectedText, ActualText);
        }

        [TestMethod]
        public void TestPDFReader()
        {
            string FilePath = "C:\\Users\\allan\\OneDrive\\Desktop\\Week41Project\\TextClassification\\TextClassification\\bin\\Debug\\TestFiles\\PDFFileOpenerTester.pdf";
            string ExpectedText = "You have opened  a PDF file.";
            string ActualText = PDFReader.ReadTextFromPDF(FilePath).Trim();
            Assert.AreEqual(ExpectedText, ActualText);
        }
    }
}
