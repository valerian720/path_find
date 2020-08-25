using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getFastestWay
{
    class Program
    {
       public static int[,] field;
       public static  Queue<agent> queue;

        public static string[] file;

        static void Main(string[] args)
        {
            /*
             * на вход файл с подобным содержанием (на вход лабиринт)
             * #..s
             * ..#.
             * ....
             * #.f#
             * 
             *  где:
             *  s - начальна точка
             *  f - конечная точка
             *  # - ячейка, где ходить нельзя
             *  . - ячейка где можно ходить
             * 
             * на выходефайл с подобным содержанием (решение)
             * can`t connect s and f
             * 
             * или
             * 
             * #..s
             * ..#*
             * ..**
             * #.f#
             * 
             */

            /* алгоритм
             * на вход подаётся "маска", сам лабиринт
             * 
             * внутри хранится поле с ходами а так же очередь ячеек на проверку
             * 
             * запускается цикл, идущий до того момента, когда в очереди не останется ни одной ячейки на проверку или до того момента когда найдено решение
             *   внутри цикла:
             *     берётся ячейка из очереиди и ей даётся "задача на расширение", т.е. ячейка проверяет проверяет можно ли перейти в ту или иную ячейку
             *     затем добавляет в очередь найденную ячейку и пишет на поле на место ячейки номер шага
             * 
             * затем в случае если найдено решение берётся последний правильная ячейка и закрашивается путь в обратную сторону следуя по уменьшаемымся числам в исходных данных
             * выводим получившиеся строки (как в консоль так и в файл)
             */

            //-----------------------------------------------
            file = File.ReadAllLines("maze.txt");

            foreach (string str in file) Console.WriteLine(str);
            Console.WriteLine();

            int n = file.Length;
            //
            bool isFin = false;

            field = new int[n, file[0].Length]; // поле для передвижения "агентов" // y - высота, x - длина
            queue = new Queue<agent>(); // "агенты" // deque // enqueue
            //
            agent start = findStart();
            //
            agent selected;
            //
            if (start != null)
            {
                queue.Enqueue(start);

                do
                {
                    selected = queue.Dequeue();
                    selected.move();
                    if (selected.finished) {
                        isFin = true;

                        printField();

                        selected.drawPath();
                        
                        

                    }
                }
                while (queue.Count > 0 && (!isFin));

            }
            else
                Console.WriteLine("start was not found");

            

                //
                if (isFin)
            {
                Console.WriteLine();
                foreach (string str in file) Console.WriteLine(str);
                

                Console.WriteLine("success");
                File.WriteAllLines("result.txt", file);
            } 
            else
            {
                Console.WriteLine("can`t connect s and f");
                File.WriteAllText("result.txt", "can`t connect s and f");
            }

            Console.ReadLine();
        }

        static agent findStart() {
            for (int i = 0; i < field.GetLength(0); i++ )
            {
                for (int j = 0; j < field.GetLength(1); j++) {
                    if (file[i][j] == 's') return new agent(i,j);
                }
            }
            return null;
            
        }
        //
        static void printField() {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)                
                    Console.Write(field[i, j] + " ");                
                Console.WriteLine();
            }
        }

    }
}
