using QuadraticLib;

namespace QuadraticConsoleApp
{
    class Program
    {
        static void TestEquation()
        {
            Console.WriteLine("\n==========Тестування рiвняння в найпростiшому варiантi:==========");
            // Квадратне рівняння:
            Console.WriteLine("------Загальний випадок (x^2 + x - 2)------");
            new Quadratic() { F = t => 1, G = t => 1, C = -2 }.Test(0); // t не впливає на результат

            Console.WriteLine("------Загальний випадок (x^2 + 2x + 1)------");
            new Quadratic() { F = t => 1, G = t => t, C = 1 }.Test(2);

            Console.WriteLine("------Немає розв'язкiв (x^2 + x + 10)------");
            new Quadratic() { F = t => t, G = t => t, C = 10 }.Test(1);

            // Лінійне рівняння:
            Console.WriteLine("------Лiнiйне рiвняння (один корiнь)-----");
            new Quadratic() { F = t => 0, G = t => 1, C = -0.5 }.Test(0);

            Console.WriteLine("------Лiнiйне рiвняння (немає коренiв)-----");
            new Quadratic() { F = t => 0, G = t => 0, C = 1 }.Test(0);

            Console.WriteLine("------Лiнiйне рiвняння (безмежна кiлькiсть коренiв)-----");
            new Quadratic() { F = t => 0, G = t => 0, C = 0 }.Test(0);
        }

        static void TestFunctions()
        {
            Console.WriteLine("\n=====Тестування функцiй та рiвняння з використанням спискiв:=====");
            // Тестування функції F:
            FFunction fFunction = new()
            {
                ACoefs = new List<ACoef>
                {
                    new() { Index = 0, Value = 1 },
                    new() { Index = 1, Value = 2 },
                    new() { Index = 2, Value = 3 }
                }
            };
            fFunction.Test(name: "F", from: -5, to: 15, step: 1);

            // Тестування функції G:
            GFunction gFunction = new()
            {
                XYCoefs = new List<XYCoef>
                {
                    new() { X = 1, Y = 2 },
                    new() { X = 3, Y = 4 },
                    new() { X = 5, Y = 6 }
                }
            };
            gFunction.Test(name: "G", from: -5, to: 15, step: 1);
        }

        /// <summary>
        /// Тестує квадратне рівняння, дані про яке 
        /// зберігаються в XML-файлах
        /// </summary>
        static void TestEquationWithXML()
        {

            Console.WriteLine("\n==Тестування рiвняння, данi про яке зберiгаються в XML-файлах:===");
            XMLQuadratic quadratic = new();
            // Немає даних:
            Console.WriteLine(quadratic);
            // Загальний випадок:
            quadratic.ReadFromXML("EquationCommon.xml");
            Console.WriteLine("    Файл: EquationCommon.xml");
            // Не розв'язували рівняння:
            Console.WriteLine(quadratic);
            Console.WriteLine(quadratic.Solve(0));
            Console.WriteLine(quadratic.Solve(-5));
            Console.WriteLine(quadratic.Solve(-10));
            quadratic.GenerateReport("Common.html", -10);
           
            // Лінійне рівняння:
            Console.WriteLine("    Файл: LinearEquation.xml");
            Console.WriteLine(quadratic.ReadFromXML("LinearEquation.xml").Solve(0));
            quadratic.GenerateReport("Linear.html", 0);
            // Немає коренів:
            Console.WriteLine("    Файл: LinearEquationNoSolutions.xml");
            Console.WriteLine(quadratic.ReadFromXML("LinearEquationNoSolutions.xml").Solve(0));
            quadratic.GenerateReport("NoSolutions.html", 0);
       
       
            // Створюємо рівняння "з нуля":
            quadratic.ClearEquation();
            quadratic.Data.FFunction.AddA(1, 0);
            quadratic.Data.FFunction.AddA(0, 1);
            quadratic.Data.GFunction.AddXY(1, 2);
            quadratic.C = 1;
            Console.WriteLine(quadratic.Solve(0));
        }

        /// <summary>
        /// Стартова точка застосунку. 
        /// Послідовно тестує квадратне рівняння у загальному вигляді,
        /// функції, які використовують списки для зберігання даних
        /// і квадратне рівняння, дані про яке зберігаються в XML-файлах
        /// </summary>
        static void Main()
        {
            TestEquation();
            TestFunctions();
            TestEquationWithXML();
        }
    }
}