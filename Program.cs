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
        static int Money = 0;

        static void Add()
        {
            for(int i = 0; i < 1000000; ++i)
            {
                Money++;
            }
        }

        static void Remove()
        {
            for (int i = 0; i < 1000000; ++i)
            {
                Money--;
            }
        }

        //foreground, main thread 종료 되면 나머지 쓰레드는 다 종료
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(Add));
            Thread thread2 = new Thread(new ThreadStart(Remove));

            //B함수 따로 실행 시켜줘 (Thread) -> OS 부탁
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
