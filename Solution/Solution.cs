using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Solution {
    public class Solution {
        //3. 无重复字符的最长子串
        public int LengthOfLongestSubstring (string s) {
            if (s.Length == 0) return 0;

            Hashtable ht = new Hashtable ();
            int max = 0;
            int lastChange = 0;
            int firstChange = 65535;
            for (int i = 0; i < s.Length; i++) {
                if (ht.ContainsKey (s[i]) == false) {
                    ht.Add (s[i], i);
                } else {
                    if (i - (int) ht[s[i]] >= max) {
                        max = i - (int) ht[s[i]];
                    }
                    lastChange = i;
                    firstChange = (int) ht[s[i]] < firstChange ? (int) ht[s[i]] : firstChange;
                    ht[s[i]] = i;
                }
            }
            if (firstChange == 65535) {
                firstChange = -1;
            }
            firstChange++;
            int t = s.Length - lastChange;
            t = t > firstChange ? t : firstChange;
            return t > max ? t : max;
        }

        //4. 寻找两个正序数组的中位数
        public double FindMedianSortedArrays (int[] nums1, int[] nums2) {
            int n = nums1.Length;
            int m = nums2.Length;
            int left = (n + m + 1) / 2;
            int right = (n + m + 2) / 2;
            return (getKth (nums1, 0, n - 1, nums2, 0, m - 1, left) + getKth (nums1, 0, n - 1, nums2, 0, m - 1, right)) * 0.5;

            int getKth (int[] nums1, int start1, int end1, int[] nums2, int start2, int end2, int k) {
                int len1 = end1 - start1 + 1;
                int len2 = end2 - start2 + 1;
                if (len1 > len2) return getKth (nums2, start2, end2, nums1, start1, end1, k);
                if (len1 == 0) return nums2[start2 + k - 1];
                if (k == 1) return Math.Min (nums1[start1], nums2[start2]);
                int i = start1 + Math.Min (len1, k / 2) - 1;
                int j = start2 + Math.Min (len2, k / 2) - 1;

                if (nums1[i] > nums2[j]) {
                    return getKth (nums1, start1, end1, nums2, j + 1, end2, k - (j - start2 + 1));
                } else {
                    return getKth (nums1, i + 1, end1, nums2, start2, end2, k - (i - start1 + 1));
                }

            }
        }

        //5. 最长回文子串
        public string LongestPalindrome (string s) {
            int max = 0;
            int mark = 0;
            int offset = 0;
            for (int i = 0; i < s.Length; i++) {
                int t = getPalindromeLengh (s, i, 0);
                if (max < t) {
                    max = t;
                    mark = i;
                    offset = 0;
                }
                t = getPalindromeLengh (s, i, 1);
                if (max < t) {
                    max = t;
                    mark = i;
                    offset = 1;
                }

            }
            string re = s.Substring (mark - (max - offset) / 2, max);
            return re;

            int getPalindromeLengh (string s, int left, int offset) {
                int i = left;
                int j = left + offset;
                int sum = 0;
                if (offset == 0) {
                    i--;
                    j++;
                    sum++;
                }
                while (i >= 0 && j < s.Length) {

                    if (s[i] == s[j]) {
                        sum += 2;
                    } else {
                        return sum;
                    }
                    i--;
                    j++;
                }
                return sum;
            }
        }

        //6. Z 字形变换
        public string Convert (string s, int numRows) {
            string rt = "";
            if (numRows == 1) return s;
            int n = numRows + numRows - 2; //一个N的一个单位字母数字
            if (s.Length - numRows < 0) return s;
            for (int i = 0; i <= (s.Length - 1) / n; i++) {
                rt += s[i * n];
            }

            for (int i = 1; i < numRows - 1; i++) {
                for (int j = 0; j <= (s.Length - i - 1) / n; j++) {
                    rt += s[i + j * n];
                    if (i + j * n + 2 * (numRows - i - 2) + 2 < s.Length)
                        rt += s[i + j * n + 2 * (numRows - i - 2) + 2];
                }
            }
            if (s.Length - numRows < 0) return rt;
            for (int i = 0; i <= (s.Length - numRows) / n; i++) {
                rt += s[numRows - 1 + i * n];
            }
            return rt;
        }

        //7. 整数反转
        public int Reverse (int x) {
            int sym = 1;
            if (x < 0) {
                sym = -1;
                x = -x;
            }

            int sum = 0;
            while (x > 0) {
                if (sum > int.MaxValue / 10) {
                    return 0;
                }
                sum *= 10;
                sum += x % 10;
                x /= 10;
            }
            return sum * sym;
        }

        //8. 字符串转换整数 (atoi)
        public int MyAtoi (string str) {
            int sum = 0;
            int sym = 1;
            int i = 0;
            //排除空格与符号
            for (; i < str.Length; i++) {
                if (str[i] == ' ') continue;
                if (str[i] == '+') break;
                if (str[i] == '-') {
                    sym = -1;
                    break;
                }
                if (str[i] >= '0' && str[i] <= '9') {
                    i--;
                    break;
                } else {
                    return 0;
                }
            }
            i++;

            for (; i < str.Length; i++) {
                if (str[i] >= '0' && str[i] <= '9') {
                    try {
                        checked {
                            sum *= 10;
                            sum += str[i] - '0';
                        }
                    } catch (OverflowException) {

                        if (sym == 1) return int.MaxValue;
                        if (sym == -1) return int.MinValue;

                    }
                } else {
                    if (str[i] == ' ' || str[i] == '.') return sum * sym;
                    else {
                        return sum * sym;
                    }
                }
            }
            return sum * sym;
        }

        //9. 回文数
        public bool IsPalindrome (int x) {
            if (x < 0) return false;
            if (x < 10) return true;
            if (x % 10 == 0) return false;
            int y = 0;
            while (x > y) {
                y *= 10;
                y += x % 10;
                x /= 10;
            }
            if (x == y) return true;
            else if (x < 10 && y < 10) return false;

            y /= 10;
            if (x == y)
                return true;
            else
                return false;
        }

        //10. 正则表达式匹配
        public bool IsMatch (string s, string p) {
            return true;
            //TODO:
        }

        //11. 盛最多水的容器
        public int MaxArea (int[] height) {
            int l = 0;
            int r = height.Length - 1;
            int max = int.MinValue;
            while (l < r) {
                int t = Math.Min (height[l], height[r]) * (r - l);
                if (t > max) {
                    max = t;
                }
                if (height[l] < height[r]) {
                    l++;
                } else {
                    r--;
                }
            }
            return max;
        }

        //12.整数转罗马数字
        public string IntToRoman (int num) {
            string roman = "";
            int[] NUMBER_ORDERS = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            string[] ROMANSYM = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            while (num > 0) {
                int i = 0;
                for (; i < NUMBER_ORDERS.Length; i++) {
                    if (num < NUMBER_ORDERS[i]) {
                        break;
                    }
                }
                i--;
                num -= NUMBER_ORDERS[i];
                roman += ROMANSYM[i];
            }
            return roman;
        }

        //13.罗马数字转整数
        public int RomanToInt (string s) {
            Hashtable htRoman = new Hashtable ();
            htRoman.Add ('I', 1);
            htRoman.Add ('V', 5);
            htRoman.Add ('X', 10);
            htRoman.Add ('L', 50);
            htRoman.Add ('C', 100);
            htRoman.Add ('D', 500);
            htRoman.Add ('M', 1000);
            int num = (int) htRoman[s[0]];
            for (int i = 1; i < s.Length; i++) {
                if ((int) htRoman[s[i]] > (int) htRoman[s[i - 1]]) {
                    num += (int) htRoman[s[i]] - (int) htRoman[s[i - 1]] - (int) htRoman[s[i - 1]];
                } else {
                    num += (int) htRoman[s[i]];
                }
            }
            return num;
        }

        //14.最长公共前缀
        public string LongestCommonPrefix (string[] strs) {
            if (strs == null || strs.Length == 0) return "";
            Array.Sort (strs);
            string res = "";
            for (int i = 0; i < strs[0].Length; i++) {
                if (i < strs[strs.Length - 1].Length && strs[0][i] == strs[strs.Length - 1][i]) {
                    res += strs[0][i];
                } else {
                    break;
                }
            }
            return res;
        }

        //15.三数之和
        public IList<IList<int>> ThreeSum (int[] nums) {
            IList<IList<int>> res = new List<IList<int>> ();
            Array.Sort (nums);

            for (int i = 0; i < nums.Length - 2; i++) {
                int l = i + 1;
                int r = nums.Length - 1;
                if (nums[i] > 0) break;
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                while (l < r) {
                    int sum = nums[i] + nums[l] + nums[r];
                    if (sum == 0) {
                        res.Add (new List<int> (new int[] { nums[i], nums[l], nums[r] }));
                        while (l < r && nums[l + 1] == nums[l]) l++;
                        while (l < r && nums[r] == nums[r - 1]) r--;
                        l++;
                        r--;
                    } else if (sum < 0) l++;
                    else if (sum > 0) r--;
                }
            }
            return res;
        }

        //16. 最接近的三数之和
        public int ThreeSumClosest (int[] nums, int target) {
            int res = 0;
            int offset = int.MaxValue;
            Array.Sort (nums);
            for (int i = 0; i < nums.Length - 2; i++) {
                int l = i + 1;
                int r = nums.Length - 1;
                while (l < r) {
                    int sum = nums[i] + nums[l] + nums[r];
                    int dif = sum - target;

                    if (dif == 0) {
                        return target;
                    }
                    int tOffset = Math.Abs (dif);
                    if (tOffset < offset) {
                        offset = tOffset;
                        res = sum;
                    }

                    if (dif > 0) {
                        r--;
                    } else {
                        l++;
                    }

                }
            }
            return res;
        }

        //17. 电话号码的字母组合
        public IList<string> LetterCombinations (string digits) {
            Dictionary<char, string> letterMap = new Dictionary<char, string> ();
            letterMap.Add ('2', "abc");
            letterMap.Add ('3', "def");
            letterMap.Add ('4', "ghi");
            letterMap.Add ('5', "jkl");
            letterMap.Add ('6', "mno");
            letterMap.Add ('7', "pqrs");
            letterMap.Add ('8', "tuv");
            letterMap.Add ('9', "wxyz");
            return getCombinations (digits);
            IList<string> strToList (string str) {
                IList<string> res = new List<string> ();
                for (int i = 0; i < str.Length; i++) {
                    res.Add (str[i].ToString ());
                }
                return res;
            }

            IList<string> strsMulti (IList<string> s1, IList<string> s2) {
                IList<string> res = new List<string> ();
                for (int i = 0; i < s1.Count; i++) {
                    for (int j = 0; j < s2.Count; j++) {
                        res.Add (s1[i] + s2[j]);
                    }
                }
                return res;
            }

            IList<string> getCombinations (string digits) {
                if (digits.Length <= 0) return new List<string> ();
                if (digits.Length == 1) {
                    return strToList (letterMap[digits[0]]);
                } else {
                    IList<string> res = strToList (letterMap[digits[0]]);
                    return strsMulti (res, getCombinations (digits.Substring (1)));
                }
            }

        }

        //18. 四数之和
        public IList<IList<int>> FourSum (int[] nums, int target) {
            IList<IList<int>> res = new List<IList<int>> ();
            Array.Sort (nums);

            for (int i = 0; i < nums.Length - 3; i++) {
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                for (int j = i + 1; j < nums.Length - 2; j++) {
                    int l = j + 1;
                    int r = nums.Length - 1;
                    //if (nums[i] + nums[j] > target) break;
                    if (j > i + 1 && nums[j] == nums[j - 1]) continue;
                    while (l < r) {
                        int sum = nums[i] + nums[l] + nums[r] + nums[j];
                        if (sum == target) {
                            res.Add (new List<int> (new int[] { nums[i], nums[j], nums[l], nums[r] }));
                            while (l < r && nums[l + 1] == nums[l]) l++;
                            while (l < r && nums[r] == nums[r - 1]) r--;
                            l++;
                            r--;
                        } else if (sum < target) l++;
                        else if (sum > target) r--;
                    }
                }
            }
            return res;
        }

        //19. 删除链表的倒数第N个节点
        public class ListNode {
            public int val;
            public ListNode next;
            public ListNode (int x) { val = x; next = null; }
            public ListNode (int val = 0, ListNode next = null) {
                this.val = val;
                this.next = next;
            }
        }
        public ListNode RemoveNthFromEnd (ListNode head, int n) {
            ListNode left, right;
            left = head;
            right = head;
            for (int i = 0; i < n; i++) {
                if (right.next != null) {
                    right = right.next;
                } else {
                    if (i == n - 1) {
                        return head.next;
                    }
                    //给定的n保证有效故没有else

                }
            }
            while (right.next != null) {
                right = right.next;
                left = left.next;
            }
            left.next = left.next.next;
            return head;
        }

        //20. 有效的括号
        public bool IsValid (string s) {
            Dictionary<char, char> pair = new Dictionary<char, char> ();
            pair.Add (')', '(');
            pair.Add (']', '[');
            pair.Add ('}', '{');
            Stack<char> stack = new Stack<char> ();
            foreach (char i in s) {
                switch (i) {
                    case '(':
                    case '[':
                    case '{':
                        stack.Push (i);
                        break;
                    case ')':
                    case ']':
                    case '}':
                        if (stack.Count == 0) return false;
                        if (stack.Peek () == pair[i]) {
                            stack.Pop ();
                        } else {
                            return false;
                        }
                        break;
                }
            }
            if (stack.Count == 0) return true;
            else return false;
        }

        //21. 合并两个有序链表
        public ListNode MergeTwoLists (ListNode l1, ListNode l2) {
            ListNode res = new ListNode ();
            ListNode pRes = res;
            while (l1 != null && l2 != null) {
                if (l1.val < l2.val) {
                    pRes.next = new ListNode (l1.val);
                    l1 = l1.next;
                } else {
                    pRes.next = new ListNode (l2.val);
                    l2 = l2.next;
                }
                pRes = pRes.next;
            }
            if (l1 != null) {
                pRes.next = l1;
            } else if (l2 != null) {
                pRes.next = l2;
            }
            return res.next;
        }

        //22. 括号生成
        public IList<string> GenerateParenthesis (int n) {
            return null;
        }

        //23.合并K个排序链表
        public ListNode MergeKLists (ListNode[] lists) {
            if (lists.Length == 0) return null;
            else if (lists.Length == 1) return lists[0];
            else if (lists.Length == 2) return MergeTwoLists (lists[0], lists[1]);
            else {
                return MergeTwoLists (MergeKLists (lists.Take<ListNode> (lists.Length / 2).ToArray ()),
                    MergeKLists (lists.Skip<ListNode> (lists.Length / 2).ToArray ()));
            }
        }

        //24. 两两交换链表中的节点
        public ListNode SwapPairs (ListNode head) {
            // If the list has no node or has only one node left.
            if ((head == null) || (head.next == null)) {
                return head;
            }

            // Nodes to be swapped
            ListNode firstNode = head;
            ListNode secondNode = head.next;

            // Swapping
            firstNode.next = SwapPairs (secondNode.next);
            secondNode.next = firstNode;

            // Now the head is the second node
            return secondNode;
        }

        //25.K 个一组翻转链表
        public ListNode ReverseKGroup (ListNode head, int k) {
            ListNode[] listsNodes = new ListNode[k];
            ListNode p = head;

            for (int i = 0; i < k; i++, p = p.next) {
                if (p == null) return head;
                listsNodes[i] = p;
            }
            for (int i = 1; i < k; i++) {
                listsNodes[i].next = listsNodes[i - 1];
            }
            listsNodes[0].next = ReverseKGroup (p, k);

            return listsNodes[k - 1];
        }

        //26. 删除排序数组中的重复项
        public int RemoveDuplicates (int[] nums) {
            if (nums.Length <= 1) return nums.Length;
            int len = 0;
            for (int i = 1; i < nums.Length; i++) {
                if (nums[i] == nums[len]) continue;
                else {
                    len++;
                    nums[len] = nums[i];
                }
            }
            return len + 1;
        }

        //27. 移除元素
        public int RemoveElement (int[] nums, int val) {
            int len = 0;
            for (int i = 0; i < nums.Length; i++) {
                if (nums[i] != val) {
                    nums[len] = nums[i];
                    len++;
                }
            }
            return len;
        }

        //28. 实现 strStr()
        public int StrStr (string haystack, string needle) {
            if (needle.Length == 0) return 0;
            int stat = 0;
            for (int i = 0; i < haystack.Length; i++) {
                if (haystack[i] == needle[stat]) {
                    if (stat == needle.Length - 1) return i - needle.Length + 1;
                    else stat++;
                } else {
                    i -= stat;
                    stat = 0;
                }
            }
            return -1;
        }

        //29. 两数相除
        public int Divide (int dividend, int divisor) {
            return 0; //TODO:
        }

        //30. 串联所有单词的子串
        public IList<int> FindSubstring (string s, string[] words) {
            return null; //TODO:
        }
    }
}