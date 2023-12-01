using System.Diagnostics.Contracts;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace console_chess
{
    class bot
    {
        //Value piece move
        List<byte[]> fulllist = new List<byte[]>();
        int playerColour = Globals.playerColour;
        public void scanXMovesAhead(Int16 X)
        {
            //To break it down;
            //int[2] = array to store both value and location of each move
            //List<int[2]> = all possible moves a piece can make and the associated value for each move
            //List<List<int[2]>> = all pieces on a board
            //List<List<List<int[2]>>> = all possible boards in a move
            //LLL<int>[X] = amount of boards ahead
            
            foreach (var item in allMoveValidations(Globals.board))
            {
                fulllist.Add(assignValue(item));
            }

            fulllist.Add(null);

            // Generating next boards
            //foreach (var item in fulllist)
            for (int i = 0; i < fulllist.Count(); i++)
            {   
                var item = fulllist[i];
                if (item != null)
                {
                    foreach (var item2 in allMoveValidations(generateNewBoard(item)))
                    {
                        fulllist.Add(item.Concat(assignValue(item2)).ToArray());
                    }
                }
                else if (item == null)
                {
                    break;
                }
            }
            Console.WriteLine(fulllist.Count());
        }
        private List<byte[]> allMoveValidations(object[] board) //runs move validation for every position on the board and adds it to a list
        {
            List<byte[]> list = new List<byte[]>();
            byte[] aaarg = new byte[3];
            for (int i = 0; i < 64; i++)
            {
                //aaarg[0] is the position of the piece
                //aaarg[1] is the position of the place the piece wants to go to
                //aaarg[2] is the data of the piece
                
                Globals.parameters[0] = i;
                Globals.parameters[1] = board;
                List<int> validmoves = Globals.validateMoves(board[i]);
                Console.Clear();
                for (int j = 0; j < validmoves.Count();j++)
                {
                    int item = validmoves[j];
                    if (Globals.mDside(board[i]) != playerColour)
                    {
                        //allmoves.Add(item);
                        //aaarg[0] = convStringToByteArray(Globals.mDid(board[i]) + i + Globals.mDside(board[i])); //position of piece
                        //convert string to byte array( id of the piece + number + side of the piece )
                        aaarg[0] = Convert.ToByte(i);
                        //aaarg[1] = convStringToByteArray(Globals.mDid(board[i]) + item.ToString() + Globals.mDside(board[i])); //destination of piece
                        aaarg[1] = Convert.ToByte(item);
                        aaarg[2] = convStringToByteArray(Globals.mDside(board[i]).ToString() + Globals.mDside(board[item]).ToString());
                        list.Add(aaarg);
                    }
                }
            }
            return list;
        }
        private byte convStringToByteArray(string str)
        {
            //REPUROSED now it splits the byte into two nibbles, the lhs stores data about the original piece, the rhs stores data about the destination

            //each byte stores both side and type of piece
            //e.g. black horsey would be 0001 0011, white bishop would be 0000 0100
            byte tmp = 0;
            // switch (str.Substring(0, 1).ToLower())
            // {
            //     default:break;
            //     //case "empty": tmp += 0; break;
            //     case "p": tmp += 1; break;
            //     case "r": tmp += 2; break;
            //     case "n": tmp += 3; break;
            //     case "b": tmp += 4; break; 
            //     case "q": tmp += 5; break;
            //     case "k": tmp += 6; break;
            //     //case "knook": tmp += 7; break;
            //     //case "knishop": tmp += 8; break;
            // }
            
            //lhs
            if (str.Substring(0,1) == "2") //black
            {
                tmp += 16;
            }

            //rhs
            if (str.EndsWith("2"))
            {
                tmp += 1;
            }
            else if (str.EndsWith("0"))
            {
                tmp += 8;
            }

            return tmp;
        }
        private object[] generateNewBoard(byte[] byteArray)
        {
            object[] board = Globals.board;

            board[byteArray[1]] = board[byteArray[0]];
            board[byteArray[0]] = null;

            return board;
        }
        private byte[] assignValue(byte[] origMove)
        {
            object[] board = Globals.board;
            byte[] moveWAttValue = new byte[] {origMove[0], origMove[1], origMove[2], };
            
            //move with attached value

            //if location is occupied and the piece occupied is a different side
            if (origMove[2] < 8 && (origMove[2] == 16 | origMove[2] == 1))
            {
                moveWAttValue[3] = Convert.ToByte(Globals.mDvalue(origMove[1]));
            }

            return moveWAttValue;
        }
        private object[] convByteToBoard(object[] board)
        {
            return null;
            //29292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929
            //bqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbq
        }
    }
}