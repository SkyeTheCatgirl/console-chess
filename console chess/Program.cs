using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

//Side 1 is black, side 2 is white
//Notes log:
//
//I FUCKING FORGOT ABOUT TAKING PIECES
//
//add unicode chess symbols 🙿 🙾 ♙♘♗♖♕♔♚♛♜♝♞♟
//fix up moving, currently in a "which horse??" situation, make user select which piece to move and then where to, may have to include some basic move validation

namespace Project
{
	class Program
	{
		static void Main()
		{
			Pieces[] board = initBoard();

			int vcchoice;
			Console.WriteLine("Welcome to Skye's dumb chess NEA project");
			Console.WriteLine("Play against: \n[1] Another player \n[2] The computer \n[3] Debug");
			vcchoice = int.Parse(Console.ReadLine());
			Console.Clear();
			if (vcchoice == 1) { SinglePlayerGame(board); }
			else if (vcchoice == 2)
			{
				Console.WriteLine("                                                  \r\n                        %%#                       \r\n                        %%%%%%######,             \r\n                       ...........#####*          \r\n                     ...............######        \r\n                 /#%*%*..##%%%%%.....##/          \r\n                 ,#%%%&..##%%%%%#.*#####          \r\n                   ,#/,.,,(%(,,...####            \r\n                   . .,,.. , .....%%#/            \r\n                  ..,,,,,.......,%#.,,...         \r\n               #&&&%***#%%%%,%%%%%%..,.           \r\n             .&&&&&%%%%%%%.%%%%%%%%%,             \r\n            /&&&&&%%%%%%%%%%%%%%%%%%.             \r\n            @@@&&%%%%%&&%%%%%%&&&%%,..            \r\n                &%%%%%%&&%%%%***,,,.../           \r\n                 ##.  ....*,,/%&&&%%&%%.          \r\n                   %%&%%&&%%&%%%&&%%&&%%          \r\n               .%%%%&&%%%&%%%&%%%&%%%%%%%%%%,     \r\n            .%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%, \r\n           %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%\r\n");
				Console.WriteLine("\nNot finished yet");
			}
            Console.ReadLine();
		}

		static Pieces[] initBoard()
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
		static void printBoard(Pieces[] board)
		{
			for (int i = 0; i < board.Length; i++)
			{
				if (i % 8 == 0 & i != 0)
				{
					Console.WriteLine();
				}
                Console.Write(board[i].getIdentifier() + " ");
            }
		}

		static void SinglePlayerGame(Pieces[] board)
		{
			Console.WriteLine("Starting single player game\n...\n...\n...\n...");
			printBoard(board);
			Console.WriteLine();
			singleWhiteMove(board);
		}

		static void singleWhiteMove(Pieces[] board)
		{
			string Wresponse_location;
			string piece_L; string square_L;
			string validMovesReturn;

			//Location of piece to move
			Console.WriteLine("\nWhite's turn \nPlease input the location of the piece you want to move \nPlease input just the name of the square, e.g. b2");
			Wresponse_location = Console.ReadLine();

			square_L = Wresponse_location.Substring(1);
			//messy way to turn a into 1, b into 2, etc
			int temp = char.ToUpper(char.Parse(square_L.Substring(0, 1))) - 64;
			square_L = ((temp - 1) + ((int.Parse(square_L.Substring(1)) - 1) * 8)).ToString();

			//Destination of piece to move
			Console.WriteLine("\nPlease input just the square that you want to move the piece to from the options below:");

			validMovesReturn = board[int.Parse(square_L)].validMoves(board, int.Parse(square_L));
			if (validMovesReturn == "invalid")
			{
				Console.WriteLine("Press enter to try again");
				Console.ReadLine();
				Console.Clear(); printBoard(board);
				singleWhiteMove(board);
				return;
			}
			//

		}
		static void singleBlackMove(Pieces[] board)
		{

		}

	}

