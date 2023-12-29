using System.Diagnostics.Contracts;
using System.Formats.Tar;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading;

namespace console_chess
{
    class bot
    {
        //Value piece move
        List<byte[]> fulllist = new List<byte[]>();
        int playerColour = 1;
        public static bool botPlaying = false;
        public void scanXMovesAhead(Int16 X) //X is one full move (comprised of two half moves, one for each player)
        {
            //To break it down;
            //int[2] = array to store both value and location of each move
            //List<int[2]> = all possible moves a piece can make and the associated value for each move
            //List<List<int[2]>> = all pieces on a board
            //List<List<List<int[2]>>> = all possible boards in a move
            //LLL<int>[X] = amount of boards ahead
            
            playerColour = 1;
            fulllist.Add(null);
            // foreach (var item in allMoveValidations(Globals.board))
            // {
            //     if (item != null)
            //     {
            //         fulllist.Add(assignValue(item));
            //     }
            // }

            fulllist.Add(null);
            
            // Generating next boards
            //foreach (var item in fulllist)
            int prevLim = 0;
            for (int h = 0; h < (X*2) - 1; h++)
            {
                playerColour = playerColour == 1 ? 2 : 1; //Player colour flipper, just changes 1 to 2 and 2 to 1
                int lim = fulllist.Count();
                for (int i = prevLim; i < lim; i++)
                {   
                    byte[][] prevMoves = new byte[h + 1][];
                    byte[] item = fulllist[i]; //for h = 0, item length 3; h++, length++
                    byte[] index;
                    if (i > 65535) //16 bits
                    {
                        index = new byte[3] {255, 255, Convert.ToByte(i > 16)};
                    }
                    else if (i > 255) // 8 bits
                    {
                        index = new byte[2] {255, Convert.ToByte(i >> 8)};
                    }
                    else
                    {
                        index = new byte[1] {Convert.ToByte(i)};
                    }
                    
                    if (item != null)
                    {
                        byte[] subMove = item;
                        for (int k = 0; k < h; k++)
                        {
                            int sumofprevmoves = 0;
                            for (int l = 4; l < subMove.Length - 2; l++)
                            {
                                sumofprevmoves += subMove[l];
                            }
                            prevMoves[k] = fulllist[sumofprevmoves];
                            if (fulllist[sumofprevmoves] != null)
                            {
                                subMove = fulllist[sumofprevmoves];
                            }
                        }
                        // foreach (var item2 in allMoveValidations(generateNewBoard(item, prevMoves)))
                        // {
                        //     //fulllist.Add(item.Concat(assignValue(item2)).ToArray());
                        //     fulllist.Add(assignValue(item2).Concat(index).ToArray());
                        // }
                    }
                    // else if (item == null)
                    // {
                    //     break;
                    // }
                    Console.Clear();
                    Console.WriteLine((((double)i/lim)*100) + "%");
                    prevLim = lim;
                }
            }
            Console.WriteLine(fulllist.Count());
            
        }
        private List<byte[]> allMoveValidations(object[] board, int start, int end) //runs move validation for every position on the board and adds it to a list
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = start; i < end; i++)
            {
                
                //aaarg[0] is the position of the piece
                //aaarg[1] is the position of the place the piece wants to go to
                //aaarg[2] is the data of the piece
                object[] parameters = new object[2];
                parameters[0] = i;
                parameters[1] = board;
                List<int> validmoves = Globals.validateMoves(board[i], parameters);
                //Console.Clear();
                for (int j = 0; j < validmoves.Count();j++)
                {
                    byte[] aaarg = new byte[3];
                    int item = validmoves[j];
                    if (Globals.mDside(board[i]) == playerColour)
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
        protected List<byte[]> threaded_validations(object[] board)
        {
            List<byte[]> list1 = new List<byte[]>();
            List<byte[]> list2 = new List<byte[]>();

            Thread _thread_1 = new Thread(() => {list1 = allMoveValidations(board, 0, 32);});
            Thread _thread_2 = new Thread(() => {list2 = allMoveValidations(board, 32, 64);});

            _thread_1.Start();_thread_2.Start();
            _thread_1.Join();_thread_2.Join();

            list1.AddRange(list2);
            return list1;
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
        private object[] generateNewBoard(byte[] byteArray, byte[][] prevMoves)
        {
            //object[] board = Globals.board;
            object[] board = new object[Globals.board.Length];
            for (int i = 0; i < Globals.board.Length; i++)
            {
                board[i] = Globals.board[i];
            }

            if (prevMoves != null)
            {
                //prevMoves = prevMoves.Reverse().ToArray();
                foreach (byte[] item in prevMoves)
                {
                    if (item != null)
                    {
                        board[item[1]] = board[item[0]];
                        board[item[0]] = null;
                    }
                }
            }

            board[byteArray[1]] = board[byteArray[0]];
            board[byteArray[0]] = null;
            return board;
        }
        private byte[] assignValue(byte[] origMove)
        {
            object[] board = Globals.board;
            byte[] value = new byte[1];
            
            //move with attached value

            //if location is occupied and the piece occupied is a different side
            if (origMove[2] < 8 && (origMove[2] == 16 | origMove[2] == 1))
            {
                value[0] = Convert.ToByte(Globals.mDvalue(origMove[1]));
            }

            return origMove.Concat(value).ToArray();
        }
        private object[] convByteToBoard(object[] board)
        {
            return null;
            //29292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929292929
            //bqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbqbq
        }
        public void minimaxinitialisaiton()
        {
            botPlaying = true;
            byte[][] test = new byte[0][];
            //Console.WriteLine("\n\n\n" + minimax(Globals.board, 4, 4, -1000, 1000, true, null, test));
            byte[] nya = minimax(Globals.board, 4, 4, -1000, 1000, true, null, test);
            Globals.board[nya[1]] = Globals.board[nya[0]];
            Globals.board[nya[0]] = null;
            botPlaying = false;
        }
        private byte[] minimax(object[] position, int depth, int InitalDepth, int alpha, int beta, bool maxPlayer, byte[] moveData, byte[][] prevMove)
        {
            if (moveData != null) {fulllist.Add(moveData);}
            byte[][] prevMoves = new byte[InitalDepth - depth][];
            for (int i = 0; i < prevMove.Length; i++)
            {
                prevMoves[i] = prevMove[i];
            }
            if (depth < InitalDepth) {prevMoves[InitalDepth - depth - 1] = moveData;}

            if (depth == 0 /* | game is over in position */)
            {
                byte[] temp = new byte[1] {altAssignValue(position)};
                //return value of position
                //return altAssignValue(position);
                return moveData.Concat(temp).ToArray();
            }
            if (maxPlayer) //the bot
            {
                playerColour = 2;
                //int maxEval = -1000; //a number it'll never reach
                //dont want bit to be signed so 115 is the new 0
                byte[] maxEval = new byte[4] {0, 0, 0, 0}; //0 is the lowest number a bit can store and the min value of the board is 1
                //for each next move (i.e. using allMoveValidations)
                foreach (var child in allMoveValidations(position, 0, 64)/*threaded_validations(position)*/)
                {
                    moveData = child;
                    //Console.Write(moveData[0]);Console.Write(moveData[1]);
                    //int eval = minimax(position with next move (generate new board), depth - 1, alpha, beta, false)
                    //int eval = minimax(generateNewBoard(child, prevMoves), depth - 1, InitalDepth, alpha, beta, false, moveData, prevMoves);
                    byte[] eval = new byte[4] {moveData[0], moveData[1], moveData[2], minimax(generateNewBoard(child, prevMoves), depth - 1, InitalDepth, alpha, beta, false, moveData, prevMoves)[3]};
                    //maxEval = max between maxEval and eval
                    //maxEval[3] = maxEval[3] > eval[3] ? maxEval[3] : eval[3];
                    if (maxEval[3] < eval[3])
                    {
                        maxEval[0] = eval[0];
                        maxEval[1] = eval[1];
                        maxEval[2] = eval[2];
                        maxEval[3] = eval[3]; 
                    }
                    //alpha = max between alpha and eval
                    alpha = alpha > eval[3] ? alpha : eval[3];
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return maxEval;
            }
            else //the player
            {
                playerColour = 1;
                //int minEval = 1000; //a number it'll never reach
                byte[] minEval = new byte[4] {0, 0, 0, 255}; //255 is the largest number a bit can store and the value of the board caps at 228

                foreach (var child in allMoveValidations(position, 0, 64)/*threaded_validations(position)*/)
                {
                    moveData = child;
                    //int eval = minimax(position with next move (generate new board), depth - 1, alpha, beta, true)
                    byte[] eval = new byte[4] {moveData[0], moveData[1], moveData[2], minimax(generateNewBoard(child, prevMoves), depth - 1, InitalDepth, alpha, beta, true, moveData, prevMoves)[3]};
                    //minEval = min between minEval and eval
                    //minEval[3] = minEval[3] < eval[3] ? minEval[3] : eval[3];
                    if (minEval[3] > eval[3])
                    {
                        minEval[0] = eval[0];
                        minEval[1] = eval[1];
                        minEval[2] = eval[2];
                        minEval[3] = eval[3]; 
                    }
                    //beta = min between beta and eval
                    beta = beta < eval[3] ? beta : eval[3];
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return minEval;
            }
        }
        private byte altAssignValue(object[] board)
        {
            int totalSum = 115;
            for (int i = 0; i < board.Length; i++)
            {
                if (Globals.mDside(board[i]) == 1)
                {
                    totalSum -= Globals.mDvalue(board[i]);
                }
                else if (Globals.mDside(board[i]) == 2)
                {
                    totalSum += Globals.mDvalue(board[i]);
                }
            }
            return Convert.ToByte(totalSum);
        }
    }
}