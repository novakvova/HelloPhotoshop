using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Photoshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.comboColors.SelectedIndex = 0;
        }

        private void comboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string colorToUse =
                (this.comboColors.SelectedItem as StackPanel).Tag.ToString();
            this.myInkCanvas.DefaultDrawingAttributes.Color =
                (Color)ColorConverter.ConvertFromString(colorToUse);
        }

        private void RadioButtonClick(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Content.ToString())
            {
                case "Ink Mode":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "Erase Mode":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "Select Mode":
                    this.myInkCanvas.EditingMode = InkCanvasEditingMode.Select;
                    break;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Create))
            {
                this.myInkCanvas.Strokes.Save(fs);
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream("StrokeData.bin", FileMode.Open))
                {
                    StrokeCollection stroke = new StrokeCollection(fs);
                    this.myInkCanvas.Strokes = stroke;
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error file not found {0}", ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Other Error: {0}", ex.Message);
            }
        }
    }
}
