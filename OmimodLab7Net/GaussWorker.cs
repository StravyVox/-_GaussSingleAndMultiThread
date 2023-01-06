using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OmimodLab7Net
{
    internal class GaussWorker
    {
        List<Vector2> points;
        List<double> squares;
        List<long> times;
        object locker = new();
        int taskIndex;
        int pointsCount;
        public GaussWorker(List<Vector2> points)
        {
            this.points = points;
            pointsCount= points.Count;
            squares= new List<double>();
            times = new List<long>();

        }
        public void SingleMethod()
        {
            Stopwatch clock = Stopwatch.StartNew();
            int iter = 0;
            List<int> iterations = new List<int>();
            squares.Clear();
            times.Clear();
            //Однопоточный режим
            for (int k = 0; k < 4; k++)
            {
                clock.Restart();
                float x1, x2;
                for (int i = 0; i < pointsCount; i++)
                {
                    x1 = points[i].X;
                    x2 = points[i].Y;
                    double result = Gauss.Square(x1, x2);
                    squares.Add(result);
                }
                clock.Stop();
                long end_time = clock.ElapsedMilliseconds;
                long time = end_time;
                times.Add(time);
                iterations.Add(iter);
                iter += pointsCount;
            }
            Console.WriteLine("Однопоточный");
            PrintResults(iterations);
        }
        public void MultiThreadMethod() {
            
            squares.Clear();
            times.Clear();
            Stopwatch clock = Stopwatch.StartNew();
            List<int> iterations = new List<int>();
            int iter = 0;
            taskIndex = 0;
            int threadsCount = 3;
            for (int k = 0; k < 4; k++)
            {
                clock.Restart();
                ThreadsMethod(threadsCount);
                clock.Stop();
                iterations.Add(iter);
                times.Add(clock.ElapsedMilliseconds);
                iter += pointsCount;
            }

            Console.WriteLine("Многопоточный");
            PrintResults(iterations);
        }
        void PrintResults(List<int> iterations)
        {
            Console.WriteLine("Times is \n");
            for (int i = 0; i < iterations.Count; i++)
            {
                Console.WriteLine("Iterations: {0} Time: {1} ms", iterations[i], times[i]);
            }
        }
        void ThreadsMethod(int threadsCount)
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < threadsCount; i++)
            {
                threads.Add(new Thread(ThreadProcess));
                threads[i].Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join(10);
            }
        }
        void ThreadProcess()
        {
            int pointsCount = points.Count;
            float x1, x2;
            int n;
            while (taskIndex < pointsCount - 1)
            {
                //Синхронизация
                bool acquiredLock = false;

                try
                {
                    Monitor.Enter(locker, ref acquiredLock);
                    x1 = points[taskIndex].X;
                    x2 = points[taskIndex].Y;
                    taskIndex += 1;
                    double result = Gauss.Square(x1, x2);
                    squares.Add(result);

                }
                finally { if (acquiredLock) Monitor.Exit(locker); }
            }
        }
    }
}
