using System;
using Xunit;
using VideoPoker;

namespace HandTester
{
    public class HandEvaluationTest
    {
        //Arrange
        [Theory]
        [InlineData(new string[] { "HA", "HQ", "HT", "HK", "HJ" }, "royal flush")]
        //straight flush has to return false, because the logic is incomplete.
        [InlineData(new string[] { "H2", "H3", "H4", "H5", "H6" }, "straight flush")]
        [InlineData(new string[] { "HA", "DA", "CA", "SA", "HJ" }, "four of a kind")]
        [InlineData(new string[] { "H5", "D5", "H7", "C5", "D7" }, "full house")]
        [InlineData(new string[] { "HA", "HQ", "HT", "H9", "H5" }, "flush")]
        //straight has to return false, because the logic is incomplete.
        [InlineData(new string[] { "HA", "CQ", "ST", "SK", "DJ" }, "straight")]
        [InlineData(new string[] { "HA", "DA", "CA", "HK", "HJ" }, "three of a kind")]
        [InlineData(new string[] { "HA", "DA", "HT", "DT", "HJ" }, "two pair")]
        [InlineData(new string[] { "HA", "D9", "C6", "D5", "S2" }, "jacks or better")]
        [InlineData(new string[] { "C8", "H3", "HT", "D5", "S6" }, "nothing")]
        public void ShowResult_PassingACustomHandAndWaitingForTheExpectedResult(string[] hand, string expected)
        {
            //Act 
            var result = Program.ShowResult(hand);

            //Assert
            Assert.Equal(expected, result.Item1);
        }
    }
}
