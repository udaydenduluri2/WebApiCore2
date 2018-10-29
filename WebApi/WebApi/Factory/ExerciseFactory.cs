using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Model;

namespace WebApi.Factory
{
    public static class ExerciseFactory
    {
        private static readonly Dictionary<int, IEnumerable<Exercise>> ExerciseDict;

        static ExerciseFactory()
        {
            ExerciseDict = new Dictionary<int, IEnumerable<Exercise>>
            {
                {(int) RankLevel.Beginner, new List<Exercise>
                {
                    new Exercise {Operand1 = 2, Operand2 = 3, Rank = RankLevel.Beginner, Options = new List<int>{5, 6, 7, 8}},
                    new Exercise {Operand1 = 4, Operand2 = 5, Rank = RankLevel.Beginner, Options = new List<int>{5, 6, 7, 9}},
                    new Exercise {Operand1 = 6, Operand2 = 8, Rank = RankLevel.Beginner, Options = new List<int>{5, 6, 14, 13}},
                    new Exercise {Operand1 = 8, Operand2 = 9, Rank = RankLevel.Beginner, Options = new List<int>{5, 6, 17, 1}}
                }},
                {(int) RankLevel.Talented, new List<Exercise>
                {
                    new Exercise {Operand1 = 12, Operand2 = 13, Rank = RankLevel.Talented, Options = new List<int>{5, 25, 14, 13}},
                    new Exercise {Operand1 = 14, Operand2 = 15, Rank = RankLevel.Talented, Options = new List<int>{5, 1, 29, 13}},
                    new Exercise {Operand1 = 16, Operand2 = 18, Rank = RankLevel.Talented, Options = new List<int>{5, 6, 34, 13}},
                    new Exercise {Operand1 = 18, Operand2 = 19, Rank = RankLevel.Talented, Options = new List<int>{5, 6, 37, 13}}
                }},
                {
                    (int) RankLevel.Intermediate, new List<Exercise>
                    {
                        new Exercise {Operand1 = 22, Operand2 = 33, Rank = RankLevel.Intermediate, Options = new List<int>{5, 55, 45, 13}},
                        new Exercise {Operand1 = 24, Operand2 = 35, Rank = RankLevel.Intermediate, Options = new List<int>{5, 55, 45, 59}},
                        new Exercise {Operand1 = 26, Operand2 = 38, Rank = RankLevel.Intermediate, Options = new List<int>{5, 55, 64, 59}},
                        new Exercise {Operand1 = 28, Operand2 = 39, Rank = RankLevel.Intermediate, Options = new List<int>{15, 55, 45, 67}},
                    }
                },
                {(int) RankLevel.Advanced, new List<Exercise>
                {
                    new Exercise {Operand1 = 32, Operand2 = 33, Rank = RankLevel.Advanced, Options = new List<int>{65, 55, 45, 59}},
                    new Exercise {Operand1 = 34, Operand2 = 35, Rank = RankLevel.Advanced, Options = new List<int>{5, 55, 45, 69}},
                    new Exercise {Operand1 = 36, Operand2 = 38, Rank = RankLevel.Advanced, Options = new List<int>{74, 55, 45, 59}},
                    new Exercise {Operand1 = 38, Operand2 = 39, Rank = RankLevel.Advanced, Options = new List<int>{5, 55, 77, 59}},
                }},
                {(int) RankLevel.Expert, new List<Exercise>
                {
                    new Exercise {Operand1 = 42, Operand2 = 43, Rank = RankLevel.Expert, Options = new List<int>{85, 55, 45, 59}},
                    new Exercise {Operand1 = 44, Operand2 = 45, Rank = RankLevel.Expert, Options = new List<int>{90, 55, 45, 89}},
                    new Exercise {Operand1 = 46, Operand2 = 48, Rank = RankLevel.Expert, Options = new List<int>{94, 95, 45, 59}},
                    new Exercise {Operand1 = 48, Operand2 = 49, Rank = RankLevel.Expert, Options = new List<int>{95, 15, 97, 59}},
                }},
            };
        }

        public static Exercise Create(RankLevel rank, int? attempt = null)
        {
            var rand = new Random();
            
            var exercises = ExerciseDict[(int) rank];
            var enumerable = exercises as Exercise[] ?? exercises.ToArray();
            var toSkip = rand.Next(0, enumerable.Count());

            var exercise = enumerable.Skip(toSkip).Take(1).First();

            exercise.QuestionNumber = attempt.HasValue ? attempt.Value : 1;

            return exercise;
        }
    }
}