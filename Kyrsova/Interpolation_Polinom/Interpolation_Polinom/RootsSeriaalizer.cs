using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Polinom_Interpolation
{
    public class RootsSerializer
    {
        public Dataset LoadDatasetFromFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Dataset));

            using (StreamReader reader = new StreamReader(fileName))
            {
                return (Dataset)serializer.Deserialize(reader);
            }
        }
        public void SaveDatasetToFile(Dataset dataset, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Dataset));

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, dataset);
            }
        }
    }
}
