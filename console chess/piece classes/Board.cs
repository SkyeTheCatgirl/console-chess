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
                Globals.board[location] = this;
                return true;
            }
        }
        protected bool takePiece(int location)
        {
            bool successfulKill = false;
            killCount += Globals.mDvalue(Globals.board[location]);
            string selection = "-1";
            Random random = new Random();
            //pick a number
            while (true)
            {
                Console.WriteLine("You're trying to take a piece! \nPick a number between 1 and {0}", killCount + 2);
                try
                {
                    selection = Console.ReadLine();
                    if (int.Parse(selection) >= 1 && int.Parse(selection) <= killCount + 2)
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input.");
                    Globals.invalidInput();
                }
            }
            if (int.Parse(selection) != random.Next(1, killCount + 2))
            {
                Globals.Loading();
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
                Globals.Loading();
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