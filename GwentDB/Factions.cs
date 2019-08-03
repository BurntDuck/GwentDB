using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentDB
{
    /// <summary>
    /// A list of Gwent factions.
    /// </summary>
    public enum Factions
    {
        Monsters,
        Neutral,
        [Description("Northern Realms")]
        NorthernRealms,
        Niflgaard,
        [Description("Scoia'tael")]
        ScoiaTael,
        Skellige
    }
}
