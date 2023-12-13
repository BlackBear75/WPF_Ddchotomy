using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Polinom_Interpolation
{
    public class ReportShow
    {
        public static void GenerateReport()
        {
            try
            {

                string interpolationPointsInfo = $"Точки для інтерполяції: {string.Join(", ", Variable.points.Select(p => $"({p.X}, {p.Y})"))}";
                string intervalInfo = $"Інтервал: [{Variable.start}, {Variable.end}]";
                string precisionInfo = $"Задана точніcть: [{Variable.precision}]";

                string rootsInfo = $"Кількість коренів: {Variable.roots.Count}\nКорені: {string.Join(", ", Variable.roots)}";

                string reportText = $"{Variable.polinom.PrintPolynomial()}\n{interpolationPointsInfo}\n{intervalInfo}\n{precisionInfo}\n{rootsInfo}";

                // Створення та відображення нового вікна звіту
                ReportWindow reportWindow = new ReportWindow(reportText);
                reportWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при створенні звіту  : {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void ShowAuthorInfo()
        {
            try
            {
                string authorInfo = " Ім'я автора: Bohdan Poryvai\n Email автора: fanat991@ukr.net" + "\n Дякую що скорстались цією програмою ";
                // Виклик вікна ReportWindow з інформацією про автора
                ReportWindow reportWindow = new ReportWindow(authorInfo);
                reportWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void ShowHelp()
        {
            try
            {
                
                string helpInfo = $"Привіт дорогий користувач це помічник в цій програмі\n" +
                    "І так а нас є меню з кнопками :\n"+
                    "File в нього є такі функції :\n"+
                    "Новий набір даних - це допоможе тобі очистити дані які були уведені\n"+
                    "Загрузка даних - це допоможе тобі завантажити вхідні дані в програму\n"+
                    "Сохранить набір даних - це допоможе тобі сохранить дані для уведення в xml формат\n"+
                    "Сохранить HTML - це допоможе тобі зберегти звіт про результат у форматі html який ти потім зможеш відкрити в Переглянути(Звіт в HTML)\n"+
                    "Вихід - ну це уже кнопка виходу з програми\n"+
                    "Так потім в нас є кнопка Розв'зки вона володіє \n"+
                    "Розв'язати рівняння - це в нас  найти корені рівняння хоча ця кнопка є і в головному вікні\n"+
                    "Показати графік - це тобі допоможе відобразити графік хоча і є кнопка в головному вікні\n"+
                    "Кнопка Переглянути вона має такі функції:\n"+
                    "Звіт - це допоможе відобразити звіт про результат в окремому вікні нашої програми\n"+
                    "Автор - кнопка допоможе тобі найти автора його почту\n"+
                    "Звіт HTML - ця кнопка відкриває браузер і показує наш звіт html форматі ЩОБ СКОРИСТАТИСЯ ЦІЄЮ ФУНКЦІЄЮ ПОТРІБНО СОХРАНИТИ ФАЙЛ В HTML ФОРМАТ (ЯКЩО ВИ НЕ ВИКОНУВАЛИ ГРАФІК ДЛЯ ВАШОГО НАБОРУ ДАНИХ ВІН НЕ ВІДОБРАЗИТЬСЯ В ЗВІТІ )"

                    ;
                    
                // Виклик вікна ReportWindow з інформацією про автора
                Helper reportWindow = new Helper(helpInfo);
                reportWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
