using System.Xml;
using System.Xml.Serialization;

namespace QuadraticLib
{
    /// <summary>
    /// Представляє дані для розв'язання квадратного рівняння
    /// </summary>
    public class EquationData
    {
        /// <summary>
        /// Представляє функцію f списком
        /// </summary>
        public FFunction FFunction { get; set; }

        /// <summary>
        /// Представляє функцію g списком
        /// </summary>
        public GFunction GFunction { get; set; }

        /// <summary>
        /// Коефіцієнт C
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double CCoef { get; set; }

        /// <summary>
        /// здійснює ініціалізацію властивостей
        /// </summary>
        public EquationData()
        {
            FFunction = new();
            GFunction = new();
        }
    }

    /// <summary>
    /// Розширює клас для представлення квадратного рівняння 
    /// можливостями читання та запису вихідних даних
    /// </summary>
    public class XMLQuadratic : Quadratic
    {
        /// <summary>
        /// Дані для розв'язання квадратного рівняння
        /// </summary>
        public EquationData Data { get; set; } = new EquationData();

        /// <summary>
        /// коефіцієнт c
        /// </summary>
        public override double C
        {
            get { return Data.CCoef; }
            set { Data.CCoef = value; }
        }

        public void ClearEquation()
        {
            Data = new();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public XMLQuadratic()
        {
            Data = new EquationData();
            F = Data.FFunction.Func;
            G = Data.GFunction.Func;
        }

        public XMLQuadratic(string fileName)
        {
            ReadFromXML(fileName);
        }

        /// <summary>
        /// Здійснює читання з XML-документу
        /// </summary>
        /// <param name="fileName">ім'я файлу</param>
        /// <returns>поточний об'єкт зі зміненим станом</returns>
        public XMLQuadratic ReadFromXML(string fileName)
        {
            XmlSerializer deserializer = new(typeof(EquationData));
            using TextReader textReader = new StreamReader(fileName);
            Data = (deserializer.Deserialize(textReader) as EquationData) ?? new();
            F = Data.FFunction.Func;
            G = Data.GFunction.Func;
            State = EquationState.NotSolved;
            return this;
        }

        /// <summary>
        /// Здійснює запис у XML-документ
        /// </summary>
        /// <param name="fileName">ім'я файлу</param>
        public void WriteToXML(string fileName)
        {
            XmlSerializer serializer = new(typeof(EquationData));
            using var textWriter = new StreamWriter(fileName);
            using var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { Indent = true });
            serializer.Serialize(xmlWriter, Data);
        }
        /// <summary>
        /// Генерує звіт про роботу програми в форматі HTML
        /// </summary>
        /// <param name="fileName">iм\'я файлу</param>
        /// <param name="t">параметр t</param>
        public void GenerateReport(string fileName, double t)
        {
            using StreamWriter writer = new(fileName);
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>");
            writer.WriteLine("<title>Звіт з розв\'язання квадратного рівняння</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<h2>Звіт</h2>");
            writer.WriteLine("<p>У результаті розв\'язання квадратного рівняння, з наступними вихідними даними:</p>");
            writer.WriteLine("<p>Функція F:</p>");
            writer.WriteLine("<table border = '1' cellpadding=4 cellspacing=0>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<th>№</th>");
            writer.WriteLine("<th>a</th>");
            writer.WriteLine("</tr>");
            for (int i = 0; i < Data.FFunction.ACoefs.Count; i++)
            {
                writer.WriteLine("<tr>");
                writer.WriteLine("<td>" + Data.FFunction.ACoefs[i].Index + "</td>");
                writer.WriteLine("<td>" + Data.FFunction.ACoefs[i].Value + "</td>");
                writer.WriteLine("</tr>");
            }
            writer.WriteLine("</table>");
            writer.WriteLine("<p>Функція G:</p>");
            writer.WriteLine("<table border = '1' cellpadding=4 cellspacing=0>");
            writer.WriteLine("<tr>");
            writer.WriteLine("<th>№</th>");
            writer.WriteLine("<th>X</th>");
            writer.WriteLine("<th>Y</th>");
            writer.WriteLine("</tr>");
            for (int i = 0; i < Data.GFunction.XYCoefs.Count; i++)
            {
                writer.WriteLine("<tr>");
                writer.WriteLine("<td>" + (i + 1) + "</td>");
                writer.WriteLine("<td>" + Data.GFunction.XYCoefs[i].X + "</td>");
                writer.WriteLine("<td>" + Data.GFunction.XYCoefs[i].Y + "</td>");
                writer.WriteLine("</tr>");
            }
            writer.WriteLine("</table>");
            Solve(t);
            if (State == EquationState.InfinityOfRoots)
                writer.WriteLine("<p>було встановлено, що рівняння має безліч коренів.</p>");
            else
            {
                if (RootsCount > 0)
                {
                    writer.WriteLine("<p>були отримані наступні корені: </p>");
                    writer.WriteLine("<p>");
                    for (int i = 0; i < RootsCount; i++)
                        writer.WriteLine(GetRoot(i) + "<br>");
                    writer.WriteLine("</p>");
                }
                else
                {
                    writer.WriteLine("було встановлено, що рівняння не має коренів");
                }
            }
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
    }
}