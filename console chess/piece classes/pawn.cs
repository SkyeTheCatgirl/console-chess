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
        try
        {
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
        
                if (square % 8 == 0 && Globals.mDside(board[square + 7]) == 2)
                {
                    tempArrayPawn[2] = square + 7;
                }
                if (square % 8 == 7 && Globals.mDside(board[square + 9]) == 2)
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

                if (square % 8 == 0 && Globals.mDside(board[square - 7]) == 1)
                {
                    tempArrayPawn[2] = square - 7;
                }
                if (square % 8 == 7 && Globals.mDside(board[square - 9]) == 1)
                {
                    tempArrayPawn[3] = square - 9;
                }
            }
        }
        catch
        {
            
        }

        return printPossibleMoves(tempArrayPawn);
    }
}