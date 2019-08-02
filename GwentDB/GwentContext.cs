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
        public GwentContext() : base("GwentDB")
        {
            Database.SetInitializer<GwentContext>(new DropCreateDatabaseIfModelChanges<GwentContext>());
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Ability> Abilities { get; set; }
    }
}
