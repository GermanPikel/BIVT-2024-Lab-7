using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) { return null; }
                    int[] newArr = new int[_scores.Length];
                    Array.Copy(_scores, newArr, _scores.Length);
                    return newArr;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null) { return 0; }
                    int s = 0;
                    foreach (int i in _scores)
                    {
                        s += i;
                    }
                    return s;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) { return; }
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.Write($"{_name} ");
                if (_scores == null)
                {
                    Console.Write("0");
                }
                else
                {
                    Console.Write($"{TotalScore} ");
                }
                Console.WriteLine();
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class Group
        {
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;

            private int _manTeams_added;
            private int _womanTeams_added;

            public string Name => _name;
            public Team[] ManTeams => _manTeams;
            public Team[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _manTeams_added = 0;
                _womanTeams_added = 0;
            }

            public void Add(Team team_to_add)
            {
                if (team_to_add is ManTeam mt)
                {
                    if (_manTeams_added >= 12 || _manTeams == null)
                    {
                        return;
                    }
                    _manTeams[_manTeams_added++] = mt;
                }
                else if (team_to_add is WomanTeam wt)
                {
                    if (_womanTeams_added >= 12 || _womanTeams == null)
                    {
                        return;
                    }
                    _womanTeams[_womanTeams_added++] = wt;
                }
            }

            public void Add(Team[] teams_to_add)
            {
                foreach (var team in teams_to_add)
                {
                    Add(team);
                }
            }

            private void SortMaleOrFemale(Team[] t)
            {
                if (t == null) { return; }
                Team[] sorted_teams = null;
                sorted_teams = t.OrderByDescending(team => team.TotalScore).ToArray();
                t = sorted_teams;
            }

            public void Sort()
            {
                SortMaleOrFemale(_manTeams);
                SortMaleOrFemale(_womanTeams);
            }

            private static void MergeBySex(Team[] group1, Team[] group2, Team[] group, int size)
            {
                int i = 0, j = 0, k = 0;
                while (i < group1.Length && j < group2.Length)
                {
                    if (group1[i].TotalScore >= group2[j].TotalScore)
                    {
                        group[k++] = group1[i++];
                    }
                    else
                    {
                        group[k++] = group2[j++];
                    }
                }

                while (i < group1.Length)
                {
                    group[k++] = group1[i++];
                }
                while (j < group2.Length)
                {
                    group[k++] = group2[j++];
                }

            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group group = new Group("Финалисты");
                MergeBySex(group1.ManTeams, group2.ManTeams, group.ManTeams, size);
                MergeBySex(group1.WomanTeams, group2.WomanTeams, group.WomanTeams, size);
                return group;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} ");
                foreach (var team in _manTeams)
                {
                    team.Print();
                }
                Console.WriteLine();
                foreach (var team in _womanTeams)
                {
                    team.Print();
                }
                Console.WriteLine();
            }
        }
    }
}
