using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentDB
{
    public class Ability
    {
        public Ability() { }

        public Ability(string ability)
        {
            Name = ability;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
