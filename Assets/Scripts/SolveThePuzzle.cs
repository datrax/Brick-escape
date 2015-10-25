using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Debug = UnityEngine.Debug;

public class SolveThePuzzle : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private List<string> steps=new List<string>();
    public int stepNumber=0;
    public void Solve()
    {
     
        string level = MakeCurrentLevelMap();
        Board initial = getPuzzles(level);
        var sln = FindSolutionBFS(initial);

        List<string> answer = new List<string>();
        while (sln.Moves.Count > 0)
        {
            var move = sln.Moves.Pop();
            move.RenderToConsole(answer);

        }
        steps = MakeMovingMap(answer);
        KeepSolving();      
    }

    public void KeepSolving()
    {
        var t=steps[stepNumber];
        var objs = GameObject.FindGameObjectsWithTag("Block");
        foreach (var ob in objs)
        {
            if (ob.GetComponent<BlockScript>().codeName == t.Substring(0, 4))
            {
                int x = Int32.Parse(t.Substring(6, 1));
                int y = Int32.Parse(t.Substring(7, 1));
                print(ob.GetComponent<BlockScript>().codeName);
                print(x+" "+y);
                print("-----");
                ob.GetComponent<BlockScript>().StartMovingTo(x, y);
                stepNumber++;
                return;
            }
        }
        

    }

    public string MakeCurrentLevelMap()
    {
        string answer = "";
        var objs = GameObject.FindGameObjectsWithTag("Block");
        foreach (var ob in objs)
        {
            answer += ob.GetComponent<BlockScript>().codeName;
        }
        return answer;
    }







    List<string> solutions = new List<string>();
    static Board getPuzzles(string level)
    {
        List<Block> blocks = new List<Block>();
        for (int i = 0; i < level.Length; i += 4)
        {
            int row = Int32.Parse(level[i + 3].ToString()) - 1;
            int col = Int32.Parse(level[i + 2].ToString()) - 1;
            int size = Int32.Parse(level[i + 1].ToString());

            if (level[i] == 'v')
            {
                blocks.Add(new Block(BlockOrientation.Vertical, row, col, size));
            }
            else
            {
                if (level[i] == 'g' || !blocks.Any())
                {
                    blocks.Add(new Block(BlockOrientation.Horizontal, row, col, size));
                }
                else//if key block put it as first
                {
                    blocks.Add(blocks[0]);
                    blocks[0] = new Block(BlockOrientation.Horizontal, row, col, size);
                }
            }
        }
        var array = blocks.ToArray();
        return new Board(array);
    }

    static private List<string> MakeMovingMap(List<string> strings)
    {
        List<string> answer = new List<string>();
        for (int i = 1; i < strings.Count(); i++)
        {
            string s1 = strings[i];
            string s2 = strings[i - 1];
            for (int j = 0; j < s1.Length; j += 4)
            {
                if (s1.Substring(j, 4) != s2.Substring(j, 4))
                {
                    answer.Add(s2.Substring(j, 4) + s1.Substring(j, 4));
                }
            }
        }
        return answer;
    }

    static void Main(string[] args)
    {
        // First beginner puzzle:

        string level = "g254v255v361g311v332v214g316h213";
        Board initial = getPuzzles(level);

        // initial.RenderToConsole("");
        Console.WriteLine();

        Console.WriteLine("Solving...");
        Stopwatch timer = Stopwatch.StartNew();
        var sln = FindSolutionBFS(initial);
        timer.Stop();

        if (sln == null)
            Console.WriteLine("No solution {0} ms", timer.ElapsedMilliseconds);
        List<string> answer = new List<string>();
        //      Console.WriteLine("Solved in {0} moves in {1} ms", sln.MoveCount, timer.ElapsedMilliseconds);
        while (sln.Moves.Count > 0)
        {
            var move = sln.Moves.Pop();
            move.RenderToConsole(answer);

        }
        var t = MakeMovingMap(answer);
        foreach (var VARIABLE in t)
        {
            Console.WriteLine(VARIABLE);
        }
        //     Console.WriteLine("Solved in {0} moves in {1} ms", sln.MoveCount, timer.ElapsedMilliseconds);
    }

    class BoardSolution
    {
        public int MoveCount { get; set; }
        public Stack<Board> Moves { get; set; }
    }

    class BoardMove
    {
        public Board Board { get; set; }
        public int MoveCount { get; set; }
        public BoardMove PreviousMove { get; set; }
        public HashSet<Board> KnownBoards { get; set; }
    }

    /// <summary>
    /// Breadth-first search implementation.
    /// </summary>
    /// <param name="initial"></param>
    /// <returns></returns>
    private static BoardSolution FindSolutionBFS(Board initial)
    {
        Queue<BoardMove> moves = new Queue<BoardMove>(1024);
        // Queue up the first board:
        moves.Enqueue(new BoardMove { Board = initial, MoveCount = 0, PreviousMove = null, KnownBoards = new HashSet<Board>() });

        HashSet<Board> knownBoards = new HashSet<Board>();

        // Process the queue until a solution is found:
        while (moves.Count != 0)
        {
            BoardMove move = moves.Dequeue();

            // Is this the winning move?
            if (move.Board.Blocks[0].Column >= Board.Width - 1)
            {
                // Build up the stack of previous moves leading to the initial move:
                Stack<Board> solutionMoves = new Stack<Board>(move.MoveCount);
                BoardMove tmp = move;
                while (tmp != null)
                {
                    solutionMoves.Push(tmp.Board);
                    tmp = tmp.PreviousMove;
                }
                // Return the solution:
                return new BoardSolution { MoveCount = move.MoveCount, Moves = solutionMoves };
            }

            // Queue up the child legal moves that we haven't seen before on this thread:
#if UseExcept
                var remainingMoves = move.Board.GetLegalMoves().Except(knownBoards).ToList();
                knownBoards.UnionWith(remainingMoves);
#else
            var remainingMoves = move.Board.GetLegalMoves();
#endif

            foreach (var validBoard in remainingMoves)
            {
#if !UseExcept
                if (knownBoards.Contains(validBoard)) continue;
                knownBoards.Add(validBoard);
#endif
                moves.Enqueue(new BoardMove
                {
                    Board = validBoard,
                    MoveCount = move.MoveCount + 1,
                    PreviousMove = move
                });
            }
        }

        // No solution:
        return null;
    }
}

