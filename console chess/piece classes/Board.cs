using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace console_chess
{
    public class Board
    {
        //Properties
        protected int value; protected int side; protected int killCount;
        protected string name; protected string identifier;
        
        //Methods
        public string getIdentifier()
        {
            return identifier;
        }
        public string getName()
        {
            return name;
        }
        public int getSide()
        {
            return side;
        }
        public int getValue()
        {
            return value;
        }
        protected List<int> printPossibleMoves(int[] array)
        {
            List<int> curatedList = new List<int>();
            foreach (var item in array)
            {
                if (item >= 0 && item < 64 && item != null)
                {
                    int temp = (item / 8) + 1;
                    string temp1 = Globals.convNumToLetter(item);
                    if (!bot.botPlaying && Globals.vcchoice != 3)
                    {
                        Console.Write(temp1 + temp.ToString() + " ");
                    }
                    curatedList.Add(item);
                }
            }
            if (!bot.botPlaying && Globals.vcchoice != 3) { Console.WriteLine(); }
            return curatedList;
        }

        public virtual bool movePiece(int location, int currentPos)
        {
            //location is where the piece is going to, this method runs from the object that is moving

            if (Globals.mDside(Globals.board[location]) != side && Globals.mDside(Globals.board[location]) != 0)
            {
                return takePiece(location);
            }
            else
            {
                if (Globals.mDid(Globals.board[currentPos]) == "K")
                {
                    if (((king)Globals.board[currentPos]).castledleft)
                    {
                        Globals.board[location + 1] = Globals.board[(currentPos / 8) * 8];
                        Globals.board[(currentPos / 8) * 8] = null;
                    }
                    else if (((king)Globals.board[currentPos]).casltedright)
                    {
                        Globals.board[location - 1] = Globals.board[(currentPos / 8) * 8 + 7];
                        Globals.board[(currentPos / 8) * 8 + 7] = null;
                    }
                }
                Globals.board[location] = this;
                return true;
            }
        }
        protected bool takePiece(int location)
        {
            bool successfulKill = false;
            killCount += Globals.mDvalue(Globals.board[location]);
            Random random = new Random();
            //play funny event
            //pick a number
            Console.WriteLine("You're trying to take a piece! \nPick a number between 1 and {0}", killCount);
            if (int.Parse(Console.ReadLine()) == random.Next(1, 1))
            {
                Globals.funnyStall();
                Console.WriteLine("It worked!");
                
                if (Globals.mDid(Globals.board[location]).ToUpper() == "K")
                {
                    Globals.kingDead = side;
                    Console.WriteLine("And in one fell swoop, your {0} executes the enemy king, ending the long and \nmiserable war.", this.name);
                    Console.ReadLine();
                }

                Globals.board[location] = this;
                Thread.Sleep(1000);
                successfulKill = true;
            }
            else
            {
                Globals.funnyStall();
                killCount -= Globals.mDvalue(Globals.board[location]);
                Console.WriteLine("It failed!");
                Thread.Sleep(1000);
                return false;
            }

            if (successfulKill)
            {
                return true;
            }

            return false;
        }
    }
}