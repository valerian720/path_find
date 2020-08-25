using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generator
{
    class Program
    {   // by Mr_V
        /* данное приложение представляет из себя генератор RND лабиринтов 
         * высота: 5 - 50 строчек
         * длина: 5 - 50 символов
         * 
         * при каждой генерации плотность # разная (изменяется вероятность в пределах 50% - 10%  (1/2 и 1/(2+8) соответственно) )
         * 
         * используемые символы: . и # 
         *
         *  обязательно должен быть s и f 
         *  по 1му разу
         */

        static void Main(string[] args)
        {
            Random rnd = new Random();

            int height = 5 + rnd.Next(45); 
            int width = 5 + rnd.Next(45);

            string[] result = new string[height];

            int cap =2 + rnd.Next(8);

            // создание поля
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    result[i] += rnd.Next(cap) ==1 ? '#' : '.';                   
                }
            }

            // создание входа и выхода
            int xS = rnd.Next(height - 1);
            int yS = rnd.Next(width-1);

            result[xS] = result[xS].Remove(yS, 1).Insert(yS, "s");

            int xF = 1 +  rnd.Next(height - 1);
            int yF = rnd.Next(width - 1);

            if (xS == xF && yS == yF) {
                xS = (xS + height/2) % height;
            }

            result[xF] = result[xF].Remove(yF, 1).Insert(yF, "f");

            foreach (string str in result)
            Console.WriteLine(str);

            File.WriteAllLines("maze.txt",result);
            Console.WriteLine("finished");

            

            Console.ReadLine();

        }

    }
}