enum BlockOrientation
{
    Horizontal,
    Vertical
}

[DebuggerDisplay("{_orientation} [{_row}, {_col}] {_length}")]
struct Block
{
    int _row;
    int _col;
    int _length;
    BlockOrientation _orientation;

    public int Row { get { return _row; } }
    public int Column { get { return _col; } }
    public int Length { get { return _length; } }
    public BlockOrientation Orientation { get { return _orientation; } }

    public Block(BlockOrientation orientation, int row, int col, int length)
    {
        _orientation = orientation;
        _row = row;
        _col = col;
        _length = length;
    }

    static bool IsInRange(int x, int start, int end)
    {
        return x >= start && x <= end;
    }

    public bool Intersects(Block other)
    {
        if (this.Orientation == BlockOrientation.Horizontal)
        {
            // Horizontal this:
            if (other.Orientation == BlockOrientation.Horizontal)
            {
                // Horizontal this, Horizontal other:

                if (this.Row != other.Row) return false;

                if (IsInRange(this.Column, other.Column, other.Column + other.Length - 1)) return true;
                if (IsInRange(this.Column + this.Length - 1, other.Column, other.Column + other.Length - 1)) return true;
                if (IsInRange(other.Column, this.Column, this.Column + this.Length - 1)) return true;
                if (IsInRange(other.Column + other.Length - 1, this.Column, this.Column + this.Length - 1)) return true;

                return false;
            }
            else
            {
                // Horizontal this, Vertical other:

                if (!IsInRange(this.Row, other.Row, other.Row + other.Length - 1)) return false;
                if (IsInRange(other.Column, this.Column, this.Column + this.Length - 1)) return true;

                return false;
            }
        }
        else
        {
            // Vertical this:
            if (other.Orientation == BlockOrientation.Horizontal)
            {
                // Vertical this, Horizontal other:

                if (!IsInRange(other.Row, this.Row, this.Row + this.Length - 1)) return false;
                if (IsInRange(this.Column, other.Column, other.Column + other.Length - 1)) return true;

                return false;
            }
            else
            {
                // Vertical this, Vertical other:

                if (this.Column != other.Column) return false;

                if (IsInRange(this.Row, other.Row, other.Row + other.Length - 1)) return true;
                if (IsInRange(this.Row + this.Length - 1, other.Row, other.Row + other.Length - 1)) return true;
                if (IsInRange(other.Row, this.Row, this.Row + this.Length - 1)) return true;
                if (IsInRange(other.Row + other.Length - 1, this.Row, this.Row + this.Length - 1)) return true;

                return false;
            }
        }
    }
}

[DebuggerDisplay("{Render(\"\")}")]
struct Board
{
    Block[] _blocks;
    int _highlightIndex;

    public const int Width = 6;
    public const int Height = 6;
    public const int HoleRow = 2;

