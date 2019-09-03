using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentDB
{
    /// <summary>
    /// A list of Gwent board position
    /// </summary>
    public enum Positions
    {
        Unknown = 0,
        Melee = 1,
        Archer = 2,
        Siege = 3,
        Any = 4,
        Leader = 5
    }
}
