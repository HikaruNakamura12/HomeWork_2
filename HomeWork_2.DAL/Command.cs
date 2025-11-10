using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2.DAL
{
    public class Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Won { get; set; }
        public int Lose { get; set; }
        public int Draw {  get; set; }
        public int Score { get; set; }

        public List<Player> Players { get; set; }
        public List<Goal> Goals { get; set; }
    }
}
