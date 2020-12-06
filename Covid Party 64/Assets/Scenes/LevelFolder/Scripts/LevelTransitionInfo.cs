using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitionInfos
{
    static class LevelTransitionInfo
    {
        private static bool isNextLevelPlayerUpgrade = false;
        private static int gunLevel = 1;
        private static int armorLevel = 1;
        
        public static bool IsNextLevelPlayerUpgrade { get => isNextLevelPlayerUpgrade; set => isNextLevelPlayerUpgrade = value; }
        public static int GunLevel { get => gunLevel; set => gunLevel = value; }
        public static int ArmorLevel { get => armorLevel; set => armorLevel = value; }
    }
}
