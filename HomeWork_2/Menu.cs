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

        public void Mmenu()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("0.Exit\n1.Add Comand\n2.Update Command\n3.Delete Command");
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
                }
            }
        }
    }
}
