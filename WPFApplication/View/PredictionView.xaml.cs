using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextClassification._4_FileIO;
using TextClassification.Domain;
using WPFApplication.ViewModel;


namespace WPFApplication.View
{
    /// <summary>
    /// Interaction logic for PredictionView.xaml
    /// </summary>
    public partial class PredictionView : UserControl
    {
        private PredictionViewModel ViewModel = null;
        
        public PredictionView()
        {
            ViewModel = (PredictionViewModel)((App)App.Current).GetViewModel("PredictionViewModel");
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Calculate distances here
            PredictionViewModel.GetRandomCalculations();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PredictOnInputtedString();
        }

        public void PredictOnInputtedString()
        {
            string TextCorpus = InputTextBox.Text;
            //Use the text to construct a BuildPredctionVector object
            List<int> PredictionIntVector = PredictionViewModel.GetVectorOnInputText(TextCorpus);


            //Now that I have the prediction vector, I should get all the Training vectors. 
            //And then find the ones with the closest distance
            //Dynamically set the number of NNs in KNN

            int prediction = PredictionViewModel.MakeClassPrediction();
            char ClassPrediction;

            if(prediction == 0)
            {
                ClassPrediction = 'A';
            }
            else
            {
                ClassPrediction = 'B';
            }

            PredictionConclusionLabel.Content = "Prediction Conclusion: Class" + ClassPrediction;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            string filePath = "";
            if (result == true)
            {
                // Open document 
                filePath = dlg.FileName;
            }
            //Here you have the file path. 
            //Then depending  on whether text or pdf file, choose the correct adapter
            bool isTextFilePath = false;

            if(filePath.Substring(filePath.Length -4) == ".txt")
            {
                isTextFilePath = true;
            }

            string ReadText = "";
            if (isTextFilePath)
            {
                ReadText = PredictionViewModel.GetTextFromFile(filePath);
                
            }
            else
            {
                ReadText = PredictionViewModel.GetTextFromPDF(filePath);
            }

            InputTextBox.Text = ReadText;



        }
    }
}
