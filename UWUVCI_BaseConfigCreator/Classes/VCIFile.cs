using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UWUVCI_BaseConfigCreator.Classes.ENUM;

namespace UWUVCI_BaseConfigCreator.Classes
{
    /// <summary>
    /// A class to export/import shit
    /// </summary>
    class VCIFile
    {
        public static void ExportFile(List<GameBases> precomp, GameConsole console)
        {
            CheckAndFixConfigFolder();
            Stream createConfigStream = new FileStream($@"configs\bases.vcb{console.ToString().ToLower()}", FileMode.Create, FileAccess.Write);
            GZipStream compressedStream = new GZipStream(createConfigStream, CompressionMode.Compress);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(compressedStream, precomp);
            compressedStream.Close();
            createConfigStream.Close();
            ReadFromConfig($@"configs\bases.vcb{console.ToString().ToLower()}");
        }
        private static void CheckAndFixConfigFolder()
        {
            if (!Directory.Exists(@"configs"))
            {
                Directory.CreateDirectory(@"configs");
            }
        }
        public static void ReadFromConfig(string configPath)
        {
            FileInfo fn = new FileInfo(configPath);
            if (fn.Extension.Contains("vcb"))
            {
                FileStream inputConfigStream = new FileStream(configPath, FileMode.Open, FileAccess.Read);
                GZipStream decompressedConfigStream = new GZipStream(inputConfigStream, CompressionMode.Decompress);
                IFormatter formatter = new BinaryFormatter();
                List<GameBases> to_export = (List<GameBases>)formatter.Deserialize(decompressedConfigStream);
               
            }

        }
    }
}
