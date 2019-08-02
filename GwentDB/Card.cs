using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentDB
{
    public class Card
    {
        public Card() { }

        public Card(string faction, string name, string position, int strength, Ability ability, string type)
        {
            Faction = faction;
            Name = name;
            Position = position;
            Strength = strength;
            Ability = ability;
            Type = type;
        }

        public int Id { get; set; }
        public string Faction { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Strength { get; set; }
        public Ability Ability { get; set; }
        public string Type { get; set; }
    }
}
