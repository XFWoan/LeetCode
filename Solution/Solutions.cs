using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Solutions {
    static public class Solution {
        //31. 下一个排列
        static public void NextPermutation (int[] nums) {
            if (nums.Length < 2) {
                return;
            }
            int i, j, temp;
            i = nums.Length - 2;
            while (nums[i + 1] <= nums[i]) {
                if (i == 0) {
                    Array.Reverse (nums);
                    return;
                }
                i--;
            }
            j = i + 1;
            while (nums[j] > nums[i]) {
                j++;
                if (j >= nums.Length) {
                    temp = nums[i];
                    nums[i] = nums[j - 1];
                    nums[j - 1] = temp;
                    Array.Reverse (nums, i + 1, nums.Length - i - 1);
                    return;
                }
            }
            temp = nums[i];
            nums[i] = nums[j - 1];
            nums[j - 1] = temp;
            Array.Reverse (nums, i + 1, nums.Length - i - 1);
        }

        
    }
}