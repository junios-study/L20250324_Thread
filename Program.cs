using System;
using System.Threading;

namespace L20250324_Thread
{
    class Program
    {
        static Object _lock = new Object(); //동기화 객체
        static Object _lock2 = new Object(); //동기화 객체

        //atomic, 공유영역 작업은 원자성, 중간 끊지 말라고
        //나 끝날때까지 다 하지마
        static int Money = 0;

        static void Add()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                lock (_lock)
                {
                    lock (_lock2)
                    {
                        Money++;
                    }
                }
            }
        }

        static void Remove()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                lock (_lock2)
                {
                    lock (_lock)
                    {
                        Money--;
                    }
                }
            }
        }

        //foreground, main thread 종료 되면 나머지 쓰레드는 다 종료
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(Add));
            Thread thread2 = new Thread(new ThreadStart(Remove));

            thread1.IsBackground = true;
            thread1.Start();
            thread2.IsBackground = true;
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine(Money);
        }
    }
}
