using Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //35
        [Fact]
        public void SearchInsertTest()
        {
            Assert.Equal(2, Solution.SearchInsert(new int[] { 1, 3, 5, 6 }, 5));
            Assert.Equal(1, Solution.SearchInsert(new int[] { 1, 3, 5, 6 }, 2));
            Assert.Equal(4, Solution.SearchInsert(new int[] { 1, 3, 5, 6 }, 7));
            Assert.Equal(0, Solution.SearchInsert(new int[] { 1, 3, 5, 6 }, 0));
            Assert.Equal(1, Solution.SearchInsert(new int[] { 1 }, 2));
            Assert.Equal(0, Solution.SearchInsert(new int[] { 1 }, 0));
        }

        //36
        [Fact]
        public void IsValidSudokuTest()
        {
            string[][] _board =
            {
                new string[]{ "5","3",".",".","7",".",".",".","." },
                new string[]{ "6",".",".","1","9","5",".",".","." },
                new string[]{ ".","9","8",".",".",".",".","6","." },
                new string[]{ "8",".",".",".","6",".",".",".","3" },
                new string[]{ "4",".",".","8",".","3",".",".","1" },
                new string[]{ "7",".",".",".","2",".",".",".","6"},
                new string[]{".","6",".",".",".",".","2","8","." },
                new string[]{ ".",".",".","4","1","9",".",".","5" },
                new string[]{ ".",".",".",".","8",".",".","7","9" },
            };
            char[][] board = new char[9][];

            for (int i = 0; i < 9; i++)
            {
                board[i] = new char[9];
                for (int j = 0; j < 9; j++)
                {
                    board[i][j] = _board[i][j][0];
                }
            }
            Assert.True(Solution.IsValidSudoku(board));
            board[0][0] = '8';
            Assert.False(Solution.IsValidSudoku(board));
        }

        //38
        [Fact]
        public void CountAndSayTest()
        {
            for (int i = 1; i <= 30; i++)
            {
                //TODO
            }
        }

        //42
        [Fact]
        public void TrapTest()
        {
            Assert.Equal(2, Solution.Trap(new int[] { 2, 0, 2 }));
        }

        //43
        [Fact]
        public void MultiplyTest()
        {
            Assert.Equal("6", Solution.Multiply("2", "3"));
            Assert.Equal("408", Solution.Multiply("12", "34"));
            Assert.Equal("7006652", Solution.Multiply("1234", "5678"));
        }

        //44
        [Fact]
        public void IsMatchTest()
        {
            Assert.False(Solution.IsMatch("aa", "a"));
            Assert.True(Solution.IsMatch("aa", "*"));
            Assert.False(Solution.IsMatch("cb", "?a"));
            Assert.True(Solution.IsMatch("adceb", "*a*b"));
            Assert.False(Solution.IsMatch("acdcb", "a*c?b"));
            Assert.False(Solution.IsMatch("aab", "c*a*b"));
        }

        //45
        [Fact]
        public void JumpTest()
        {
            Assert.Equal(2, Solution.Jump(new int[] { 2, 3, 1, 1, 4 }));
            Assert.Equal(0, Solution.Jump(new int[] { 0 }));
        }

        //46
        [Fact]
        public void PermuteTest()
        {
            IList<IList<int>> ans = new List<IList<int>>();
            ans.Add(new List<int>(new int[] { 1, 2, 3 }));
            ans.Add(new List<int>(new int[] { 1, 3, 2 }));
            ans.Add(new List<int>(new int[] { 2, 1, 3 }));
            ans.Add(new List<int>(new int[] { 2, 3, 1 }));
            ans.Add(new List<int>(new int[] { 3, 1, 2 }));
            ans.Add(new List<int>(new int[] { 3, 2, 1 }));
            Assert.Equal(ans, Solution.Permute(new int[] { 1, 2, 3 }));
        }

        //49
        [Fact]
        public void GroupAnagramsTest()
        {
            string[] input = new string[] { "eat", "tea", "tan", "ate", "nat", "bat" };
            IList<IList<string>> ans = new List<IList<string>>();
            ans.Add(new List<string>(new string[] { "ate", "eat", "tea" }));
            ans.Add(new List<string>(new string[] { "nat", "tan" }));
            ans.Add(new List<string>(new string[] { "bat" }));
            Assert.Equal(ans, Solution.GroupAnagrams(input));
        }
    }
}