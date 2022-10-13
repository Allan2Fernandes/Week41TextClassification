using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TextClassification._1_Controller;
using TextClassification.Controller;
using TextClassification.Domain;
using WPFApplication.Model;
using WPFApplication.View;

namespace WPFApplication.ViewModel
{
    public class TrainingViewModel : Model.Bindable
    {
        static KnowledgeBuilder nb;
        static Knowledge k;
        static BagOfWords bof;
        static ICollection<string> Words;   
        List<string> WordsInDictionary;
        static Vectors ListOfVectors;

        public static List<List<bool>> _vectorsInA;
        public static List<List<bool>> _vectorsInB;
        static List<List<int>> IntVectorA;
        static List<List<int>> IntVectorB;

        public static List<List<bool>> ClassATextVectors = new List<List<bool>>();
        public static List<List<bool>> ClassBTextVectors = new List<List<bool>>();
        public static List<List<int>> ClassATestTextIntVectors = new List<List<int>>();
        public static List<List<int>> ClassBTestTextIntVectors = new List<List<int>>();
        


        public TrainingViewModel()
        {

        }

        public static double train()
        {
            double StartTime = Stopwatch.GetTimestamp();
            nb = new KnowledgeBuilder();

            // initiate the learning process

            nb.Train();

            // getting the (whole) knowledge found in ClassA and in ClassB
            k = nb.GetKnowledge();


            // get a part of the knowledge - here just for debugging
            bof = k.GetBagOfWords();
            PredictionViewModel.bof = bof;
            Words = bof.GetAllWordsInDictionary();
            ModelEvaluationhandler.bof = bof;

            ListOfVectors = nb.GetVectors();


            _vectorsInA = ListOfVectors.GetVectorsInA();
            _vectorsInB = ListOfVectors.GetVectorsInB();

            PredictionViewModel.updateVectors(_vectorsInA, _vectorsInB);
            
            //DisplayWordVector(_vectorsInA);
            
            //DisplayWordVector(_vectorsInB);
            double TrainingTime = Stopwatch.GetTimestamp() - StartTime;

            return TrainingTime;
        }

        public static int[] GetDatasetSizes()
        {
            int size1 = _vectorsInA.Count;
            int size2 = _vectorsInB.Count;
            return new int[] {size1, size2};
        }

        public static int GetNumberOfDimensions()
        {
            List<bool> AnyVector = _vectorsInA[0];
            int length = AnyVector.Count;
            return length;
        }

        

        public static void DisplayWordVector(List<List<bool>>  VectorOfClass)
        {
            foreach(List<bool> vector in VectorOfClass)
            {
                List<int> intVector = GetintVector(vector);
                DisplayIntVector(intVector);
                Debug.WriteLine("\n");
            }
            
        }

        public static List<int> GetintVector(List<bool> vector)
        {
            List<int> intVector = VectorCalculations.ConvertVectorToInt(vector);
            return intVector;
        }

        public static void DisplayIntVector(List<int> vector)
        {
            foreach(int value in vector)
            {
                Debug.Write(value + " ");
            }
        }

        public static double EvaluateTheModel()
        {
            ModelEvaluationhandler.CreateTextVectors();
            ClassATextVectors = ModelEvaluationhandler.ClassATextVectors;
            ClassBTextVectors = ModelEvaluationhandler.ClassBTextVectors;

            //Convert from bool lists to int lists
            foreach(List<bool> TextVector in ClassATextVectors)
            {
                ClassATestTextIntVectors.Add(VectorCalculations.ConvertVectorToInt(TextVector)); //list list int
            }

            foreach(List<bool> TextVector in ClassBTextVectors)
            {
                ClassBTestTextIntVectors.Add(VectorCalculations.ConvertVectorToInt(TextVector));
            }

            IntVectorA = PredictionViewModel.IntVectorA; //list list int
            IntVectorB = PredictionViewModel.IntVectorB;

            //Use IntvectorA and Intvector B with EACH list int in ClassATestTextIntVectors to get predictions
            double CorrectPredictions = 0;
            double WrongPredictions = 0;

            foreach(List<int> IntVector in ClassATestTextIntVectors)
            {
                int prediction = VectorCalculations.MakeKNNPrediction(3, IntVectorA, IntVectorB, IntVector);
                if(prediction == 0)
                {
                    CorrectPredictions += 1;
                }
                else
                {
                    WrongPredictions += 1;
                }
            }

            foreach (List<int> IntVector in ClassBTestTextIntVectors)
            {
                int prediction = VectorCalculations.MakeKNNPrediction(3, IntVectorA, IntVectorB, IntVector);
                if (prediction == 1)
                {
                    CorrectPredictions += 1;
                }
                else
                {
                    WrongPredictions += 1;
                }
            }
            Debug.WriteLine("Correct Predictions = " + CorrectPredictions);
            Debug.WriteLine("Wrong Predictions: " + WrongPredictions);
            double accuracy = CorrectPredictions / (CorrectPredictions + WrongPredictions) * 100;

            return accuracy;
        }

      

 

        public ICommand ChangePageCMD { get; set; } = new DelegateCommand(() => { ((App)System.Windows.Application.Current).ChangeUserControl(new PredictionView()); });

    }
}
