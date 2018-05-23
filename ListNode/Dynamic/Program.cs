using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 爬楼梯
            //Console.WriteLine(ClimbStaris2(1));
            //Console.WriteLine(ClimbStaris2(2));
            //Console.WriteLine(ClimbStaris2(3));
            //Console.WriteLine(ClimbStaris2(4));
            //Console.WriteLine(ClimbStaris2(5));
            #endregion

            #region 打家劫舍
            //Console.WriteLine(Rob(new int[] { 5,3, 2, 6,9, 1, 2,3,7 }));
            #endregion

            #region 最大子段和
            //Console.WriteLine(MaxSubArray(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }));
            #endregion

            #region 找零钱
            //Console.WriteLine(CoinChange(new int[] { 2,10 },10));
            #endregion

            #region 三角形 *
            //List<int[]> list = new List<int[]>();
            //list.Add(new int[] { 2 });
            //list.Add(new int[] { 3, 4 });
            //list.Add(new int[] { 6, 5, 7 });
            //list.Add(new int[] { 4, 1, 8, 3 });
            //Console.WriteLine(MinimumTotal(list));
            #endregion

            #region 最长上升子序列 *
            //Console.WriteLine(GetLengthOfLis2(new int[] { 1, 3, 4, 2, 3, 1, 4 }));
            #endregion

            #region 最小路径和 *
            //int[,] map = { { 1, 3, 1 }, { 1, 5, 1 }, { 4, 2, 1 } };
            //Console.WriteLine(MinPathSum(map));
            #endregion

            #region 地牢游戏 *
            //Console.WriteLine(CalMinHp(new int[,] { { -2, -3, 3 }, { -5, -10, 1 }, { 10, 30, -5 } }));
            #endregion


            Console.ReadKey();
        }

        #region 爬楼梯
        static int ClimbStaris(int n)
        {
            if (n == 1)
                return 1;
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            q.Enqueue(2);
            int count = 0;
            while (q.Count != 0)
            {
                int next = q.Dequeue();
                if (next == n)
                    count++;

                else
                {
                    if (++next <= n)
                        q.Enqueue(next);
                    if (++next <= n)
                        q.Enqueue(next);
                }

            }
            return count;
        }

        static int ClimbStaris2(int n)
        {
            if (n == 1)
                return 1;
            else if (n == 2)
                return 2;

            int count = 0;
            int an = 2;
            int a = 1;
            while (count < n - 2)
            {
                int tmp = an;
                an += a;
                a = tmp;
                count++;
            }
            return an;
        }
        #endregion

        #region 打家劫舍
        static int Rob(int[] nums)
        {
            if (nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            List<int> list = new List<int>();
            list.Add(nums[0]);
            list.Add(Math.Max(nums[0], nums[1]));
            for (int i = 2; i < nums.Length; i++)
            {
                list.Add(Math.Max(list[i - 1], list[i - 2] + nums[i]));
            }
            return list[list.Count - 1];
        }
        #endregion

        #region 最大子段和
        static int MaxSubArray(int[] arr)
        {
            if (arr.Length == 0)
                return 0;

            if (arr.Length == 1)
                return arr[0];

            List<int> list = new List<int>();
            list.Add(arr[0]);
            for (int i = 1; i < arr.Length; i++)
            {
                list.Add(list[i - 1] > 0 ? list[i - 1] + arr[i] : arr[i]);
            }
            return list.Max();
        }
        #endregion

        #region 找零钱
        static int CoinChange(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
            for (int i = 0; i < amount + 1; i++)
                dp[i] = -1;

            dp[0] = 0;
            for (int i = 1; i < amount + 1; i++)
                for (int j = 0; j < coins.Length; j++)
                    if (i - coins[j] >= 0 && dp[i - coins[j]] != -1)
                        if (dp[i] == -1 || dp[i] > dp[i - coins[j]] + 1)
                            dp[i] = dp[i - coins[j]] + 1;

            return dp[amount];

        }
        #endregion

        #region 三角形
        static int MinimumTotal(List<int[]> triangle)
        {
            if (triangle.Count == 0)
                return 0;

            List<int[]> list = new List<int[]>(triangle.Count);

            for (int i = 0; i < triangle.Count; i++)
                list.Add(new int[triangle[i].Length]);

            for (int i = 0; i < triangle[triangle.Count - 1].Length; i++)
                list[list.Count - 1][i] = triangle[list.Count - 1][i];

            for (int i = list.Count - 2; i >= 0; i--)
                for (int j = 0; j < list[i].Length; j++)
                    list[i][j] = Math.Min(list[i + 1][j], list[i + 1][j + 1]) + triangle[i][j];

            return list[0][0];

        }
        #endregion

        #region 最长上升子序列
        static int GetLengthOfLis(int[] nums)
        {
            if (nums.Length <= 1)
                return nums.Length;
            int[] dp = new int[nums.Length];
            int max = 1;
            dp[0] = 1;
            for (int i = 1; i < dp.Length; i++)
            {
                dp[i] = 1;
                for (int j = i - 1; j >= 0; j--)
                    if (nums[i] > nums[j] && dp[i] < dp[j] + 1)
                        dp[i] = dp[j] + 1;

                max = max > dp[i] ? max : dp[i];
            }
            return max;
        }
        static int GetLengthOfLis2(int[] nums)
        {
            if (nums.Length <= 1)
                return nums.Length;
            List<int> list = new List<int>();
            list.Add(nums[0]);
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > list.Last())
                    list.Add(nums[i]);
                else
                {
                    int index = list.BinarySearch(nums[i], new Tmp());
                    list[index] = nums[i];
                }
            }


            return list.Count;
        }

        class Tmp : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                int t = x - y;
                if (t >= 0)
                    return 0;
                return -1;
            }
        }
        #endregion

        #region 最小路径和
        static int MinPathSum(int[,] map)
        {
            int row = map.GetLength(0);
            int col = map.GetLength(1);
            if (row == 0)
                return 0;

            int[,] dp = new int[row, col];
            dp[0, 0] = map[0, 0];

            for (int i = 1; i < col; i++)
                dp[0, i] = dp[0, i - 1] + map[0, i];

            for (int i = 1; i < row; i++)
            {
                dp[i, 0] = dp[i - 1, 0] + map[i, 0];
                for (int j = 1; j < col; j++)
                {
                    dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + map[i, j];
                }
            }
            return dp[row - 1, col - 1];
        }
        #endregion

        #region 地牢游戏
        static int CalMinHp(int[,] map)
        {
            int row = map.GetLength(0);
            int col = map.GetLength(1);
            if (row == 0)
                return 0;

            int[,] dp = new int[row, col];
            dp[row - 1, col - 1] = Math.Max(1, 1 - map[row - 1, col - 1]);
            for (int i = col - 2; i >= 0; i--)
            {
                dp[row - 1, i] = Math.Max(1, dp[row - 1, i + 1] - map[row - 1, i]);
            }
            for (int i = row - 2; i >= 0; i--)
            {
                dp[i, col - 1] = Math.Max(1, dp[i + 1, col - 1] - map[i, col - 1]);
                for (int j = col - 2; j >= 0; j--)
                {
                    int dp_min = Math.Min(dp[i + 1, j], dp[i, j + 1]);
                    dp[i, j] = Math.Max(1, dp_min - map[i, j]);
                }
            }
            return dp[0, 0];
        }
        #endregion
    }
}
