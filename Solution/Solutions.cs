﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Solutions
{
    static public class Solution
    {
        //31. 下一个排列
        static public void NextPermutation(int[] nums)
        {
            if (nums.Length < 2)    //长度为1或空无需处理
            {
                return;
            }
            int i, j, temp;
            i = nums.Length - 2;
            while (nums[i + 1] <= nums[i])
            {
                if (i == 0)
                {
                    Array.Reverse(nums);
                    return;     //数组降序，直接翻转后返回
                }
                i--;
            }
            j = i + 1;
            while (nums[j] > nums[i])
            {
                j++;
                if (j >= nums.Length)
                {
                    break;
                }
            }
            temp = nums[i];
            nums[i] = nums[j - 1];
            nums[j - 1] = temp;
            Array.Reverse(nums, i + 1, nums.Length - i - 1);
        }

        //32. 最长有效括号
        static public int LongestValidParentheses(string s)
        {
            int res = 0;
            int left = 0, right = 0;//左右括号计数器
            foreach (char i in s)
            {
                if (i == '(')
                {
                    left++;
                }
                else if (i == ')')
                {
                    right++;
                }
                if (left == right)
                {
                    if (left + right > res)
                    {
                        res = left + right;
                    }
                }
                if (left < right)
                {
                    left = 0;
                    right = 0;
                }
            }
            left = 0;
            right = 0;
            foreach (char i in s.Reverse())
            {
                if (i == '(')
                {
                    left++;
                }
                else if (i == ')')
                {
                    right++;
                }
                if (left == right)
                {
                    if (left + right > res)
                    {
                        res = left + right;
                    }
                }
                if (left > right)
                {
                    left = 0;
                    right = 0;
                }
            }
            return res;
        }

        //33. 搜索旋转排序数组
        static public int Search(int[] nums, int target)
        {
            if (nums.Length == 0) return -1;
            if (nums.Length == 1) return nums[0] == target ? 0 : -1;
            int l = 0, r = nums.Length - 1;
            while (l <= r)
            {
                int mid = (l + r) / 2;
                if (nums[mid] == target) return mid;
                if (nums[0] <= nums[mid])
                {
                    if (nums[0] <= target && target < nums[mid])
                    {
                        r = mid - 1;
                    }
                    else
                    {
                        l = mid + 1;
                    }
                }
                else
                {
                    if (nums[mid] < target && target <= nums[nums.Length - 1])
                    {
                        l = mid + 1;
                    }
                    else
                    {
                        r = mid - 1;
                    }
                }
            }
            return -1;
        }

        //34. 在排序数组中查找元素的第一个和最后一个位置
        static public int[] SearchRange(int[] nums, int target)
        {
            int[] res = new int[2] { -1, -1 };
            if (nums.Length == 0) return res;
            for (int flag = 0; flag < 2; flag++)
            {
                int l = 0, r = nums.Length - 1;
                while (l < r)
                {
                    int mid = (l + r + flag) / 2;
                    if (flag == 0)
                    {
                        if (target <= nums[mid] && target >= nums[l])
                        {
                            r = mid;
                        }
                        else
                        {
                            l = mid + 1;
                        }
                    }
                    if (flag == 1)
                    {
                        if (target >= nums[mid] && target <= nums[r])
                        {
                            l = mid;
                        }
                        else
                        {
                            r = mid - 1;
                        }
                    }
                }
                if (target == nums[l])
                {
                    res[flag] = l;
                }
                else
                {
                    res[flag] = -1;
                }
            }
            return res;
        }

        //35. 搜索插入位置
        static public int SearchInsert(int[] nums, int target)
        {
            if (nums.Length == 0) return 0;
            if (nums.Length == 1)
            {
                return target > nums[0] ? 1 : 0;
            }
            int l = 0, r = nums.Length - 1;
            while (l < r)
            {
                int mid = (l + r) / 2;
                if (target < nums[l])
                {
                    return l;
                }
                if (target > nums[r])
                {
                    return r + 1;
                }
                if (target >= nums[l] && target <= nums[mid])
                {
                    r = mid;
                }
                if (target > nums[mid] && target <= nums[r])
                {
                    l = mid + 1;
                }
            }
            return l;
        }

        //36. 有效的数独
        static public bool IsValidSudoku(char[][] board)
        {
            HashSet<int> row = new HashSet<int>();
            HashSet<int>[] columns = new HashSet<int>[9];
            HashSet<int>[] boxes = new HashSet<int>[9];
            for (int i = 0; i < 9; i++)
            {
                columns[i] = new HashSet<int>();
                boxes[i] = new HashSet<int>();
            }
            for (int i = 0; i < 9; i++)
            {
                row = new HashSet<int>();
                for (int j = 0; j < 9; j++)
                {
                    int t = board[i][j] - '0';
                    if (t == '.' - '0') continue;
                    if (row.Contains(t))
                        return false;
                    else
                        row.Add(t);
                    if (columns[j].Contains(t))
                        return false;
                    else
                        columns[j].Add(t);
                    int r = i / 3;
                    int c = j / 3;
                    int n = r * 3 + c;
                    if (boxes[n].Contains(t))
                        return false;
                    else
                        boxes[n].Add(t);
                }
            }
            return true;
        }

        //37.解数独
        static public void SolveSudoku(char[][] board)
        {
            if (board == null || board.Length != 9 || board[0] == null || board[0].Length != 9) return;
            backTrace(board, 0, 0);

            bool backTrace(char[][] _board, int row, int col)
            {
                const int N = 9;
                //当前行试探结束，切换下一行
                if (col == N)
                    return backTrace(_board, row + 1, 0);
                //最后一行试探结束，解数独完成
                if (row == N)
                    return true;
                if (_board[row][col] != '.')
                    return backTrace(_board, row, col + 1);
                for (char c = '1'; c <= '9'; c++)
                {
                    if (!isValid(_board, row, col, c))
                        continue;
                    _board[row][col] = c;
                    if (backTrace(_board, row, col + 1))
                        return true;
                    _board[row][col] = '.';
                }
                return false;
            }

            bool isValid(char[][] _board, int row, int col, char ch)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (_board[row][k] == ch) return false;
                    if (_board[k][col] == ch) return false;
                    if (_board[(row / 3) * 3 + k / 3][(col / 3) * 3 + k % 3] == ch) return false;
                }
                return true;
            }
        }

        //38.外观数列
        static public string CountAndSay(int n)
        {
            if (n == 1) return "1";
            string s = CountAndSay(n - 1);
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            char pointer = s[0];
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == pointer) counter++;
                else
                {
                    sb.Append(counter);
                    sb.Append(pointer);
                    counter = 1;
                    pointer = s[i];
                }
            }
            sb.Append(counter);
            sb.Append(pointer);
            return sb.ToString();
        }

        //39.组合总和
        static public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            int len = candidates.Length;
            IList<IList<int>> res = new List<IList<int>>();
            if (len == 0) return res;
            List<int> path = new List<int>();
            dfs(candidates, 0, len, target, path, res);
            return res;

            void dfs(int[] _candidates, int _begin, int _len, int _target, List<int> _path, IList<IList<int>> _res)
            {
                if (_target < 0) return;
                if (_target == 0)
                {
                    res.Add(new List<int>(_path));
                    return;
                }
                for (int i = _begin; i < _len; i++)
                {
                    _path.Add(candidates[i]);
                    dfs(_candidates, i, _len, _target - _candidates[i], _path, _res);
                    _path.RemoveAt(_path.Count - 1);
                }
            }
        }

        //40.组合总和 II
        static public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            Array.Sort(candidates);
            int len = candidates.Length;
            IList<IList<int>> res = new List<IList<int>>();
            if (len == 0) return res;
            List<int> path = new List<int>();
            dfs(candidates, 0, len, target, path, res);
            return res;

            void dfs(int[] _candidates, int _begin, int _len, int _target, List<int> _path, IList<IList<int>> _res)
            {
                if (_target < 0) return;
                if (_target == 0)
                {
                    res.Add(new List<int>(_path));
                    return;
                }

                for (int i = _begin; i < _len; i++)
                {
                    if (_target - _candidates[i] < 0)
                    {
                        break;
                    }
                    if (i > _begin && candidates[i] == candidates[i - 1]) { continue; }

                    _path.Add(candidates[i]);
                    dfs(_candidates, i + 1, _len, _target - _candidates[i], _path, _res);
                    _path.RemoveAt(_path.Count - 1);
                }
            }
        }

        //41. 缺失的第一个正数
        static public int FirstMissingPositive(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                int x = nums[i];
                while (1 <= x && x <= nums.Length)
                {
                    if (nums[x - 1] == x) break;
                    nums[i] = nums[x - 1];
                    nums[x - 1] = x;
                    x = nums[i];
                }
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != i + 1) return i + 1;
            }
            return nums.Length + 1;
        }

        //42. 接雨水
        static public int Trap(int[] height)
        {
            if (height.Length <= 2) return 0;
            int left = 0;
            int right = height.Length - 1;
            int lMax = height[0];//暂时可信的左max
            int rMax = height[right];//暂时可信的右max
            int ans = 0;
            while (left <= right)
            {
               
                if (lMax < rMax)    //左边暂时可信的max较小，由左向右装雨水
                { 
                    lMax = Math.Max(lMax, height[left]);
                    ans += lMax - height[left];
                    left++;
                    
                }
                else
                {
                    rMax = Math.Max(rMax, height[right]);
                    ans += rMax - height[right];
                    right--;
                    
                }
            }
            return ans;
        }

        //43. 字符串相乘
        static public string Multiply(string num1, string num2)
        {
            //10x10=100 99x99 = 9801
            //获得各位相乘结果
            bool zeroFlag = true;
            int n = num1.Length + num2.Length;
            int[] rawAns = new int[n];
            for (int i = 0; i < num1.Length; i++)
            {
                for (int j = 0; j < num2.Length; j++)
                {
                    rawAns[1+i+j] += (num1[i] - '0') * (num2[j] - '0');
                    if (rawAns[1 + i + j] != 0) zeroFlag = false;
                }
            }
            if (zeroFlag) return "0";
            //处理进位
            //rawAns[0]必然是尚未使用的
            for (int i = rawAns.Length-1; i > 0; i--)
            {
                int t = rawAns[i] % 10;
                rawAns[i] /= 10;
                int j = 1;
                while (rawAns[i] > 0)
                {
                    rawAns[i - j] += rawAns[i] % 10;
                    rawAns[i] /= 10;
                    j++;
                }
                rawAns[i] = t;
            }
            StringBuilder ans = new StringBuilder();

            for (int i = rawAns[0] == 0 ? 1 : 0; i < rawAns.Length; i++)
            {
                ans.Append(rawAns[i]);
            }
            return ans.ToString();
        }

        //44. 通配符匹配
        static public bool IsMatch(string s, string p)
        {
            int sLen = s.Length;
            int pLen = p.Length;
            bool[][] dp = new bool[pLen+1][];
            dp[0] = new bool[sLen + 1];
            dp[0][0] = true;
            for (int i = 1; i < sLen+1; i++)
            {
                dp[0][i] = false;
            }
            for (int i = 1; i <= pLen; i++)
            {
                dp[i] = new bool[sLen+1];
                dp[i][0] = false;

                if (p[i-1] == '*')
                {
                    int j = 0;
                    while (j < sLen+1 && dp[i - 1][j] != true) j++;
                    for (; j < sLen+1; j++)
                    {
                        dp[i][j] = true;
                    }
                }
                else if (p[i-1] == '?')
                {
                    for (int j = 0; j < sLen; j++)
                    {
                        if (dp[i-1][j] == true)
                        {
                            dp[i][j + 1] = true;
                        }
                        else
                        {
                            dp[i][j + 1] = false;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < sLen; j++)
                    {
                        if (dp[i-1][j] == true)
                        {
                            if (p[i-1] == s[j])
                            {
                                dp[i][j+1] = true;
                            }
                            else
                            {
                                dp[i][j+1] = false;
                            }
                        }
                    }
                }
            }
            return dp[pLen][sLen];
        }

        //45. 跳跃游戏 II
        static public int Jump(int[] nums)
        {
            if (nums.Length == 1) return 0;
            int ans = 0;
            for (int i = 0; i < nums.Length;)
            {
                int index = 0;
                int max = 0;
                for (int j = 0; j < nums[i]; j++)
                {
                    if (i+j+1 >= nums.Length-1)
                    {
                        ans++;
                        return ans;
                    }
                    if (nums[i+j+1] + j + 1 > max)
                    {
                        index = i + j + 1;
                        max = nums[i + j + 1] + j + 1;
                    }
                }
                i = index;
                ans++;
            }
            return ans;
        }

        //46. 全排列
        static public IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            backtrack(new List<int>(nums), new List<int>(), ans);
            return ans;

            void backtrack(IList<int>input, IList<int> output, IList<IList<int>> res)
            {
                if (input.Count == 0)
                {
                    IList<int> newList = new List<int>(output);
                    res.Add(newList);
                    return;
                }

                for (int i = 0;  i < input.Count; i ++)
                {
                    int temp = input[i];
                    input.RemoveAt(i);
                    output.Add(temp);
                    backtrack(input, output, res);
                    output.RemoveAt(output.Count - 1);
                    input.Insert(i, temp);
                }
            }
        }

        //47. 全排列 II
        static public IList<IList<int>> PermuteUnique(int[] nums)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            Array.Sort(nums);
            backtrack(new List<int>(nums), new List<int>(), ans);
            return ans;

            void backtrack(IList<int> input, IList<int> output, IList<IList<int>> res)
            {
                if (input.Count == 0)
                {
                    IList<int> newList = new List<int>(output);
                    res.Add(newList);
                    return;
                }

                for (int i = 0; i < input.Count; i++)
                {
                    if (i > 0 && input[i] == input[i-1])
                    {
                        continue;
                    }
                    int temp = input[i];
                    input.RemoveAt(i);
                    output.Add(temp);
                    backtrack(input, output, res);
                    output.RemoveAt(output.Count - 1);
                    input.Insert(i, temp);
                }
            }
        }

        //48. 旋转图像
        static public void Rotate(int[][] matrix)
        {
            int n = matrix.Length;
            for (int i = 0; i < (n+1)/2; i++)
            {
                for (int j = 0; j < n/2; j++)
                {
                    int temp = matrix[n - 1 - j][i];
                    matrix[n - 1 - j][i] = matrix[n - 1 - i][n - j - 1];
                    matrix[n - 1 - i][n - j - 1] = matrix[j][n - 1 - i];
                    matrix[j][n - 1 - i] = matrix[i][j];
                    matrix[i][j] = temp;
                }
            }
        }

        //49. 字母异位词分组
        static public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            if (strs.Length == 0) return new List<IList<string>>();
            Dictionary<string, List<string>> ans = new Dictionary<string, List<string>>();
            foreach (string s in strs)
            {
                char[] c = s.ToCharArray();
                Array.Sort(c);
                string key = new string(c);
                if (!ans.ContainsKey(key))
                {
                    ans.Add(key, new List<string>());
                }
                ans[key].Add(s);
            }
            IList<IList<string>> res = new List<IList<string>>(ans.Values);
            return res;
        }


    }
}