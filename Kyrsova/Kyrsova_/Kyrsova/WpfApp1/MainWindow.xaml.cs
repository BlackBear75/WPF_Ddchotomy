using System;

using System.Windows;

using Microsoft.Win32;


namespace Polinom_Interpolation
{
    public partial class MainWindow : Window
    {
      
      
        public MainWindow()
        {
            InitializeComponent();
            Load_Icon.Loadicon(this);

        }

        #region Метод для задання нових даних
        private void NewDataset_Click(object sender, RoutedEventArgs e)
        {
            // Обнулення текстових полів для коефіцієнтів полінома, кількості точок, точок, інтервалу та точності.
            coefficientsTextBox.Text = string.Empty;
            numberOfPointsTextBox.Text = string.Empty;
            pointsTextBox.Text = string.Empty;
            intervalTextBox.Text = string.Empty;
            precisionTextBox.Text = string.Empty;

            // Очистка текстового блоку з результатами
            resultTextBlock.Text = string.Empty;
        }
        #endregion

        #region ЗАГРУЗКА
        private void LoadDataset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Виклик методу для отримання шляху до існуючого XML-файлу через діалогове вікно
              Variable.fileName = GetOpenFileName();

                // Перевірка, чи користувач не вибрав "Cancel" у діалоговому вікні
                if (Variable.fileName != null)
                {
                    // Виклик методу для завантаження набору даних з файлу
                    Dataset loadedDataset = Variable.rootsSerializer.LoadDatasetFromFile(Variable.fileName);

                    // Виведення завантажених даних на екран або використання їх у вашій програмі
                    // Наприклад, можна вивести їх у текстові поля або зробити інші дії за необхідності
                    coefficientsTextBox.Text = loadedDataset.Coefficients;
                    numberOfPointsTextBox.Text = loadedDataset.NumberOfPoints;
                    pointsTextBox.Text = loadedDataset.Points;
                    intervalTextBox.Text = loadedDataset.Interval;
                    precisionTextBox.Text = loadedDataset.Precision;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dataset: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetOpenFileName()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.Title = "Завантаження даних";

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null; // Користувач вибрав "Cancel" у діалоговому вікні
        }

        #endregion

        #region Зберігання 

        private void SaveDataset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Отримання значень з текстових полів
                string coefficientsText = coefficientsTextBox.Text;
                string numberOfPointsText = numberOfPointsTextBox.Text;
                string pointsText = pointsTextBox.Text;
                string intervalText = intervalTextBox.Text;
                string precisionText = precisionTextBox.Text;

                // Створення об'єкту для зберігання значень набору даних
                Dataset dataset = new Dataset
                {
                    Coefficients = coefficientsText,
                    NumberOfPoints = numberOfPointsText,
                    Points = pointsText,
                    Interval = intervalText,
                    Precision = precisionText
                };

                // Виклик методу для отримання шляху до нового XML-файлу через діалогове вікно
                string fileName = GetSaveFileName();

                // Перевірка, чи користувач не вибрав "Cancel" у діалоговому вікні
                if (fileName != null)
                {
                    // Виклик методу для збереження набору даних у файл
                    Variable.rootsSerializer.SaveDatasetToFile(dataset, fileName);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка не вдалось зберегти: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetSaveFileName()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Dataset";

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }

            return null; // Користувач вибрав "Cancel" у діалоговому вікні
        }
        #endregion

        #region Метод виходу
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic for exiting the application
            Close();
        }
        #endregion

        #region Відображення результату
        private void SolveEquation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = this;
                ProcessEquation.Equation(mainWindow);

            }
            catch (Exception ex)
            {
                resultTextBlock.Text = "Помилка: " + ex.Message;
            }
        }
        #endregion

        #region Відображення графіку
        private void ShowGraph_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = this;
                GraphShow.ShowGraph(mainWindow);
            }



            catch (Exception ex)
            {
                resultTextBlock.Text = "Помилка : " + ex.Message;
            }
        }
        #endregion


        #region Відображення звіту
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            ReportShow.GenerateReport();
        }
        


        private void Report_HTML_Click(object sender, RoutedEventArgs e)
        {
            HTML_Logic.Report_HTML();
          
        }

        private void GenerateHtmlReport_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = this; 
            HTML_Logic.GenerateHtmlReport(mainWindow);
        }
   


        private void ShowAuthorInfo_Click(object sender, RoutedEventArgs e)
        {
           ReportShow.ShowAuthorInfo();
        }




        #endregion

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            ReportShow.ShowHelp();
        }
    }

}

