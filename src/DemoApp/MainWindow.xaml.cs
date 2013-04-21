using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDictionary<string, string> images;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HtmlUtils.DownloadImages di = new HtmlUtils.DownloadImages(txtUrl.Text, @"C:\Users\Home\Documents\Reader\images\");
            images = di.Download();
            di.Document.Save(@"C:\Users\Home\Documents\Reader\images\out.html");
        }
    }
}
