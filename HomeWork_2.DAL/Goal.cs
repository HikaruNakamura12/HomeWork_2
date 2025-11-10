using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2.DAL
{
    public class Goal
    {
        public int Id { get; set; }
        public int MatchId {  get; set; }
        public Matches Match { get; set; }
        public int PlayerId {  get; set; }

        public int Year {  get; set; }
    }
}
