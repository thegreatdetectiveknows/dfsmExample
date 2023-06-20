using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class Transition
    {
        public string StartState { get; private set; }
        public char Symbol { get; private set; }
        public string EndState { get; private set; }

        public Transition(string startState, char symbol, string endState)
        {
            StartState = startState;
            Symbol = symbol;
            EndState = endState;
        }
    }
}
