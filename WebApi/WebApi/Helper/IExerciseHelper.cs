using WebApi.Model;

namespace WebApi.Helper
{
    public interface IExerciseHelper
    {
        bool IsRightAnswer(ExerciseAnswer answer);
        Exercise GetFirstExercise();
        Exercise GetNextExercise(Exercise exercise);
        bool IsEndOfExercise(RankLevel rank);
        int GetTimeOut(RankLevel rankLevel);
    }
}