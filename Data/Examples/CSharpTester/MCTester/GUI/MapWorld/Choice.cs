using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCTester.MapWorld
{
    public class Choice
    {
        public int Name { get; private set; }
        public int Value { get; private set; }
        public Choice(int name, int value)
        {
            Name = name;
            Value = value;
        }

        private static readonly List<Choice> possibleChoices = new List<Choice>();


        public static List<Choice> GetChoices(int maxNum)
        {
            possibleChoices.Clear();
            for (int i = 0; i <= maxNum; i++)
            {
                possibleChoices.Add(new Choice(i, i));
            }

            return possibleChoices;
        }
    }
}
