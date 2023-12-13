namespace QuadraticLib
{
    /// <summary>
    /// Абстрактне представлення функції та методу її тестування
    /// </summary>
    abstract public class AFunction
    {
        /// <summary>
        /// Абстрактний опис функції
        /// </summary>
        /// <param name="t">параметр</param>
        /// <returns>дійсне значення</returns>
        abstract public double Func(double t);

        /// <summary>
        /// Виводить на консоль таблицю значень аргументу та функції
        /// </summary>
        /// <param name="name">ім'я функції</param>
        /// <param name="from">початок інтервалу</param>
        /// <param name="to">кінець інтервалу</param>
        /// <param name="step">крок</param>
        public void Test(string name, double from, double to, double step)
        {
            Console.WriteLine("*********** " + GetType() + " ***********");
            for (double t = from; t <= to; t += step)
                // Форматоване виведення аргументу та функції:
                Console.WriteLine("t = {1}   \t {0}(t) = {2}", name, t, Func(t));
        }
    }

    /// <summary>
    /// Коефіцієнт функції f(t)
    /// </summary>
    public struct ACoef
    {
        // Атрибути необхідні для керування записом у XML-документ 
        // під час майбутньої серіалізації
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Value { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Index { get; set; }
    }

    /// <summary>
    /// Клас, який представляє функцію f(t)
    /// </summary>
    public class FFunction : AFunction
    {
        /// <summary>
        /// Список коефіцієнтів
        /// </summary>
        public List<ACoef> ACoefs { get; set; } = new();

        /// <summary>
        /// Індексатор для доступу до коефіцієнтів
        /// </summary>
        public ACoef this[int index]
        {
            get { return ACoefs[index]; }
            set { ACoefs[index] = value; }
        }

        /// <summary>
        /// Повертає кількість коефіцієнтів A
        /// </summary>
        public int ACount
        {
            get { return ACoefs.Count; }
        }

        /// <summary>
        /// Додає новий елемент до списку
        /// </summary>
        /// <param name="value">новий елемент</param>
        public void AddA(double value, int index)
        {
            ACoefs.Add(new ACoef { Value = value, Index = index });
        }

        /// <summary>
        /// Видаляє останній елемент зі списку
        /// </summary>
        public void RemoveLastA()
        {
            ACoefs.RemoveAt(ACoefs.Count - 1);
        }

        public double GetValue(int index)
        {
            return new List<ACoef>(from a in ACoefs where a.Index == index select a)[0].Value;
        }
        /// <summary>
        /// Обчислює функцію f(t)
        /// </summary>
        /// <param name="t">параметр</param>
        /// <returns>дійсне значення</returns>
        public override double Func(double t)
        {
            double p = 1;
            for (int i = 0; i < ACount - 1; i++)
                p *= GetValue(i) + GetValue(i + 1);
            return p - t;
        }
    }

    /// <summary>
    /// Пара чисел
    /// </summary>
    public struct XYCoef
    {
        // Атрибути необхідні для керування записом у XML-документ 
        // під час майбутньої серіалізації
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double X { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Y { get; set; }
    }

    /// <summary>
    /// Клас, який представляє функцію g(t)
    /// </summary>
    public class GFunction : AFunction
    {
        /// <summary>
        /// Список пар
        /// </summary>
        public List<XYCoef> XYCoefs { get; set; } = new();

        /// <summary>
        /// Реалізація індексатору через список
        /// </summary>
        public XYCoef this[int index]
        {
            get { return XYCoefs[index]; }
            set { XYCoefs[index] = value; }
        }

        /// <summary>
        /// Повертає кількість пар
        /// </summary>
        public int PairsCount
        {
            get { return XYCoefs.Count; }
        }

        /// <summary>
        /// Додає новий елемент до списку пар
        /// </summary>
        /// <param name="p">нова пара</param>
        public void AddXY(XYCoef p)
        {
            XYCoefs.Add(p);
        }

        /// <summary>
        /// Додає новий елемент до списку пар
        /// </summary>
        /// <param name="x">нове значення x</param>
        /// <param name="y">нове значення y</param>
        public void AddXY(double x, double y)
        {
            XYCoefs.Add(new XYCoef { X = x, Y = y });
        }
        /// <summary>
        /// Видаляє останній елемент зі списку пар
        /// </summary>
        public void RemoveLastPair()
        {
            XYCoefs.RemoveAt(XYCoefs.Count - 1);
        }
        /// <summary>
        /// Генерує ітератор для обходу пар
        /// </summary>
        /// <returns>ітератр</returns>
        public IEnumerator<XYCoef> GetEnumerator()
        {
            for (int i = 0; i < PairsCount; i++)
                yield return this[i];
        }

        /// <summary>
        /// Обчислює функцію g(t)
        /// </summary>
        /// <param name="t">параметр</param>
        /// <returns>дійсне значення</returns>
        public override double Func(double t)
        {
            double sum = 0;
            foreach (XYCoef p in this)
                sum += p.X * p.Y;
            return sum + t;
        }
    }
}