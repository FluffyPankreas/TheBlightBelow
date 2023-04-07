using System;

namespace DarkMushroomGames.GameArchitecture
{
    [Serializable]
    public class ModifierInformation
    {
        public ModifierType type;
        public int value;

        public ModifierInformation(ModifierType type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
