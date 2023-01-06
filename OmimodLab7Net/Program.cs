using OmimodLab7Net;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;


namespace Lab7 {
    class Program {
        public static void Main()
        {
            List<Vector2> points = new List<Vector2>();
            int pointsCount;
            string path = @"D:\Учеба\ОМИМОД\omimod_lab_3\Debug\intervals.txt";
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            // асинхронное чтение
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                pointsCount = int.Parse(reader.ReadLine());
                for (int i = 0; i < pointsCount; i++)
                {//точки в формате х x

                    string x1 = reader.ReadLine();
                    string x2 = reader.ReadLine();
                    points.Add(new Vector2(float.Parse(x1,NumberStyles.Any,ci), float.Parse(x2, NumberStyles.Any, ci)));
                }
            }
            GaussWorker gauss = new GaussWorker(points);
            gauss.SingleMethod();
            gauss.MultiThreadMethod();
        }
       
    }
}
