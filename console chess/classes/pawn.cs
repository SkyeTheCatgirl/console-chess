using console_chess;

public class pawn : Board
{
    public pawn(int input)
    {
        value = 1;
        name = "Pawn";
        identifier = "P";
        side = input;
    }
    protected List<int> validatePawn(int square)
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
            if (Globals.board[square + 8] == " -")
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
}