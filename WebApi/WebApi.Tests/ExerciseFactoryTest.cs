using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Factory;
using WebApi.Model;

namespace WebApi.Tests
{
    [TestClass]
    public class ExerciseFactoryTest
    {
        [TestMethod]
        public void CreateBeginnerTest()
        {
            var exercise = ExerciseFactory.Create(RankLevel.Beginner, 1);
            Assert.AreEqual(RankLevel.Beginner, exercise.Rank);
            Assert.AreEqual(1, exercise.QuestionNumber);
        }

        [TestMethod]
        public void CreateTalentedTest()
        {
            var exercise = ExerciseFactory.Create(RankLevel.Talented, 2);
            Assert.AreEqual(RankLevel.Talented, exercise.Rank);
            Assert.AreEqual(2, exercise.QuestionNumber);
        }

        [TestMethod]
        public void CreateIntermediateTest()
        {
            var exercise = ExerciseFactory.Create(RankLevel.Intermediate, 3);
            Assert.AreEqual(RankLevel.Intermediate, exercise.Rank);
            Assert.AreEqual(3, exercise.QuestionNumber);
        }

        [TestMethod]
        public void CreateAdvancedTest()
        {
            var exercise = ExerciseFactory.Create(RankLevel.Advanced, 1);
            Assert.AreEqual(RankLevel.Advanced, exercise.Rank);
            Assert.AreEqual(1, exercise.QuestionNumber);
        }

        [TestMethod]
        public void CreateExpertTest()
        {
            var exercise = ExerciseFactory.Create(RankLevel.Expert, 2);
            Assert.AreEqual(RankLevel.Expert, exercise.Rank);
            Assert.AreEqual(2, exercise.QuestionNumber);
        }
    }
}
