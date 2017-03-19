using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightAndDark
{
    public class Statistics
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int checkID { get; set; }
        public int check { get; set; }
        public int enemycheck { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int HP { get; set; }
        public int AP { get; set; }
        //In game currency, obtainable from monsters, Fragmets or shards of light, tears..
        public int Tears { get; set; }
        public Statistics()
        {
        }
        // Statistics input
        public override string ToString()
        {
            return "ID" + ID + " Name " + Name + " Type " + Type + " HP " + HP;
        }

    }
}
