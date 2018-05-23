using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Greedy
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 摇摆序列
            //int[] arr1 = { 1, 7, 4, 9, 2, 5 };
            //int[] arr2 = { 1, 17, 5, 10, 10, 15, 10, 5, 16, 8 };
            //int[] arr3 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //Console.WriteLine(WiggleMaxLength(arr1.ToList()));
            //Console.WriteLine(WiggleMaxLength(arr2.ToList()));
            //Console.WriteLine(WiggleMaxLength(arr3.ToList()));
            #endregion

            #region RemoveKdigits
            //Console.WriteLine(RemoveKdigits("1432219",3));
            //Console.WriteLine(RemoveKdigits("100200",1));
            #endregion

            #region CanJump
            //Console.WriteLine(CanJump(new int[] { 2, 3, 1, 1, 4 }));
            //Console.WriteLine(CanJump(new int[] { 3, 0, 1, 0, 4 }));
            #endregion

            #region CanJumpPlus
            //Console.WriteLine(CanJumpPlus(new int[] { 2, 1, 1, 1, 4 }));
            //Console.WriteLine(CanJump(new int[] { 3, 0, 1, 0, 4 }));
            #endregion

            #region 射击气球
            //Console.WriteLine(MinShoters((new Point[] { new Point(13,16), new Point(2, 8), new Point(1, 6), new Point(7, 12) }).ToList()));
            #endregion

            #region 最优加油方法
            GlassStation[] gs = { new GlassStation(15, 2), new GlassStation(11, 5), new GlassStation(10, 3), new GlassStation(4, 4) };
            Console.WriteLine(MinGlass(gs,25,16));
            #endregion

            Console.ReadKey();
        }

        static int Gandy(List<int> g, List<int> s)
        {
            g.Sort();
            s.Sort();
            int child = 0;
            int cookie = 0;
            while (child < g.Count && cookie < g.Count)
            {
                if (g[child] <= s[cookie])
                {
                    child++;
                }
                cookie++;
            }
            return child;
        }

        static int WiggleMaxLength(List<int> list)
        {
            bool up = false;
            bool equal = false;
            List<int> tmp = new List<int>();
            tmp.Add(list[0]);
            if (list[1] > list[0])
            {
                up = true;
            }
            for (int i = 1; i < list.Count; i++)
            {
                equal = false;
                if (list[i] > list[i - 1])
                {
                    if (!up)
                        tmp.Add(list[i - 1]);
                    up = true;
                }
                else if (list[i] == list[i - 1])
                {
                    equal = true;
                }
                else
                {
                    if (up)
                        tmp.Add(list[i - 1]);
                    up = false;
                }
            }
            if (equal)
            {
                return tmp.Count;
            }
            return tmp.Count + 1;
        }

        static string RemoveKdigits(string s, int k)
        {
            List<int> list = new List<int>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                int b = s[i] - '0';
                while (list.Count != 0 && list[list.Count - 1] > b && k > 0)
                {
                    list.RemoveAt(list.Count - 1);
                    k--;
                }
                if (b != 0 || list.Count != 0)
                {
                    list.Add(b);
                }
            }
            while (list.Count != 0 && k > 0)
            {
                list.RemoveAt(list.Count - 1);
                k--;
            }
            for (int j = 0; j < list.Count; j++)
            {
                sb.Append(list[j].ToString());
            }
            string ss = sb.ToString();
            if (ss == "")
                return "0";
            return ss;
        }

        static bool CanJump(int[] arr)
        {
            int i = 0;
            while (i < arr.Length)
            {
                if (arr[i] == 0)
                {
                    return false;
                }
                int max = -1;
                int step = 0;
                for (int j = 1; j <= arr[i]; j++)
                {
                    if (i + j >= arr.Length - 1)
                        return true;
                    if (arr[i + j] + j >= max)
                    {
                        max = arr[i + j] + j;
                        step = i + j;
                    }
                }
                i = step;
            }
            return false;
        }

        static int CanJumpPlus(int[] arr)
        {
            int i = 0;
            int min = 0;
            while (i < arr.Length)
            {
                int max = -1;
                int step = 0;
                for (int j = 1; j <= arr[i]; j++)
                {
                    if (i + j >= arr.Length - 1)
                        return min + 1;
                    if (arr[i + j] + j >= max)
                    {
                        max = arr[i + j] + j;
                        step = i + j;
                    }
                }
                i = step;
                min++;
            }
            return min;
        }

        static int MinShoters(List<Point> list)
        {
            list.Sort();
            int n = 1;
            int x1 = list[0].X1;
            int x2 = list[0].X2;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].X1 <= x2)
                {
                    continue;
                }
                else
                {
                    x2 = list[i].X2;
                    n++;
                }
            }
            return n;
        }

        class Point : IComparable<Point>
        {
            int x1;
            int x2;

            public int X1 { get => x1; set => x1 = value; }
            public int X2 { get => x2; set => x2 = value; }


            public Point(int x1, int x2)
            {
                this.x1 = x1;
                this.x2 = x2;
            }

            public int CompareTo(Point p)
            {
                return x1 - p.X1;
            }
        }

        static int MinGlass(GlassStation[] arr, int distance, int glass)
        {
            //Queue<int> q = new Queue<int>();
            //IComparer<int> comparer = 
            SortedSet<int> set = new SortedSet<int>();
            int result = 0;
            List<GlassStation> gs = new List<GlassStation>(arr);
            gs.Add(new GlassStation(0, 0));
            for (int i = 0; i < gs.Count; i++)
            {
                int dis = distance - gs[i].Distance;
                while (set.Count != 0 && glass < dis)
                {
                    glass += set.Last();
                    set.Remove(set.Last());
                    result++;
                }
                if (set.Count == 0 && glass < dis)
                    return -1;
                glass = glass - dis;
                distance = gs[i].Distance;
                set.Add(gs[i].Glass);
            }
            return result;
        }
        class GlassStation
        {
            int d = 0;
            int g = 0;

            public GlassStation(int d, int g)
            {
                this.d = d;
                this.g = g;
            }

            public int Distance { get => d; set => d = value; }
            public int Glass { get => g; set => g = value; }
        }


    }
}

