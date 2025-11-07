using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2.DAL
{
    public class Repository
    {
        private AppDBcontext dBContext;

        public Repository()
        {
            dBContext = new AppDBcontext();
        }

        public void Add(Command command)
        {
            dBContext.Commands.Add(command);
            dBContext.SaveChanges();
        }
        public void Update(Command command)
        {
            dBContext.Commands.Update(command);
            dBContext.SaveChanges();
        }
        public void Delete(Command command)
        {
            dBContext.Remove(command);
            dBContext.SaveChanges();
        }

        public List<Command> GetAll() { return dBContext.Commands.ToList();}

        public List<Command> GetByName(string name)
        {
            return dBContext.Commands.Where(n => n.Name == name).ToList();
        }
        public List<Command> GetByCity(string city)
        {
            return dBContext.Commands.Where(n => n.City == city).ToList();
        }
        public List<Command> GetByNameCity(string name, string city)
        {
            return dBContext.Commands.Where(n => n.Name == name && n.City == city).ToList();
        }
        public List<Command> MostWon()
        {
            var max = dBContext.Commands.Max(w => w.Won);
            return dBContext.Commands.Where(n => n.Won == max).ToList();
        }
        public List<Command> MostLose()
        {
            var max = dBContext.Commands.Max(w => w.Lose);
            return dBContext.Commands.Where(n => n.Lose == max).ToList();
        }
        public List<Command> MostDraw()
        {
            var max = dBContext.Commands.Max(w => w.Draw);
            return dBContext.Commands.Where(n => n.Draw == max).ToList();
        }
        public List<Command> MostScore()
        {
            var max = dBContext.Commands.Max(w => w.Score);
            return dBContext.Commands.Where(n => n.Score == max).ToList();
        }
    }
}
