﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump 
        { 
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants {
                get {
                    if (_participants == null) { return null; }
                    Participant[] copyParticipants = new Participant[_participants.Length];
                    Array.Copy(_participants, copyParticipants, _participants.Length);
                    return copyParticipants;
                }
            }

            public abstract double[] Prize {
                get;
            }

            public WaterJump(string name, int bank) {
                _name = name;
                _bank = bank;
                Participant[] participants = new Participant[0];
            }

            public void Add(Participant participant) {
                if (_participants == null) { return; }
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(Participant[] participants) { 
                if (participants == null) { return; }
                foreach (Participant p in participants) {
                    Array.Resize(ref _participants, _participants.Length + 1);
                    _participants[_participants.Length - 1] = p;
                }
            }
        }

        public class WaterJump3m : WaterJump 
        { 
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize {
                get 
                { 
                    if (this.Participants == null) { return null; }
                    if (this.Participants.Length < 3) { return null; }
                    Participant.Sort(this.Participants);
                    double[] prizes = new double[3];
                    prizes[0] = 0.5 * this.Bank; prizes[1] = 0.3 * this.Bank; prizes[2] = 0.2 * this.Bank;
                    return prizes;
                }
            }
        }
        public class WaterJump5m : WaterJump 
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null) { return null; }
                    if (this.Participants.Length < 3) { return null; }
                    Participant.Sort(this.Participants);
                    int extra_percents = this.Participants.Length / 2;
                    double[] prizes = default(double[]);
                    if (extra_percents <= 3) { prizes = new double[3]; }
                    else if (extra_percents <= 10) { prizes = new double[extra_percents]; }
                    else { prizes = new double[10]; }
                    prizes[0] = 0.5 * this.Bank; prizes[1] = 0.3 * this.Bank; prizes[2] = 0.2 * this.Bank;
                    for (int i = 0; i < extra_percents; i++) {
                        prizes[i] += ((20.0 / extra_percents) / 100) * this.Bank;
                    }
                    return prizes;
                }
            }
        }

        public struct Participant 
        {
            private string _name;
            private string _surname;
            private int[,] _marks;

            private int _estimated_jumps;  // вспомогательное поле, не указанное в задании

            public Participant(string name, string surname) { 
                _name = name;
                _surname = surname;
                _marks = new int[2, 5] { {0, 0, 0, 0, 0}, {0, 0, 0, 0, 0} };
                _estimated_jumps = 0;
            }

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks {
                get
                {
                    if (_marks == null) { return null; }
                    int[,] m = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            m[i, j] =  _marks[i, j];
                        }
                    }
                    return m;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            sum += _marks[i, j];
                        }
                    }
                    return sum;
                }
            }


            public void Jump(int[] result) {
                if (result == null || _marks == null) return;
                if (_estimated_jumps < 2)
                {
                    int r = 0;
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _marks[_estimated_jumps, j] = result[r++];
                    }
                    _estimated_jumps++;
                }
            }

            public static void Sort(Participant[] array) {
                if (array == null) { return; }
                for (int i = 0; i < array.Length; i++) {
                    for (int j = i; j < array.Length; j++) {
                        if (array[i].TotalScore < array[j].TotalScore) { 
                            Participant tmp = array[i];
                            array[i] = array[j];
                            array[j] = tmp;
                        }
                    }
                }
            }

            public void Print() {
                Console.WriteLine($"{_name} {_surname} {TotalScore}");
            }
        }
    }
}
