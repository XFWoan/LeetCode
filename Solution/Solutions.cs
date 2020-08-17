using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
                for (char c = '1';c <='9';c++)
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

            bool isValid(char[][] _board, int row,int col, char ch)
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


    }
}