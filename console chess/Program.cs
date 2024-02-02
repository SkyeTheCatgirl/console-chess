//test
//Side 2 is black, side 1 is white
//Notes log:
//need to add stalemate and draws
//
//Fix oop, array defined as object doesn't work for calling functions
//
//En passant is broken
//
//Pieces move just doesn't check if they should move to the space. Need to add checking, and taking pieces process, Also the game is currently make 1 move and end, should change that.
//
//I FUCKING FORGOT ABOUT TAKING PIECES
//
//add unicode chess symbols 🙿 🙾 ♙♘♗♖♕♔♚♛♜♝♞♟
//fix up moving, currently in a "which horse??" situation, make user select which piece to move and then where to, may have to include some basic move validation

using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace console_chess
{
    static class Globals
    {
        //Board variable that is needed throughout the entire code
        //and multiple functions that I need to use in various places
        
        public static object[] board = initBoard();
        public static bot bot = new bot();
        public static int vcchoice; //I can't remember why i called it this, its the decision for the player playing against the computer or another player
        public static int playerColour = 1; //white plays first
        public static int kingDead = 0; //0 = neither, 1 = white king dead, 2 = black king dead 

         //Funcs
         //mD = method delegate
        public static Func<object, int> mDside = obj =>
            {
                if (obj != null)
                {
                    MethodInfo method = obj.GetType().GetMethod("getSide");
                    return Convert.ToInt16(method.Invoke(obj, null));
                }
                else
                {
                    return 0;
                }
            };
        public static Func<object, string> mDid = obj =>
            {
                if (obj != null)
                {
                    MethodInfo method = obj.GetType().GetMethod("getIdentifier");
                    return Convert.ToString(method.Invoke(obj, null));
                }
                else
                {
                    return " -";
                }
            };
        public static Func<object, string> mDname = obj =>
            {
                if (obj != null)
                {
                    MethodInfo method = obj.GetType().GetMethod("getName");
                    return Convert.ToString(method.Invoke(obj, null));
                }
                else
                {
                    return "empty";
                }
            };
        public static Func<object, int> mDvalue = obj =>
        {
            if (obj != null)
            {
                MethodInfo method = obj.GetType().GetMethod("getValue");
                return Convert.ToInt16(method.Invoke(obj, null));
            }
            else
            {
                return 0;
            }
        };
        public static Func<object, object[], List<int>> validateMoves = (obj, param) =>
            {
                if (obj != null)
                {
                    MethodInfo method = obj.GetType().GetMethod("validate" + obj.GetType());
                    return (List<int>)method.Invoke(obj, param);
                }
                else
                {
                    List<int> emptyList = new List<int>();
                    return emptyList;
                }
            };
        public static Func<object, object[],  bool> movePiece = (obj, param) =>
            {
                if (obj != null)
                {
                    MethodInfo method = obj.GetType().GetMethod("movePiece");
                    return Convert.ToBoolean(method.Invoke(obj, param));
                }
                else
                {
                    return false;
                }
            };
        
        //methods
        public static object[] initBoard()
        {
            object[] board = new object[64];

            //Black and White backranks
            // board[0] = new rook(1); board[7] = new rook(1);
            // board[56]  = new rook(2); board[63] = new rook(2);
            // board[1] = new knight(1); board[6] = new knight(1);
            // board[57]  = new knight(2); board[62]  = new knight(2);
            board[2]  = new bishop(1); board[5]  = new bishop(1);
            board[58]  = new bishop(2); board[61]  = new bishop(2);
            board[3] = new queen(1); board[4]  = new king(1);
            board[59] = new queen(2); board[60] = new king(2);

            // for (int i = 8; i < 16; i++)
            // {
            //     board[i] = new pawn(1);
            // }
            // for (int i = 48; i < 56; i++)
            // {
            //     board[i] = new pawn(2);
            // }
            return board;
        }
        public static void invalidInput()
        {
            Console.WriteLine("Press enter to try again.");
            Console.ReadLine();
            Console.Clear();
            Program.printBoard();
        }
        public static string convLetterToNum(string letter)
        {
            //messy way to turn a into 1, b into 2, etc
            int temp = char.ToUpper(char.Parse(letter.Substring(0, 1))) - 64;
            letter = ((temp - 1) + ((int.Parse(letter.Substring(1)) - 1) * 8)).ToString();
            return letter;
        }
        public static string convNumToLetter(int number)
        {
            return Convert.ToChar(number % 8 + 65).ToString().ToLower();
        }
        public static void funnyStall()
        {
            for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("...");
                    Thread.Sleep(1000);
                }
        }
    }
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Skye's dumb chess NEA project");
            Console.WriteLine("Play against: \n[1] Another player \n[2] The computer \n[3] Debug");
            Globals.vcchoice = int.Parse(Console.ReadLine());
            Console.Clear();
            if (Globals.vcchoice == 1) { SinglePlayerGame(); }
            else if (Globals.vcchoice == 2)
            {
                Console.WriteLine("                                                  \r\n                        %%#                       \r\n                        %%%%%%######,             \r\n                       ...........#####*          \r\n                     ...............######        \r\n                 /#%*%*..##%%%%%.....##/          \r\n                 ,#%%%&..##%%%%%#.*#####          \r\n                   ,#/,.,,(%(,,...####            \r\n                   . .,,.. , .....%%#/            \r\n                  ..,,,,,.......,%#.,,...         \r\n               #&&&%***#%%%%,%%%%%%..,.           \r\n             .&&&&&%%%%%%%.%%%%%%%%%,             \r\n            /&&&&&%%%%%%%%%%%%%%%%%%.             \r\n            @@@&&%%%%%&&%%%%%%&&&%%,..            \r\n                &%%%%%%&&%%%%***,,,.../           \r\n                 ##.  ....*,,/%&&&%%&%%.          \r\n                   %%&%%&&%%&%%%&&%%&&%%          \r\n               .%%%%&&%%%&%%%&%%%&%%%%%%%%%%,     \r\n            .%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%, \r\n           %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%\r\n");
                Console.WriteLine("\nTesting");
                Console.ReadLine();
                BotGame();
            }
            else if (Globals.vcchoice == 3)
            {
                Globals.bot.minimaxinitialisaiton();
                //Globals.bot.scanXMovesAhead(3);
            }
            Console.ReadLine();
        }
        public static void printBoard()
        {
            Random r = new Random();
            int Tside;
            int counter = 0;
            for (int i = 0; i < Globals.board.Length; i++)
            {
                if (i % 8 == 0 && i != 0)
                {
                    Console.ResetColor();
                    Console.Write(" " + i / 8);
                    Console.WriteLine();
                }
                //Console.ForegroundColor = (ConsoleColor)r.Next(1,15);
                //if ((counter % 16 >= 8 && counter % 2 != 0) ^ counter % 2 == 0)
                if  (counter % 16 >= 8)
                {
                    if (counter % 2 == 0)
                    {Console.BackgroundColor = ConsoleColor.DarkGray; }
                    else
                    {Console.BackgroundColor = ConsoleColor.Green; }
                }
                else
                {
                    if (counter % 2 != 0)
                    {Console.BackgroundColor = ConsoleColor.DarkGray; }
                    else
                    {Console.BackgroundColor = ConsoleColor.Green; }
                }
                Tside = Globals.mDside(Globals.board[i]);
                switch (Tside)
                {
                    default:break;
                    case 0: break;
                    case 1: Console.Write("w"); break;
                    case 2: Console.Write("b"); break;
                }
                Console.Write(Globals.mDid(Globals.board[i]) + " ");
                counter++;
            }
            Console.ResetColor();
            Console.WriteLine(" 8");
            Console.Write(" a  b  c  d  e  f  g  h\n");
        }

        static void SinglePlayerGame()
        {
            Console.WriteLine("Starting single player game");
            Globals.funnyStall();
            printBoard();
            Console.WriteLine();
            while (Globals.kingDead == 0)
            {
                playerMove();
            }
            Globals.kingDead = Globals.kingDead == 1 ? 2 : 1;
            Console.WriteLine("The game is now over! \nPlayer {0} has conquered the other and may now claim their prize.", Globals.kingDead);
        }
        static void BotGame()
        {
            Console.WriteLine("Starting bot game");
            Globals.funnyStall();
            Console.Clear();
            printBoard();
            Console.WriteLine();
            Console.WriteLine("Player turn");
            while (Globals.kingDead == 0)
            {
                playerMove();
                if (Globals.playerColour > 0)
                {
                    botMove();
                }
                else
                {
                    Globals.playerColour = 1;
                }
            }
            if (Globals.kingDead == 1)
            {
                Console.WriteLine("Checkmate \nThe game is now over, man has succummed to machine and all humanity is doomed.\n...\nFarewell.");
            }
            else if (Globals.kingDead == 2)
            {
                Console.WriteLine("The game is now over, man has won over technology and the revolution will not happen today.\nCongratulations on prolonging the takeover of robots, you may now claim your prize.");
            }
        }

        static void playerMove()
        {
            string square;
            int piece;
            int IntSquare;
            List<int> lsPossibleMoves = new List<int>();

            //Location of piece to move
            if (Globals.playerColour == 1) { Console.WriteLine("White's turn"); }
            else if (Globals.playerColour == 2) { Console.WriteLine("Black's turn"); }

            for (int i = 24; i < 40; i++)
            {
                if (Globals.mDid(Globals.board[i]) == "P")
                {
                    if (((pawn)Globals.board[i]).en_passant(i, false))
                    {
                        Globals.board[i] = null;
                        endTurn();
                        return;
                    }
                }
            }
            
            Console.WriteLine("\nPlease input the location of the piece you want to move \nPlease input just the name of the square, e.g. b2");
            square = Console.ReadLine();

            IntSquare = int.Parse(Globals.convLetterToNum(square));
            
            if (Globals.board[IntSquare] == null)
            {
                Console.WriteLine("There's no piece there!");
                Globals.invalidInput();
                playerMove();
                return;
            }
            else if (Globals.mDside(Globals.board[IntSquare]) != Globals.playerColour)
            {
                Console.Write("The trainer blocked the ball!"); Console.ReadLine(); Console.WriteLine("Don't be a thief! \n(You selected a piece that wasn't yours)");
                Globals.invalidInput();
                playerMove();
                return;
            }

            Console.WriteLine("Selecting " + Globals.mDname(Globals.board[IntSquare]));
            //Destination of piece to move
            Console.WriteLine("\nPlease input just the square that you want to move the piece to from the options below:");

            piece = IntSquare;
            object[] parameters = new object[2];
            parameters[0] = IntSquare;
            parameters[1] = Globals.board;
            lsPossibleMoves = Globals.validateMoves(Globals.board[IntSquare], parameters);
            if (lsPossibleMoves.Count() == 0) //If there's nowhere the piece can move
            {
                Console.WriteLine("This piece can't move anywhere!");
                Globals.invalidInput();
                playerMove();
                return;
            }

            IntSquare = int.Parse(Globals.convLetterToNum(Console.ReadLine()));

            foreach (var item in lsPossibleMoves)
            {
                if (IntSquare == item)
                {
                    if (Globals.mDname(Globals.board[piece]) == "Pawn") {((pawn)Globals.board[piece]).hasMoved = true;}
                    else if (Globals.mDname(Globals.board[piece]) == "King")
                    {
                        if (piece - item == 2) {((king)Globals.board[piece]).castledleft = true;}
                        else if (piece - item == -2) {((king)Globals.board[piece]).casltedright = true;}
                        ((king)Globals.board[piece]).hasMoved = true;
                    }
                    else if (Globals.mDname(Globals.board[piece]) == "Rook") {((rook)Globals.board[piece]).hasMoved = true;}
                    parameters = new object[2];
                    parameters[0] = IntSquare;
                    parameters[1] = piece;
                    if (Globals.movePiece(Globals.board[piece], parameters))
                    {
                        Globals.board[piece] = null;
                    }
                    endTurn();
                    return;
                }
                else if (item == piece)
                {
                    Console.WriteLine("Nuh uh, the piece is already there.");
                    Globals.invalidInput();
                    playerMove();
                    return;
                }
            }
            Console.WriteLine("Nuh uh, the piece can't move there.");
            Globals.invalidInput();
            playerMove();
            return;
        }
        static void botMove()
        {
            Console.WriteLine("Bots turn");
            for (int i = 24; i < 40; i++)
            {
                if (Globals.mDid(Globals.board[i]) == "P")
                {
                    if (((pawn)Globals.board[i]).en_passant(i, true))
                    {
                        Globals.board[i] = null;
                        endTurn();
                        return;
                    }
                }
            }
            Globals.bot.minimaxinitialisaiton();
            //Thread.Sleep(5000);
            endTurn();
            return;
        }

        static void endTurn()
        {
            Console.Clear();
            printBoard();
            if (Globals.vcchoice == 1)
            {
                Globals.playerColour = Globals.playerColour == 1 ? 2 : 1;
            }
        }
    }
}