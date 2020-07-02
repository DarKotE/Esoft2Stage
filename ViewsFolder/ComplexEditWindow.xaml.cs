﻿using System;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Esoft.ViewModelsFolder;


namespace Esoft.ViewsFolder
{
    /// <summary>
    /// Interaction logic for ComplexEditWindow.xaml
    /// </summary>
    public partial class ComplexEditWindow : Window
    {
        internal Delegate UpdateActor;
        public ComplexEditWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var complexVM = new ComplexVM();
            this.DataContext = null;
            this.DataContext = complexVM;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UpdateActor?.DynamicInvoke();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            UpdateActor?.DynamicInvoke();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}


