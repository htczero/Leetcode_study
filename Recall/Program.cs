using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recall
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 求没有重复的集合的所有子集
            //List<int> list = new List<int>(new int[] { 1, 2, 3 });
            //List<List<int>> listSub = new List<List<int>>();
            //List<int> sub = new List<int>();
            //GetSubCollection(list, listSub, sub, 0);
            //listSub = GetSubCollection(list);
            //foreach (var sc in listSub)
            //{
            //    foreach (var item in sc)
            //    {
            //        Console.Write(item + "\t");
            //    }
            //    Console.WriteLine();
            //}
            #endregion

            #region 求有重复的集合的所有子集，结果中无重复的子集
            //List<int> list = new List<int>(new int[] { 2,1, 2, 2 });
            //List<List<int>> listSub = new List<List<int>>();
            //List<int> sub = new List<int>();
            ////GetSubCollection(list, listSub, sub, 0);
            //listSub = GetSubCollectionWithDup(list);
            //foreach (var sc in listSub)
            //{
            //    foreach (var item in sc)
            //    {
            //        Console.Write(item + "\t");
            //    }
            //    Console.WriteLine();
            //}
            #endregion

            #region 求有重复的集合的所有子集，结果中无重复的子集,子集元素和为整数target
            //List<int> list = new List<int>(new int[] { 10, 1, 2, 7, 6, 1, 5 });
            //List<List<int>> listSub = new List<List<int>>();
            //List<int> sub = new List<int>();
            //ListComparer ls = new ListComparer();
            //HashSet<List<int>> hs = new HashSet<List<int>>(ls);
            //list.Sort();
            //GetSubCollection(hs, sub, 0, list, 8, 0);
            //foreach (var sc in hs)
            //{
            //    foreach (var item in sc)
            //    {
            //        Console.Write(item + "\t");
            //    }
            //    Console.WriteLine();
            //}

            #endregion

            #region 生成合法的括号
            //List<string> list = new List<string>();
            //GenerateBracket("", list, 3, 3);
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region N皇后问题
            //NQueens nq = new NQueens();
            //nq.SloveNQueens(7);
            #endregion

            #region 归并排序
            //int[] arr = new int[] { 2, 6, 3, 4, 2, 9, 78, 23, 45, 1, 2, 78, 43, 4 };
            //foreach (var item in arr)
            //{
            //    Console.Write(item + "\t");
            //}
            //Console.WriteLine("\r\n========================================================");
            //MergeSort(arr);
            //foreach (var item in arr)
            //{
            //    Console.Write(item + "\t");
            //}
            #endregion

            #region 逆序数
            ReverseCount rc = new ReverseCount();
            int[] arr = new int[] { 5, -7, 9, 1, 3, 5, -2, 1 };
            foreach (var item in rc.CountSmaller(arr))
            {
                Console.Write(item + "\t");
            }
            #endregion

            Console.ReadKey();
        }

        static void GetSubCollection(List<int> list, List<List<int>> listSub, List<int> sub, int i)
        {
            if (i >= list.Count)
                return;
            sub.Add(list[i]);
            listSub.Add(new List<int>(sub));
            GetSubCollection(list, listSub, sub, i + 1);
            sub.RemoveAt(sub.Count - 1);
            GetSubCollection(list, listSub, sub, i + 1);
        }

        static List<List<int>> GetSubCollection(List<int> list)
        {
            int all = 1 << list.Count;
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < all; i++)
            {
                List<int> tmp = new List<int>();
                for (int j = 0; j < list.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                        tmp.Add(list[j]);
                }
                result.Add(tmp);
            }
            return result;
        }

        static List<List<int>> GetSubCollectionWithDup(List<int> list)
        {
            list.Sort();
            ListComparer lc = new ListComparer();
            HashSet<List<int>> hs = new HashSet<List<int>>(lc);
            List<List<int>> result = GetSubCollection(list);
            foreach (var item in result)
            {
                hs.Add(item);
            }
            result.Clear();
            foreach (var item in hs)
            {
                result.Add(item);
            }
            return result;
        }

        class ListComparer : IEqualityComparer<List<int>>
        {
            public bool Equals(List<int> x, List<int> y)
            {
                if (x.Count != y.Count)
                    return false;
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i] != y[i])
                        return false;
                }
                return true;
            }

            public int GetHashCode(List<int> obj)
            {
                int n = 0;
                for (int i = 0; i < obj.Count; i++)
                {
                    n += (i * obj[i]);
                }
                return n;
            }
        }


        static void GetSubCollection(HashSet<List<int>> hs, List<int> sub, int i, List<int> num, int target, int sum)
        {
            if (i >= num.Count || sum > target)
                return;
            sum += num[i];
            sub.Add(num[i]);
            if (sum == target)
            {
                hs.Add(new List<int>(sub));
            }

            GetSubCollection(hs, sub, i + 1, num, target, sum);
            sub.RemoveAt(sub.Count - 1);
            sum -= num[i];
            GetSubCollection(hs, sub, i + 1, num, target, sum);
        }

        static void GenerateBracket(string sb, List<string> list, int left, int right)
        {
            if (left == 0 && right == 0)
            {
                list.Add(sb);
                return;
            }
            if (left > 0)
            {
                GenerateBracket(sb + "(", list, left - 1, right);
            }

            if (left < right)
            {
                GenerateBracket(sb + ")", list, left, right - 1);
            }
        }

        class NQueens
        {
            private readonly int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
            private readonly int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };

            public NQueens()
            {

            }

            public void PutDownQueen(int x, int y, List<int[]> mark)
            {
                mark[x][y] = 1;
                for (int i = 1; i < mark.Count; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int new_x = x + i * dx[j];
                        int new_y = y + i * dy[j];
                        if (new_x >= 0 && new_y >= 0 && new_x < mark.Count && new_y < mark.Count)
                        {
                            mark[new_x][new_y] = 1;
                        }
                    }
                }
            }

            public void SloveNQueens(int n)
            {
                List<List<int[]>> result = new List<List<int[]>>(n);
                List<int[]> mark = new List<int[]>(n);
                List<int[]> location = new List<int[]>();
                for (int i = 0; i < n; i++)
                {
                    mark.Add(new int[n]);
                    location.Add(new int[n]);
                }
                Generate(0, n, location, result, mark);
                foreach (var sq in result)
                {
                    foreach (var row in sq)
                    {
                        foreach (var item in row)
                        {
                            Console.Write(item + "\t");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.WriteLine("=======================================");
                }
            }

            public void Generate(int k, int n, List<int[]> location, List<List<int[]>> result, List<int[]> mark)
            {
                if (k == n)
                {
                    result.Add(Copy(location));
                    return;
                }
                for (int i = 0; i < n; i++)
                {
                    if (mark[k][i] == 0)
                    {
                        List<int[]> tmpMark = Copy(mark);
                        location[k][i] = 1;
                        PutDownQueen(k, i, mark);
                        Generate(k + 1, n, location, result, mark);
                        mark = tmpMark;
                        location[k][i] = 0;
                    }
                }
            }

            private List<int[]> Copy(List<int[]> list)
            {
                List<int[]> copy = new List<int[]>();
                for (int i = 0; i < list.Count; i++)
                {
                    copy.Add(new int[list[0].Length]);
                    for (int j = 0; j < list[0].Length; j++)
                    {
                        copy[i][j] = list[i][j];
                    }
                }
                return copy;
            }


        }


        static void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
        }

        static void MergeSort(int[] arr)
        {
            int right = arr.Length - 1;
            MergeSort(arr, 0, right);
        }

        static void Merge(int[] arr, int left, int mid, int right)
        {
            List<int> list = new List<int>(right - left + 1);
            int i = left, j = mid + 1;
            while (i <= mid && j <= right)
            {
                if (arr[i] < arr[j])
                    list.Add(arr[i++]);
                else
                    list.Add(arr[j++]);
            }
            while (i <= mid)
                list.Add(arr[i++]);
            while (j <= right)
                list.Add(arr[j++]);
            int k = 0;
            while (k < list.Count)
            {
                arr[k + left] = list[k];
                k++;
            }
        }

        class ReverseCount
        {
            class Pair
            {
                int x = 0;
                int y = 0;
                public Pair(int x, int y)
                {
                    X = x;
                    Y = y;
                }

                public int X { get => x; set => x = value; }
                public int Y { get => y; set => y = value; }
            }

            public List<int> CountSmaller(int[] arr)
            {
                List<Pair> pairs = new List<Pair>(arr.Length);
                List<int> count = new List<int>(arr.Length);
                for (int i = 0; i < arr.Length; i++)
                {
                    pairs.Add(new Pair(arr[i], i));
                    count.Add(0);
                }
                MergeSort(pairs, count, 0, arr.Length - 1);
                return count;
            }

            private void MergeSort(List<Pair> pairs, List<int> count, int left, int right)
            {
                if (left < right)
                {
                    int mid = (left + right) / 2;
                    MergeSort(pairs, count, left, mid);
                    MergeSort(pairs, count, mid + 1, right);
                    MergeTwoArray(pairs, count, left, mid, right);
                }
            }

            private void MergeTwoArray(List<Pair> list, List<int> count, int left, int mid, int right)
            {
                int i = left;
                int k = mid + 1;
                int j = mid + 1;
                List<Pair> tmp = new List<Pair>(right - left + 1);
                while (i <= mid && j <= right)
                {
                    if (list[i].X < list[j].X)
                    {
                        tmp.Add(list[i]);
                        i++;
                    }
                    else
                    {
                        tmp.Add(list[j]);
                        count[list[i].Y]++;
                        j++;
                    }
                }
                if (i <= mid)
                {
                    tmp.Add(list[i++]);
                }
                while (i <= mid)
                {
                    tmp.Add(list[i]);
                    count[list[i].Y] += (right - mid);
                    i++;
                }
                while (j <= right)
                {
                    tmp.Add(list[j]);
                    j++;
                }
                k = 0;
                while (k < tmp.Count)
                {
                    list[k + left] = tmp[k];
                    k++;
                }
            }


        }
    }
}
