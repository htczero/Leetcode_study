using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 用queue实现stack
            //MyStack myStck = new MyStack();
            //myStck.Push(1);
            //myStck.Push(2);
            //myStck.Push(3);
            //myStck.Push(4);

            //Console.WriteLine(myStck.Pop());
            //Console.WriteLine(myStck.Top());
            //Console.WriteLine(myStck.Pop());
            //Console.WriteLine(myStck.Pop());
            //Console.WriteLine(myStck.IsEmpty());
            //Console.WriteLine(myStck.Size());
            #endregion

            #region 用queue实现stack
            //MyQueue myQueue = new MyQueue();
            //myQueue.Push(1);
            //myQueue.Push(2);
            //myQueue.Push(3);

            //Console.WriteLine(myQueue.Pop());
            //Console.WriteLine(myQueue.Front());
            //Console.WriteLine(myQueue.Pop());
            //Console.WriteLine(myQueue.Pop());
            //Console.WriteLine(myQueue.IsEmpty());
            //Console.WriteLine(myQueue.Size());
            #endregion

            #region 带min的Stack
            //Stack myStck = new Stack();
            //myStck.Push(3);
            //myStck.Push(2);
            //myStck.Push(0);
            //myStck.Push(4);
            //Console.WriteLine(myStck.GetMin());
            //myStck.Pop();
            //Console.WriteLine(myStck.GetMin());
            //myStck.Pop();
            //Console.WriteLine(myStck.GetMin());
            //myStck.Pop();
            //Console.WriteLine(myStck.GetMin());
            //Console.WriteLine();
            #endregion

            #region 检查出栈顺序是否合法
            //string s = "(1+121)-(14+(5-6))";
            //Console.WriteLine(Calculate2(s));
            #endregion


            Console.ReadKey();


        }

        class Stack
        {
            int _top = -1;
            List<int> listMin = new List<int>();
            List<int> list = new List<int>();

            public int Top()
            {
                if (_top == -1)
                {
                    return int.MinValue;
                }
                else
                {
                    return list[_top];
                }
            }

            public bool IsEmpty()
            {
                if (_top == -1)
                {
                    return true;
                }
                return false;
            }

            public void Push(int x)
            {
                if (_top == list.Count - 1 && _top != -1)
                {
                    list.Add(x);
                    int min = listMin[_top];
                    if (x < min)
                    {
                        listMin.Add(x);
                    }
                    else
                    {
                        listMin.Add(min);
                    }
                    _top++;

                }
                else if (_top == -1)
                {
                    if (list.Count != 0)
                    {
                        listMin[++_top] = x;
                        list[_top] = x;
                    }
                    else
                    {
                        list.Add(x);
                        listMin.Add(x);
                        _top++;
                    }

                }
                else
                {
                    int min = listMin[_top];
                    if (x < min)
                    {
                        listMin[++_top] = x;
                    }
                    else
                    {
                        listMin[++_top] = min;
                    }
                    list[_top] = x;
                }
            }

            public int Pop()
            {
                if (_top == -1)
                {
                    return int.MinValue;
                }
                return list[_top--];
            }

            public int Size()
            {
                return _top + 1;
            }

            public int GetMin()
            {
                return listMin[_top];
            }
        }

        class Queue
        {
            List<int> list = new List<int>();

            public bool IsEmpty()
            {
                if (list.Count == 0)
                    return true;
                return false;
            }

            public int Front()
            {
                if (list.Count == 0)
                {
                    return int.MinValue;
                }
                return list[0];
            }

            public int Back()
            {
                if (list.Count == 0)
                {
                    return int.MinValue;
                }
                return list[list.Count - 1];
            }

            public void Push(int x)
            {
                list.Add(x);
            }

            public int Pop()
            {
                if (list.Count == 0)
                {
                    return int.MinValue;
                }
                int x = list[0];
                list.RemoveAt(0);
                return x;
            }

            public int Size()
            {
                return list.Count;
            }
        }

        class MyStack
        {
            Queue q = new Queue();
            public void Push(int x)
            {
                if (q.IsEmpty())
                {
                    q.Push(x);
                }
                else
                {
                    Queue tmp = new Queue();
                    tmp.Push(x);
                    while (!q.IsEmpty())
                    {
                        tmp.Push(q.Pop());
                    }
                    q = tmp;
                }
            }
            public int Pop()
            {
                return q.Pop();
            }

            public int Size()
            {
                return q.Size();
            }

            public int Top()
            {
                return q.Front();
            }

            public bool IsEmpty()
            {
                return q.IsEmpty();
            }
        }

        class MyQueue
        {
            Stack q = new Stack();
            public void Push(int x)
            {
                if (q.IsEmpty())
                {
                    q.Push(x);
                }
                else
                {
                    Stack tmp = new Stack();
                    while (!q.IsEmpty())
                    {
                        tmp.Push(q.Pop());
                    }
                    q.Push(x);
                    while (!tmp.IsEmpty())
                    {
                        q.Push(tmp.Pop());
                    }
                }
            }
            public int Pop()
            {
                return q.Pop();
            }

            public int Size()
            {
                return q.Size();
            }

            public int Front()
            {
                return q.Top();
            }

            public bool IsEmpty()
            {
                return q.IsEmpty();
            }
        }

        static bool CheckIsVaildOrder(Queue q)
        {
            Stack s = new Stack();
            for (int i = 0; i < q.Size(); i++)
            {
                s.Push(i);
                while (!s.IsEmpty() && s.Top() == q.Front())
                {
                    s.Pop();
                    q.Pop();
                }
            }
            if (s.IsEmpty())
                return true;
            return false;
        }

        static int Calculate(string s)
        {
            s = s.Replace(" ", "");
            Queue num = new Queue();
            Queue op = new Queue();
            string m = "";
            bool bAdd = true;
            foreach (var item in s)
            {
                if (item >= '0' && item < '9')
                {
                    m += item;
                }
                else
                {
                    if (m != "")
                    {
                        num.Push(int.Parse(m));
                        m = "";
                    }
                    if ((item == '+' && bAdd) || (item == '-' && !bAdd))
                    {
                        op.Push(0);
                    }
                    else if ((item == '+' && !bAdd) || (item == '-' && bAdd))
                    {
                        op.Push(1);
                    }
                    else if (item == '(')
                    {
                        if (op.Back() == 1)
                            bAdd = false;
                        else
                            bAdd = true;
                    }
                    else
                    {
                        bAdd = true;
                    }
                }
            }
            int a = num.Pop();
            while (!op.IsEmpty())
            {
                int tmp = op.Pop();
                int b = num.Pop();
                if (tmp == 0)
                {
                    a += b;
                }
                else
                {
                    a -= b;
                }
            }
            return a;
        }

        static int Calculate2(string s)
        {
            s = s.Replace(" ", "");
            Stack num = new Stack();
            Stack op = new Stack();
            string m = "";
            bool cal = true;
            foreach (var item in s)
            {
                if (item >= '0' && item < '9')
                {
                    m += item;
                }
                else
                {
                    if (m != "")
                    {
                        num.Push(int.Parse(m));
                        m = "";
                    }
                    if (item == '+' || item == '-')
                    {
                        if (cal&&num.Size()>1)
                        {
                            Cal(num, op);
                        }
                        if (item == '+')
                        {
                            op.Push(0);
                        }
                        else
                        {
                            op.Push(1);
                        }
                    }

                    else if (item == '(')
                        cal = false;
                    else
                        cal = true;
                }
            }
            while (!op.IsEmpty())
            {
                Cal(num, op);
            }
            return num.Pop();
        }

        static void Cal(Stack num, Stack op)
        {
            int tmp = op.Pop();
            int b = num.Pop();
            int a = num.Pop();
            if (tmp == 0)
            {
                num.Push(a + b);
            }
            else
            {
                num.Push(a - b);
            }
        }
    }
}