    public Block[] Blocks { get { return _blocks; } }
    public int HighlightIndex { get { return _highlightIndex; } }

    /// <summary>
    /// Create a new board with a highlighted block.
    /// </summary>
    /// <param name="highlightIndex"></param>
    /// <param name="blocks"></param>
    public Board(int highlightIndex, params Block[] blocks)
    {
        _highlightIndex = highlightIndex;
        _blocks = blocks;

        testBlocks();
    }

    /// <summary>
    /// Create a new board.
    /// </summary>
    /// <param name="blocks"></param>
    public Board(params Block[] blocks)
    {
        _highlightIndex = -1;
        _blocks = blocks;

        testBlocks();
    }

    /// <summary>
    /// Test the block configuration to make sure it is legal.
    /// </summary>
    private void testBlocks()
    {
        // First block is the solution block and it must be on the hole row:
        Debug.Assert(_blocks.Length >= 1);
        Debug.Assert(_blocks[0].Row == HoleRow);

        // Self-test the blocks:
        for (int i = 0; i < _blocks.Length; ++i)
            for (int j = 0; j < _blocks.Length; ++j)
            {
                if (i == j) continue;
                Debug.Assert(!_blocks[i].Intersects(_blocks[j]));
            }
    }

    /// <summary>
    /// Finds all legal moves from this board position.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Board> GetLegalMoves()
    {
        IEnumerable<Board> legalMoves = Enumerable.Empty<Board>();

        // Loop through each block to find legal moves:
        for (int i = 0; i < _blocks.Length; ++i)
        {
            int idx = i;
            Block curr = _blocks[idx];
            Block[] tmpBlocks = _blocks;
            Block[] testBlocks = _blocks.ExceptElement(idx);

            // Try all valid step-sizes of motions for this block:
            if (curr.Orientation == BlockOrientation.Horizontal)
            {
                // Check moving left and right:
                var movesLeft = (
                        from t in Extensions.ReverseRange(curr.Column - 1, curr.Column)
                        select new Block(BlockOrientation.Horizontal, curr.Row, t, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                // Block #0 is the solution block and can move one further to the right into the hole on row #2.
                var movesRight = (
                        from t in Enumerable.Range(curr.Column + 1, (idx == 0 ? Width + 1 : Width) - curr.Length - curr.Column)
                        select new Block(BlockOrientation.Horizontal, curr.Row, t, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                // Don't evaluate edge cases of left or right:
                if (curr.Column == 0) movesLeft = Enumerable.Empty<Block>();
                if (curr.Column + curr.Length - 1 == Board.Width - 1) movesRight = Enumerable.Empty<Block>();

                // Concatenate the board builders:
                legalMoves = legalMoves.Concat(
                    movesRight.Concat(movesLeft)
                    .Select(x => new Board(idx, tmpBlocks.ReplaceElement(idx, x)))
                );
            }
            else
            {
                // Check moving up and down:
                var movesUp = (
                        from t in Extensions.ReverseRange(curr.Row - 1, curr.Row)
                        select new Block(BlockOrientation.Vertical, t, curr.Column, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                var movesDown = (
                        from t in Enumerable.Range(curr.Row + 1, Height - curr.Length - curr.Row)
                        select new Block(BlockOrientation.Vertical, t, curr.Column, curr.Length)
                    ).TakeWhile(x => testBlocks.All(b => !b.Intersects(x)))
                    .Reverse();

                // Don't evaluate edge cases of left or right:
                if (curr.Row == 0) movesUp = Enumerable.Empty<Block>();
                if (curr.Row + curr.Length - 1 == Board.Height - 1) movesDown = Enumerable.Empty<Block>();

                // Concatenate the board builders:
                legalMoves = legalMoves.Concat(
                    movesUp.Concat(movesDown)
                    .Select(x => new Board(idx, tmpBlocks.ReplaceElement(idx, x)))
                );
            }
        }

        return legalMoves;
    }

    private static char[] hexChars = new char[16] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

    /// <summary>
    /// Renders a colored board with highlighted block to the console.
    /// </summary>
    /// <param name="prefix"></param>
    public void RenderToConsole(List<string> answer)
    {
        var r = renderBoard(answer);


        /*    var prefix = "   ";
            for (int y = 0; y < Height; ++y)
            {
                // Write the prefix indentation:
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(prefix);

                // Write the left border:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write('[');

                // Write out spans of color.
                for (int x = 0; x < Width; ++x)
                {
                    Console.ForegroundColor = r.Colors[y][x];
                    Console.Write(r.Ascii[y][x]);
                }

                // Write the right border or hole:
                if (y == HoleRow)
                {
                    Console.ForegroundColor = r.Colors[y][Width];
                    Console.Write(r.Ascii[y][Width]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(']');
                }

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }*/
    }

    /// <summary>
    /// Renders an ascii board to a string.
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public string Render(string prefix, List<string> answer)
    {
        var r = renderBoard(answer);

        return prefix + String.Join(Environment.NewLine + prefix, r.Ascii.Select(
            (row, i) => String.Concat(
                "[",
                new string(row, 0, (i == HoleRow) ? Width + 1 : Width),
                ((i != HoleRow) ? "]" : "")
            ))
            .ToArray()
        );
    }

    private struct RenderedBoard
    {
        char[][] _ascii;
        ConsoleColor[][] _colors;

        public char[][] Ascii { get { return _ascii; } }
        public ConsoleColor[][] Colors { get { return _colors; } }

        public RenderedBoard(char[][] ascii, ConsoleColor[][] colors)
        {
            _ascii = ascii;
            _colors = colors;
        }
    }

    private RenderedBoard renderBoard(List<string> answer)
    {
        char[][] boardAscii = new char[Board.Height][];
        ConsoleColor[][] boardColors = new ConsoleColor[Board.Height][];

        // Set the default board state:
        for (int y = 0; y < Board.Height; ++y)
        {
            boardAscii[y] = new char[Board.Width + 2];
            boardColors[y] = new ConsoleColor[Board.Width + 2];
            for (int x = 0; x < Board.Width + 1; ++x)
            {
                boardAscii[y][x] = '*';
                boardColors[y][x] = ConsoleColor.DarkGray;
            }
        }

        // Render each block on the board:
        string str = "";
        for (int i = 0; i < _blocks.Length; ++i)
        {

            if (i == 0)
            {
                str += "h";
            }
            else
                str += _blocks[i].Orientation == BlockOrientation.Horizontal ? "g" : "v";
            str += _blocks[i].Length.ToString() + (_blocks[i].Column + 1).ToString() + (_blocks[i].Row + 1).ToString();

            if (_blocks[i].Orientation == BlockOrientation.Horizontal)
            {
                for (int t = _blocks[i].Column; t < _blocks[i].Column + _blocks[i].Length; ++t)
                {
                    Debug.Assert(boardAscii[_blocks[i].Row][t] == '*');
                    if (i == _highlightIndex)
                        boardColors[_blocks[i].Row][t] = ConsoleColor.White;
                    else if (i == 0)
                        boardColors[_blocks[i].Row][t] = ConsoleColor.Red;
                    else
                        boardColors[_blocks[i].Row][t] = ConsoleColor.DarkYellow;
                    boardAscii[_blocks[i].Row][t] = hexChars[i];
                }
            }
            else
            {
                for (int t = _blocks[i].Row; t < _blocks[i].Row + _blocks[i].Length; ++t)
                {
                    Debug.Assert(boardAscii[t][_blocks[i].Column] == '*');
                    if (i == _highlightIndex)
                        boardColors[t][_blocks[i].Column] = ConsoleColor.White;
                    else if (i == 0)
                        boardColors[t][_blocks[i].Column] = ConsoleColor.Red;
                    else
                        boardColors[t][_blocks[i].Column] = ConsoleColor.DarkYellow;
                    boardAscii[t][_blocks[i].Column] = hexChars[i];
                }
            }
        }
        answer.Add(str);
        return new RenderedBoard(boardAscii, boardColors);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Board)) return false;

        Board other = (Board)obj;

        if (other._blocks.Length != this._blocks.Length) return false;

        for (int i = 0; i < this._blocks.Length; ++i)
            if (!other._blocks[i].Equals(this._blocks[i]))
                return false;

        return true;
    }

    public override int GetHashCode()
    {
        return this._blocks.Aggregate(0, (i, b) => i ^ b.GetHashCode());
    }
}
public static class Extensions
{
    public static T[] ExceptElement<T>(this T[] arr, int idx)
    {
        T[] newarr = new T[arr.Length - 1];

        if (idx > 0) Array.Copy(arr, newarr, idx);
        if (idx < arr.Length - 1) Array.Copy(arr, idx + 1, newarr, idx, arr.Length - idx - 1);

        return newarr;
    }

    public static T[] ReplaceElement<T>(this T[] arr, int idx, T elem)
    {
        T[] newarr = new T[arr.Length];

        Array.Copy(arr, newarr, arr.Length);
        newarr[idx] = elem;

        return newarr;
    }

    public static IEnumerable<int> ReverseRange(int start, int count)
    {
        int x = start;
        for (int i = 0; i < count; ++i)
        {
            yield return x;
            --x;
        }
        yield break;
    }

}
