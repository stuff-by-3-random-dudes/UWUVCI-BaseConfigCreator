using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWUVCI_BaseConfigCreator.Classes.ENUM;

namespace UWUVCI_BaseConfigCreator.Classes
{
    [Serializable]
    public class GameBases
    {
        public string Name { get; set; }
        public Regions Region { get; set; }
        public string Path { get; set; }
        public string Tid { get; set; }
        public int KeyHash { get; set; }
    }
}
