using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 二分查找
            //int[] source = { -1, 2, 5, 20, 90, 100, 207, 800 };
            //int[] target = { 50, 90, 3, -1, 207, 80 };
            //foreach (var item in SearchArray(source, target))
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region 插入位置
            //int[] num = { 1, 3, 5, 6 };
            //int[] target = { 0, 1, 2, 3, 4, 5, 6, 7 };
            //foreach (var item in target)
            //{
            //    Console.WriteLine(SearchInsert(num, item));
            //}
            #endregion

            #region 区间查找
            //int[] num = { 5, 7, 7, 8, 8, 8, 8, 10 };
            //int[] result = SearchBlock(num, 5);
            //Console.WriteLine("["+result[0]+","+result[1]+"]");
            #endregion

            #region 旋转数组查找
            //int[] num = { 7, 9, 12, 15, 20, 1, 3, 6 };
            //foreach (var item in num)
            //{
            //    Console.WriteLine(SearchRotatedArray(num, item));
            //}

            #endregion

            #region 二叉查找树构建
            BinaryTreeNode root = new BinaryTreeNode(8);
            int[] num = { 3, 10, 1, 6, 15 };
            foreach (var item in num)
            {
                root.AddToTree(new BinaryTreeNode(item));
            }
            //root.Print();
            #endregion

            #region 二叉查找树数值
            //Console.WriteLine(root.Search(15));
            //Console.WriteLine(root.Search(4));
            #endregion

            #region 二叉查找树编码与解码
            //BinaryTreeNode newRoot = BinaryTreeNode.Decode(root.Encode());
            //newRoot.Print();
            #endregion

            #region 逆序数
            //int[] nums = { 5, 2, 6, 1 };
            //PrintList(new BinaryTreeNode().CountSmaller(nums));
            //nums = new int[] { 6, 6, 6, 1, 1, 1 };
            // PrintList(new BinaryTreeNode().CountSmaller(nums));
            //nums = new int[] { 5, -7, 9, 1, 3, 5, -2, 1 };
            //PrintList(new BinaryTreeNode().CountSmaller(nums));
            #endregion


            Console.ReadKey();
        }

        static int[] SearchArray(int[] source, int[] target)
        {
            int[] find = new int[target.Length];
            for (int i = 0; i < find.Length; i++)
            {
                find[i] = BinarySearch(source, target[i]);
            }
            return find;
        }

        static int BinarySearch(int[] source, int target)
        {
            int left = 0;
            int right = source.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (source[mid] == target)
                    return 1;
                else if (source[mid] > target)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return 0;
        }

        static int SearchInsert(int[] num, int target)
        {
            int left = 0;
            int right = num.Length - 1;
            if (num[left] > target)
                return 0;
            if (num[right] < target)
                return ++right;
            while (left != right - 1)
            {
                int mid = (left + right) / 2;
                if (num[mid] == target)
                    return mid;
                else if (num[mid] > target)
                    right = mid;
                else
                    left = mid;
            }
            if (num[left] == target)
                return left;

            return right;
        }

        static int[] SearchBlock(int[] num, int target)
        {
            int[] result = { -1, -1 };
            int left = 0;
            int right = num.Length;
            bool bSearch = false;
            int mid = 0;
            while (left <= right && !bSearch)
            {
                mid = (left + right) / 2;
                if (num[mid] == target)
                {
                    bSearch = true;
                    break;
                }
                else if (num[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            if (bSearch)
            {
                result[0] = SearchBlock(num, target, left, mid, 0);
                result[1] = SearchBlock(num, target, mid, right, 1);
            }
            return result;
        }

        static int SearchBlock(int[] num, int target, int left, int right, int flag)
        {
            while (left != right)
            {
                int mid = (left + right) / 2;
                if (num[mid] == target)
                {
                    if (flag == 0)
                        right = mid;
                    else
                    {
                        if (left + 1 == right)
                            break;
                        left = mid;
                    }
                }
                else if (num[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return left;
        }

        static int SearchRotatedArray(int[] num, int target)
        {
            int left = 0;
            int right = num.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (num[mid] == target)
                {
                    return mid;
                }
                else if (target < num[mid])
                {
                    if (num[left] < num[mid])
                    {
                        if (target >= num[left])
                            right = mid - 1;
                        else
                            left = mid + 1;
                    }
                    else if (num[left] > num[mid])
                        right = mid - 1;
                    else if (num[left] == num[mid])
                        left = mid + 1;
                }
                else if (target > num[mid])
                {
                    if (num[left] < num[mid])
                        left = mid + 1;
                    else if (num[left] > num[mid])
                    {
                        if (num[left] <= target)
                            right = mid - 1;
                        else
                            left = mid + 1;
                    }
                    else if (num[left] == num[mid])
                        left = mid + 1;
                }
            }
            return -1;
        }

        static void PrintList(List<int> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("=============================");
        }

    }

    class BinaryTreeNode
    {
        int value = 0;
        BinaryTreeNode left = null;
        BinaryTreeNode right = null;
        int leftCount = 0;

        public int Value { get => value; set => this.value = value; }
        internal BinaryTreeNode Left { get => left; set => left = value; }
        internal BinaryTreeNode Right { get => right; set => right = value; }
        public int LeftCount { get => leftCount; set => leftCount = value; }

        public BinaryTreeNode(int value, BinaryTreeNode left, BinaryTreeNode right)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public BinaryTreeNode(int value)
        {
            Value = value;
        }

        public BinaryTreeNode(params int[] nums)
        {
            Value = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                AddToTree(new BinaryTreeNode(nums[i]));
            }
        }

        public BinaryTreeNode() { }

        public void AddToTree(BinaryTreeNode node)
        {
            BinaryTreeNode tmp = this;
            while (true)
            {
                if (tmp.Value > node.Value)
                {
                    if (tmp.Left == null)
                    {
                        tmp.Left = node;
                        return;
                    }
                    else
                        tmp = tmp.Left;
                }
                else
                {
                    if (tmp.Right == null)
                    {
                        tmp.Right = node;
                        return;
                    }
                    else
                        tmp = tmp.Right;
                }
            }
        }

        public List<int> BFS()
        {
            List<int> list = new List<int>();
            Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                BinaryTreeNode tmp = queue.Dequeue();
                list.Add(tmp.Value);
                if (tmp.Left != null)
                    queue.Enqueue(tmp.Left);
                if (tmp.Right != null)
                    queue.Enqueue(tmp.Right);
            }
            return list;
        }

        public bool Search(int value)
        {
            BinaryTreeNode tmp = this;
            while (tmp != null)
            {
                if (tmp.Value == value)
                    return true;

                else if (tmp.Value < value)
                    tmp = tmp.Right;

                else
                    tmp = tmp.Left;
            }
            return false;
        }

        public string Encode()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in BFS())
            {
                sb.Append(item + ",");
            }
            return sb.ToString();
        }

        public static BinaryTreeNode Decode(string str)
        {
            string[] strs = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            BinaryTreeNode root = new BinaryTreeNode(int.Parse(strs[0]));
            for (int i = 1; i < strs.Length; i++)
            {
                root.AddToTree(new BinaryTreeNode(int.Parse(strs[i])));
            }
            return root;
        }

        public void Print()
        {
            foreach (var item in BFS())
            {
                Console.WriteLine(item);
            }
        }

        public List<int> CountSmaller(int[] nums)
        {
            int[] result = new int[nums.Length];
            Value = nums[nums.Length - 1];
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                BinaryTreeNode node = this;
                while (true)
                {
                    if (node.Value < nums[i])
                    {
                        result[i] += node.LeftCount + 1; ;

                        if (node.Right == null)
                        {
                            node.Right = new BinaryTreeNode(nums[i]);
                            break;
                        }

                        else
                            node = node.Right;
                    }
                    else
                    {
                        node.LeftCount++;
                        if (node.Left == null)
                        {
                            node.Left = new BinaryTreeNode(nums[i]);
                            break;
                        }

                        else
                            node = node.Left;
                    }
                }
            }
            return result.ToList();
        }
    }

}
