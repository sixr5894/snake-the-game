using System.Drawing;
using System.Text;
using System.Threading;

namespace ConsoleSnake
{
    internal class LinkNode
    {
        public LinkNode Next { get; set; }
        public Point Value { get; set; }
    }
    public enum Direction
    {
        None, Up, Down, Left, Right
    }
    internal struct Point
    {
        public int Row;
        public int Column;
    }
    internal class Program
    {

        internal Program()
        {
            _matrix = new char[50][];
            for (int i = 0; i < 50; i++)
            {
                _matrix[i] = new char[50];
            }
            _currentDirection = GetInitialDirection();
            _head = InitializeSnake(3, 25, 25);
            _matrix = MapSnake(_matrix, _head);
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Green;
        }
        internal static readonly char[] _topBottomRow = ['▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣', '▣',];
        internal static readonly char[] _emptyRow = ['▣', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '▣',];
        internal LinkNode _head;
        internal char[][] _matrix;
        internal const char _singleBox = '▣';
        internal Direction _currentDirection;
        internal const int _snakeSize = 3;
        static void Main(string[] args)
        {
            Program program = new Program();
            while (true)
            {
                program.ReRender();
                program._head = program.MoveRight(program._head);
                program._matrix = new char[50][];
                for (int i = 0; i < 50; i++)
                {
                    program._matrix[i] = new char[50];
                    if (i != 0 && i != 49)
                        Array.Copy(_emptyRow, program._matrix[i], _emptyRow.Length);
                    else
                        Array.Copy(_topBottomRow, program._matrix[i], _topBottomRow.Length);
                }
                program._matrix = program.MapSnake(program._matrix, program._head);
                Thread.Sleep(200);
            }
        }
        public Direction GetInitialDirection()
        {
            return Direction.Right;
        }

        public LinkNode InitializeSnake(int size, int row, int column)
        {
            if (size == 0)
                return null;
            return new LinkNode
            {
                Value = new Point
                {
                    Row = row,
                    Column = column - (column - size)
                },
                Next = InitializeSnake(size - 1, row, column)
            };
        }
        public char[][] MapSnake(char[][] matrix, LinkNode head)
        {
            if (this._currentDirection == Direction.None)
                throw new Exception("Invalid snake direction.");
            LinkNode current = head;
            if (_currentDirection == Direction.Right)
                while (current != null)
                {
                    Point point = current.Value;
                    matrix[point.Row][point.Column] = _singleBox;
                    current = current.Next;
                }
            return matrix;
        }

        public LinkNode MoveRight(LinkNode head)
        {
            if (head.Value.Column == 49)
                throw new Exception("Dump");

            LinkNode _new = new LinkNode
            {
                Value = new Point
                {
                    Row = head.Value.Row,
                    Column = head.Value.Column + 1
                }
            };
            _new.Next = head;

            LinkNode current = head;
            LinkNode previous = head;
            while (current != null)
            {
                if (current.Next == null)
                    break;
                previous = current;
                current = current.Next;
            }
            previous.Next = null;
            return _new;
        }
        public void ReRender()
        {
            Console.Clear();
            Console.Clear();
            Console.Clear();

            Console.Clear();
            Render();
        }
        public void Render()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _matrix.Length; i++)
            {
                stringBuilder.Append(new string(_matrix[i]));
                stringBuilder.Append('\n');
            }
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
