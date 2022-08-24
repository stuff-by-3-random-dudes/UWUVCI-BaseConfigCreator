using GameBaseClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace UWUVCI_BaseConfigCreator.Classes
{
    /// <summary>
    /// A class to export/import shit
    /// </summary>
    class VCIFile
    {
        public static void ExportFile(List<GameBases> precomp, GameConsoles console)
        {
            CheckAndFixConfigFolder();
            using (Stream createConfigStream = new FileStream($@"configs\bases.vcb{console.ToString().ToLower()}", FileMode.Create, FileAccess.Write))
                using(GZipStream compressedStream = new(createConfigStream, CompressionMode.Compress)) 
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(compressedStream, precomp);
                }

            ReadFromConfig($@"configs\bases.vcb{console.ToString().ToLower()}");
        }
        private static void CheckAndFixConfigFolder()
        {
            Directory.CreateDirectory(@"configs");
        }
        public static void ReadFromConfig(string configPath)
        {
            FileInfo fn = new(configPath);
            if (fn.Extension.Contains("vcb"))
            {
                using FileStream inputConfigStream = new(configPath, FileMode.Open, FileAccess.Read);
                using GZipStream decompressedConfigStream = new(inputConfigStream, CompressionMode.Decompress);
                IFormatter formatter = new BinaryFormatter();
                List<GameBases> to_export = (List<GameBases>)formatter.Deserialize(decompressedConfigStream);
            }
        }
    }
}
