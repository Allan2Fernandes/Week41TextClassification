using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WPFApplication.ViewModel;

namespace WPFApplication.Model
{
    public class VectorCalculations
    {
        public static  double DistanceBetweenVectorsBoolType(List<bool> vector1, List<bool> vector2)
        {

            List<int> vec1 = ConvertVectorToInt(vector1);
            //DisplayVector(vec1);
            List<int> vec2 = ConvertVectorToInt(vector2);
            //DisplayVector(vec2);

            double Distance = DistanceBetweenVectorsIntType(vec1, vec2);
            return Distance;
        }

        public static double DistanceBetweenVectorsIntType(List<int> vector1, List<int> vector2)
        {
            int NumberOfDimensions = vector1.Count;
            //Normalize the vectors
            double vec1Magnitude = 0;
            double vec2Magnitude = 0;

            for (int i = 0; i < NumberOfDimensions; i++)
            {
                vec1Magnitude += Math.Pow(vector1[i], 2);
                vec2Magnitude += Math.Pow(vector2[i], 2);
            }
            vec1Magnitude = Math.Sqrt(vec1Magnitude);
            vec2Magnitude = Math.Sqrt(vec2Magnitude);

            List<double> vec1Normalized = new List<double>();
            List<double> vec2Normalized = new List<double>();

            for (int i = 0; i < vector1.Count; i++)
            {
                vec1Normalized.Add(vector1[i] / vec1Magnitude);
                vec2Normalized.Add(vector2[i] / vec2Magnitude);
            }


            
            double SumSquare = 0;
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                SumSquare += Math.Pow(vec2Normalized[i] - vec1Normalized[i], 2);
            }
            double distance = Math.Sqrt(SumSquare);

            return distance;
        }

        public static void DisplayVector(List<int> vector)
        {
            foreach (int value in vector)
            {
                Debug.Write(value + " ");
            }
            Debug.WriteLine("\n");
        }

        public static List<int> ConvertVectorToInt(List<bool> vector)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i] == false)
                {
                    result.Add(0);
                }else if (vector[i] == true)
                {
                    result.Add(1);
                }
            }
            return result;
        }

        public static int MakeKNNPrediction(int NumOfNeighbours, 
            List<List<int>> Class1Vectors, 
            List<List<int>> Class2Vectors, 
            List<int> InputTextIntVector) //Return 0 for class1 and 1 for class2
        {
            List<double> DistancesFromA = new List<double>();
            List<double> DistancesFromB = new List<double>(); 

            foreach(List<int> TrainingDataPointVector in Class1Vectors)
            {
                double Distance = DistanceBetweenVectorsIntType(TrainingDataPointVector, InputTextIntVector);
                DistancesFromA.Add(Distance);
            }
            foreach (List<int> TrainingDataPointVector in Class2Vectors)
            {
                double Distance = DistanceBetweenVectorsIntType(TrainingDataPointVector, InputTextIntVector);
                DistancesFromB.Add(Distance);
            }
            //Now merge the two list, but merge them with class and not distance
            //Have to sort the lists first
            DistancesFromA.Sort();
            DistancesFromB.Sort();
            List<int> MergedClassVectors = new List<int>();
            while(DistancesFromA.Count >0 || DistancesFromB.Count > 0)
            {
                if(DistancesFromA.Count == 0)
                {
                    //If A is empty, remove from B and put it in the merged list
                    MergedClassVectors.Add(1);
                    DistancesFromB.RemoveAt(0);
                    continue;
                }
                else if(DistancesFromB.Count == 0)
                {
                    MergedClassVectors.Add(0);
                    DistancesFromA.RemoveAt(0);
                    continue;
                }           
                if (DistancesFromA[0] < DistancesFromB[0])
                {
                    //Put the correct class vector in the merged list
                    MergedClassVectors.Add(0);
                    //Remove the first element which was added to the merged list
                    DistancesFromA.RemoveAt(0);
                }
                else
                {
                    MergedClassVectors.Add(1);
                    DistancesFromB.RemoveAt(0); 
                }
            }
            //MergedClassVectors.ForEach((x) => Debug.WriteLine(x));
            int classACounter = 0;
            int classBCounter = 0;
            int TraversalIndex = 0;
            while(TraversalIndex < NumOfNeighbours)
            {
                if (MergedClassVectors[TraversalIndex] == 0)
                {
                    classACounter++;
                }
                else
                {
                    classBCounter++;
                }
                TraversalIndex++;
            }
            //Uncomment if model has 0% accuracy
            //Random rand = new Random(2);
            //return rand.Next();
            if(classACounter > classBCounter)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }


    }
}
