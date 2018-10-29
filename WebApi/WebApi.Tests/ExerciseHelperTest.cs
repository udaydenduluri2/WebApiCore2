using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Helper;
using WebApi.Model;


namespace WebApi.Tests
{
    [TestClass]
    public class ExerciseHelperTest
    {
        [TestMethod]
        public void GetNextLevelTest()
        {
            var exerciseHelper = new ExerciseHelper();
            var nextLevel = exerciseHelper.GetNextLevel(RankLevel.Beginner);
            Assert.AreEqual(RankLevel.Talented, nextLevel);

            nextLevel = exerciseHelper.GetNextLevel(RankLevel.Talented);
            Assert.AreEqual(RankLevel.Intermediate, nextLevel);

            nextLevel = exerciseHelper.GetNextLevel(RankLevel.Intermediate);
            Assert.AreEqual(RankLevel.Advanced, nextLevel);

            nextLevel = exerciseHelper.GetNextLevel(RankLevel.Advanced);
            Assert.AreEqual(RankLevel.Expert, nextLevel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNextLevelTest_Invalid()
        {
            var exerciseHelper = new ExerciseHelper();
            exerciseHelper.GetNextLevel(RankLevel.Expert);
        }

        [TestMethod]
        public void PromoteToNextLevelTest()
        {
            var exerciseHelper = new ExerciseHelper();
            var promoted = exerciseHelper.PromoteToNextLevel(3);
            Assert.IsTrue(promoted);

            promoted = exerciseHelper.PromoteToNextLevel(1);
            Assert.IsFalse(promoted);
        }

        [TestMethod]
        public void IsEndOfExerciseTest()
        {
            var exerciseHelper = new ExerciseHelper();
            var isEndOf = exerciseHelper.IsEndOfExercise(RankLevel.Expert);
            Assert.IsTrue(isEndOf);

            isEndOf = exerciseHelper.IsEndOfExercise(RankLevel.Intermediate);
            Assert.IsFalse(isEndOf);
        }

        [TestMethod]
        public void GetNextExerciseTest()
        {
            var exerciseHelper = new ExerciseHelper();
            var exercise = new Exercise {Rank = RankLevel.Beginner, QuestionNumber = 1};
            var nextExercise = exerciseHelper.GetNextExercise(exercise);

            Assert.AreEqual(exercise.Rank, nextExercise.Rank);
            Assert.AreEqual(exercise.QuestionNumber + 1, nextExercise.QuestionNumber);

            exercise = new Exercise { Rank = RankLevel.Beginner, QuestionNumber = 3 };
            nextExercise = exerciseHelper.GetNextExercise(exercise);
            Assert.AreEqual(RankLevel.Talented, nextExercise.Rank);
            Assert.AreEqual(1, nextExercise.QuestionNumber);
        }
    }
}