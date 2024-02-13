using System.Text.Json.Serialization;

namespace GameEntitiesBase.Entities
{
    public static class Available
    {
        public enum Profession
        {
            Barbarian,
            Knight
        }
        public enum Race
        {
            Human,
            Dwarf,
            Elf
        }
        public enum Gender
        {
            Male,
            Female
        }
    }
}
