using console_chess;

public class king : Board
{
    public bool hasMoved = false;
    public bool castledleft = false;
    public bool casltedright = false;
    public king(int input)
    {
        value = 20;
        name = "King";
        identifier = "K";
        side = input;
    }
    public king(king input, bool x)
    {
        value = input.value;
        side = input.side;
        killCount = input.killCount;
        name = input.name;
        identifier = input.identifier;
        hasMoved = input.hasMoved;
        castledleft = input.castledleft;
        casltedright = input.casltedright;
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
        return printPossibleMoves(tempArrayKing.Concat(castling(square, board)).ToArray());
    }
    public int[] castling(int square, object[] board)
    {
        int[] castlelocations = new int[2] {-1, -1};

        if (((side == 1 && square == 4) | (side == 2 && square == 60)) && 
        !hasMoved && Globals.mDid(board[(square / 8) * 8]) == "R" && !((rook)board[(square / 8) * 8]).hasMoved &&
        board[(square / 8) * 8 + 1] == null && board[(square / 8) * 8 + 2] == null && board[(square / 8) * 8 + 3] == null)
        //if the king hasn't moved, and the piece in the a rank on the same row as the king is a rook, and said rook hasn't moved, and there's nothing in b, c, and d rank
        {
            castlelocations[0] = (square / 8) * 8 + 2; //c rank on the same row as king
        }
        if (((side == 1 && square == 4) | (side == 2 && square == 60)) && 
        !hasMoved && Globals.mDid(board[(square / 8) * 8 + 7]) == "R" && !((rook)board[(square / 8) * 8 + 7]).hasMoved &&
        board[(square / 8) * 8 + 6] == null && board[(square / 8) * 8 + 5] == null)
        //same logic but for other side
        {
            castlelocations[1] = (square / 8) * 8 + 6;
        }
        return castlelocations;
    }

    public override bool movePiece(int location, int currentPos)
    {
        //location is where the piece is going to, this method runs from the object that is moving

        if (Globals.mDside(Globals.board[location]) != side && Globals.mDside(Globals.board[location]) != 0)
        {
            return takePiece(location);
        }
        else
        {
            if (((king)Globals.board[currentPos]).castledleft)
            {
                Globals.board[location + 1] = Globals.board[(currentPos / 8) * 8];
                Globals.board[(currentPos / 8) * 8] = null;
            }
            else if (((king)Globals.board[currentPos]).casltedright)
            {
                Globals.board[location - 1] = Globals.board[(currentPos / 8) * 8 + 7];
                Globals.board[(currentPos / 8) * 8 + 7] = null;
            }
            Globals.board[location] = this;
            return true;
        }
    }
}