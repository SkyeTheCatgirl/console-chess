using System.Diagnostics.Contracts;
using System.IO.Compression;

namespace console_chess
{
    class bot
    {
        //Value piece move
        List<byte[]> list = new List<byte[]>();
        public void scanXMovesAhead(Int16 X)
        {
            //To break it down;
            //int[2] = array to store both value and location of each move
            //List<int[2]> = all possible moves a piece can make and the associated value for each move
            //List<List<int[2]>> = all pieces on a board
            //List<List<List<int[2]>>> = all possible boards in a move
            //LLL<int>[X] = amount of boards ahead
            
            for (int i = 0; i < X; i++)
            {
                allMoveValidations(Globals.board);
            }
        }
        private void allMoveValidations(object[] board) //runs move validation for every position on the board and adds it to a list
        {
            byte[] aaarg = new byte[2]; 
            for (int i = 0; i < 64; i++)
            {
                //allmoves.Add(Globals.validateMoves(board[i]));
                

                foreach (var item in Globals.validateMoves(board[i]))
                {
                    //allmoves.Add(item);
                    aaarg[0] = convStringToByteArray(Globals.mDid(board[i]) + i + Globals.mDside(board[i]));
                    aaarg[1] = convStringToByteArray(Globals.mDside(board[i]) + item.ToString() + Globals.mDside(board[i]));
                    list.Add(aaarg);
                }
            }
        }
        private byte convStringToByteArray(string str)
        {
            //each byte stores both side and type of piece
            //e.g. black horsey would be 0001 0011, white bishop would be 0000 0100
            byte tmp = 0;
            switch (str.Substring(0, 1))
            {
                default:break;
                //case "empty": tmp += 0; break;
                case "p": tmp += 1; break;
                case "r": tmp += 2; break;
                case "n": tmp += 3; break;
                case "b": tmp += 4; break; 
                case "q": tmp += 5; break;
                case "k": tmp += 6; break;
                //case "knook": tmp += 7; break;
                //case "knishop": tmp += 8; break;
            }
            if (str.EndsWith("2")) //black
            {
                tmp += 16;
            }
            else if (str.EndsWith("0")) //empty
            {
                tmp += 128;
            }

            return tmp;
        }
        private object[] generateNewBoard(byte[] byteArray)
        {
            object[] board = Globals.initBoard();

            //check if location is occupied
            if (byteArray[1] < 128 && byteArray[1] >= 16)
            {

            }
            else
            {
                //Globals.board[location] = this;
            }

            return null;
        }
        private object[] convByteToBoard(object[] board)
        {
            return null;
            //29292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929
            //bqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbq
        }
    }
}