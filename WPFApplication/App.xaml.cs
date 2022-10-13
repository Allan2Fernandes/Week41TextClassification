using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFApplication.Model;
using WPFApplication.ViewModel;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //List holding the viewmodels
        List<KeyValuePair<string, Bindable>> listOfViewModels = new List<KeyValuePair<string, Bindable>>();


        /// <summary>
        /// Reference to the content "View" 
        /// </summary>
        public ContentControl ContentControlRef { get; set; } = null;

        public App()
        {
            //Create instances of the viewmodels, these should be kept alive during the lifetime of the application
            listOfViewModels.Add(new KeyValuePair<string, Bindable>("PredictionViewModel", new PredictionViewModel()));
            listOfViewModels.Add(new KeyValuePair<string, Bindable>("TrainingViewModel", new TrainingViewModel()));
        }

        /// <summary>
        /// Used to change the content view content!
        /// </summary>
        /// <param name="view"></param>
        public void ChangeUserControl(UserControl view)
        {
            this.ContentControlRef.Content = view;
        }


        /// <summary>
        /// This is for finding the viewmodel instances in the collection of viewmodels (listOfViewModels)
        /// </summary>
        /// <param name="viewModelName"></param>
        /// <returns></returns>
        public Bindable GetViewModel(String viewModelName)
        {
            Bindable foundViewModel = null;

            foreach (KeyValuePair<string, Bindable> kvp in listOfViewModels)
            {
                if (kvp.Key.Equals(viewModelName))
                {
                    foundViewModel = kvp.Value;
                }
            }
            if (foundViewModel == null)
                throw new Exception("ViewModel not found");

            return foundViewModel;
        }
    }
}
