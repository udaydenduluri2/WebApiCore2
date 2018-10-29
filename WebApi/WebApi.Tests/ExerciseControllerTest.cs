using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WebApi.Controllers;
using WebApi.Helper;
using WebApi.Model;

namespace WebApi.Tests
{
    [TestClass]
    public class ExerciseControllerTest
    {
        [TestMethod]
        public void GetExerciseTest()
        {
            var exerciseHelper = Substitute.For<IExerciseHelper>();
            exerciseHelper.GetFirstExercise()
                .Returns(new Exercise {Operand1 = 1, Operand2 = 2, Rank = RankLevel.Beginner});

            var exerciseController = new ExerciseController(exerciseHelper);
            exerciseHelper.IsEndOfExercise(Arg.Any<RankLevel>()).Returns(false);

            var exerciseWrapper = exerciseController.GetExercise();

            var exercise = exerciseWrapper.Exercise;
            Assert.AreEqual(1, exercise.Operand1);
            Assert.AreEqual(2, exercise.Operand2);
            Assert.AreEqual(RankLevel.Beginner, exercise.Rank);
            Assert.AreEqual(1, exercise.QuestionNumber);
            Assert.AreEqual(ExerciseStatus.StartOfExercise, exerciseWrapper.Status);
        }

        [TestMethod]
        public void PostAnswerTest_Incorrect()
        {
            var exerciseHelper = Substitute.For<IExerciseHelper>();
            exerciseHelper.GetFirstExercise().Returns(new Exercise {Rank = RankLevel.Beginner});
            var exerciseController = new ExerciseController(exerciseHelper);

            exerciseHelper.IsRightAnswer(Arg.Any<ExerciseAnswer>()).Returns(false);
            exerciseHelper.IsEndOfExercise(Arg.Any<RankLevel>()).Returns(false);

            var answer = new ExerciseAnswer {Exercise = new Exercise()};
            var wrapper = exerciseController.PostAnswer(answer);

            Assert.AreEqual(ExerciseStatus.IncorrectAnswer, wrapper.Status);
            Assert.IsNull(wrapper.Exercise);
        }

        [TestMethod]
        public void PostAnswer_RightAnswer_Continue_ToNext()
        {
            var exerciseHelper = Substitute.For<IExerciseHelper>();
            exerciseHelper.GetNextExercise(Arg.Any<Exercise>()).Returns(new Exercise { Rank = RankLevel.Advanced });
            var exerciseController = new ExerciseController(exerciseHelper);

            exerciseHelper.IsRightAnswer(Arg.Any<ExerciseAnswer>()).Returns(true);
            exerciseHelper.IsEndOfExercise(Arg.Any<RankLevel>()).Returns(false);

            var answer = new ExerciseAnswer { Exercise = new Exercise() };
            var wrapper = exerciseController.PostAnswer(answer);

            Assert.AreEqual(ExerciseStatus.Continue, wrapper.Status);
            Assert.AreEqual(RankLevel.Advanced, wrapper.Exercise.Rank);
            Assert.AreEqual(1, wrapper.Exercise.QuestionNumber);
        }

        [TestMethod]
        public void PostAnswer_RightAnswer_EndOfQuestions()
        {
            var exerciseHelper = Substitute.For<IExerciseHelper>();
            exerciseHelper.GetNextExercise(Arg.Any<Exercise>()).Returns(new Exercise { Rank = RankLevel.Advanced });
            var exerciseController = new ExerciseController(exerciseHelper);

            exerciseHelper.IsRightAnswer(Arg.Any<ExerciseAnswer>()).Returns(true);
            exerciseHelper.IsEndOfExercise(Arg.Any<RankLevel>()).Returns(true);

            var answer = new ExerciseAnswer { Exercise = new Exercise() };
            var wrapper = exerciseController.PostAnswer(answer);

            Assert.AreEqual(ExerciseStatus.EndOfExercise, wrapper.Status);
            Assert.IsNull(wrapper.Exercise);
        }
    }
}