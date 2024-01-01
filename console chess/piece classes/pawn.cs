using System.Reflection;
using console_chess;

public class pawn : Board
{
    public bool hasMoved = false;
    private bool passantAble = false;

    public pawn(int input)
    {
        value = 1;
        name = "Pawn";
        identifier = "P";
        side = input;
    }

    public List<int> validatepawn(int square, object[] board)
    {
        int[] tempArrayPawn = new int[4] { -1, -1, -1, -1 }; //the first two places are for forward movement, the latter 2 are for taking pieces

        //moving and taking pieces
            if (side == 1) //white
            {
                //moving forward
                if (board[square + 8] == null)
                {
                    tempArrayPawn[0] = square + 8;
                    if (hasMoved == false && board[square + 16] == null)
                    {
                        tempArrayPawn[1] = square + 16;
                        passantAble = true;
                    }
                }
        
                if (square % 8 != 0 && Globals.mDside(board[square + 7]) == 2)
                {
                    tempArrayPawn[2] = square + 7;
                }
                if (square % 8 != 7 && Globals.mDside(board[square + 9]) == 2)
                {
                    tempArrayPawn[3] = square + 9;
                }
            }
            else if (side == 2) //black
            {
                //moving forward
                if (board[square - 8] == null)
                {
                    tempArrayPawn[0] = square - 8;
                    if (hasMoved == false && board[square - 16] == null)
                    {
                        tempArrayPawn[1] = square - 16;
                        passantAble = true;
                    }
                }

                if (square % 8 != 7 && Globals.mDside(board[square - 7]) == 1)
                {
                    tempArrayPawn[2] = square - 7;
                }
                if (square % 8 != 0 && Globals.mDside(board[square - 9]) == 1)
                {
                    tempArrayPawn[3] = square - 9;
                }
            }

        return printPossibleMoves(tempArrayPawn);
    }
    public override void movePiece(int location)
    {
        //location is where the piece is going to, this method runs from the object that is moving

            if (location / 8 == (0|7) && identifier.ToLower() == "p")
            {
                //promotion
                Console.WriteLine("Your pawn's epic adventure has met it's mighty conclusion. Through it's trials\nand tribulations it has now specialised itself into a new piece and has \nthe skills necessary to pick one.");
                Console.WriteLine("Promote to...? Knight[N] Rook[R] Bishop[B] Queen[Q] ???[?] Explosives[E] \nCorpse[C] Politician[P] Marching Band[M] Experience[X]");
                switch (Console.ReadLine().ToUpper())
                {
                    default:
                        break;
                    case "N":
                        break;
                    case "R":
                        break;
                    case "B":
                        break;
                    case "Q":
                        break;
                    case "?":
                        break;
                    case "E":
                        explosives(location);
                        break;
                    case "C":
                        break;
                    case "P":
                        Console.WriteLine("Your newly gained politician realises that they're almost taking an active role in something and promptly flees the scene, likely to spread misinformation and hate in a bid to gain voters.\n(You lose your pawn, but maybe one day that politician will be of use?)");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        break;
                    case "M":
                        Console.WriteLine("From out of nowhere, a marching band appears, stomping across the board and taking your mighty pawn with them, all whilst playing a delightful tune");
                        
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        break;
                    case "X":
                        Console.WriteLine("You feel a sense of greater knowledge as you watch your beloved pawn vanish forever (+1 EXP) ((Note: exp and levels coming in a future update))");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        break;
                }
            }
            else if (Globals.mDside(Globals.board[location]) != side && Globals.mDside(Globals.board[location]) != 0)
            {
                takePiece(location);
            }
            else
            {
                Globals.board[location] = this;
            }
    }
    //promotions
    protected void explosives(int location)
    {
        Console.WriteLine("Your pawn transforms into neatly layered explosives, ready to cause destruction.\n(Any adjacent pieces are going to be destroyed)");
        Console.WriteLine("Say any goodbyes then press enter to continue");
        Console.ReadLine();
        Console.WriteLine("5");
        Thread.Sleep(1000); Console.WriteLine("4");
        Thread.Sleep(1000); Console.WriteLine("3");
        Thread.Sleep(1000); Console.WriteLine("2");
        Thread.Sleep(1000); Console.WriteLine("1");
        Thread.Sleep(1000); Console.WriteLine("BOOM!");

        if (!(location - 8 < 0))
        {
            Globals.board[location - 8] = null; //up
            if (!(location % 8 == 0))
            {
                Globals.board[location - 9] = null; //up left
                Globals.board[location - 1] = null; //left
            }
            if (!(location % 8 == 7))
            {
                Globals.board[location - 7] = null; //up right
                Globals.board[location + 1] = null; //right
            }
        }
        else
        {
            Globals.board[location + 8] = null; //down
            if (!(location % 8 == 0))
            {
                Globals.board[location + 7] = null; //down left
                Globals.board[location - 1] = null; //left
            }
            if (!(location % 8 == 7))
            {
                Globals.board[location + 9] = null; //down right
                Globals.board[location + 1] = null; //right
            }
        }
    }
}