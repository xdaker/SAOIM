using System;
using System.IO;
using System.Xml.Serialization;

namespace Tools
{
    public static class Xml序列化
    {
        public static void 保存(string filePath, object sourceObj,
            Type type, string xmlRootName)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
            {
                type = type ?? sourceObj.GetType();

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                    xns.Add("", "");
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        string.IsNullOrWhiteSpace(xmlRootName)
                            ? new System.Xml.Serialization.XmlSerializer(type)
                            : new System.Xml.Serialization.XmlSerializer(type,
                                new XmlRootAttribute(xmlRootName));
                    xmlSerializer.Serialize(writer, sourceObj,xns);
                }
            }
        }

        public static T 读取<T>(string filePath) where T : class 
        {
            object result = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(T));
                    result = xmlSerializer.Deserialize(reader);
                }
            }

            return result as T;
        }
    }
}