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
using TextClassification.Controller;
using TextClassification.Domain;
using TextClassification.FileIO;
using WPFApplication.ViewModel;

namespace WPFApplication.View
{
    /// <summary>
    /// Interaction logic for TrainingView.xaml
    /// </summary>
    public partial class TrainingView : UserControl
    {
        private TrainingViewModel ViewModel = null;
        public TrainingView()
        {
            ViewModel = (TrainingViewModel)((App)App.Current).GetViewModel("TrainingViewModel");
            this.DataContext = ViewModel;
            InitializeComponent();
            
        }    

        public void SetTrainingTimeLabel(double Time)
        {
            TrainingTimeLabel.Content = String.Format("Training Time: {0:0.0#}ms", Time);

        }

        public void SetTrainingDimensionsLabel(int Dimensions)
        {
            TrainingDimensionsLabel.Content = String.Format("Training Dimensions: {0}", Dimensions);
        }

        public void SetDatasetSizeLabels(int[] DatasetSizes)
        {
            int size1 = DatasetSizes[0];
            int size2 = DatasetSizes[1];

            DSALabel.Content = String.Format("Size of DatasetA: {0}", size1);
            DSBLabel.Content = String.Format("Size of DatasetB: {0}", size2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisplayTrainingTime();

            DisplayNumberOfDimensions();

            DisplayDatasetSizes();

            //Testing purposes only
            double Accuracy = TrainingViewModel.EvaluateTheModel();
            AccuracyLabel.Content = String.Format("Model Accuracy = {0}%", Accuracy);
            

        }

        public void DisplayTrainingTime()
        {
            double TrainingTime = TrainingViewModel.train();
            TrainingTime /= Math.Pow(10, 6);
            SetTrainingTimeLabel(TrainingTime);
        }

        public void DisplayNumberOfDimensions()
        {
            int Dimensions = TrainingViewModel.GetNumberOfDimensions();
            SetTrainingDimensionsLabel(Dimensions);
        }

        public void DisplayDatasetSizes()
        {
            int[] DatasetSizes = TrainingViewModel.GetDatasetSizes();
            SetDatasetSizeLabels(DatasetSizes);
        }


    }
}
