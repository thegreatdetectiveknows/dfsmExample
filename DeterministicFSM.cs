using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class DeterministicFSM
    {
        private readonly List<string> Q = new List<string>();
        private readonly List<char> Sigma = new List<char>();
        private readonly List<Transition> Delta = new List<Transition>();
        private string Q0;
        private readonly List<string> F = new List<string>();

        public DeterministicFSM(List<string> q, List<char> sigma, List<Transition> delta, string q0, List<string> f)
        {
            Q = q;
            Sigma = sigma;
            Delta = delta;
            Q0 = q0;
            F = f;
        }

        public string Accepts(string input)
        {
            var currentState = Q0;
            foreach (var symbol in input.ToCharArray())
            {
                var transition = Delta.Find(t => t.StartState == currentState &&
                                                 t.Symbol == symbol);

                if (transition == null) throw new Exception();
                
                currentState = transition.EndState;
            }
            return currentState;
        }

        public bool Contains(string state) 
        {
            return F.Contains(state);
        }

    }
}
