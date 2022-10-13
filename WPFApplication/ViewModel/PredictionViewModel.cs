using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TextClassification._1_Controller;
using TextClassification.Domain;
using WPFApplication.Model;
using WPFApplication.View;

namespace WPFApplication.ViewModel
{
    public class PredictionViewModel : Model.Bindable
    {
        static List<List<bool>> _vectorsInA;
        static List<List<bool>> _vectorsInB;
        public static List<List<int>> IntVectorA;
        public static List<List<int>> IntVectorB;
        static List<int> InputTextIntVector;
        public static BagOfWords bof;
        public static CustomPredictionHandler PredictionVectorBuilder;
        public PredictionViewModel()
        {
            IntVectorA = new List<List<int>>();
            IntVectorB = new List<List<int>>();
        }

        public static void updateVectors(List<List<bool>> vec1, List<List<bool>> vec2)
        {
            _vectorsInA = vec1;
            _vectorsInB = vec2;

            getIntVectors();
            
        }

        public static void getIntVectors()
        {
            //Loop through vectors in class A and convert each one
            foreach (List<bool> vector in _vectorsInA)
            {
                List<int> IntVector = VectorCalculations.ConvertVectorToInt(vector);
                IntVectorA.Add(IntVector);
            }
            //Repeat that for class B
            foreach(List<bool> vector in _vectorsInB)
            {
                List<int> IntVector = VectorCalculations.ConvertVectorToInt(vector);
                IntVectorB.Add(IntVector);
            }


        }

        public static void GetRandomCalculations()
        {
            Random rand = new Random();
            
            int size1 = IntVectorA.Count;
            int size2 = IntVectorB.Count;

            int index1 = rand.Next(0, size1);
            int index2 = rand.Next(0, size2);

            List<bool> VectorFromA = _vectorsInA[index1];
            List<bool> VectorFromB = _vectorsInB[index2];

            double distance = VectorCalculations.DistanceBetweenVectorsBoolType(VectorFromA, VectorFromB);
            Debug.WriteLine("Distance between vectors is: " + distance); 
        }

        public static List<int> GetVectorOnInputText(string text)
        {
            PredictionVectorBuilder = new CustomPredictionHandler(text, PredictionViewModel.bof);
            PredictionVectorBuilder.SetPredictionVector();
            List<bool> TextBoolVector = PredictionVectorBuilder.GetPredictionVector();

            //Conver the bool list to an int list
            List<int> TextIntVector = VectorCalculations.ConvertVectorToInt(TextBoolVector);
            InputTextIntVector = TextIntVector;
            return TextIntVector;
        }

        public static int MakeClassPrediction()
        {
            int prediction = VectorCalculations.MakeKNNPrediction(3, IntVectorA, IntVectorB, InputTextIntVector);
            return prediction;
        }

        public static string GetTextFromFile(string FilePath)
        {
            return CustomPredictionHandler.GetTextFromFile(FilePath);
        }

        public static string GetTextFromPDF(string FilePath)
        {
            return CustomPredictionHandler.GetTextFromPDF(FilePath);
        }

        public ICommand ChangePageCMD { get; set; } = new DelegateCommand(() => { ((App)System.Windows.Application.Current).ChangeUserControl(new TrainingView()); });
    }
}
