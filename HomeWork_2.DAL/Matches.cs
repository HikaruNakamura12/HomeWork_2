using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2.DAL
{
    public class Matches
    {
        public int Id { get; set; }
        public int First_Command_Id { get; set; }
        public Command Command1 { get; set; }
        public int Second_Command_Id { get; set; }
        public Command Command2 { get; set; }
        public int Cound_Goals_1 { get; set; }
        public int Count_Goals_2 { get; set; }
        public int MatchYear { get; set; }

        public List<Goal> Goals { get; set; }
    }
}
