using ChristmasSpecial;

namespace ChristmasSpecial.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckNormalOutput() //Level 1
        {
            var solution = new Solution();
            var gears = new List<int> { 1, 1, 2, 2, 3 };
            var distances = new List<int> { 3, 4, 5, 3 };
            var ratio = 2;

            var result = solution.GearOrder(distances, gears, ratio);
            var expectedSolution = new List<int>() { 2, 1, 3, 2, 1 };

            CollectionAssert.AreEqual(result, expectedSolution);
        }

        [Test]
        public void CheckExtraGears() //Level 2
        {
            var solution = new Solution();
            var gears = new List<int> { 1, 2, 3, 3, 3, 3, 4, 5, 5, 8 };
            var distances = new List<int> { 8, 8, 11, 13, 9, 7, 5, 3 };
            var ratio = 3;

            var result = solution.GearOrder(distances, gears, ratio);
            var expectedSolution = new List<int>() { 3, 5, 3, 8, 5, 4, 3, 2, 1 };

            CollectionAssert.AreEqual(result, expectedSolution);
        }
        [Test]
        public void CheckNotEnoughGears() //Level 3
        {
            var solution = new Solution();
            var gears = new List<int> { 1, 1, 1, 1, 2, 3, 3, 4, 6, 8 };
            var distances = new List<int> { 5, 3, 5, 9, 14, 11, 7, 5, 2, 2 };
            var ratio = 4;

            var result = solution.GearOrder(distances, gears, ratio);
            var expectedSolution = new List<int>() { 4, 1, 2, 3, 6, 8, 3, 4, 1, 1, 1 };

            CollectionAssert.AreEqual(result, expectedSolution);
        }

        [Test]
        public void CheckNoGearsProvided() //Level 4
        {
            var solution = new Solution();
            var gears = new List<int> { };
            var distances = new List<int> { 13, 9, 10, 11, 16, 14 };
            var ratio = 2;

            var result = solution.GearOrder(distances, gears, ratio);
            var expectedSolution = new List<int>() { 10, 3, 6, 4, 7, 9, 5 };

            CollectionAssert.AreEqual(result, expectedSolution);
        }
    }
}