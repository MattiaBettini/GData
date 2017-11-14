﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GData
{
    public static class GData
    {
        public static T Load<T>(string path,ModeType mode)
            where T : class, new()
        {
            if (mode == ModeType.Binary)
                return BinaryLoad<T>(path);
            else
                return XmlLoad<T>(path);
        }

        #region Load Implementation
        private static T BinaryLoad<T>(string path)
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

        private static T XmlLoad<T>(string path)
            where T : class, new()
        {
            T instance = new T();
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                instance = (T)serializer.Deserialize(stream);

                return instance;
            }
        }
        #endregion

        public static void Save<T>(string path, ModeType mode)
            where T : class, new()
        {
            if (mode == ModeType.Binary)
                BinarySave<T>(path);
            else
                XmlSave<T>(path);
        }

        #region Save Implementation
        private static void BinarySave<T>(string path)
            where T : class, new()
        {
            using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, new T());
            }
        }

        private static void XmlSave<T>(string path)
            where T : class, new()
        {
            using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, new T());
            }
        }
        #endregion

    }
}
