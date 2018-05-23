using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree a = new BTree(5);
            BTree b = new BTree(4);
            BTree c = new BTree(8);
            BTree d = new BTree(11);
            BTree e = new BTree(13);
            BTree f = new BTree(4);
            BTree g = new BTree(7);
            BTree h = new BTree(2);
            BTree x = new BTree(5);
            BTree y = new BTree(1);
            a.Left = b;
            a.Right = c;
            b.Left = d;
            c.Left = e;
            c.Right = f;
            d.Left = g;
            d.Right = h;
            f.Left = x;
            f.Right = y;

            #region 路径之和
            //foreach (var item in a.GetPathSum(a))
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region 最近的公共祖先
            //Console.WriteLine(a.LowestCommonAncestor(a, e, f).Value);
            #endregion

            #region 二叉树转链表
            //a.ConvertToLinkedList(a);
            //while (a != null)
            //{
            //    Console.WriteLine(a.Value);
            //    a = a.Right;
            //}
            #endregion

            #region 二叉树层遍历
            //a.BFS(a);
            #endregion

            #region 侧面观察二叉树
            //foreach (var item in a.LeftSideView())
            //{
            //    Console.WriteLine((item.Value));
            //}
            //foreach (var item in a.RightSideView())
            //{
            //    Console.WriteLine((item.Value));
            //}
            #endregion

            Console.ReadKey();
        }
    }

    class BTree
    {
        int value = 0;
        BTree left = null;
        BTree right = null;
        public BTree(int x)
        {
            Value = x;
        }

        public int Value { get => value; set => this.value = value; }
        internal BTree Left { get => left; set => left = value; }
        internal BTree Right { get => right; set => right = value; }

        public List<int> GetPathSum(BTree node)
        {
            List<int> sumList = new List<int>();
            PathSum(node, sumList, 0);
            return sumList;
        }

        private void PathSum(BTree node, List<int> sumList, int sum)
        {
            if (node == null)
                return;
            if (node.Left == null && node.Right == null)
            {
                sumList.Add(sum + node.value);
                return;
            }
            sum += node.value;
            PathSum(node.left, sumList, sum);
            PathSum(node.right, sumList, sum);
        }

        public BTree LowestCommonAncestor(BTree root, BTree p, BTree q)
        {
            List<BTree> pathListQ = new List<BTree>();
            List<BTree> pathListP = new List<BTree>();
            bool[] b = { false };
            SearchPath(root, p, pathListP, b);
            b[0] = false;
            SearchPath(root, q, pathListQ, b);
            int i = 0;
            while (pathListP[i] == pathListQ[i])
            {
                i++;
            }
            return pathListQ[i - 1];
        }

        private void SearchPath(BTree root, BTree node, List<BTree> pathList, bool[] b)
        {
            if (root == node)
            {
                pathList.Add(root);
                b[0] = true;
                return;
            }
            if (root != null && b[0] == false)
            {
                pathList.Add(root);
                SearchPath(root.Left, node, pathList, b);
                SearchPath(root.right, node, pathList, b);
                if (!b[0])
                    pathList.RemoveAt(pathList.Count - 1);
            }

        }

        public void ConvertToLinkedList()
        {
            List<BTree> list = new List<BTree>();
            ConvertToLinkedList(this, list);
            int i = 0;
            while (i + 1 < list.Count)
            {
                list[i].Left = null;
                list[i].Right = list[i + 1];
                i++;
            }
        }

        private void ConvertToLinkedList(BTree node, List<BTree> list)
        {
            if (node != null)
            {
                list.Add(node);
                ConvertToLinkedList(node.Left, list);
                ConvertToLinkedList(node.Right, list);
            }
        }


        public void ConvertToLinkedList(BTree node)
        {
            BTree last = new BTree(0);
            ConvertToLinkedList(node, last);

        }
        public void ConvertToLinkedList(BTree node, BTree last)
        {
            if (node == null)
            {
                return;
            }
            if (node.Left == null && node.Right == null)
            {
                return;
            }
            BTree left = node.Copy(node.Left);
            BTree right = node.Copy(node.Right);
            node.Right = left;
            last = left;
            node.Left = null;
            ConvertToLinkedList(left, last);
            ConvertToLinkedList(right, last);
            if (right != null)
            {
                last.Right = right;
                last = right;
            }

        }


        public BTree Copy(BTree bt)
        {
            if (bt == null)
                return null;
            BTree node = new BTree(bt.value);
            node.Left = bt.Left;
            node.Right = bt.Right;
            return node;
        }

        public void BFS(BTree node)
        {
            Queue<BTree> queue = new Queue<BTree>();
            queue.Enqueue(node);
            while (queue.Count != 0)
            {
                BTree tmp = queue.Dequeue();
                Console.WriteLine(tmp.Value);
                if (tmp.Left != null)
                    queue.Enqueue(tmp.Left);
                if (tmp.Right != null)
                    queue.Enqueue(tmp.Right);
            }
        }

        public List<BTree> RightSideView()
        {
            List<BTree> result = new List<BTree>();
            Queue<BTree> nodeQueue = new Queue<BTree>();
            Queue<int> deepQueue = new Queue<int>();
            nodeQueue.Enqueue(this);
            deepQueue.Enqueue(0);
            int currentDeep = 0;
            BTree preNode = this;
            while (deepQueue.Count != 0)
            {
                BTree tmpNode = nodeQueue.Dequeue();
                int nodeDeep = deepQueue.Dequeue();
                if (currentDeep != nodeDeep)
                {
                    result.Add(preNode);
                    currentDeep = nodeDeep;
                }
                if (tmpNode.Left != null)
                {
                    nodeQueue.Enqueue(tmpNode.Left);
                    deepQueue.Enqueue(nodeDeep + 1);
                }
                if (tmpNode.Right != null)
                {
                    nodeQueue.Enqueue(tmpNode.Right);
                    deepQueue.Enqueue(nodeDeep + 1);
                }
                preNode = tmpNode;
            }
            result.Add(preNode);
            return result;
        }

        public List<BTree> LeftSideView()
        {
            List<BTree> result = new List<BTree>();
            Queue<BTree> nodeQueue = new Queue<BTree>();
            Queue<int> deepQueue = new Queue<int>();
            nodeQueue.Enqueue(this);
            deepQueue.Enqueue(0);
            int nextDeep = 0;           
            while (deepQueue.Count != 0)
            {
                BTree tmpNode = nodeQueue.Dequeue();
                int nodeDeep = deepQueue.Dequeue();
                if (nextDeep == nodeDeep)
                {
                    result.Add(tmpNode);
                    nextDeep++;
                }
                if (tmpNode.Left != null)
                {
                    nodeQueue.Enqueue(tmpNode.Left);
                    deepQueue.Enqueue(nodeDeep + 1);
                }
                if (tmpNode.Right != null)
                {
                    nodeQueue.Enqueue(tmpNode.Right);
                    deepQueue.Enqueue(nodeDeep + 1);
                }
            }
            return result;
        }




    }
}
