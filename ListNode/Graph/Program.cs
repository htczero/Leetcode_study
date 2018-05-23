using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 图的邻接表的构造
            GraphNode[] nodes = new GraphNode[5];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new GraphNode(i);
            }
            nodes[0].AddToNeighbos(nodes[2], nodes[4]);
            nodes[1].AddToNeighbos(nodes[0], nodes[2]);
            nodes[2].AddToNeighbos(nodes[3]);
            nodes[3].AddToNeighbos(nodes[4]);
            nodes[4].AddToNeighbos(nodes[1]);
            GraphLinkedList gl = new GraphLinkedList(nodes);

            #endregion

            #region 图的邻接矩阵的构造
            GraphMatrix gm = new GraphMatrix(5);
            gm.AddPath(0, 2);
            gm.AddPath(0, 4);
            gm.AddPath(1, 0);
            gm.AddPath(1, 2);
            gm.AddPath(2, 3);
            gm.AddPath(3, 4);
            gm.AddPath(4, 3);
            #endregion

            #region 深度优先遍历图（邻接表）
            //foreach (var item in gl.DFS())
            //{
            //    Console.WriteLine(item.Value);
            //}
            #endregion

            #region 宽度优先遍历图（邻接表）
            //foreach (var item in gl.BFS())
            //{
            //    Console.WriteLine(item.Value);
            //}
            #endregion

            #region 判断是否有环
            //Console.WriteLine(gl.HaveCircleDFS());
            //Console.WriteLine(gl.HaveCircleBFS());
            #endregion


            Console.ReadKey();
        }
    }

    class GraphMatrix
    {
        private int n = 0;
        private List<int[]> matrix = new List<int[]>();

        public int N { get => n; set => n = value; }
        public List<int[]> Matrix { get => matrix; set => matrix = value; }

        public GraphMatrix(int n)
        {
            N = n;
            for (int i = 0; i < n; i++)
            {
                matrix.Add(new int[n]);
            }
        }

        public void AddPath(int i, int j)
        {
            if (i < n && j < n)
            {
                Matrix[i][j] = 1;
            }
        }
    }

    class GraphNode
    {
        private int value = 0;
        private List<GraphNode> neighbors = new List<GraphNode>();

        public int Value { get => value; set => this.value = value; }
        internal List<GraphNode> Neighbors { get => neighbors; set => neighbors = value; }


        public GraphNode(int x)
        {
            Value = x;
        }

        public GraphNode(int x, params GraphNode[] nodes)
        {
            Value = x;
            foreach (var item in nodes)
            {
                Neighbors.Add(item);
            }
        }

        public void AddToNeighbos(params GraphNode[] nodes)
        {
            foreach (var item in nodes)
            {
                Neighbors.Add(item);
            }
        }
    }

    class GraphLinkedList
    {
        private List<GraphNode> graphList = new List<GraphNode>();
        internal List<GraphNode> GraphList { get => graphList; set => graphList = value; }

        public int Count { get => GraphList.Count; }

        public GraphLinkedList(GraphNode[] gm)
        {
            GraphList.AddRange(gm);
        }

        public GraphLinkedList()
        {

        }

        public List<GraphNode> DFS()
        {
            int[] visit = new int[Count];
            List<GraphNode> result = new List<GraphNode>();
            int i = 0;
            while (result.Count != Count && i < Count)
            {
                DFS(GraphList[i++], visit, result);
            }
            return result;
        }

        private void DFS(GraphNode gn, int[] visit, List<GraphNode> result)
        {
            if (visit[gn.Value] != 1)
            {
                visit[gn.Value] = 1;
                result.Add(gn);
                foreach (var item in gn.Neighbors)
                {
                    DFS(item, visit, result);
                }
            }
        }

        public List<GraphNode> BFS()
        {
            int[] visit = new int[Count];
            List<GraphNode> result = new List<GraphNode>();
            int i = 0;
            Queue<GraphNode> q = new Queue<GraphNode>();
            while (result.Count != Count && i < Count)
            {
                q.Enqueue(GraphList[i++]);
                while (q.Count != 0)
                {
                    GraphNode gn = q.Dequeue();
                    if (visit[gn.Value] != 1)
                    {
                        foreach (var item in gn.Neighbors)
                        {
                            q.Enqueue(item);
                        }
                        result.Add(gn);
                        visit[gn.Value] = 1;
                    }
                }
            }
            return result;
        }

        public bool HaveCircleDFS()
        {
            for (int i = 0; i < Count; i++)
            {
                int[] visit = new int[Count];
                if (HaveCircle(GraphList[i], visit))
                    return true;
            }
            return false;
        }

        private bool HaveCircle(GraphNode gn, int[] visit)
        {
            foreach (var item in gn.Neighbors)
            {
                if (visit[item.Value] == 1)
                    return true;
                visit[gn.Value] = 1;
                return HaveCircle(item, visit);
            }
            return false;
        }

        public bool HaveCircleBFS()
        {
            int[] inDegree = new int[Count];
            foreach (var item in GraphList)
            {
                foreach (var node in item.Neighbors)
                {
                    inDegree[node.Value]++;
                }
            }

            Queue<GraphNode> q = new Queue<GraphNode>();
            for (int i = 0; i < inDegree.Length; i++)
            {
                if (inDegree[i] == 0)
                    q.Enqueue(GraphList[i]);
            }

            while (q.Count != 0)
            {
                foreach (var item in q.Dequeue().Neighbors)
                {
                    inDegree[item.Value]--;
                    if (inDegree[item.Value] == 0)
                    {
                        q.Enqueue(item);
                    }
                }
            }
            if (inDegree.Sum() != 0)
                return true;
            return false;
        }

    }

}
