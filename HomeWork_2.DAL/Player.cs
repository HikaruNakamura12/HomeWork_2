using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2.DAL
{
    public class Player
    {
        public int Id {  get; set; }
        public string FName { get; set; }
        public string City { get; set; }
        public int PlayerNumber { get; set; }
        public string Position { get; set; }

        public int CommandId {  get; set; }
        public Command Comand { get; set; }

        public List<Goal> Goals { get; set; }
    }
}
