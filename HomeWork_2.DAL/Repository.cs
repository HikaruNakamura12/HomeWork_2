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





















        public List<Player> Top3ScorerByCommand(int commandId)
        {
            var players = dBContext.Players.Where(p => p.CommandId == commandId).ToList();
            var goals = dBContext.Goals.ToList();
            var playerGoals = new List<(Player Player, int Goals)>();

            foreach (var player in players)
            {
                int goalCount = 0;
                foreach (var goal in goals)
                {
                    if (goal.PlayerId == player.Id)
                    {
                        goalCount++;
                    }
                }
                playerGoals.Add((player, goalCount));
            }

            return playerGoals.OrderByDescending(p => p.Goals).Take(3).Select(p => p.Player).ToList();
        }
        public Player GetTopScorerByCommand(int commandId)
        {
            var players = dBContext.Players.Where(p => p.CommandId == commandId).ToList();
            var goals = dBContext.Goals.ToList();
            var playerGoals = new List<(Player Player, int Goals)>();

            foreach (var player in players)
            {
                int goalCount = 0;
                foreach (var goal in goals)
                {
                    if (goal.PlayerId == player.Id)
                    {
                        goalCount++;
                    }
                }
                playerGoals.Add((player, goalCount));
            }

            return playerGoals.OrderByDescending(p => p.Goals).Select(p => p.Player).FirstOrDefault();
        }
        public List<Player> GetTop3Scorers_ofAll()
        {
            var players = dBContext.Players.ToList();
            var goals = dBContext.Goals.ToList();
            var playerGoals = new List<(Player Player, int Goals)>();

            foreach (var player in players)
            {
                int goalCount = 0;
                foreach (var goal in goals)
                {
                    if (goal.PlayerId == player.Id)
                    {
                        goalCount++;
                    }
                }
                playerGoals.Add((player, goalCount));
            }

            return playerGoals.OrderByDescending(p => p.Goals).Take(3).Select(p => p.Player).ToList();
        }

        public Player GetTopScorer_ofAll()
        {
            return GetTop3Scorers_ofAll().FirstOrDefault();
        }

        public List<Command> GetTop3Score_Commands()
        {
            var commands = dBContext.Commands.ToList();
            var matches = dBContext.Matches.ToList();
            var commandGoals = new List<(Command Command, int Goals)>();

            foreach (var command in commands)
            {
                int goalsScored = 0;
                foreach (var match in matches)
                {
                    if (match.First_Command_Id == command.Id)
                        goalsScored += match.Cound_Goals_1;
                    if (match.Second_Command_Id == command.Id)
                        goalsScored += match.Count_Goals_2;
                }
                commandGoals.Add((command, goalsScored));
            }

            return commandGoals.OrderByDescending(c => c.Goals).Take(3).Select(c => c.Command).ToList();
        }

        public Command  GetTopScore_Commands()
        {
            return GetTop3Score_Commands().FirstOrDefault();
        }
        public List<Command> GetTop3Def_Commands()
        {
            var commands = dBContext.Commands.ToList();
            var matches = dBContext.Matches.ToList();
            var commandGoals = new List<(Command Command, int Goals)>();

            foreach (var command in commands)
            {
                int goalsConceded = 0;
                foreach (var match in matches)
                {
                    if (match.First_Command_Id == command.Id)
                        goalsConceded += match.Count_Goals_2;
                    if (match.Second_Command_Id == command.Id)
                        goalsConceded += match.Cound_Goals_1;
                }
                commandGoals.Add((command, goalsConceded));
            }

            return commandGoals.OrderBy(c => c.Goals).Take(3).Select(c => c.Command).ToList();
        }
        public Command GetTopDef_Commands()
        {
            return GetTop3Def_Commands().FirstOrDefault();
        }

        public List<Command> GetTop3CommandsByPoints()
        {
            var commands = dBContext.Commands.ToList();
            var matches = dBContext.Matches.ToList();
            var commandPoints = new List<(Command Command, int Points)>();

            foreach (var command in commands)
            {
                int points = 0;
                foreach (var match in matches)
                {
                    if (match.First_Command_Id == command.Id)
                    {
                        if (match.Cound_Goals_1 > match.Count_Goals_2)
                            points += 3;
                        else if (match.Cound_Goals_1 == match.Count_Goals_2)
                            points += 1;
                    }
                    else if (match.Second_Command_Id == command.Id)
                    {
                        if (match.Count_Goals_2 > match.Cound_Goals_1)
                            points += 3;
                        else if (match.Count_Goals_2 == match.Cound_Goals_1)
                            points += 1;
                    }
                }
                commandPoints.Add((command, points));
            }

            return commandPoints.OrderByDescending(c => c.Points).Take(3).Select(c => c.Command).ToList();
        }
        public Command GetTopCommandByPoints()
        {
            return GetTop3CommandsByPoints().FirstOrDefault();
        }

        public List<Command> GetTop3CommandBy_lose_Points()
        {
            var commands = dBContext.Commands.ToList();
            var matches = dBContext.Matches.ToList();
            var commandPoints = new List<(Command Command, int Points)>();

            foreach (var command in commands)
            {
                int points = 0;
                foreach (var match in matches)
                {
                    if (match.First_Command_Id == command.Id)
                    {
                        if (match.Cound_Goals_1 > match.Count_Goals_2)
                            points += 3;
                        else if (match.Cound_Goals_1 == match.Count_Goals_2)
                            points += 1;
                    }
                    else if (match.Second_Command_Id == command.Id)
                    {
                        if (match.Count_Goals_2 > match.Cound_Goals_1)
                            points += 3;
                        else if (match.Count_Goals_2 == match.Cound_Goals_1)
                            points += 1;
                    }
                }
                commandPoints.Add((command, points));
            }

            return commandPoints.OrderBy(c => c.Points).Take(3).Select(c => c.Command).ToList();
        }

        public Command GetTopCommandBy_lose_Points()
        {
            return GetTop3CommandBy_lose_Points().FirstOrDefault();
        }


















        public void AddMatch(Matches match)
        {
            dBContext.Matches.Add(match);
            dBContext.SaveChanges();
        }

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

        public void GenerateRandomMatch()
        {
            var random = new Random();
            var comands = dBContext.Commands.ToList();
            var existMatchs = dBContext.Matches.ToList();

            var command1 = comands[random.Next(comands.Count)];
            var command2 = comands[random.Next(comands.Count)];

            while(command1.Id == command2.Id)
            {
                command2 = comands[random.Next(comands.Count)];
            }

            bool check = false;
            foreach(var match in existMatchs)
            {
                if ((match.First_Command_Id == command1.Id && match.Second_Command_Id == command2.Id) || (match.First_Command_Id == command2.Id && match.Second_Command_Id == command1.Id))
                {
                    check = true; break;
                }
            }
            if (check == false)
            {
                var match = new Matches
                {
                    First_Command_Id = command1.Id,
                    Second_Command_Id = command2.Id,
                    Cound_Goals_1 = random.Next(0, 6),
                    Count_Goals_2 = random.Next(0, 6),
                    MatchYear = random.Next(1800, 2025)
                };
                dBContext.Matches.Add(match);
                dBContext.SaveChanges();
                Console.WriteLine($"Generated match: {command1.Name} - {command2.Name}");
            }
            else
            {
                Console.WriteLine("Generation cancel");
            }
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
