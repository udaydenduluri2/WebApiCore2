using System;
using System.Linq;
using System.Runtime.CompilerServices;
using WebApi.Factory;
using WebApi.Model;
namespace WebApi.Helper
{
    public class ExerciseHelper : IExerciseHelper
    {
        public bool IsRightAnswer(ExerciseAnswer answer)
        {
            return answer.Answer == answer.Exercise.Operand1 + answer.Exercise.Operand2;
        }

        public Exercise GetFirstExercise()
        {
            return ExerciseFactory.Create(RankLevel.Beginner);
        }

        public Exercise GetNextExercise(Exercise exercise)
        {
            if (exercise == null)
            {
                return ExerciseFactory.Create(RankLevel.Beginner);
            }

            if (PromoteToNextLevel(exercise.QuestionNumber))
            {
                var currentRank = GetNextLevel(exercise.Rank);
                return ExerciseFactory.Create(currentRank);
            }
            return ExerciseFactory.Create(exercise.Rank, exercise.QuestionNumber + 1);
        }

        public bool IsEndOfExercise(RankLevel rank)
        {
            return rank == RankLevel.Expert;
        }

        internal bool PromoteToNextLevel(int exerciseAttempt)
        {
            return exerciseAttempt == 3;
        }

        internal RankLevel GetNextLevel(RankLevel rank)
        {
            if (Enum.GetValues(typeof(RankLevel)).Cast<int>().Max() == (int) rank)
            {
                throw new ArgumentException($"{rank} is maximum");
            }

            return (RankLevel)((int)rank + 1);
        }

        public int GetTimeOut(RankLevel rankLevel)
        {
            switch (rankLevel)
            {
                case RankLevel.Beginner:
                    return 30000;
                case RankLevel.Talented:
                    return 29000;
                case RankLevel.Intermediate:
                    return 28000;
                case RankLevel.Advanced:
                    return 27000;
                case RankLevel.Expert:
                    return 26000;
                default:
                    return 30000;
            }
        }
    }
}