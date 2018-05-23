using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            #region   最长回文
            //string s = "abccccgjhgjhgjhkjkljlddaa";
            //Console.WriteLine(LongestPalindrome(s));
            #endregion

            #region 词语模式
            //string pattern = "abba";
            //string str = "dog cat cat dog";
            //string str2 = "dog cat cat dof";
            //Console.WriteLine(WordPattern(pattern, str));
            //Console.WriteLine(WordPattern(pattern, str2));
            #endregion

            #region 同字符词语分组
            //string[] strs = { "eat", "tea", "tan", "ate", "nat", "bat","dbt" };
            //foreach (var strList in Anagram(strs.ToList()))
            //{
            //    foreach (var item in strList)
            //    {
            //        Console.Write(item + "\t");
            //    }
            //    Console.WriteLine();
            //}
            #endregion

            #region 无重复字符的最长子串
            //string[] strs = { "abcabcde", "bbbbb", "pwwkew" };
            //foreach (var item in strs)
            //{
            //    Console.WriteLine(GetLongestSubstring(item));
            //}
            #endregion

            #region 重复的DNA序列
            //string s = "AAAAACCCCCAAAAACCCCCAAAAAGGGTTT";
            //foreach (var item in GetRepeatedDnsSequence(s,10))
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region 最小窗口子串
            //string s = "ADOBECODEBANC";
            //string t = "ABC";
            //Console.WriteLine(GetShortestWindowsSubstring(s, t));
            #endregion


            Console.ReadKey();
        }

        static int LongestPalindrome(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            foreach (var item in s)
            {
                if (dic.ContainsKey(item))
                    dic[item]++;
                else
                    dic.Add(item, 1);
            }
            int center = 0;
            int maxLength = 0;
            foreach (KeyValuePair<char, int> item in dic)
            {
                if (item.Value % 2 == 0)
                    maxLength += item.Value;
                else
                {
                    maxLength += item.Value - 1;
                    center = 1;
                }
            }
            return maxLength + center;
        }

        static bool WordPattern(string pattern, string str)
        {
            string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length != pattern.Length)
                return false;

            Dictionary<char, string> dic = new Dictionary<char, string>();
            for (int i = 0; i < words.Length; i++)
            {
                if (dic.ContainsKey(pattern[i]) && dic[pattern[i]] != words[i])
                    return false;
                else if (!dic.ContainsKey(pattern[i]))
                    dic.Add(pattern[i], words[i]);
            }
            return true;
        }

        static List<List<string>> Anagram(List<string> strList)
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

            foreach (var str in strList)
            {
                List<char> charList = new List<char>();
                StringBuilder sb = new StringBuilder();
                foreach (var ch in str)
                {
                    charList.Add(ch);
                }
                charList.Sort();
                foreach (var item in charList)
                {
                    sb.Append(item);
                }
                string key = sb.ToString();
                if (dic.ContainsKey(key))
                    dic[key].Add(str);
                else
                {
                    dic.Add(key, new List<string>());
                    dic[key].Add(str);
                }
            }
            List<List<string>> result = new List<List<string>>(dic.Count);
            foreach (KeyValuePair<string, List<string>> item in dic)
            {
                result.Add(item.Value);
            }
            return result;
        }

        static int GetLongestSubstring(string str)
        {
            int[] mark = new int[26];
            int max = 0;
            int sum = 0;
            foreach (var ch in str)
            {
                int index = ch - 'a';
                if (mark[index] == 1)
                {
                    max = sum > max ? sum : max;
                    mark = new int[26];
                    sum = 1;
                }
                else
                {
                    sum++;
                }
                mark[index] = 1;
            }
            max = sum > max ? sum : max;
            return max;
        }

        static List<string> GetRepeatedDnsSequence(string s, int n)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>(s.Length - n + 1);
            for (int i = 0; i < s.Length - n + 1; i++)
            {
                string key = s.Substring(i, n);
                if (dic.ContainsKey(key))
                    dic[key]++;

                else
                    dic.Add(key, 1);
            }
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, int> item in dic)
            {
                if (item.Value > 1)
                    list.Add(item.Key);
            }
            return list;
        }


        static string GetMinimumWindowSubstring(string str, string t)
        {
            int[] flag = new int[26];
            int[] target = new int[t.Length];
            int i = 0;
            foreach (var item in t)
            {
                flag[item - 'A'] = 1;
                target[i++] = item;
            }
            string[] result = new string[1];
            int min = int.MaxValue;
            Dictionary<StringBuilder, List<int>> dic = new Dictionary<StringBuilder, List<int>>();
            int[] find = new int[t.Length];
            foreach (var item in str)
            {
                if (flag[item - 'A'] == 1)
                    dic.Add(new StringBuilder(), new List<int>(target));
                min = Add(dic, item, min, result);
            }
            return result[0];
        }

        static int Add(Dictionary<StringBuilder, List<int>> sbDic, char t, int min, string[] result)
        {
            int newMin = min;
            foreach (var item in sbDic)
            {
                if (item.Value.Count != 0)
                {
                    item.Key.Append(t);
                    item.Value.Remove(t);
                    if (item.Value.Count == 0)
                    {
                        if (min > item.Key.Length)
                        {
                            result[0] = item.Key.ToString();
                            newMin = result[0].Length;
                        }
                    }
                }
            }
            return newMin;
        }

        static string GetShortestWindowsSubstring(string str, string t)
        {
            string result = null;
            int[] mark = new int[128];
            int[] tmp = new int[128];
            List<int> tList = new List<int>();
            int start = 0;

            foreach (var item in t)
                mark[item]++;

            for (int i = 0; i < mark.Length; i++)
            {
                if (mark[i] > 0)
                    tList.Add(i);
            }
            
            for (int i = 0; i < str.Length; i++)
            {
                tmp[str[i]]++;
                while (start < i)
                {
                    char ch = str[start];
                    if (mark[ch] == 0)
                        start++;
                    else if (tmp[ch] > mark[ch])
                    {
                        start++;
                        tmp[ch]--;
                    }
                    else
                        break;
                }
                if (FindWindow(tmp, mark, tList))
                {
                    int n = i - start + 1;
                    if (result == null || result.Length > n)
                        result = str.Substring(start, n);
                }
            }
            return result;
        }

        static bool FindWindow(int[] s, int[] t, List<int> tList)
        {
            foreach (var item in tList)
            {
                if (s[item] < t[item])
                    return false;
            }
            return true;
        }

    }
}
