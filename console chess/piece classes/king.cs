using console_chess;

public class king : Board
{
    public king(int input)
    {
        value = 20;
        name = "King";
        identifier = "K";
        side = input;
    }
    public List<int> validateking(int square, object[] board)
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
                    if (board[square + item] == null)
                    {
                        tempArrayKing[Kingcounter] = square + item;
                    }
                    else if (Globals.mDside(board[square + item]) != side)
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
}