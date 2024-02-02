using console_chess;

public class rook : Board
{
    public bool hasMoved = false;
    public rook(int input)
    {
        value = 5;
        name = "Rook";
        identifier = "R";
        side = input;
    }
    public rook(rook input, bool x)
    {
        value = input.value;
        side = input.side;
        killCount = input.killCount;
        name = input.name;
        identifier = input.identifier;
        hasMoved = input.hasMoved;
    }
    public List<int> validaterook(int square, object[] board)
    {
        int[] tempArrayRook = new int[16];
        int tempValRook1 = square % 8; // horizontal column number, 0,1,2,3, etc. Starts from 0 not 1. the abcs
        int tempValRook2 = (square / 8) * 8; // verticle row number multipled by eight, 0,8,16,24, etc

        //verticle row check
        for (int i = 0; i < 8; i++)
        {
            tempArrayRook[i] = tempValRook1 + (i * 8); //adds the position of each row in a column, eg column d would be 3,11,19,27,35,43,51,59
            if (board[tempArrayRook[i]] != null) //if the position we're looking at on the Globals.board isn't empty
            {
                if (side == Globals.mDside(board[tempArrayRook[i]])) //if our piece and the piece we're looking at are the same colour
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
            if (Globals.board[tempValRook2 + i] != null) //if position we're looking at is not empty
            {
                if (side == Globals.mDside(Globals.board[tempValRook2 + i])) //if our piece and the piece we're looking at are the same colour
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
}