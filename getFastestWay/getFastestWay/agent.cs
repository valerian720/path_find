using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getFastestWay
{

    class agent
    {
        public bool finished = false;

        private int x = 0;
        private int y = 0;
        private int step = 0;

        void goUp() {
            add(x, y - 1);          
        }
        void goDown() {
            add(x, y + 1);
        }
        void goLeft() {
            add(x - 1, y);
        }
        void goRight() {            
            add(x + 1, y);
        }
        //-----------------------------------------------------
        bool canGetThere(int posX, int posY) {
            if (posX < 0 || posX > Program.field.GetLength(0)-1 ) return false;
            if (posY < 0 || posY > Program.field.GetLength(1)-1 ) return false;

            if (Program.file[posX][posY] == '#') return false;
            if (Program.file[posX][posY] == 's') return false;
            if (Program.field[posX, posY] != 0) return false;

            return true;
        }
        //
        void add(int posX, int posY) {
            if (canGetThere(posX, posY))
            {
                Program.field[posX, posY] = Program.field[x, y] + 1;
                Program.queue.Enqueue(new agent(posX, posY));
            }
        }
        //------------------------------------------------------
        public agent(int posX, int posY) {
            x = posX;
            y = posY;

            step = Program.field[posX, posY];
        }
        //------------------------------------------------------
        public void move() {
            if (Program.file[x][y] == 'f')
                finished = true;
            else {
                goUp();
                goRight();
                goDown();
                goLeft();
            }        
        }
        //=====================================
        public void drawPath() {
            int curX = x;
            int curY = y;
            

            do
            {
                if (checkStepsBack(curX+1, curY)) { markPath(curX+1, curY); step--; curX++; continue; }
                if (checkStepsBack(curX-1, curY)) { markPath(curX-1, curY); step--; curX--; continue; }
                if (checkStepsBack(curX, curY+1)) { markPath(curX, curY+1); step--; curY++; continue; }
                if (checkStepsBack(curX, curY-1)) { markPath(curX, curY-1); step--; curY--; continue; }
                Console.WriteLine(Program.file[curX]);
            }
            while (Program.file[curX][curY] !='s' && step !=1);

        }
        //
        bool checkStepsBack(int cX, int cY ) {
           // Console.ReadKey();
            if (cX < 0 || cX > Program.field.GetLength(0)-1) return false;
            if (cY < 0 || cY > Program.field.GetLength(1)-1) return false;

            if (Program.field[cX,cY] != (step - 1)) return false;

            return true;
        }
        //
        void markPath(int curX, int curY) {
            if (Program.file[curX][curY] != 's')
            {
                Program.file[curX] = Program.file[curX].Remove(curY, 1);
                Program.file[curX] = Program.file[curX].Insert(curY, "*");
            }
        }
    }
    
}
