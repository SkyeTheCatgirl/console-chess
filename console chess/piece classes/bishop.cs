using console_chess;

public class bishop : Board
{
    public bishop(int input)
    {
        value = 3;
        name = "Bishop";
        identifier = "B";
        side = input;
    }
    public bishop(bishop input, bool x)
    {
        value = input.value;
        side = input.side;
        killCount = input.killCount;
        name = input.name;
        identifier = input.identifier;
    }
    public List<int> validatebishop(int square, object[] board)
    {
        return validateDiags(square, -9, board).Concat(validateDiags(square, -7, board))
        .Concat(validateDiags(square, 7, board)).Concat(validateDiags(square, 9, board)).ToList();
    }
    private List<int> validateDiags(int square, int diagDir, object[] board)
    {
        int[] tempArrayBishop = new int[8]; for (int i = 0; i < tempArrayBishop.Length; i++) { tempArrayBishop[i] = -1; }
        int distFromBishop = 1;
        bool loopingCondition = true; bool blockadeB = false;

        while (loopingCondition)
        {
            if (square + (diagDir * distFromBishop) >= 0 && 
            (square + (diagDir * distFromBishop) < 64) && 
            !(((diagDir == -9 | diagDir ==  7) && square % 8 == 0) | ((diagDir == 9 | diagDir ==  -7) && square % 8 == 7))) //if not too high and not at the left edge
            {
                if (board[square + (diagDir * distFromBishop)] != null | blockadeB) //if there's something there
                {
                    if (Globals.mDside(board[square + (diagDir * distFromBishop)]) == side | blockadeB) //if that something is on the same side
                    {
                        tempArrayBishop[distFromBishop] = -1;
                        loopingCondition = false;
                    }
                    else
                    {
                        tempArrayBishop[distFromBishop] = square + (diagDir * distFromBishop);
                    }
                    blockadeB = true;
                }
                else
                {
                    tempArrayBishop[distFromBishop] = square + (diagDir * distFromBishop);
                }
                if ((square + (diagDir * distFromBishop)) % 8 == 0 || (square + (diagDir * distFromBishop)) % 8 == 7)
                {
                    loopingCondition = false;
                }
                distFromBishop += 1;
            }
            else
            {
                loopingCondition = false;
            }
        }
        
        return printPossibleMoves(tempArrayBishop);
    }
}