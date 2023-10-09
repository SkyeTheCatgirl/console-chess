using console_chess;

public class knight : Board
{
   public knight(int input)
    {
        value = 3;
        name = "Knight";
        identifier = "K";
        side = input;
    } 
    public List<int> validateknight(int square)
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
                    if (Globals.board[square + item] == null) //if nothing is there
                    {
                        tempArrayKnight[Kcounter] = square + item;
                    }
                    else if (Globals.mDside(Globals.board[square + item]) != side)
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
}