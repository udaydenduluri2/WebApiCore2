using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helper;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class ExerciseController : Controller
    {
        private readonly IExerciseHelper _exerciseHelper;
        public ExerciseController() : this(new ExerciseHelper())
        {
        }

        internal ExerciseController(IExerciseHelper exerciseHelper)
        {
            _exerciseHelper = exerciseHelper;
        }

        // GET api/exercise
        [HttpGet]
        public ExerciseWrapper GetExercise()
        {
            var exercise = _exerciseHelper.GetFirstExercise();
            return new ExerciseWrapper
            {
                Exercise = exercise,
                Status = ExerciseStatus.StartOfExercise
            };
        }


        // POST api/exercise
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public ExerciseWrapper PostAnswer([FromBody]ExerciseAnswer answer)
        {
            var isRightAnswer = _exerciseHelper.IsRightAnswer(answer);

            Exercise newExercise = null;
            ExerciseStatus exerciseStatus;

            if (isRightAnswer)
            {
                if (_exerciseHelper.IsEndOfExercise(answer.Exercise.Rank))
                {
                    exerciseStatus = ExerciseStatus.EndOfExercise;
                }
                else
                {
                    newExercise = _exerciseHelper.GetNextExercise(answer.Exercise);
                    exerciseStatus = ExerciseStatus.Continue;
                }
            }
            else
            {
                exerciseStatus = ExerciseStatus.IncorrectAnswer;
            }

            return new ExerciseWrapper
            {
                Status = exerciseStatus,
                Exercise = newExercise,
                TimeOut = _exerciseHelper.GetTimeOut(newExercise != null ? newExercise.Rank : RankLevel.Beginner)
            };
        }
    }
}