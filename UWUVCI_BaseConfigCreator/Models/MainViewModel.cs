using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWUVCI_BaseConfigCreator.Classes.Exceptions;
using GameBaseClassLibrary;
using System.Windows.Forms;

namespace UWUVCI_BaseConfigCreator
{
    class MainViewModel : BaseModel
    {
        private List<Regions> regions = new List<Regions>();

        public List<Regions> Regions
        {
            get { return regions; }
            set { regions = value;
                OnPropertyChanged();
            }
        }

        private List<GameConsoles> lConsoles = new List<GameConsoles>();

        public List<GameConsoles> LConsoles
        {
            get { return lConsoles; }
            set { 
                lConsoles = value;
                OnPropertyChanged();
            }
        }

        private GameConsoles gameConsole;

        public GameConsoles GameConsole
        {
            get { return gameConsole; }
            set { gameConsole = value; }
        }

        private List<GameBases> lGameBases = new List<GameBases>();

        public List<GameBases> LGameBases
        {
            get { return lGameBases; }
            set 
            {
                lGameBases = value;
                OnPropertyChanged();
            }
        }

       

        private string nonHashedKey;

        public string NonHashedKey
        {
            get { return nonHashedKey; }
            set { nonHashedKey = value;
                OnPropertyChanged();
            }
        }
        private GameBases tempBase = new GameBases();

        public GameBases TempBase
        {
            get { return tempBase; }
            set { tempBase = value;
                OnPropertyChanged();
            }
        }

        private GameBases selectedGameBase;

        public GameBases SelectedGameBase
        {
            get { return selectedGameBase; }
            set 
            {
                selectedGameBase = value;
                OnPropertyChanged();
            }
        }



        public MainViewModel()
        {
            Regions = Enum.GetValues(typeof(GameBaseClassLibrary.Regions)).Cast<Regions>().ToList();
            LConsoles = Enum.GetValues(typeof(GameConsoles)).Cast<GameConsoles>().ToList();
        }
        public void ReadBases()
        {
            var ofd = new OpenFileDialog();
            DialogResult res = ofd.ShowDialog();
            if(res == DialogResult.OK)
            {
                LGameBases = VCBTool.ReadBasesFromVCB(ofd.FileName);
            }
            
        }
        public void AddBaseToList()
        {
          
            if (String.IsNullOrWhiteSpace(TempBase.Name))
            {
                throw new GameBaseException($"A BaseName with no value is not valid!");
            }
            if (String.IsNullOrWhiteSpace(TempBase.Tid))
            {
                throw new GameBaseException($"A TitleID with no value is not valid!");
            }
            if (String.IsNullOrWhiteSpace(NonHashedKey))
            {
                throw new GameBaseException($"A TitleKey with no value is not valid!");
            }
            TempBase.KeyHash = NonHashedKey.ToLower().GetHashCode();
            NonHashedKey = string.Empty;
            LGameBases.Add(TempBase);
            TempBase = new GameBases();
        }

        public void RemoveSelectedGameBase()
        {
            if (SelectedGameBase == null)
            {
                throw new GameBaseException("The GameBase that you want to delete has to be selected first");
            }
            else
            {
                LGameBases.Remove(SelectedGameBase);
                SelectedGameBase = null;
            }
        }

        public void SaveBasesToFile()
        {
            if(LGameBases.Count < 1)
            {
                throw new GameBaseException("You need to add Bases first!");
            }
            if (GameConsole == GameConsoles.GCN) GameConsole = GameConsoles.WII;
            VCBTool.ExportFile(LGameBases, GameConsole, @"config");
        }

        public void moveSelBaseUp()
        {

            int length = LGameBases.Count - 1;
            if(length > 0 && selectedGameBase != null)

            {
                List<GameBases> backupList = new List<GameBases>();
                foreach (GameBases gb in LGameBases)
                {
                    backupList.Add(gb);
                }
                    int selBaseID = 0;
                foreach (GameBases gb in backupList)
                {
                    if (gb.Name == SelectedGameBase.Name && gb.KeyHash == SelectedGameBase.KeyHash && gb.Region == SelectedGameBase.Region && gb.Tid == SelectedGameBase.Tid)
                    {
                        break;
                    }
                    selBaseID++;
                }
                if (selBaseID == length)
                {
                    GameBases SavedBase = backupList[selBaseID - 1];
                    backupList[selBaseID - 1] = selectedGameBase;
                    backupList[selBaseID] = SavedBase;
                }
                else
                {
                    GameBases SavedBase = backupList[selBaseID - 1];
                    backupList[selBaseID - 1] = selectedGameBase;
                    backupList[selBaseID] = SavedBase;
                }
                LGameBases = backupList;
            }
            
        }
        public void moveSelBaseDown()
        {

            int length = LGameBases.Count - 1;
            if (length > 0 && selectedGameBase != null)

            {
                List<GameBases> backupList = new List<GameBases>();
                foreach (GameBases gb in LGameBases)
                {
                    backupList.Add(gb);
                }
                int selBaseID = 0;
                foreach (GameBases gb in backupList)
                {
                    if (gb.Name == SelectedGameBase.Name && gb.KeyHash == SelectedGameBase.KeyHash && gb.Region == SelectedGameBase.Region && gb.Tid == SelectedGameBase.Tid)
                    {
                        break;
                    }
                    selBaseID++;
                }
                if (selBaseID == length)
                {
                }
                else
                {
                    GameBases SavedBase = backupList[selBaseID + 1];
                    backupList[selBaseID + 1] = selectedGameBase;
                    backupList[selBaseID] = SavedBase;
                }
                LGameBases = backupList;
            }

        }
    }
}
