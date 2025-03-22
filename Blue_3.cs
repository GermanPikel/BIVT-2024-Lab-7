using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penalties;

            protected bool _is_expelled;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {
                    if (_penalties == null) return null;
                    int[] newArr = new int[_penalties.Length];
                    Array.Copy(_penalties, newArr, _penalties.Length);
                    return newArr;
                }
            }

            public int Total
            {
                get
                {
                    if (_penalties == null || _penalties.Length == 0) { return 0; }
                    return _penalties.Sum();
                }
            }

            public virtual bool IsExpelled => _is_expelled;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penalties = new int[0];
                _is_expelled = false;
            }

            public virtual void PlayMatch(int time)
            {
                if (time < 0) { Console.WriteLine("Введите корректное время"); }
                else
                {
                    if (_penalties == null) return;
                    if (time >= 10) { _is_expelled = true; }
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = time;
                }
            }

            public static void Sort(Participant[] participants)
            {
                if (participants == null) { return; }
                var newArr = participants.OrderBy(p => p.Total).ToArray();
                participants = newArr;
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {Total} {IsExpelled}");
            }
        }

        public class BasketballPlayer : Participant
        {
            private double _mathches_with_5_fouls;
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _mathches_with_5_fouls = 0;
            }

            public override bool IsExpelled => _is_expelled;

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0) { Console.WriteLine("Введите корректное количество фолов"); }
                else
                {
                    if (_penalties == null) return;
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = fouls;
                    if (fouls == 5) { _mathches_with_5_fouls++; }
                    if (_penalties.Length / _mathches_with_5_fouls * 100 > 10 || Total / _penalties.Length == 2) { _is_expelled = true; }
                }
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _totalPlayers = 0;
            private static double _allPlayersPenalties;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _allPlayersPenalties = 0;
                _totalPlayers++;
            }

            public override bool IsExpelled => _is_expelled;

            public override void PlayMatch(int time)
            {
                if (time < 0) { Console.WriteLine("Введите корректное время"); }
                else
                {
                    if (_penalties == null) return;
                    Array.Resize(ref _penalties, _penalties.Length + 1);
                    _penalties[_penalties.Length - 1] = time;
                    _allPlayersPenalties += time;
                    if (time >= 10 || Total / (_allPlayersPenalties / _totalPlayers) > 0.1) { _is_expelled = true; }
                }
            }

        }
    }
}
