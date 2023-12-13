namespace QuadraticLib
{
    /// <summary>
    /// Перелік, який визначає можливі стани об'єкту "Квадратне рівняння"
    /// </summary>
    public enum EquationState
    {
        NoData,
        NotSolved,
        NoRoots,
        RootsFound,
        InfinityOfRoots
    }

    /// <summary>
    /// Опис типу функції, яка приймає параметр дійсного типу
    /// та повертає дійсне значення
    /// </summary>
    /// <param name="t">параметр</param>
    /// <returns>значення функції</returns>
    public delegate double DFunction(double t);

    /// <summary>
    /// Представляє квадратне рівняння. 
    /// Коефіцієнти a і b визначаються функціями f(t) та g(t)
    /// </summary>
    public class Quadratic
    {
        /// <summary>
        /// Функція f(t)
        /// </summary>
        public DFunction F { get; set; } = t => 0;

        /// <summary>
        /// Функція g(t)
        /// </summary>
        public DFunction G { get; set; } = t => 0;

        /// <summary>
        /// коефіцієнт c
        /// властивість віртуальна, і це дозволяє у похідних класах
        /// визначити інший спосіб представлення даних
        /// </summary>
        virtual public double C { get; set; }

        /// <summary>
        /// Стан рівняння
        /// </summary>
        public EquationState State { get; set; } = EquationState.NoData;

        // Список коренів:
        private List<double> roots = new();

        /// <summary>
        /// Повертає корінь рівняння за вказаним індексом
        /// </summary>
        /// <param name="i">індекс кореня</param>
        /// <returns>корінь</returns>
        public double GetRoot(int i)
        {
            return roots[i];
        }

        /// <summary>
        /// Повертає кількість коренів після розв'язання рівняння
        /// </summary>
        public int RootsCount
        {
            get { return roots.Count; }
        }
        /// <summary>
        /// Розв'язує квадратне рівняння. Коефіцієнти a та b залежать від параметру.
        /// Після завершення виконання функції у списку коренів два значення
        /// (квадратне рівняння, дискримінант більше або дорівнює 0),
        /// одне значення (лінійне рівняння, яке можна розв'язати),
        /// Якщо список порожній, коренів немає.
        /// Відповідно змінюється значення властивості State
        /// </summary>
        /// <param name="t">параметр</param>
        /// <returns>поточний об'єкт зі зміненим станом</returns>
        public Quadratic Solve(double t)
        {
            roots = new();
            double a = F(t);
            double b = G(t);
            if (a == 0)
            {
                if (b == 0 && C == 0)
                {
                    State = EquationState.InfinityOfRoots; // Безмежна кількість розв'язків
                    return this;
                }
                if (b == 0 && C != 0)
                {
                    State = EquationState.NoRoots;
                    return this;          //
                }
                roots.Add(-C / b); // Один корінь
                State = EquationState.RootsFound;
                return this;
            }
            // Обчислення дискримінанту:
            double d = b * b - 4 * a * C;
            if (d < 0)
            {
                State = EquationState.NoRoots;
                return this;         // Немає коренів
            }
            roots.Add((-b - Math.Sqrt(d)) / (2 * a));
            roots.Add((-b + Math.Sqrt(d)) / (2 * a));
            State = EquationState.RootsFound;
            return this;              // Два кореня
        }

        override public String ToString()
        {
            return State switch
            {
                EquationState.NoData => "Немає даних",
                EquationState.NotSolved => "Рiвняння не було розв\'язане",
                EquationState.InfinityOfRoots => "Безлiч коренiв",
                _ => RootsCount switch
                {
                    0 => "Немає коренiв",
                    1 => "Корiнь: " + GetRoot(0),
                    2 => "Коренi: " + GetRoots(),
                    _ => "Невiдома помилка!",
                },
            };
        }

        /// <summary>
        /// Формує рядок з результатами рівняння (два корені)
        /// </summary>
        /// <returns>рядок з результатами рівняння</returns>
        private string GetRoots()
        {
            return "X1 = " + roots[0] + " \tX2 = " + roots[1];
        }

        /// <summary>
        /// Здійснює тестування розв'язання рівняння для одного значення t
        /// </summary>
        /// <param name="t">параметр</param>
        public void Test(double t)
        {
            Console.Write(" Quadratic: t = " + t);
            Solve(t);
            switch (State)
            {
                case EquationState.NotSolved:
                    {
                        Console.WriteLine(" \tРівняння не розв\'язувалось!");
                        return;
                    }
                case EquationState.NoRoots:
                    {
                        Console.WriteLine(" \tНемає коренiв!");
                        return;
                    }
                case EquationState.InfinityOfRoots:
                    {
                        Console.WriteLine(" \tБезмежна кiлькiсть коренiв!");
                        return;
                    }
            }
            for (int i = 0; i < roots.Count; i++)
            {
                Console.Write("   \t X{0} = {1:F6} ", i + 1, roots[i]);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Здійснює тестування розв'язання рівняння для діапазону значень t
        /// </summary>
        /// <param name="from">початок інтервалу</param>
        /// <param name="to">кінець інтервалу</param>
        /// <param name="step">крок</param>
        public void Test(double from, double to, double step)
        {
            for (double t = from; t <= to; t += step)
            {
                Test(t);
            }
        }
    }
}