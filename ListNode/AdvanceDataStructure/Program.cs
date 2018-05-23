using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            #region trie树（字典树）
            //TrieNode root = new TrieNode();
            //root.Insert("abc");
            //root.Insert("abcd");
            //root.Insert("abd");
            //root.GetAllWords();
            //Console.WriteLine(root.Search("abde"));
            //Console.WriteLine(root.StartsWith("acb"));
            #endregion

            #region 添加与查找单词(trie树)
            //WordDictionary wd = new WordDictionary();
            //wd.Insert("bad");
            //wd.Insert("dad");
            //wd.Insert("mad");
            //Console.WriteLine(wd.Search("pad"));
            //Console.WriteLine(wd.Search("bad"));
            //Console.WriteLine(wd.Search(".ad"));
            //Console.WriteLine(wd.Search("b.."));
            #endregion

            #region 朋友圈(并查集) *

            #region 并查集：数组实现
            //DisjointSet ds = new DisjointSet(8);
            //ds.Print();
            //Console.WriteLine("Union(0,5)");
            //ds.Union(0, 5);
            //Console.WriteLine("Find(0) = " + ds.Find(0) + ", " + "Find(5) = " + ds.Find(5));
            //Console.WriteLine("Find(2) = " + ds.Find(2) + ", " + "Find(5) = " + ds.Find(5));
            //ds.Union(2, 4);
            //ds.Print();
            //ds.Union(0, 4);
            //ds.Print();
            //Console.WriteLine("Find(2) = " + ds.Find(2) + ", " + "Find(5) = " + ds.Find(5));
            #endregion

            #region 并查集：森林实现 *
            //DisjoinTree ds = new DisjoinTree(8);
            //ds.Print();
            //Console.WriteLine("Union(0,5)");
            //ds.Union(0, 5);
            //Console.WriteLine("Find(0) = " + ds.Find(0) + ", " + "Find(5) = " + ds.Find(5));
            //Console.WriteLine("Find(2) = " + ds.Find(2) + ", " + "Find(5) = " + ds.Find(5));
            //ds.Union(2, 4);
            //ds.Print();
            //ds.Union(0, 4);
            //ds.Print();
            //Console.WriteLine("Find(2) = " + ds.Find(2) + ", " + "Find(5) = " + ds.Find(5));
            //Console.WriteLine(ds.Find(4));
            #endregion

            #region 朋友圈
            //Console.WriteLine(FindCircleNum(new int[3, 3] { { 1, 1, 0 }, { 1, 1, 0 }, { 0, 0, 1 } }));
            #endregion

            #endregion

            #region 区域和的查询（线段树）
            int[] nums = { 0, 1, 2, 3, 4, 5 };
            SegmentTree st = new SegmentTree(nums);
            Console.WriteLine(st.SumRangeSegmentTree(0,5));
            st.Print();
            Console.WriteLine();
            int sum = st.SumRangeSegmentTree(2, 4);
            st.Update(2, 10);
            st.Print();
            #endregion

            Console.ReadKey();
        }

        #region trie树（字典树）
        class TrieNode
        {
            protected TrieNode[] child = new TrieNode[26];
            protected bool isEnd;

            public bool IsEnd { get => isEnd; set => isEnd = value; }
            public TrieNode[] Child { get => child; set => child = value; }
            public TrieNode()
            {

            }

            public void PreOrderTrie(int layer)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (Child[i] != null)
                    {
                        for (int j = 0; j < layer; j++)
                            Console.Write("---");

                        Console.Write((char)(i + 'a'));

                        if (Child[i].isEnd)
                            Console.Write("<end>");

                        Console.WriteLine();
                        Child[i].PreOrderTrie(layer + 1);
                    }
                }
            }

            public void GetAllWords()
            {
                List<string> list = new List<string>();
                StringBuilder sb = new StringBuilder();
                GetAllWords(list, sb, this);
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }

            protected void GetAllWords(List<string> list, StringBuilder sb, TrieNode tn)
            {
                for (int i = 0; i < tn.Child.Length; i++)
                {
                    if (tn.Child[i] != null)
                    {
                        sb.Append((char)(i + 'a'));
                        if (tn.Child[i].isEnd)
                            list.Add(sb.ToString());
                        GetAllWords(list, sb, tn.Child[i]);

                    }
                }
                if (sb.Length != 0)
                    sb.Remove(sb.Length - 1, 1);
                return;
            }

            public void Insert(string word)
            {
                TrieNode node = this;
                for (int i = 0; i < word.Length; i++)
                {
                    int index = word[i] - 'a';
                    if (node.Child[index] == null)
                    {
                        node.Child[index] = new TrieNode();
                        node = node.Child[index];
                    }
                    else
                        node = node.Child[index];
                }
                node.IsEnd = true;
            }

            public bool Search(string word)
            {
                TrieNode node = this;
                foreach (var item in word)
                {
                    int index = item - 'a';
                    if (node.Child[index] == null)
                        return false;
                    node = node.Child[index];
                }
                return node.IsEnd;
            }

            public bool StartsWith(string prefix)
            {
                TrieNode node = this;
                foreach (var item in prefix)
                {
                    int index = item - 'a';
                    if (node.Child[index] == null)
                        return false;
                    node = node.Child[index];
                }
                return true;
            }

        }
        #endregion 添加与查找单词

        #region 添加与查找单词(支持正则)
        class WordDictionary : TrieNode
        {
            public new bool Search(string word)
            {
                return Search(this, word, 0);
            }

            private bool Search(TrieNode node, string word, int i)
            {
                if (i == word.Length)
                    return node.IsEnd;

                if (word[i] == '.')
                {
                    foreach (var item in node.Child)
                        if (item != null && Search(item, word, i + 1))
                            return true;
                    return false;
                }
                else
                {
                    int index = word[i] - 'a';
                    if (node.Child[index] != null)
                        return Search(node.Child[index], word, i + 1);
                }
                return false;
            }
        }

        #endregion

        #region 朋友圈

        #region 数组实现
        class DisjointSet
        {
            List<int> id = null;
            public DisjointSet(int n)
            {
                id = new List<int>(n);
                for (int i = 0; i < n; i++)
                    id.Add(i);
            }

            public int Find(int p)
            {
                return id[p];
            }

            public void Union(int a, int b)
            {
                int aid = Find(a);
                int bid = Find(b);
                if (aid == bid)
                    return;

                for (int i = 0; i < id.Count; i++)
                    if (id[i] == aid)
                        id[i] = bid;
            }

            public void Print()
            {
                Console.Write("元素 ：");
                for (int i = 0; i < id.Count; i++)
                    Console.Write(" " + i);

                Console.WriteLine();
                Console.Write("集合 ：");
                foreach (var item in id)
                    Console.Write(" " + item);

                Console.WriteLine();
            }
        }
        #endregion

        #region 森林实现
        class DisjoinTree
        {
            private List<int> id = null;
            private List<int> size = null;
            private int count = 0;

            public int Count { get => count; set => count = value; }

            public DisjoinTree(int n)
            {
                id = new List<int>(n);
                size = new List<int>(n);
                for (int i = 0; i < n; i++)
                {
                    id.Add(i);
                    size.Add(1);
                }
                Count = n;
            }

            public int Find(int p)
            {
                if (p != id[p])
                {
                    id[p] = id[id[p]];
                    p = id[p];
                }
                return p;
            }

            public void Union(int a, int b)
            {
                int i = Find(a);
                int j = Find(b);
                if (i == j)
                    return;

                if (size[i] < size[j])
                {
                    id[i] = j;
                    size[j] += size[i];
                }
                else
                {
                    id[j] = i;
                    size[i] += size[j];
                }
                Count--;
            }
            public void Print()
            {
                Console.Write("元素 ：");
                for (int i = 0; i < id.Count; i++)
                    Console.Write(" " + i);

                Console.WriteLine();
                Console.Write("集合 ：");
                foreach (var item in id)
                    Console.Write(" " + item);

                Console.WriteLine();
            }
        }
        #endregion

        #region 朋友圈
        static int FindCircleNum(int[,] map)
        {
            int n = map.GetLength(0);
            DisjoinTree ds = new DisjoinTree(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (map[i, j] == 1)
                        ds.Union(i, j);
            }
            return ds.Count;
        }
        #endregion

        #endregion

        #region 区域和的查询（线段树）
        class SegmentTree
        {
            int[] value = null;
            int rightEnd = 0;

            private int RightEnd { get => rightEnd; }
            public SegmentTree(int[] nums)
            {
                value = new int[nums.Length * 4];
                rightEnd = nums.Length - 1;
                BuildTree(value, nums, 0, 0, RightEnd);
            }

            private void BuildTree(int[] value, int[] nums, int pos, int left, int right)
            {
                if (left == right)
                {
                    value[pos] = nums[left];
                    return;
                }
                int mid = (left + right) / 2;
                BuildTree(value, nums, pos * 2 + 1, left, mid);
                BuildTree(value, nums, pos * 2 + 2, mid + 1, right);
                value[pos] = value[pos * 2 + 1] + value[pos * 2 + 2];
            }

            public void Print()
            {
                Print(0, 0, RightEnd, 0);
            }

            private void Print(int pos, int left, int right, int layer)
            {
                for (int i = 0; i < layer; i++)
                {
                    Console.Write("---");
                }
                Console.WriteLine("[" + left + ", " + right + "][" + pos + "] : " + value[pos]);
                if (left == right)
                    return;
                int mid = (left + right) / 2;
                Print(pos * 2 + 1, left, mid, layer + 1);
                Print(pos * 2 + 2, mid + 1, right, layer + 1);
            }

            public int SumRangeSegmentTree(int left, int right)
            {
                return SumRangeSegmentTree(0, 0, RightEnd, left, right);
            }

            private int SumRangeSegmentTree(int pos, int left, int right, int qleft, int qright)
            {
                if (qleft > right || qright < left)
                    return 0;

                if (qleft <= left && qright >= right)
                    return value[pos];

                int mid = (left + right) / 2;
                return SumRangeSegmentTree(pos * 2 + 1, left, mid, qleft, qright) + SumRangeSegmentTree(pos * 2 + 2, mid + 1, right, qleft, qright);
            }

            public void Update(int index, int newValue)
            {
                Update(0, 0, RightEnd, index, newValue);
            }

            private void Update(int pos, int left, int right, int index, int newValue)
            {
                if (left == right && left == index)
                {
                    value[pos] = newValue;
                    return;
                }
                int mid = (left + right) / 2;

                if (index <= mid)
                    Update(pos * 2 + 1, left, mid, index, newValue);

                else
                    Update(pos * 2 + 2, mid + 1, right, index, newValue);

                value[pos] = value[pos * 2 + 1] + value[pos * 2 + 2];
            }
        }
        #endregion

    }
}
