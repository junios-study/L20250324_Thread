using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace L20250324_Thread
{
    class Program
    {

        static Object _lock = new Object(); //동기화 객체
        static SpinLock spinLock = new SpinLock();

        //atomic, 공유영역 작업은 원자성, 중간 끊지 말라고
        //나 끝날때까지 다 하지마
        volatile static int Money = 0;

        static bool lockTaken = false;

        static void Add()
        {
            int Gold = 0;

            for (int i = 0; i < 100000000; ++i)
            {
                lock (_lock)
                {
                    //Interlocked.Increment(ref Money);

                    //이동 때리고
                    //에너지 달고
                    //이펙트 계산1
                    Money++;
                    //int temp = Money;
                    //temp = temp + 1;
                    //Money = temp;

                    Gold++;
                }
            }

        }

        static void Remove()
        {
            int Gold = 0;


            for (int i = 0; i < 100000000; ++i)
            {
                lock (_lock)
                {
                    //Interlocked.Decrement(ref Money);

                    Money--;
                    //int temp = Money;
                    //temp = temp - 1;
                    //Money = temp;
                    //}

                    Gold--;

                }
            }
        }

        //foreground, main thread 종료 되면 나머지 쓰레드는 다 종료
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(Add));
            Thread thread2 = new Thread(new ThreadStart(Remove));
            Thread thread3 = new Thread(new ThreadStart(Add));
            Thread thread4 = new Thread(new ThreadStart(Remove));

            //B함수 따로 실행 시켜줘 (Thread) -> OS 부탁
            thread1.IsBackground = true;
            thread1.Start();
            thread2.IsBackground = true;
            thread2.Start();

            thread3.IsBackground = true;
            thread3.Start();
            thread4.IsBackground = true;
            thread4.Start();


            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();


            Console.WriteLine(Money);
        }
    }
}
