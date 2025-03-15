using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_5;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman { 
            private string _name;
            private string _surname;
            private int _place;

            private bool _setted_place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname) { 
                _name = name;
                _surname = surname;
                _place = 0;
                _setted_place = false;
            }

            public void SetPlace(int place) {
                if (_setted_place == false)
                {
                    _place = place;
                    _setted_place = true;
                }
                else {
                    Console.WriteLine($"Этот спортсмен уже занял {_place} место.");
                }
            }

            public void Print() {
                Console.WriteLine($"{_name} {_surname} {_place}");
            }
        }

        public abstract class Team { 
            private string _name;
            private Sportsman[] _sportsmen;

            private int _added_sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public int SummaryScore {
                get {
                    if (_sportsmen == null) { return 0; }
                    int sum = 0;
                    foreach (Sportsman sportsman in _sportsmen) {
                        if (5 - sportsman.Place + 1 > 0 && sportsman.Place != 0)
                        {
                            sum += 5 - sportsman.Place + 1;
                        }
                    }
                    return sum;
                }
            }

            public int TopPlace {
                get {
                    if (_sportsmen == null) { return 0; }
                    int maxi = 18;
                    foreach (Sportsman sportsman in _sportsmen) {
                        if (sportsman.Place != 0)
                        {
                            maxi = Math.Min(maxi, sportsman.Place);
                        }
                    }
                    return maxi;
                }
            }

            public Team(string name) {
                _name = name;
                _sportsmen = new Sportsman[6];
                _added_sportsmen = 0;
            }

            public void Add(Sportsman sportsman) {
                if (_sportsmen == null) return;
                _sportsmen[_added_sportsmen++] = sportsman;
            }

            public void Add(Sportsman[] new_sportsmen) { 
                if (_sportsmen == null) return;
                for (int i = 0; i < new_sportsmen.Length; i++) {
                    _sportsmen[_added_sportsmen++] = new_sportsmen[i];
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = i; j < teams.Length; j++)
                    {
                        if (teams[i].SummaryScore < teams[j].SummaryScore)
                        {
                            Team tmp = teams[i];
                            teams[i] = teams[j];
                            teams[j] = tmp;
                        }
                    }
                }
                if (teams.Length >= 2)
                {
                    if (teams[0].SummaryScore == teams[1].SummaryScore)
                    {
                        int priority = 0;
                        foreach (Sportsman sportsman in teams[0].Sportsmen)
                        {
                            if (sportsman.Place == 1) { priority = 1; }
                        }
                        if (priority == 0)
                        {
                            foreach (Sportsman sportsman in teams[1].Sportsmen)
                            {
                                if (sportsman.Place == 1) { priority = 2; }
                            }
                        }
                        if (priority == 0 || priority == 1)
                        {
                            return;
                        }
                        Team tmp = teams[0];
                        teams[0] = teams[1];
                        teams[1] = tmp;
                    }
                }

            }

            protected abstract double GetTeamStrength();

            private static Team[] SortByStrength(Team[] teams) {
                var sortedTeams = teams.OrderByDescending(t => t.GetTeamStrength()).ToArray();
                // teams = sortedTeams;
                return sortedTeams;
            }

            public static Team GetChampion(Team[] teams) {
                if (teams == null) return null;
                SortByStrength(teams);
                if (teams as ManTeam[] != null)
                {
                    Console.WriteLine($"Чемпионом среди мужских команд является {teams[0].Name}");
                }
                else {
                    Console.WriteLine($"Чемпионом среди женских команд является {teams[0].Name}");
                }
                return teams[0];
            }

            public void Print() {
                Console.WriteLine($"{_name} {TopPlace} {SummaryScore}");
            }
        }

        public class ManTeam : Team { 
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength() {
                double avg = 0; double n = 0; double s = 0;
                foreach (Sportsman sportsman in Sportsmen) {
                    s += sportsman.Place;
                    n++;
                }
                avg = s / n;
                return 100.0 / avg;
            }
        }
        
        public class WomanTeam : Team {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength() {
                double s = 0; double n = 0; double p = 1;
                foreach (Sportsman sportsman in Sportsmen)
                {
                    s += sportsman.Place;
                    p *= sportsman.Place;
                    n++;
                }
                double multiplier = s * n / p;
                return 100.0;
            }
        }
    }
}
