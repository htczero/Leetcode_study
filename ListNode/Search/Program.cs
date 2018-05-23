using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 岛屿数量
            //List<int[]> map = new List<int[]>(4);
            //map.Add(new int[] { 0, 1, 1, 0, 1 });
            //map.Add(new int[] { 1, 1, 0, 1, 0 });
            //map.Add(new int[] { 1, 1, 1, 1, 0 });
            //map.Add(new int[] { 0, 0, 0, 0, 1 });
            //Console.WriteLine(GetIslandCount(map));
            #endregion

            #region 词语阶梯
            //string[] wordList = { "hot", "dot", "dog", "lot", "log", "cog" };
            //Console.WriteLine(WordLadder("hit", "dot", wordList));
            #endregion

            #region 词语阶梯2-最短路径
            //string[] wordList = { "hot", "dot", "dog", "lot", "log", "cog" };
            //foreach (var item in ShortestWordLadder("hit", "log", wordList))
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();
            #endregion

            #region 火柴棍摆正方形
            //int[] nums = { 1, 1, 2, 2, 2 };
            //Console.WriteLine(MakeSquare(nums));
            //nums = new int[] { 3, 3, 4, 4, 4 };
            //Console.WriteLine(MakeSquare(nums));
            //nums = new int[] { 1, 1, 2, 4, 3, 2, 3 };
            //Console.WriteLine(MakeSquare(nums));
            //nums = new int[] { 1, 3, 3, 1 };
            //Console.WriteLine(MakeSquare(nums));
            #endregion

            #region 收集雨水
            //int[,] arr = { { 1, 4, 3, 1, 3, 2 }, { 3, 1, 2, 1, 2, 4 }, { 2, 3, 3, 3, 3, 1 } };;
            //Console.WriteLine(TrapRainWater(arr));
            #endregion

            Console.ReadKey();
        }

        #region 岛屿数量
        static int GetIslandCount(List<int[]> map)
        {
            int count = 0;
            int row = map.Count;
            int cow = map[0].Length;
            int[,] flag = new int[row, cow];
            Queue<MapPoint> q = new Queue<MapPoint>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cow; j++)
                {
                    if (map[i][j] == 1 && flag[i, j] == 0)
                    {
                        SearchBoundary(new MapPoint(i, j), flag, map);
                        count++;
                    }
                    flag[i, j] = 1;
                }
            }

            return count;
        }

        static void SearchBoundary(MapPoint point, int[,] flag, List<int[]> map)
        {
            Queue<MapPoint> q = new Queue<MapPoint>();
            q.Enqueue(point);
            flag[point.X, point.Y] = 1;
            while (q.Count != 0)
            {
                MapPoint mp = q.Dequeue();
                foreach (var item in mp.Directory)
                {
                    int x = mp.X + item.X;
                    int y = mp.Y + item.Y;

                    if (x < 0 || x >= map.Count || y < 0 || y >= map[0].Length)
                        continue;

                    if (flag[x, y] == 0 && map[x][y] == 1)
                    {
                        q.Enqueue(new MapPoint(x, y));
                        flag[x, y] = 1;
                    }
                }
            }
        }

        class MapPoint
        {
            Point position = new Point(0, 0);
            readonly Point[] directory = { new Point(-1, 0), new Point(0, -1), new Point(1, 0), new Point(0, 1) };
            int currentDirectory = 0;

            public int X { get => position.X; }

            public int Y { get => position.Y; }
            public Point[] Directory => directory;

            public int CurrentDirectory { get => currentDirectory; set => currentDirectory = value; }

            public MapPoint(int x, int y)
            {
                position.X = x;
                position.Y = y;
            }
        }

        class Point
        {
            int x = 0;
            int y = 0;

            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        #endregion

        #region 词语阶梯
        static int WordLadder(string start, string end, string[] wordList)
        {
            Queue<string> wordQueue = new Queue<string>();
            Queue<int> stepQueue = new Queue<int>();
            int[] flag = new int[wordList.Length];
            wordQueue.Enqueue(start);
            stepQueue.Enqueue(1);
            while (stepQueue.Count != 0)
            {
                int step = stepQueue.Dequeue();
                string pre = wordQueue.Dequeue();
                for (int i = 0; i < wordList.Length; i++)
                {
                    if (flag[i] == 0 && CanConvert(pre, wordList[i]))
                    {
                        if (wordList[i] == end)
                            return step + 1;
                        flag[i] = 1;
                        wordQueue.Enqueue(wordList[i]);
                        stepQueue.Enqueue(step + 1);
                    }
                }
            }
            return 0;
        }

        static bool CanConvert(string str1, string str2)
        {
            int count = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                    count++;
            }
            return count == 1;
        }
        #endregion

        #region 词语阶梯2-最短路径
        static List<string> ShortestWordLadder(string start, string end, string[] wordList)
        {
            Queue<string> wordQueue = new Queue<string>();
            Queue<int> stepQueue = new Queue<int>();
            Queue<List<int>> pathQueue = new Queue<List<int>>();
            List<List<int>> pathList = new List<List<int>>();
            pathQueue.Enqueue(new List<int>());
            wordQueue.Enqueue(start);
            stepQueue.Enqueue(1);
            int min = int.MaxValue;
            while (stepQueue.Count != 0)
            {
                int step = stepQueue.Dequeue();
                string pre = wordQueue.Dequeue();
                List<int> path = pathQueue.Dequeue();
                for (int i = 0; i < wordList.Length; i++)
                {
                    if (!path.Contains(i) && CanConvert(pre, wordList[i]))
                    {
                        List<int> newPath = new List<int>(path);
                        newPath.Add(i);
                        if (wordList[i] == end)
                        {
                            if (min > step + 1)
                            {
                                min = step + 1;
                                pathList.Clear();
                                pathList.Add(newPath);
                            }
                            else if (min == step + 1)
                            {
                                pathList.Add(newPath);
                            }
                        }
                        pathQueue.Enqueue(newPath);
                        wordQueue.Enqueue(wordList[i]);
                        stepQueue.Enqueue(step + 1);
                    }
                }
            }
            List<string> minPath = new List<string>();
            foreach (var path in pathList)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[" + start);
                foreach (var item in path)
                {
                    sb.Append("," + wordList[item]);
                }
                sb.Append("]");
                minPath.Add(sb.ToString());
            }
            return minPath;
        }
        #endregion

        #region 火柴棍摆正方形
        static bool MakeSquare(int[] arr)
        {
            int sum = arr.Sum();
            if (sum % 4 != 0 || arr.Length < 4)
                return false;

            int length = sum / 4;
            List<int> list = new List<int>(arr);
            list.Sort();

            if (list[list.Count - 1] > length)
                return false;

            int[] flag = new int[arr.Length];
            int count = 0;
            int t = 0;
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int border = list[i];
                if (border == length)
                {
                    count++;
                    continue;
                }
                int j = t;
                for (; j < i; j++)
                {
                    border += list[j];
                    if (border > length)
                        return false;

                    if (border == length)
                    {
                        count++;
                        t = j + 1;
                        break;
                    }
                }
            }
            return count == 4;
        }
        #endregion

        #region 收集雨水
        class Cube : IComparable<Cube>
        {
            int x = 0;
            int y = 0;
            int high = 0;

            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }
            public int High { get => high; set => high = value; }

            public Cube(int x, int y, int h)
            {
                X = x;
                Y = y;
                High = h;
            }

            public int CompareTo(Cube other)
            {
                int value = High - other.High;
                if (value > 0)
                    return 1;
                return -1;
            }
        }
        class PriorityQueue<T>
        {
            private SortedList<T, T> my = new SortedList<T, T>();

            public void Enqueue(T x)
            {
                my.Add(x, x);
            }

            public T Dequeue()
            {
                if (my.Count == 0)
                    return default(T);
                T value = my.Keys[0];
                my.RemoveAt(0);
                return value;
            }

            public int Count { get => my.Count; }
        }

        static int TrapRainWater(int[,] map)
        {
            int row = map.GetLength(0);
            int col = map.GetLength(1);
            if (row < 3 || col < 3)
                return 0;

            PriorityQueue<Cube> q = new PriorityQueue<Cube>();
            int[,] mark = new int[row, col];

            for (int i = 0; i < row; i++)
            {
                q.Enqueue(new Cube(i, 0, map[i, 0]));
                mark[i, 0] = 1;
                q.Enqueue(new Cube(i, col - 1, map[i, col - 1]));
                mark[i, col - 1] = 0;
            }
            for (int i = 1; i < col - 1; i++)
            {
                q.Enqueue(new Cube(0, i, map[0, i]));
                mark[0, i] = 1;
                q.Enqueue(new Cube(row - 1, i, map[row - 1, i]));
                mark[row - 1, i] = 0;
            }
            int[] dx = new int[4] { -1, 1, 0, 0 };
            int[] dy = new int[4] { 0, 0, -1, 1 };

            int water = 0;
            while (q.Count != 0)
            {
                Cube cube = q.Dequeue();
                int x = cube.X;
                int y = cube.Y;
                int h = cube.High;
                for (int i = 0; i < 4; i++)
                {
                    int newx = x + dx[i];
                    int newy = y + dy[i];
                    if (newx < 0 || newx >= row || newy < 0 || newy >= col || mark[newx, newy] == 1)
                        continue;

                    if (h > map[newx, newy])
                    {
                        water += h - map[newx, newy];
                        map[newx, newy] = h;
                    }
                    q.Enqueue(new Cube(newx, newy, map[newx, newy]));
                    mark[newx, newy] = 1;
                }
            }
            return water;
        }
        #endregion

    }


}

