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
            while (l <=r)
            {
                int mid = (l + r) / 2;
                if (nums[mid] == target) return mid;
                if (nums[0] <= nums[mid])
                {
                    if(nums[0]<=target && target<nums[mid])
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
                    if(nums[mid]<target && target <= nums[nums.Length - 1])
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
    }
}