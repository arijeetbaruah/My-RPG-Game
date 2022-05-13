using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace RPG.Combat
{
    public class CharacterStats : SerializedMonoBehaviour
    {
        public Dictionary<Skill, Stat> stats = null;
    }

    [InlineProperty]
    public struct Stat
    {
        [ProgressBar(0, 999), HideLabel]
        public int value;
    }

    public enum Skill
    {
        MaxHP,
        Strength,
        Intelligence,
        Endurance,
        Agility,
        Luck
    }
}
