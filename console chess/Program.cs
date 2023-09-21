//Side 2 is black, side 1 is white
//Notes log:
//En passant is broken
//
//Pieces move just doesn't check if they should move to the space. Need to add checking, and taking pieces process, Also the game is currently make 1 move and end, should change that.
//
//I FUCKING FORGOT ABOUT TAKING PIECES
//
//add unicode chess symbols 🙿 🙾 ♙♘♗♖♕♔♚♛♜♝♞♟
//fix up moving, currently in a "which horse??" situation, make user select which piece to move and then where to, may have to include some basic move validation

namespace Project
{
    static class Globals
    {
        //Board variable that is needed throughout the entire code
        //and multiple functions that I need to use in various places
        public static Pieces[] board = initBoard();
        public static Pieces emptyTemplate = new Pieces();
        public static int vcchoice; //I can't remember why i called it this, its the decision for the player playing against the computer or another player
        public static int playerColour = 1; //white plays first

        //methods
        public static Pieces[] initBoard()
		{
			Pieces[] board = new Pieces[64];

			for (int i = 0; i < board.Length; i++)
			{
				board[i] = new Pieces();
				board[i].assignPiece("empty", 0);
			}
			//Black and White backranks
			board[0].assignPiece("R", 1); board[7].assignPiece("R", 1); board[1].assignPiece("N", 1); board[6].assignPiece("N", 1);
			board[2].assignPiece("B", 1); board[5].assignPiece("B", 1); board[3].assignPiece("Q", 1); board[4].assignPiece("K", 1);
			board[56].assignPiece("R", 2); board[63].assignPiece("R", 2); board[57].assignPiece("N", 2); board[62].assignPiece("N", 2);
			board[58].assignPiece("B", 2); board[61].assignPiece("B", 2); board[59].assignPiece("Q", 2); board[60].assignPiece("K", 2);
			//Black pawns
			board[8].assignPiece("P", 1); board[9].assignPiece("P", 1); board[10].assignPiece("P", 1); board[11].assignPiece("P", 1);
            board[12].assignPiece("P", 1); board[13].assignPiece("P", 1); board[14].assignPiece("P", 1); board[15].assignPiece("P", 1);
			//White pawns
            board[48].assignPiece("P", 2); board[49].assignPiece("P", 2); board[50].assignPiece("P", 2); board[51].assignPiece("P", 2);
            board[52].assignPiece("P", 2); board[53].assignPiece("P", 2); board[54].assignPiece("P", 2); board[55].assignPiece("P", 2);

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
            Globals.emptyTemplate.assignPiece("empty", 0);
			Console.WriteLine("Welcome to Skye's dumb chess NEA project");
			Console.WriteLine("Play against: \n[1] Another player \n[2] The computer \n[3] Debug");
			Globals.vcchoice = int.Parse(Console.ReadLine());
			Console.Clear();
			if (Globals.vcchoice == 1) { SinglePlayerGame(); }
			else if (Globals.vcchoice == 2)
			{
				Console.WriteLine("                                                  \r\n                        %%#                       \r\n                        %%%%%%######,             \r\n                       ...........#####*          \r\n                     ...............######        \r\n                 /#%*%*..##%%%%%.....##/          \r\n                 ,#%%%&..##%%%%%#.*#####          \r\n                   ,#/,.,,(%(,,...####            \r\n                   . .,,.. , .....%%#/            \r\n                  ..,,,,,.......,%#.,,...         \r\n               #&&&%***#%%%%,%%%%%%..,.           \r\n             .&&&&&%%%%%%%.%%%%%%%%%,             \r\n            /&&&&&%%%%%%%%%%%%%%%%%%.             \r\n            @@@&&%%%%%&&%%%%%%&&&%%,..            \r\n                &%%%%%%&&%%%%***,,,.../           \r\n                 ##.  ....*,,/%&&&%%&%%.          \r\n                   %%&%%&&%%&%%%&&%%&&%%          \r\n               .%%%%&&%%%&%%%&%%%&%%%%%%%%%%,     \r\n            .%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%, \r\n           %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%\r\n");
				Console.WriteLine("\nNot finished yet");
			}
            Console.ReadLine();
		}
		public static void printBoard()
		{
            Random r = new Random();
			for (int i = 0; i < Globals.board.Length; i++)
			{
				if (i % 8 == 0 & i != 0)
				{
                    Console.ResetColor();
                    Console.Write(" " + i / 8);
					Console.WriteLine();
				}
                //Console.ForegroundColor = (ConsoleColor)r.Next(1,15);
                Console.BackgroundColor = (ConsoleColor)r.Next(1,15);
                Console.Write(Globals.board[i].getIdentifier() + " ");
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
            while (true)
            {
			playerMove();
            }
		}

		static void playerMove()
		{
			string square;
            string piece;
            List<int> lsPossibleMoves = new List<int>();

			//Location of piece to move
            if (Globals.playerColour == 1) { Console.WriteLine("White's turn"); }
            else if (Globals.playerColour == 2) { Console.WriteLine("Black's turn"); }
			Console.WriteLine("\nPlease input the location of the piece you want to move \nPlease input just the name of the square, e.g. b2");
			square = Console.ReadLine();

			square = Globals.convLetterToNum(square);

            if (Globals.board[int.Parse(square)].getSide() != Globals.playerColour & Globals.board[int.Parse(square)].getSide() != 0)
            {
                Console.Write("The trainer blocked the ball!"); Console.ReadLine(); Console.WriteLine("Don't be a thief! \n(You selected a piece that wasn't yours)");
                Globals.invalidInput();
                playerMove();
                return;
            }
            else if (Globals.board[int.Parse(square)].getSide() == 0)
            {
                Console.WriteLine("There's no piece there!");
                Globals.invalidInput();
                playerMove();
                return;
            }

            Console.WriteLine("Selecting " + Globals.board[int.Parse(square)].getName());
			//Destination of piece to move
			Console.WriteLine("\nPlease input just the square that you want to move the piece to from the options below:");

            piece = square;
			lsPossibleMoves = Globals.board[int.Parse(square)].validMoves(int.Parse(square));
            if (lsPossibleMoves.Count() == 0) //If there's nowhere the piece can move
            {
                Console.WriteLine("This piece can't move anywhere!");
                Globals.invalidInput();
                playerMove();
                return;
            }

            square = Globals.convLetterToNum(Console.ReadLine());

            foreach (var item in lsPossibleMoves)
            {
                if (int.Parse(square) == item)
                {
                    Globals.board[int.Parse(piece)].hasMoved = true;
                    Globals.board[int.Parse(piece)].movePiece(int.Parse(square));
                    Globals.board[int.Parse(piece)] = Globals.emptyTemplate;
                    endTurn();
                    return;
                }
                else if (item == int.Parse(piece))
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

	class Pieces
	{
		private int value; private int side; private int killCount;
		private string name; private string identifier;
		public bool hasMoved = false; private bool passantAble = false; private bool passanting = false;

		public void assignPiece(string type, int Side)
		{
			switch (type.ToLower())
			{
				default:
					break;
				case "r":
					value = 5;
					name = "Rook";
					identifier = "R";
					break;
				case "b":
					value = 3;
					name = "Bishop";
					identifier = "B";
					break;
				case "n":
					value = 3;
					name = "Knight";
					identifier = "N";
					break;
				case "q":
					value = 9;
					name = "Queen";
					identifier = "Q";
					break;
				case "k":
					value = 20;
					name = "King";
					identifier = "K";
					break;
				case "p":
					value = 1;
					name = "Pawn";
					identifier = "P";
					break;
				case "Y": //custom pieces
					value = 6;
					name = "Knook";
					identifier = "Y";
					break;
				case "Z":
					value = 6;
					name = "Knishop";
					identifier = "Z";
					break;
				case "empty":
					value = 0;
					name = "Empty";
					identifier = "-";
					break;
			}
			if (Side == 1)
			{
				identifier = identifier.Insert(0, "w");
				side = 1;
			}
			else if (Side == 2)
			{
				identifier = identifier.Insert(0, "b");
				side = 2;
			}
			else if (Side == 0)
			{
				identifier = identifier.Insert(0, " ");
				side = 0;
			}
		}
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
		public List<int> validMoves(int square)
		{
			switch (identifier.ToLower().Substring(1, 1))
			{
				default:
					break;
				case "p": return validatePawn(square); //Violent
				case "r": return validateRook(square); //Violent
				case "n": return validateKnight(square); //Violent
				case "b": return validateBishop(square); //Violent
				case "q": return validateRook(square).Concat(validateBishop(square)).ToList();
				case "k": return validateKing(square); //Violent
					//Knook
                case "y": return validateKnight(square).Concat(validateRook(square)).ToList();
					//Knishop
				case "z": return validateKnight(square).Concat(validateBishop(square)).ToList();
				case "-":
					Console.WriteLine("Idk how this got activated");
                    break;
			}
            return null;
		}
		private List<int> validatePawn(int square)
		{
            int[] tempArrayPawn = new int[4] { -1, -1, -1, -1 }; //the first two places are for forward movement, the latter 2 are for taking pieces
            
            //en passant
            // if (Globals.board[square + 1].passantAble == true)
            // {
            //     Console.WriteLine("En passant is forced");
            //     passanting = true;
            //     if (side == 1)
            //     {
            //         takePiece(square + 9);
            //         //tempArrayPawn[2] = square + 9;
            //     }
            //     else if (side == 2)
            //     {
            //         takePiece(square - 9);
            //         //tempArrayPawn[2] = square - 9;
            //     }
            // }
            // if (Globals.board[square - 1].passantAble == true)
            // {
            //     Console.WriteLine("");
            //     if (side == 1)
            //     {
            //         tempArrayPawn[3] = square + 7;
            //     }
            //     else if (side == 2)
            //     {
            //         tempArrayPawn[3] = square - 7;
            //     }
            // }

            //moving and taking pieces
            if (side == 1) //white
            {
                //moving forward
                if (Globals.board[square + 8].identifier == " -")
                {
                tempArrayPawn[0] = square + 8;
                    if (Globals.board[square + 16].identifier == " -" & hasMoved == false)
                    {
                        tempArrayPawn[1] = square + 16;
                        passantAble = true;
                    }
                }

                if (Globals.board[square + 7].side == 2)
                {
                    tempArrayPawn[2] = square + 7;
                }
                if (Globals.board[square + 9].side == 2)
                {
                    tempArrayPawn[3] = square + 9;
                }
            }
            else if (side == 2) //black
            {
                //moving forward
                if (Globals.board[square - 8].identifier == " -")
                {
                    tempArrayPawn[0] = square - 8;
                    if (Globals.board[square - 16].identifier == " -" & hasMoved == false)
                    {
                        tempArrayPawn[1] = square - 16;
                        passantAble = true;
                    }
                }

                if (Globals.board[square - 7].side == 1)
                {
                    tempArrayPawn[2] = square - 7;
                }
                if (Globals.board[square - 9].side == 1)
                {
                    tempArrayPawn[3] = square - 9;
                }
            }

           return printPossibleMoves(tempArrayPawn);
        }
        private List<int> validateRook(int square)
        {
            int[] tempArrayRook = new int[16];
            int tempValRook1 = square % 8; // horizontal column number, 0,1,2,3, etc. Starts from 0 not 1. the abcs
            int tempValRook2 = (square / 8) * 8; // verticle row number multipled by eight, 0,8,16,24, etc

            //verticle row check
            for (int i = 0; i < 8; i++)
            {
                tempArrayRook[i] = tempValRook1 + (i * 8); //adds the position of each row in a column, eg column d would be 3,11,19,27,35,43,51,59
                if (Globals.board[tempArrayRook[i]].identifier != " -") //if the position we're looking at on the Globals.board isn't empty
                {
                    if (side == Globals.board[tempArrayRook[i]].side) //if our piece and the piece we're looking at are the same colour
                    {
                        if (tempValRook1 + (i * 8) == square) //if the position we're looking at is the rook
                        {
                            tempArrayRook[i] = -1;
                        }
                        else if (tempValRook1 + (i * 8) < square) //if the position we're looking at is above the rook
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                tempArrayRook[j] = -1;
                            }
                        }
                        else if (tempValRook1 + (i * 8) > square) //if the position we're looking at is below the rook
                        {
                            for (int j = i; j < 8; j++)
                            {
                                tempArrayRook[j] = -1;
                            }
                            i = 7;
                        }
                    }
                    else //the pieces are different colours
                    {
                        if (tempValRook1 + (i * 8) < square)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                tempArrayRook[j] = -1;
                            }
                        }
                        else if (tempValRook1 + (i * 8) > square)
                        {
                            for (int j = i + 1; j < 8; j++)
                            {
                                tempArrayRook[j] = -1;
                            }
                            i = 7;
                        }
                    }
                }
            }
            //horizontal column check
            for (int i = 0; i < 8; i++)
            {
                tempArrayRook[i + 8] = tempValRook2 + i; //second half of array, adds position of each column in the row, eg 16,17,18,19, etc
                if (Globals.board[tempValRook2 + i].identifier != " -") //if position we're looking at is not empty
                {
                    if (side == Globals.board[tempValRook2 + i].side) //if our piece and the piece we're looking at are the same colour
                    {
                        if (tempValRook2 + i == square) //if the position we're looking at is the rook
                        {
                            tempArrayRook[i + 8] = -1;
                        }
                        else if (tempValRook2 + i < square) //if the position we're looking at is left of the rook
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                tempArrayRook[j + 8] = -1;
                            }
                        }
                        else if (tempValRook2 + i > square) //if the positon we're looking at is right of the rook
                        {
                            for (int j = i; j < 8; j++)
                            {
                                tempArrayRook[j + 8] = -1;
                            }
                            i = 7;
                        }
                    }
                    else //the pieces are different colours
                    {
                        if (tempValRook2 + i < square)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                tempArrayRook[j + 8] = -1;
                            }
                        }
                        else if (tempValRook2 + i > square)
                        {
                            for (int j = i + 1; j < 8; j++)
                            {
                                tempArrayRook[j + 8] = -1;
                            }
                            i = 7;
                        }
                    }
                }
            }

           return printPossibleMoves(tempArrayRook);
        }
        private List<int> validateKnight(int square)
        {

            int[] tempArrayKnight = new int[8];
            int[] tempArrayDistances = new int[8] { -17, -15, -10, -6, 6, 10, 15, 17 };
            int Kcounter = 0;
            //there's only 8 positions that are always their own set 'distances' away from the knight,
            //those distances being -17, -15, -10, -6, +6, +10, +15, +17,

            foreach (var item in tempArrayDistances)
            {
                if ((square % 8 == 0 & (item == -17 | item == 15)) | (square % 8 <= 1 & (item == -10 | item == 6))
                    | (square % 8 == 7 & (item == -15 | item == 17)) | (square % 8 >= 6 & (item == -6 | item == 10)))
                {
                    tempArrayKnight[Kcounter] = -1;
                }
                else
                {
                    if (square + item >= 0 & square + item < 64)
                    {
                        if (Globals.board[square + item].identifier == " -")
                        {
                            tempArrayKnight[Kcounter] = square + item;
                        }
                        else if (Globals.board[square + item].side != side)
                        {
                            tempArrayKnight[Kcounter] = square + item;
                        }
                        else
                        {
                            tempArrayKnight[Kcounter] = -1;
                        }
                    }
                    else
                    {
                        tempArrayKnight[Kcounter] = -1;
                    }
                }
                Kcounter++;
            }

           return printPossibleMoves(tempArrayKnight);
        }
        private List<int> validateBishop(int square)
        {

            int[] tempArrayBishop = new int[28]; for (int i = 0; i < tempArrayBishop.Length; i++) { tempArrayBishop[i] = -1; }
            int BUL = 1; int BUR = 1; int BDL = 1; int BDR = 1;
            bool loopingCondition = true; bool blockadeB = false;

            //lengths checks
            //Up Left Diag
            while (loopingCondition)
            {
                if (square - (9 * BUL) >= 0 & square % 8 != 0) //if not too high and not at the left edge
                {
                    if (Globals.board[square - (9 * BUL)].identifier != " -" | blockadeB) //if there's something there
                    {
                        if (Globals.board[square - (9 * BUL)].side == side | blockadeB)
                        {
                            tempArrayBishop[BUL] = -1;
                        }
                        else
                        {
                            tempArrayBishop[BUL] = square - (9 * BUL);
                        }
                        blockadeB = true;
                    }
                    else
                    {
                        tempArrayBishop[BUL] = square - (9 * BUL);
                    }
                    if ((square - (9 * BUL)) % 8 == 0)
                    {
                        loopingCondition = false;
                    }
                    BUL += 1;
                }
                else
                {
                    loopingCondition = false;
                }
            }
            loopingCondition = true; blockadeB = false;
            //Up Right Diag
            while (loopingCondition)
            {
                if (square - (7 * BUR) >= 0 & square % 8 != 7) //if not too high and not at the right edge
                {
                    if (Globals.board[square - (7 * BUR)].identifier != " -" | blockadeB)
                    {
                        if (Globals.board[square - (7 * BUR)].side == side | blockadeB)
                        {
                            tempArrayBishop[BUR + 7] = -1;
                        }
                        else
                        {
                            tempArrayBishop[BUR + 7] = square - (7 * BUR);
                        }
                        blockadeB = true;
                    }
                    else
                    {
                        tempArrayBishop[BUR + 7] = square - (7 * BUR);
                    }
                    if ((square - (7 * BUR)) % 8 == 7)
                    {
                        loopingCondition = false;
                    }
                    BUR += 1;
                }
                else
                {
                    loopingCondition = false;
                }
            }
            loopingCondition = true; blockadeB = false;
            //Down Left Diag
            while (loopingCondition)
            {
                if (square + (7 * BDL) < 64 & square % 8 != 0) //if not too low and not at the left edge
                {
                    if (Globals.board[square + (7 * BDL)].identifier != " -" | blockadeB)
                    {
                        if (Globals.board[square + (7 * BDL)].side == side | blockadeB)
                        {
                            tempArrayBishop[BDL + 14] = -1;
                        }
                        else
                        {
                            tempArrayBishop[BDL + 14] = square + (7 * BDL);
                        }
                        blockadeB = true;
                    }
                    else
                    {
                        tempArrayBishop[BDL + 14] = square + (7 * BDL);
                    }
                    if ((square + (7 * BDL)) % 8 == 0)
                    {
                        loopingCondition = false;
                    }
                    BDL += 1;
                }
                else
                {
                    loopingCondition = false;
                }
            }
            loopingCondition = true; blockadeB = false;
            //Down Right Diag
            while (loopingCondition)
            {
                if (square + (9 * BDR) < 64 & square % 8 != 7) //if not too low and not at the right edge
                {
                    if (Globals.board[square + (9 * BDR)].identifier != " -" | blockadeB)
                    {
                        if (Globals.board[square + (9 * BDR)].side == side | blockadeB)
                        {
                            tempArrayBishop[BDR + 21] = -1;
                        }
                        else
                        {
                            tempArrayBishop[BDR + 21] = square + (9 * BDR);
                        }
                        blockadeB = true;
                    }
                    else
                    {
                        tempArrayBishop[BDR + 21] = square + (9 * BDR);
                    }
                    if ((square + (9 * BDR)) % 8 == 7)
                    {
                        loopingCondition = false;
                    }
                    BDR += 1;
                }
                else
                {
                    loopingCondition = false;
                }
            }

           return printPossibleMoves(tempArrayBishop);
        }
        private List<int> validateKing(int square)
        {
            int[] tempArrayKing = new int[8];
            int[] tempArrayDistancesK = new int[8] { -9, -8, -7, -1, 1, 7, 8, 9 };
            int Kingcounter = 0;

            foreach (var item in tempArrayDistancesK)
            {
                if ((square % 8 == 0 & (item == -9 | item == -1 | item == 7)) | (square % 8 == 7 & (item == -7 | item == 1 | item == 9)))
                {
                    tempArrayKing[Kingcounter] = -1;
                }
                else
                {
                    if (square + item >= 0 & square + item < 64)
                    {
                        if (Globals.board[square + item].identifier == " -")
                        {
                            tempArrayKing[Kingcounter] = square + item;
                        }
                        else if (Globals.board[square + item].side != side)
                        {
                            tempArrayKing[Kingcounter] = square + item;
                        }
                        else
                        {
                            tempArrayKing[Kingcounter] = -1;
                        }
                    }
                    else
                    {
                        tempArrayKing[Kingcounter] = -1;
                    }
                }
                Kingcounter++;
            }

            return printPossibleMoves(tempArrayKing);
        }
        private List<int> printPossibleMoves(int[] array)
        {
            List<int> curatedList = new List<int>();
            foreach (var item in array)
            {
                if (item >= 0 & item < 64)
                {
                    int temp = (item / 8) + 1;
                    string temp1 = Globals.convNumToLetter(item);
                    Console.Write(temp1 + temp.ToString() + " ");
                    curatedList.Add(item);
                }
            }
            Console.WriteLine();
            return curatedList;
        }


        public void movePiece(int location)
        {
            //location is where the piece is going to, this method runs from the object that is moving

            if (Globals.board[location].getSide() != side & Globals.board[location].getSide() != 0)
            {
                takePiece(location);
            }
            else
            {
                Globals.board[location] = this;
            }
        }
        private void takePiece(int location)
        {
            killCount += Globals.board[location].getValue();
            Random random = new Random();
            //play funny event
            //pick a number
            Console.WriteLine("You're trying to take a piece! \nPick a number between 1 and {0}", killCount);
            if (int.Parse(Console.ReadLine()) == random.Next(1, killCount + 1))
            {
                Globals.funnyStall();
                Console.WriteLine("It worked!");
                Globals.board[location] = this;
                Thread.Sleep(1000);
            }
            else
            {
                Globals.funnyStall();
                killCount -= Globals.board[location].getValue();
                Console.WriteLine("It failed!");
                Thread.Sleep(1000);
            }
        }
	}
}
