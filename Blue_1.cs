using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_6
{
    public class Blue_1
    {
        public class Response
        {
            // поля ↓
            private string _name;
            protected int _votes;

            // конструктор ↓
            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            // свойства ↓
            public string Name => _name;
            public int Votes => _votes;

            // методы ↓
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) { return 0; }

                _votes = 0;
                foreach (Response response in responses)
                {
                    if (response._name == _name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name} {_votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) { return 0; }

                _votes = 0;
                foreach (Response response in responses)
                {
                    if (response is HumanResponse && response.Name == Name)
                    {
                        _votes++;
                    }
                }
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Votes}");
            }
        }

    }
}