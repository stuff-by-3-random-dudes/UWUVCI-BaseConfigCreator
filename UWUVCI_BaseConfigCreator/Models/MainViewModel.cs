using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWUVCI_BaseConfigCreator.Classes;
using UWUVCI_BaseConfigCreator.Classes.ENUM;
using UWUVCI_BaseConfigCreator.Classes.Exceptions;

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

        private List<GameConsole> lConsoles = new List<GameConsole>();

        public List<GameConsole> LConsoles
        {
            get { return lConsoles; }
            set { 
                lConsoles = value;
                OnPropertyChanged();
            }
        }

        private GameConsole gameConsole;

        public GameConsole GameConsole
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
            Regions = Enum.GetValues(typeof(Regions)).Cast<Regions>().ToList();
            LConsoles = Enum.GetValues(typeof(GameConsole)).Cast<GameConsole>().ToList();
        }

        public void AddBaseToList()
        {
          
            if (String.IsNullOrWhiteSpace(TempBase.Name))
            {
                throw new GameBaseException($"A BaseName with no value is not valid");
            }
            if (String.IsNullOrWhiteSpace(TempBase.Tid))
            {
                throw new GameBaseException($"A TitleID with no value is not valid");
            }
            if (String.IsNullOrWhiteSpace(NonHashedKey))
            {
                throw new GameBaseException($"A TitleKey with no value is not valid");
            }
            TempBase.KeyHash = NonHashedKey.GetHashCode();
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
            VCIFile.ExportFile(LGameBases, GameConsole);
        }
    }
}
