using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game.Scripts.Environment;
using UnityEngine;

namespace Game.Scripts.Core
{
    public static class SaveSystem
    {
        private const string AreaPath = "/Area.game";
    
        public static void SaveArea(AreaManager areaManager)
        {
            var binaryFormatter = new BinaryFormatter();
            var path = Application.persistentDataPath + AreaPath;
            var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            var areaData = new AreaData(areaManager);
            binaryFormatter.Serialize(fileStream, areaData);
            fileStream.Close();
            Debug.Log("saved");
        }

        public static AreaData LoadArea()
        {
            string path = Application.persistentDataPath + AreaPath;

            if (File.Exists(path))
            {
                var binaryFormatter = new BinaryFormatter();
                var fileStream = new FileStream(path, FileMode.Open);
                var areaData = binaryFormatter.Deserialize(fileStream) as AreaData;
                fileStream.Close();
            
                return areaData;
            }
            else
            {
                Debug.Log("Area save file not found");

                return null;
            }
        }

        public static void ResetArea()
        {
            string path = Application.persistentDataPath + AreaPath;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.Log("Area save file not found");
            }
        }
    }
}
