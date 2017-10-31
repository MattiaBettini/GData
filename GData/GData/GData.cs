using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GData
{
    public static class GData
    {
        public static T Load<T>(string path)
            where T : class, new()
        {
            T instance = new T();
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                instance = (T)formatter.Deserialize(stream);

                return instance;
            }
        }

        public static void Save<T>(T objtosave, string path)
            where T : class, new()
        {
            using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, objtosave);
            }
        }

    }
}
