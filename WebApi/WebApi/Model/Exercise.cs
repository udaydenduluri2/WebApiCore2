using System;
using System.Collections.Generic;

namespace WebApi.Model
{
    public class Exercise
    {
        public RankLevel Rank { get; set; }
        public string RankString
        {
            get { return Rank.ToString(); }
        }
        public int QuestionNumber { get; set; } = 1;
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }
        public IEnumerable<int> Options { get; set; }
    }
}