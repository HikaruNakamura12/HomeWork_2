using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork_2.DAL;

namespace HomeWork_2
{
    public class Menu
    {
        private Repository rep;

        public Menu()
        {
            rep = new Repository();
        }
        private void AddC()
        {
            Console.WriteLine("Write name of the  command :");
            string name = Console.ReadLine();
            bool check = true;

            foreach (var comand in rep.GetAll())
            {
                if (comand.Name == name)
                {
                    Console.WriteLine("Err Command with this name is exists!");
                    check = false;
                    break;
                }
            }
            string city = "";
            int won = 0;
            int lose = 0;
            int score = 0;
            int draw = 0;

            if (!check)
            {
                Console.Write("City: ");
                city = Console.ReadLine();

                Console.Write("Write (Won, Lose, Score, Draw)\nExample\n 1, 2, 1, 2\n ");
                string[] parts = Console.ReadLine().Split(",").Select(p => p.Trim()).ToArray();
                won = Convert.ToInt32(parts[0]);
                lose = Convert.ToInt32(parts[1]);
                score = Convert.ToInt32(parts[2]);
                draw = Convert.ToInt32(parts[3]);
            }
            var newC = new Command
            {
                Name = name,
                City = city,
                Won = won,
                Lose = lose,
                Score = score,
                Draw = draw
            };
        }
        private void Change()
        {
            Console.WriteLine("For search command that you want to change, write command name:");
            string name = Console.ReadLine();
            bool check = false;
            foreach (var c in rep.GetAll())
            {
                if (c.Name == name)
                {
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                Console.WriteLine("Err Command was not found");
                return;
            }
            var command = rep.GetByName(name).FirstOrDefault();

            Console.WriteLine("\nChange\n0.Exit\n1.Name\n2.City\n3.Won\n4.Lose\n5.Draw\n6.Score\n");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    Console.Write("New Name: ");
                    string newName = Console.ReadLine();
                    if (newName == null)
                    {
                        break;
                    }
                    bool check1 = false;
                    foreach (var c in rep.GetAll())
                    {
                        if (c.Name == newName)
                        {
                            check1 = true;
                            break;
                        }
                    }
                    if (check1)
                    {
                        Console.WriteLine("Err Command with that name exists");
                        break;
                    }
                    command.Name = newName;
                    rep.Update(command);
                    break;
                case "2":
                    Console.Write("New City: ");
                    string newCity = Console.ReadLine();
                    command.City = newCity;
                    rep.Update(command);
                    break;
                case "3":
                    Console.Write("new count won: ");
                    command.Won = Convert.ToInt32(Console.ReadLine());
                    rep.Update(command);
                    break;
                case "4":
                    Console.Write("new count lose: ");
                    command.Lose = Convert.ToInt32(Console.ReadLine());
                    rep.Update(command);
                    break;
                case "5":
                    Console.Write("new count draw :");
                    command.Draw = Convert.ToInt32(Console.ReadLine());
                    rep.Update(command);
                    break;
                case "6":
                    Console.Write("new score:  ");
                    command.Score = Convert.ToInt32(Console.ReadLine());
                    rep.Update(command);
                    break;
            }
        }
        private void Delette()
        {
            Console.WriteLine("name and city to search the command to delete: for example\nName, City");
            string[] parts = Console.ReadLine().Split(",").Select(p => p.Trim()).ToArray();
            string name = parts[0]; string city = parts[1];
            var command = rep.GetByNameCity(name, city).FirstOrDefault();

            Console.Write("Are you sure you want to delete: " + command.Name + "\n(true  false) \n");
            string choice_str = Console.ReadLine();
            if (choice_str == "true" || choice_str == "1" || choice_str == "yes")
            {
                rep.Delete(command);
                Console.WriteLine("Command was delete");
            }
        }
        private void AddMatch()
        {
            Console.WriteLine("Write first command name:");
            string command1 = Console.ReadLine();
            Console.WriteLine("Write second command name:");
            string command2 = Console.ReadLine();

            bool check = true;
            foreach (var match in rep.GetAllMatches())
            {
                if ((match.Command1.Name == command1 && match.Command2.Name == command2) ||
                    (match.Command1.Name == command2 && match.Command2.Name == command1))
                {
                    Console.WriteLine("Error: Match with these commands already exists!");
                    check = false;
                    break;
                }
            }
            if (check)
            {
                Console.Write("Goals for first command: ");
                int goals1 = Convert.ToInt32(Console.ReadLine());
                Console.Write("Goals for second command: ");
                int goals2 = Convert.ToInt32(Console.ReadLine());
                Console.Write("Match year: ");
                int year = Convert.ToInt32(Console.ReadLine());

                var firstCommand = rep.GetByName(command1).FirstOrDefault();
                var secondCommand = rep.GetByName(command2).FirstOrDefault();

                if (firstCommand != null && secondCommand != null)
                {
                    var newMatch = new Matches
                    {
                        First_Command_Id = firstCommand.Id,
                        Second_Command_Id = secondCommand.Id,
                        Cound_Goals_1 = goals1,
                        Count_Goals_2 = goals2,
                        MatchYear = year
                    };
                    rep.AddMatch(newMatch);
                    Console.WriteLine("Match added successfully!");
                }
                else
                {
                    Console.WriteLine("Error: One or both commands not found!");
                }
            }
        }
        private void ChangeMatch()
        {
            Console.WriteLine("Enter match ID to change:");
            int id = Convert.ToInt32(Console.ReadLine());
            var match = rep.GetMatchById(id);

            if (match == null)
            {
                Console.WriteLine("Error: Match was not found");
                return;
            }

            Console.WriteLine("\nChange\n0.Exit\n1.First Command Goals\n2.Second Command Goals\n3.Year");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    Console.Write("New goals for first command: ");
                    match.Cound_Goals_1 = Convert.ToInt32(Console.ReadLine());
                    rep.UpdateMatch(match);
                    break;
                case "2":
                    Console.Write("New goals for second command: ");
                    match.Count_Goals_2 = Convert.ToInt32(Console.ReadLine());
                    rep.UpdateMatch(match);
                    break;
                case "3":
                    Console.Write("New year: ");
                    match.MatchYear = Convert.ToInt32(Console.ReadLine());
                    rep.UpdateMatch(match);
                    break;
            }
        }
        private void DeleteMatch()
        {
            Console.WriteLine("first command name, second command name and year to search the match to delete: for example\nCommand1, Command2, 2024");
            string[] parts = Console.ReadLine().Split(",").Select(p => p.Trim()).ToArray();
            string command1 = parts[0];
            string command2 = parts[1];
            int year = Convert.ToInt32(parts[2]);

            var match = rep.GetMatchByCommandsAndDate(command1, command2, year);

            Console.Write("Are you sure you want to delete match: " + match.Command1.Name + " vs " + match.Command2.Name + "\n(true false) \n");
            string choice_str = Console.ReadLine();
            if (choice_str == "true" || choice_str == "1" || choice_str == "yes")
            {
                rep.DeleteMatch(match);
                Console.WriteLine("Match was delete");
            }
        }
        private void ShowMatchInfoID()
        {
            Console.Write("Match Id: ");
            int Matchid = Convert.ToInt32(Console.ReadLine());
            var match = rep.GetMatchById(Matchid);
            if (match == null)
            {
                Console.WriteLine("Incorect Id");
                return;
            }
            Console.WriteLine($"{match.Id} - {match.Command1} : {match.Command2} - {match.Cound_Goals_1} : {match.Count_Goals_2}");
        }
        private void ShowMatchInfoYEAR()
        {
            Console.Write("Match Year: ");
            int MatchYear = Convert.ToInt32(Console.ReadLine());
            var match = rep.GetMatchByYear(MatchYear);
            if (match == null)
            {
                Console.WriteLine("Incorect Id");
                return;
            }
            Console.WriteLine($"{match.Id} - {match.Command1} : {match.Command2} - {match.Cound_Goals_1} : {match.Count_Goals_2}");
        }
        private void ShowMatchInfoCOMMAND()
        {
            Console.WriteLine("Command Name: ");
            string command = Console.ReadLine();
            var matches = rep.GetMatchesByCommand(command);
            if (matches == null)
            {
                Console.WriteLine("Err, Matches not found");
                return;
            }
            foreach(var match in matches)
            { 
                Console.WriteLine($"{match.Id} - {match.Command1} : {match.Command2} - {match.Cound_Goals_1} : {match.Count_Goals_2}");
            }
        }

        private void ShowPlayerByGoalYear()
        {
            Console.Write("Year: ");
            int year = Convert.ToInt32(Console.ReadLine());
            var player = rep.GetPlayerByGoalData(year);
            Console.WriteLine($"{player.Id} - {player.PlayerNumber} - {player.FName} - {player.FName}");
        }


        public void Mmenu()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("0.Exit\n1.Add Comand\n2.Update Command\n3.Delete Command\n4.Add Match\n5.Update Match\n6. Delete Match");
                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "0":
                        flag = false; break;
                    case "1":
                        AddC(); break;
                    case "2":
                        Change(); break;
                    case "3":
                        Delette(); break;
                    case "4":
                        AddMatch(); break;
                    case "5":
                        ChangeMatch(); break;
                    case "6":
                        DeleteMatch(); break;
                }
            }
        }
    }
}
