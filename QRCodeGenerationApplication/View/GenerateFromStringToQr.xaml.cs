﻿using QRCodeGenerationApplication.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QRCodeGenerationApplication.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для GenerateFromStringToQr.xaml
    /// </summary>
    public partial class GenerateFromStringToQr : Page
    {
        public GenerateFromStringToQr()
        {
            InitializeComponent();
            QRCodeViewModel qRCodeViewModel = new QRCodeViewModel();
            qRCodeViewModel.QRCode = new QRCodeModel();
            this.DataContext = qRCodeViewModel;
        }
    }
}
