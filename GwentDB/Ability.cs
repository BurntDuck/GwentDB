using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentDB
{
    public class Ability
    {
        /// <summary>
        /// Special card ability.
        /// </summary>
        public Ability() { }

        /// <summary>
        /// Special card ability.
        /// </summary>
        /// <param name="ability">The name of the ability.</param>
        public Ability(string ability)
        {
            Name = ability;
        }

        /// <summary>
        /// The ability's id in the database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the ability.
        /// </summary>
        public string Name { get; set; }

        public void Update(string ability)
        {
            Name = ability;
        }
    }
}
