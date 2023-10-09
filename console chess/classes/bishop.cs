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
    public List<int> validatebishop(int square)
    {
        return validateDiags(square, -9, 0).Concat(validateDiags(square, -7, 1))
        .Concat(validateDiags(square, 7, 2)).Concat(validateDiags(square, 9, 3)).ToList();
    }
    private List<int> validateDiags(int square, int diagDir, int timesRun)
    {
        int[] tempArrayBishop = new int[28]; for (int i = 0; i < tempArrayBishop.Length; i++) { tempArrayBishop[i] = -1; }
        int distFromBishop = 1;
        bool loopingCondition = true; bool blockadeB = false;

        while (loopingCondition)
        {
            if (square + (diagDir * distFromBishop) >= 0 & square % 8 != 0) //if not too high and not at the left edge
            {
                if (Globals.board[square + (diagDir * distFromBishop)] != null | blockadeB) //if there's something there
                {
                    if (Globals.mDside(Globals.board[square + (diagDir * distFromBishop)]) == side | blockadeB)
                    {
                        tempArrayBishop[distFromBishop + (7 * timesRun)] = -1;
                    }
                    else
                    {
                        tempArrayBishop[distFromBishop + (7 * timesRun)] = square + (diagDir * distFromBishop);
                    }
                    blockadeB = true;
                }
                else
                {
                    tempArrayBishop[distFromBishop + (7 * timesRun)] = square + (diagDir * distFromBishop);
                }
                if ((square + (diagDir * distFromBishop)) % 8 == 0)
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