	class Pieces
	{
		private int value;
		private string name;
		private string identifier;
		private int side;
		private bool hasMoved = false; private bool passantAble = false; private bool passanting = false;

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
				identifier = identifier.Insert(0, "b");
				side = 1;
			}
			else if (Side == 2)
			{
				identifier = identifier.Insert(0, "w");
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

		public Pieces[] movePawn(Pieces[] board, string piece_L, string location)
		{
            return board;
		}

		public string validMoves(Pieces[] board, int square)
		{
			switch (identifier.ToLower())
			{
				default:
					break;
				case "p": validatePawn(board, square); break; //Violent
				case "r": validateRook(board, square); break; //Violent
				case "n": validateKnight(board, square); break; //Violent
				case "b": validateBishop(board, square); break; //Violent
				case "q": validateQueen(board, square); break; //Violent
				case "k": validateKing(board, square); break; //Violent
					//Knook
                case "y":
					break;
					//Knishop
				case "z":
                    break;
				case "-":
					Console.WriteLine("There's no piece there dummy");
					return "invalid";
			}
			return "";
		}
		private void validatePawn(Pieces[] board, int square)
		{
            int[] tempArrayPawn = new int[4] { -1, -1, -1, -1 };
            bool emptyPawn = true;
            if (side == 1) //black
            {
                if (board[square + 1].identifier.ToLower() == "wp" & board[square + 1].passantAble == true)
                {
                    tempArrayPawn[0] = square - 7;
                    tempArrayPawn[1] = -1;
                    passanting = true;
                }
                if (board[square - 1].identifier.ToLower() == "wp" & board[square - 1].passantAble == true)
                {
                    tempArrayPawn[0] = square - 9;
                    tempArrayPawn[1] = -1;
                    passanting = true;
                }
                if (board[square + 8].identifier == " -" & passanting == false)
                {
                    if (hasMoved == false)
                    {
                        if (board[square + 8].identifier == " -" & board[square + 16].identifier == " -")
                        {
                            tempArrayPawn[1] = square + 16;
                        }
                        else
                        {
                            tempArrayPawn[1] = -1;
                        }
                    }
                    else
                    {
                        tempArrayPawn[1] = -1;
                    }
                    if (board[square - 9].identifier != " -")
                    {
                        tempArrayPawn[3] = square - 9;
                    }
                    if (board[square - 7].identifier != " -")
                    {
                        tempArrayPawn[4] = square - 7;
                    }
                    tempArrayPawn[0] = square + 8;
                }
            }
            else if (side == 2) //white
            {
                if (board[square + 1].identifier.ToLower() == "bp" & board[square + 1].passantAble == true)
                {
                    tempArrayPawn[0] = square + 9;
                    tempArrayPawn[1] = -1;
                    passanting = true;
                }
                if (board[square - 1].identifier.ToLower() == "bp" & board[square - 1].passantAble == true)
                {
                    tempArrayPawn[0] = square + 7;
                    tempArrayPawn[1] = -1;
                    passanting = true;
                }
                if (board[square - 8].identifier == " -" & passanting == false)
                {
                    if (hasMoved == false)
                    {
                        if (board[square - 8].identifier == " -" & board[square - 16].identifier == " -")
                        {
                            tempArrayPawn[1] = square - 16;
                        }
                        else
                        {
                            tempArrayPawn[1] = -1;
                        }
                    }
                    else
                    {
                        tempArrayPawn[1] = -1;
                    }
                    if (board[square + 7].identifier != " -")
                    {
                        tempArrayPawn[3] = square + 7;
                    }
                    if (board[square + 9].identifier != " -")
                    {
                        tempArrayPawn[4] = square + 9;
                    }
                    tempArrayPawn[0] = square - 8;
                }
            }

            foreach (var item in tempArrayPawn)
            {
                if (item >= 0 & item < 64)
                {
                    int tempPawn = (item / 8) + 1;
                    string temp1Pawn = Convert.ToChar(item % 8 + 65).ToString().ToLower();
                    Console.Write(temp1Pawn + tempPawn.ToString() + " ");
                    emptyPawn = false;
                }
            }
            if (emptyPawn == true)
            {
                Console.WriteLine("This piece can't move anywhere");
            }
        }
        private void validateRook(Pieces[] board, int square)
        {

            int[] tempArrayRook = new int[16];
            int tempValRook1 = square % 8; // horizontal column number, 0,1,2,3, etc. Starts from 0 not 1. the abcs
            int tempValRook2 = (square / 8) * 8; // verticle row number multipled by eight, 0,8,16,24, etc
            bool emptyRook = true;

            //verticle row check
            for (int i = 0; i < 8; i++)
            {
                tempArrayRook[i] = tempValRook1 + (i * 8); //adds the position of each row in a column, eg column d would be 3,11,19,27,35,43,51,59
                if (board[tempArrayRook[i]].identifier != " -") //if the position we're looking at on the board isn't empty
                {
                    if (side == board[tempArrayRook[i]].side) //if our piece and the piece we're looking at are the same colour
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
                if (board[tempValRook2 + i].identifier != " -") //if position we're looking at is not empty
                {
                    if (side == board[tempValRook2 + i].side) //if our piece and the piece we're looking at are the same colour
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

            foreach (var item in tempArrayRook)
            {
                if (item >= 0 & item < 64)
                {
                    int tempRook = (item / 8) + 1;
                    string temp1Rook = Convert.ToChar(item % 8 + 65).ToString().ToLower();
                    Console.Write(temp1Rook + tempRook.ToString() + " ");
                    emptyRook = false;
                }
            }
            if (emptyRook == true)
            {
                Console.WriteLine("This piece can't move anywhere");
            }
        }
        private void validateKnight(Pieces[] board, int square)
        {

            int[] tempArrayKnight = new int[8];
            int[] tempArrayDistances = new int[8] { -17, -15, -10, -6, 6, 10, 15, 17 };
            int Kcounter = 0;
            //there's only 8 positions that are always their own set 'distances' away from the knight,
            //those distances being -17, -15, -10, -6, +6, +10, +15, +17,
            bool emptyKnight = true;

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
                        if (board[square + item].identifier == " -")
                        {
                            tempArrayKnight[Kcounter] = square + item;
                        }
                        else if (board[square + item].side != side)
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

            foreach (var item in tempArrayKnight)
            {
                if (item >= 0 & item < 64)
                {
                    int tempKnight = (item / 8) + 1;
                    string temp1Knight = Convert.ToChar(item % 8 + 65).ToString().ToLower();
                    Console.Write(temp1Knight + tempKnight.ToString() + " ");
                    emptyKnight = false;
                }
            }
            if (emptyKnight == true)
            {
                Console.WriteLine("This piece can't move anywhere");
            }
        }
        private void validateBishop(Pieces[] board, int square)
        {

            int[] tempArrayBishop = new int[28]; for (int i = 0; i < tempArrayBishop.Length; i++) { tempArrayBishop[i] = -1; }
            int BUL = 1; int BUR = 1; int BDL = 1; int BDR = 1;
            bool loopingCondition = true; bool blockadeB = false;
            bool emptyBishop = true;

            //lengths checks
            //Up Left Diag
            while (loopingCondition)
            {
                if (square - (9 * BUL) >= 0) //if not too high
                {
                    if (board[square - (9 * BUL)].identifier != " -" | blockadeB) //if there's something there
                    {
                        if (board[square - (9 * BUL)].side == side | blockadeB)
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
                if (square - (7 * BUR) >= 0) //if not too high
                {
                    if (board[square - (7 * BUR)].identifier != " -" | blockadeB)
                    {
                        if (board[square - (7 * BUR)].side == side | blockadeB)
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
                if (square + (7 * BDL) < 64) //if not too low
                {
                    if (board[square + (7 * BDL)].identifier != " -" | blockadeB)
                    {
                        if (board[square + (7 * BDL)].side == side | blockadeB)
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
                if (square + (9 * BDR) < 64) //if not too low
                {
                    if (board[square + (9 * BDR)].identifier != " -" | blockadeB)
                    {
                        if (board[square + (9 * BDR)].side == side | blockadeB)
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

            foreach (var item in tempArrayBishop)
            {
                if (item >= 0 & item < 64)
                {
                    int tempValBishop1 = (item / 8) + 1;
                    string tempValBishop3 = Convert.ToChar(item % 8 + 65).ToString().ToLower();
                    Console.Write(tempValBishop3 + tempValBishop1.ToString() + " ");
                    emptyBishop = false;
                }
            }
            if (emptyBishop == true)
            {
                Console.WriteLine("This piece can't move anywhere");
            }
        }
        private void validateQueen(Pieces[] board, int square)
        {

            //Gross amalgamation/child of rook and bishop code
            int[] tempArrayQueen = new int[44]; for (int i = 0; i < tempArrayQueen.Length; i++) { tempArrayQueen[i] = -1; }
            int tempValQueen1 = square % 8; // horizontal column number, 0,1,2,3, etc. Starts from 0 not 1. the abcs
            int tempValQueen2 = (square / 8) * 8; // verticle row number multipled by eight, 0,8,16,24, etc
            int qBUL = 1; int qBUR = 1; int qBDL = 1; int qBDR = 1;
            bool loopingConditionQ = true; bool blockadeQ = false;
            bool emptyQueen = true;

            //verticle row check
            for (int i = 0; i < 8; i++)
            {
                tempArrayQueen[i] = tempValQueen1 + (i * 8); //adds the position of each row in a column, eg column d would be 3,11,19,27,35,43,51,59
                if (board[tempArrayQueen[i]].identifier != " -") //if the position we're looking at on the board isn't empty
                {
                    if (side == board[tempArrayQueen[i]].side) //if our piece and the piece we're looking at are the same colour
                    {
                        if (tempValQueen1 + (i * 8) == square) //if the position we're looking at is the rook
                        {
                            tempArrayQueen[i] = -1;
                        }
                        else if (tempValQueen1 + (i * 8) < square) //if the position we're looking at is above the rook
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                tempArrayQueen[j] = -1;
                            }
                        }
                        else if (tempValQueen1 + (i * 8) > square) //if the position we're looking at is below the rook
                        {
                            for (int j = i; j < 8; j++)
                            {
                                tempArrayQueen[j] = -1;
                            }
                            i = 7;
                        }
                    }
                    else //the pieces are different colours
                    {
                        if (tempValQueen1 + (i * 8) < square)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                tempArrayQueen[j] = -1;
                            }
                        }
                        else if (tempValQueen1 + (i * 8) > square)
                        {
                            for (int j = i + 1; j < 8; j++)
                            {
                                tempArrayQueen[j] = -1;
                            }
                            i = 7;
                        }
                    }
                }
            }
            //horizontal column check
            for (int i = 0; i < 8; i++)
            {
                tempArrayQueen[i + 8] = tempValQueen2 + i; //second half of array, adds position of each column in the row, eg 16,17,18,19, etc
                if (board[tempValQueen2 + i].identifier != " -") //if position we're looking at is not empty
                {
                    if (side == board[tempValQueen2 + i].side) //if our piece and the piece we're looking at are the same colour
                    {
                        if (tempValQueen2 + i == square) //if the position we're looking at is the rook
                        {
                            tempArrayQueen[i + 8] = -1;
                        }
                        else if (tempValQueen2 + i < square) //if the position we're looking at is left of the rook
                        {
                            for (int j = 0; j <= i; j++)
                            {
                                tempArrayQueen[j + 8] = -1;
                            }
                        }
                        else if (tempValQueen2 + i > square) //if the positon we're looking at is right of the rook
                        {
                            for (int j = i; j < 8; j++)
                            {
                                tempArrayQueen[j + 8] = -1;
                            }
                            i = 7;
                        }
                    }
                    else //the pieces are different colours
                    {
                        if (tempValQueen2 + i < square)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                tempArrayQueen[j + 8] = -1;
                            }
                        }
                        else if (tempValQueen2 + i > square)
                        {
                            for (int j = i + 1; j < 8; j++)
                            {
                                tempArrayQueen[j + 8] = -1;
                            }
                            i = 7;
                        }
                    }
                }
            }

            //lengths checks
            //Up Left Diag
            while (loopingConditionQ)
            {
                if (square - (9 * qBUL) >= 0) //if not too high
                {
                    if (board[square - (9 * qBUL)].identifier != " -" | blockadeQ) //if there's something there
                    {
                        if (board[square - (9 * qBUL)].side == side | blockadeQ)
                        {
                            tempArrayQueen[qBUL] = -1;
                        }
                        else
                        {
                            tempArrayQueen[qBUL] = square - (9 * qBUL);
                        }
                        blockadeQ = true;
                    }
                    else
                    {
                        tempArrayQueen[qBUL] = square - (9 * qBUL);
                    }
                    if ((square - (9 * qBUL)) % 8 == 0)
                    {
                        loopingConditionQ = false;
                    }
                    qBUL += 1;
                }
                else
                {
                    loopingConditionQ = false;
                }
            }
            loopingConditionQ = true; blockadeQ = false;
            //Up Right Diag
            while (loopingConditionQ)
            {
                if (square - (7 * qBUR) >= 0) //if not too high
                {
                    if (board[square - (7 * qBUR)].identifier != " -" | blockadeQ)
                    {
                        if (board[square - (7 * qBUR)].side == side | blockadeQ)
                        {
                            tempArrayQueen[qBUR + 7] = -1;
                        }
                        else
                        {
                            tempArrayQueen[qBUR + 7] = square - (7 * qBUR);
                        }
                        blockadeQ = true;
                    }
                    else
                    {
                        tempArrayQueen[qBUR + 7] = square - (7 * qBUR);
                    }
                    if ((square - (7 * qBUR)) % 8 == 7)
                    {
                        loopingConditionQ = false;
                    }
                    qBUR += 1;
                }
                else
                {
                    loopingConditionQ = false;
                }
            }
            loopingConditionQ = true; blockadeQ = false;
            //Down Left Diag
            while (loopingConditionQ)
            {
                if (square + (7 * qBDL) < 64) //if not too low
                {
                    if (board[square + (7 * qBDL)].identifier != " -" | blockadeQ)
                    {
                        if (board[square + (7 * qBDL)].side == side | blockadeQ)
                        {
                            tempArrayQueen[qBDL + 14] = -1;
                        }
                        else
                        {
                            tempArrayQueen[qBDL + 14] = square + (7 * qBDL);
                        }
                        blockadeQ = true;
                    }
                    else
                    {
                        tempArrayQueen[qBDL + 14] = square + (7 * qBDL);
                    }
                    if ((square + (7 * qBDL)) % 8 == 0)
                    {
                        loopingConditionQ = false;
                    }
                    qBDL += 1;
                }
                else
                {
                    loopingConditionQ = false;
                }
            }
            loopingConditionQ = true; blockadeQ = false;
            //Down Right Diag
            while (loopingConditionQ)
            {
                if (square + (9 * qBDR) < 64) //if not too low
                {
                    if (board[square + (9 * qBDR)].identifier != " -" | blockadeQ)
                    {
                        if (board[square + (9 * qBDR)].side == side | blockadeQ)
                        {
                            tempArrayQueen[qBDR + 21] = -1;
                        }
                        else
                        {
                            tempArrayQueen[qBDR + 21] = square + (9 * qBDR);
                        }
                        blockadeQ = true;
                    }
                    else
                    {
                        tempArrayQueen[qBDR + 21] = square + (9 * qBDR);
                    }
                    if ((square + (9 * qBDR)) % 8 == 7)
                    {
                        loopingConditionQ = false;
                    }
                    qBDR += 1;
                }
                else
                {
                    loopingConditionQ = false;
                }
            }



            foreach (var item in tempArrayQueen)
            {
                if (item >= 0 & item < 64)
                {
                    int tempQueen = (item / 8) + 1;
                    string temp1Queen = Convert.ToChar(item % 8 + 65).ToString().ToLower();
                    Console.Write(temp1Queen + tempQueen.ToString() + " ");
                    emptyQueen = false;
                }
            }
            if (emptyQueen == true)
            {
                Console.WriteLine("This piece can't move anywhere");
            }
        }
        private void validateKing(Pieces[] board, int square)
        {

            int[] tempArrayKing = new int[8];
            int[] tempArrayDistancesK = new int[8] { -9, -8, -7, -1, 1, 7, 8, 9 };
            int Kingcounter = 0;
            bool emptyKing = true;

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
                        if (board[square + item].identifier == " -")
                        {
                            tempArrayKing[Kingcounter] = square + item;
                        }
                        else if (board[square + item].side != side)
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

            foreach (var item in tempArrayKing)
            {
                if (item >= 0 & item < 64)
                {
                    int tempKing = (item / 8) + 1;
                    string temp1King = Convert.ToChar(item % 8 + 65).ToString().ToLower();
                    Console.Write(temp1King + tempKing.ToString() + " ");
                    emptyKing = false;
                }
            }
            if (emptyKing == true)
            {
                Console.WriteLine("This piece can't move anywhere");
            }
        }
	}
}
