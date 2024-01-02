using System.Security.Cryptography.X509Certificates;
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
    public List<int> validatebishop(int square, object[] board)
    {
        // List<int> list1 = new List<int>();List<int> list2 = new List<int>();List<int> list3 = new List<int>();
        // List<int> list4 = new List<int>();
        // Thread _thread_1 = new Thread(() => {list1.AddRange(validateDiags(square, -9, 0, board));});
        // Thread _thread_2 = new Thread(() => {list2.AddRange(validateDiags(square, -7, 1, board));});
        // Thread _thread_3 = new Thread(() => {list3.AddRange(validateDiags(square, 7, 2, board));});
        // Thread _thread_4 = new Thread(() => {list4.AddRange(validateDiags(square, 9, 3, board));});

        // _thread_1.Start();_thread_2.Start();_thread_3.Start();_thread_4.Start();
        // _thread_1.Join();_thread_2.Join();_thread_3.Join();_thread_4.Join();
        // list3.AddRange(list4);
        // list2.AddRange(list3);
        // list1.AddRange(list2);
        // return list1;

        return validateDiags(square, -9, 0, board).Concat(validateDiags(square, -7, 1, board))
        .Concat(validateDiags(square, 7, 2, board)).Concat(validateDiags(square, 9, 3, board)).ToList();
    }
    private List<int> validateDiags(int square, int diagDir, int timesRun, object[] board)
    {
        int[] tempArrayBishop = new int[28]; for (int i = 0; i < tempArrayBishop.Length; i++) { tempArrayBishop[i] = -1; }
        int distFromBishop = 1;
        bool loopingCondition = true; bool blockadeB = false;

        while (loopingCondition)
        {
            if (square + (diagDir * distFromBishop) >= 0 & (square + (diagDir * distFromBishop) < 64) && !(((diagDir == -9 | diagDir ==  7) && square % 8 == 0) | ((diagDir == 9 | diagDir ==  7) && square % 8 == -7))) //if not too high and not at the left edge
            {
                if (board[square + (diagDir * distFromBishop)] != null | blockadeB) //if there's something there
                {
                    if (Globals.mDside(board[square + (diagDir * distFromBishop)]) == side | blockadeB) //if that something is on the same side
                    {
                        tempArrayBishop[distFromBishop + (7 * timesRun)] = -1;
                        loopingCondition = false;
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
                else if ((square + (diagDir * distFromBishop)) % 8 == 7)
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