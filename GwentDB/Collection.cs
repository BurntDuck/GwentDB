using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentDB
{
    public class Collection
    {
        public Collection()
        {

        }

        public int Id { get; set; }
        public List<Card> Cards { get; set; }
    }
}
