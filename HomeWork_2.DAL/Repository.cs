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
        // for commands
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
        // for Matches

        public Matches GetMatchById(int id)
        {
            return dBContext.Matches.Where(m => m.Id == id).FirstOrDefault();
        }
        public Matches GetMatchByYear(int Year)
        {
            return dBContext.Matches.Where(m => m.MatchYear == Year).FirstOrDefault();
        }
        public List<Matches> GetMatchesByCommand(string command)
        {
            return dBContext.Matches.Where(c => c.Command1.Name == command || c.Command2.Name == command).ToList();
        }

        public void AddMatch(Matches match)
        {
            dBContext.Matches.Add(match);
            dBContext.SaveChanges();
        }
        //

        public void UpdateMatch(Matches match)
        {
            dBContext.Matches.Update(match);
            dBContext.SaveChanges();
        }

        public void DeleteMatch(Matches match)
        {
            dBContext.Matches.Remove(match);
            dBContext.SaveChanges();
        }

        public List<Matches> GetAllMatches()
        {
            return dBContext.Matches.ToList();
        }

        public Matches GetMatchByCommandsAndDate(string command1, string command2, int year)
        {
            return dBContext.Matches.FirstOrDefault(m => (m.Command1.Name == command1 && m.Command2.Name == command2 && m.MatchYear == year) || (m.Command1.Name == command2 && m.Command2.Name == command1 && m.MatchYear == year));
        }

        // Goal
        public Player GetPlayerByGoalData(int year)
        {
            var goal = dBContext.Goals.FirstOrDefault(p => p.Year == year);
            return dBContext.Players.FirstOrDefault(p => p.Id == goal.PlayerId);
        }
    }
}
