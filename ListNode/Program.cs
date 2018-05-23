using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random(50);
            List<ListNode> list = new List<ListNode>();
            for (int i = 0; i < 6; i++)
            {
                list.Add(new ListNode(r.Next(0, 50)));
            }
            for (int i = 0; i < 5; i++)
            {
                list[i].Next = list[i + 1];
            }
            ListNode.Print(list[0]);
            Console.WriteLine("===================================");

            #region reverse
            //ListNode head = ListNode.Reverse(list[0], 0, 5);
            //ListNode.Print(head);
            //Console.WriteLine("===================================");
            #endregion

            #region intersection
            //ListNode headA = new ListNode(110);
            //ListNode headB = new ListNode(220);
            //headB.Next = new ListNode(330);
            //headB.Next.Next = list[0];
            //headA.Next = list[1];
            //ListNode section = ListNode.GetIntersectionNode(headA, headB);
            //ListNode.Print(section);
            #endregion

            #region DetectCycle
            //ListNode head = list[0];
            //list[5].Next = list[2];
            //ListNode star = ListNode.DetectCycle(head);

            //if (star == null)
            //{
            //    Console.WriteLine("null");
            //}
            //else
            //{
            //    Console.WriteLine(star.Value);
            //}
            #endregion

            #region divide
            //ListNode.Print(ListNode.Partition(list[0], 25));
            #endregion

            #region copy
            //list[2].Random = list[5];
            //list[4].Random = list[0];
            //list[5].Random = list[0];

            //ListNode newCopy = ListNode.Copy(list[0]);
            //Console.WriteLine("===============================");
            //newCopy.Random = newCopy.Next.Next;
            //ListNode.PrintRandomList(list[0]);

            //Console.WriteLine("===============================");
            //ListNode.PrintRandomList(newCopy);
            #endregion

            #region merge
            //List<ListNode> list1 = new List<ListNode>();
            //List<ListNode> list2 = new List<ListNode>();
            //for (int i = 0; i < 6; i++)
            //{
            //    if (i % 2 == 0)
            //        list1.Add(new ListNode(i));
            //    else
            //        list2.Add(new ListNode(i));
            //}
            //for (int i = 0; i < list1.Count-1; i++)
            //{
            //    list1[i].Next = list1[i + 1];
            //    list2[i].Next = list2[i + 1];
            //}
            //list2[list2.Count - 1].Next = new ListNode(7);
            //ListNode.Print(list1[0]);
            //Console.WriteLine("=====================================================");
            //ListNode.Print(list2[0]);
            //Console.WriteLine("=====================================================");
            //ListNode.Print(ListNode.Merge(list1[0], list2[0]));
            #endregion

            #region multiMerge
            //List<ListNode> list1 = new List<ListNode>();
            //List<ListNode> list2 = new List<ListNode>();
            //List<ListNode> list3 = new List<ListNode>();
            //for (int i = 0; i < 12; i++)
            //{
            //    if (i % 3 == 0)
            //        list1.Add(new ListNode(i));
            //    else if (i % 3 == 1)
            //        list2.Add(new ListNode(i));
            //    else if (i % 3 == 2)
            //        list3.Add(new ListNode(i));
            //}
            //for (int i = 0; i < list1.Count - 1; i++)
            //{
            //    list1[i].Next = list1[i + 1];
            //    list2[i].Next = list2[i + 1];
            //    list3[i].Next = list3[i + 1];
            //}
            //ListNode.Print(list1[0]);
            //Console.WriteLine("=====================================================");
            //ListNode.Print(list2[0]);
            //Console.WriteLine("=====================================================");
            //List<ListNode> merge = new List<ListNode>();
            //merge.Add(list1[0]);
            //merge.Add(list2[0]);
            //merge.Add(list3[0]);
            //ListNode.Print(ListNode.Merge(merge));
            #endregion

            Console.ReadKey();

        }

        class ListNode
        {
            private int _value;
            private ListNode _next = null;
            private ListNode _random = null;

            public int Value { get => _value; set => _value = value; }
            public ListNode Next { get => _next; set => _next = value; }
            public ListNode Random { get => _random; set => _random = value; }

            public ListNode(int value)
            {
                Value = value;
            }

            public static ListNode Reverse(ListNode head)
            {
                ListNode preNode = null;
                while (head != null)
                {
                    ListNode tmp = head.Next;
                    head.Next = preNode;
                    preNode = head;
                    head = tmp;
                }
                return preNode;
            }

            public static void Print(ListNode head)
            {
                while (head != null)
                {
                    Console.WriteLine(head.Value + "\t");
                    head = head.Next;
                }
            }

            public static ListNode Reverse(ListNode head, int m, int n)
            {

                ListNode tmpHead = null;
                ListNode tmpRear = null;
                ListNode newHead = head;
                n = n - m + 1;
                while (m > 0 && head != null)
                {
                    tmpHead = head;
                    head = head.Next;
                    m--;
                }
                tmpRear = head;
                ListNode preNode = null;
                while (n > 0 && head != null)
                {
                    ListNode tmp = head.Next;
                    head.Next = preNode;
                    preNode = head;
                    head = tmp;
                    n--;
                }
                tmpRear.Next = head;
                if (tmpHead == null)
                {
                    return preNode;
                }
                tmpHead.Next = preNode;
                return newHead;
            }

            public static ListNode GetIntersectionNode(ListNode headA, ListNode headB)
            {
                //List<ListNode> listA = headA.ToList();
                //List<ListNode> listB = headB.ToList();
                //if (listA.Count < listB.Count)
                //{
                //    List<ListNode> tmp = listA;
                //    listA = listB;
                //    listB = tmp;
                //}
                //int n = listA.Count - listB.Count;
                //for (int i = 0; i < listB.Count - 1; i++)
                //{
                //    if (listA[i + n] == listB[i])
                //    {
                //        return listB[i];
                //    }
                //}
                int lengthA = GetLength(headA);
                int lengthB = GetLength(headB);
                if (lengthA > lengthB)
                {
                    int delta = lengthA - lengthB;
                    while (delta > 0)
                    {
                        headA = headA.Next;
                        delta--;
                    }
                }
                else
                {
                    int delta = lengthB - lengthA;
                    while (delta > 0)
                    {
                        headB = headB.Next;
                        delta--;
                    }
                }
                while (headA != null)
                {
                    if (headA == headB)
                    {
                        return headB;
                    }
                    headA = headA.Next;
                    headB = headB.Next;
                }
                return null;
            }

            public List<ListNode> ToList()
            {
                List<ListNode> list = new List<ListNode>();
                ListNode head = this;
                while (head != null)
                {
                    list.Add(head);
                    head = head.Next;
                }
                return list;
            }

            public static int GetLength(ListNode head)
            {
                int n = 0;
                while (head != null)
                {
                    head = head.Next;
                    n++;
                }
                return n;
            }

            public static ListNode DetectCycle(ListNode head)
            {
                ListNode fast = head;
                ListNode slow = head;
                while (fast != null && fast.Next != null)
                {
                    fast = fast.Next.Next;
                    slow = slow.Next;
                    if (fast == slow)
                    {
                        break;
                    }
                }
                if (fast != null)
                {
                    while (fast != head)
                    {
                        head = head.Next;
                        fast = fast.Next;
                    }
                    return fast;
                }
                return null;
            }

            public static ListNode Partition(ListNode head, int x)
            {
                ListNode large = new ListNode(0);
                ListNode less = new ListNode(0);
                ListNode newHead = less;
                ListNode newRear = large;
                while (head != null)
                {
                    if (head.Value < x)
                    {
                        less.Next = head;
                        less = less.Next;
                    }
                    else
                    {
                        large.Next = head;
                        large = large.Next;
                    }
                    head = head.Next;
                    large.Next = null;
                }
                less.Next = newRear.Next;
                return newHead.Next;
            }

            public static ListNode Copy(ListNode head)
            {
                Dictionary<ListNode, int> dic = new Dictionary<ListNode, int>();
                List<ListNode> list = new List<ListNode>();
                ListNode tmpHead = head;
                int i = 0;
                while (tmpHead != null)
                {
                    list.Add(new ListNode(tmpHead.Value));
                    dic.Add(tmpHead, i);
                    i++;
                    tmpHead = tmpHead.Next;
                }
                for (int j = 0; j < list.Count - 1; j++)
                {
                    list[j].Next = list[j + 1];
                    if (head.Random != null)
                        list[j].Random = list[dic[head.Random]];
                    head = head.Next;
                }
                list[list.Count - 1].Random = list[dic[head.Random]];
                return list[0];
            }

            public static void PrintRandomList(ListNode head)
            {
                Dictionary<ListNode, int> dic = new Dictionary<ListNode, int>();
                int i = 0;
                ListNode tmp = head;
                while (head != null)
                {
                    Console.Write(head.Value + "\t");
                    dic.Add(head, i);
                    head = head.Next;
                    i++;
                }
                Console.WriteLine();
                while (tmp != null)
                {
                    if (tmp.Random == null)
                        Console.Write("null\t");
                    else
                    {
                        Console.Write(dic[tmp.Random] + "\t");
                    }
                    tmp = tmp.Next;
                }
                Console.WriteLine();
            }

            public static ListNode Merge(ListNode headA, ListNode headB)
            {
                ListNode result = new ListNode(0);
                ListNode newHead = result;
                ListNode ptrA = headA;
                ListNode ptrB = headB;
                while (true)
                {
                    if (ptrA == null)
                    {
                        result.Next = ptrB;
                        break;
                    }
                    if (ptrB == null)
                    {
                        result.Next = ptrA;
                        break;
                    }
                    if (ptrA.Value < ptrB.Value)
                    {
                        result.Next = ptrA;
                        result = result.Next;
                        ptrA = ptrA.Next;
                    }
                    else
                    {
                        result.Next = ptrB;
                        result = result.Next;
                        ptrB = ptrB.Next;
                    }
                }
                return newHead.Next;
            }

            public static ListNode Merge(List<ListNode> list)
            {
                if (list.Count == 0)
                {
                    return null;
                }
                else if (list.Count == 1)
                {
                    return list[0];
                }
                else if (list.Count == 2)
                {
                    return Merge(list[0], list[1]);
                }
                int mid = list.Count / 2;
                List<ListNode> list1 = new List<ListNode>();
                List<ListNode> list2 = new List<ListNode>();
                for (int i = 0; i < mid; i++)
                {
                    list1.Add(list[i]);
                }
                for (int i = mid; i < list.Count; i++)
                {
                    list2.Add(list[i]);
                }
                ListNode l1 = Merge(list1);
                ListNode l2 = Merge(list2);
                return Merge(l1, l2);
            }
        }
    }
}
