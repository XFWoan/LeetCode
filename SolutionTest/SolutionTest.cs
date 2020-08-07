using Solutions;
using System.Collections.Generic;
using Xunit;

namespace SolutionTest
{
    public class SolutionTest
    {
        //31
        [Fact]
        public void NextPermutationTest()
        {
            int[] test1 = new int[] { 1, 2 };
            Solution.NextPermutation(test1);
            Assert.Equal(test1, new int[] { 2, 1 });

            int[] test2 = new int[] { 1, 2, 3 };
            Solution.NextPermutation(test2);
            Assert.Equal(test2, new int[] { 1, 3, 2 });

            int[] test3 = new int[] { 3, 2, 1 };
            Solution.NextPermutation(test3);
            Assert.Equal(test3, new int[] { 1, 2, 3 });

            int[] test4 = new int[] { 1, 1, 5 };
            Solution.NextPermutation(test4);
            Assert.Equal(test4, new int[] { 1, 5, 1 });

            int[] test5 = new int[] { 1, 5, 8, 4, 7, 6, 5, 3, 1 };
            Solution.NextPermutation(test5);
            Assert.Equal(test5, new int[] { 1, 5, 8, 5, 1, 3, 4, 6, 7 });

            int[] test6 = new int[] { 2, 3, 1 };
            Solution.NextPermutation(test6);
            Assert.Equal(test6, new int[] { 3, 1, 2 });

            int[] test7 = new int[] { 1 };
            Solution.NextPermutation(test7);
            Assert.Equal(test7, new int[] { 1 });

            int[] test8 = new int[] { 1, 3, 2 };
            Solution.NextPermutation(test8);
            Assert.Equal(test8, new int[] { 2, 1, 3 });

            int[] test9 = new int[] { 5, 1, 1 };
            Solution.NextPermutation(test9);
            Assert.Equal(test9, new int[] { 1, 1, 5 });
        }

        //32
        [Fact]
        public void LongestValidParenthesesTest()
        {
            Dictionary<string, int> test = new Dictionary<string, int>();
            test.Add("(()", 2);
            test.Add(")()())", 4);

            foreach (var item in test)
            {
                Assert.True(Solution.LongestValidParentheses(item.Key) == item.Value);
            }
        }

        //33
        [Fact]
        public void SearchTest()
        {
            Assert.Equal(4, Solution.Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0));
            Assert.Equal(-1, Solution.Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 3));
            Assert.Equal(1, Solution.Search(new int[] { 3, 1 }, 1));
        }

        //34
        [Fact]
        public void SearchRange()
        {
            Assert.Equal(new int[] { 3, 4 }, Solution.SearchRange(new int[] { 5, 7, 7, 8, 8, 10 }, 8));
            Assert.Equal(new int[] { -1, -1 }, Solution.SearchRange(new int[] { 5, 7, 7, 8, 8, 10 }, 6));
            Assert.Equal(new int[] { -1, -1 }, Solution.SearchRange(new int[] { }, 6));
        }
    }
}