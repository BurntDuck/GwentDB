using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentDB
{
    public class GwentContext : DbContext
    {
        /// <summary>
        /// Database context class for Entity Framework.
        /// </summary>
        public GwentContext() : base("GwentDB")
        {
            Database.SetInitializer<GwentContext>(new DropCreateDatabaseIfModelChanges<GwentContext>());
        }

        /// <summary>
        /// The database card table.
        /// </summary>
        public DbSet<Card> Cards { get; set; }
        /// <summary>
        /// The database ability table.
        /// </summary>
        public DbSet<Ability> Abilities { get; set; }
    }
}
