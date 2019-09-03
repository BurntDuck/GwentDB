using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GwentDB
{
    public class Card
    {
        private Positions position;
        private Types type;

        /// <summary>
        /// A Gwent card.
        /// </summary>
        public Card() { }

        /// <summary>
        /// A Gwent card
        /// </summary>
        /// <param name="faction">The faction to which this card belongs.</param>
        /// <param name="name">The card's name.</param>
        /// <param name="position">The card's board position.</param>
        /// <param name="strength">The card's strength value.</param>
        /// <param name="type">The card's type.</param>
        /// <param name="inCollection">Whether the card is in the user's collection.</param>
        public Card(Factions faction, string name, Positions position, int strength, Types type, bool inCollection)
        {
            Faction = faction;
            Name = name;
            Position = position;
            Strength = strength;
            Type = type;
            InCollection = inCollection;
        }

        /// <summary>
        /// A Gwent card
        /// </summary>
        /// <param name="faction">The faction to which this card belongs.</param>
        /// <param name="name">The card's name.</param>
        /// <param name="position">The card's board position.</param>
        /// <param name="strength">The card's strength value.</param>
        /// <param name="ability">The card's special ability.</param>
        /// <param name="type">The card's type.</param>
        /// <param name="inCollection">Whether the card is in the user's collection.</param>
        public Card(Factions faction, string name, Positions position, int strength, Ability ability, Types type, bool inCollection)
        {
            Faction = faction;
            Name = name;
            Position = position;
            Strength = strength;
            Ability = ability;
            Type = type;
            InCollection = inCollection;
        }

        /// <summary>
        /// The card's database id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The faction to which this card belongs.
        /// </summary>
        public Factions Faction { get; set; }
        /// <summary>
        /// The card's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The card's board position.
        /// </summary>
        public Positions Position
        {
            get => position;
            set
            {
                if(value == Positions.Leader)
                {
                    if(Type == Types.Leader || Type == Types.Unknown)
                        position = value;
                    else
                        throw new ArgumentException();
                }
            }
        }
        /// <summary>
        /// The card's strength value.
        /// </summary>
        public int Strength { get; set; }
        /// <summary>
        /// The card's special ability.
        /// </summary>
        public Ability Ability { get; set; }
        /// <summary>
        /// The card's type.
        /// </summary>
        public Types Type
        {
            get => type;
            set
            {
                if(value == Types.Leader)
                {
                    if(Position == Positions.Leader || Position == Positions.Unknown)
                        type = value;
                    else
                        throw new ArgumentException();
                }
            }
        }
        /// <summary>
        /// Whether the card is in the user's collection.
        /// </summary>
        public bool InCollection { get; set; }

        /// <summary>
        /// Update this card in the database.
        /// </summary>
        /// <param name="faction">The updated faction to which this card belongs.</param>
        /// <param name="name">The card's updated name.</param>
        /// <param name="position">The card's updated board position.</param>
        /// <param name="strength">The card's updated strength value.</param>
        /// <param name="ability">The card's updated special ability.</param>
        /// <param name="type">The card's updated type.</param>
        /// <param name="inCollection">updated collection status.</param>
        public void Update(Factions faction, string name, Positions position, int strength, Ability ability, Types type, bool inCollection)
        {
            Faction = faction;
            Name = name;
            Position = position;
            Strength = strength;
            Ability = ability;
            Type = type;
            InCollection = inCollection;
        }
        /// <summary>
        /// Update this card in the database.
        /// </summary>
        /// <param name="faction">The updated faction to which this card belongs.</param>
        /// <param name="name">The card's updated name.</param>
        /// <param name="position">The card's updated board position.</param>
        /// <param name="strength">The card's updated strength value.</param>
        /// <param name="type">The card's updated type.</param>
        /// <param name="inCollection">updated collection status.</param>
        public void Update(Factions faction, string name, Positions position, int strength, Types type, bool inCollection)
        {
            Faction = faction;
            Name = name;
            Position = position;
            Strength = strength;
            Type = type;
            InCollection = inCollection;
        }
    }
}
