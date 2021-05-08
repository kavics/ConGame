using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ConGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var width = 20;
            var height = Console.WindowHeight;

            var world = new World
            {
                Width = width,
                Height = height,
                ShapeSize = Drawings.Size,
                CurrentShapeIndex = 1,
                CurrentShapeState = 1,
                CurrentShapeX = 17,
                CurrentShapeY = 0,
                Shapes = Drawings.CreateShapes(),
                Buffer = InitializeBuffer(width, height)
            };

            InitializeScreen(width, height);
            var step = -1;
            while (true)
            {
                var input = HandleInput();
                if (input == KeyboardInput.Exit)
                    break;
                CalculateNext(input, world, width, height, ++step);
                Draw(world, width, height);
                Task.Delay(20).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }


        private static KeyboardInput HandleInput()
        {
            if (Console.KeyAvailable)
            {
                var x = Console.ReadKey(true);
                switch (x.Key)
                {
                    case ConsoleKey.LeftArrow: return KeyboardInput.Left;
                    case ConsoleKey.RightArrow: return KeyboardInput.Right;
                    case ConsoleKey.UpArrow: return KeyboardInput.RotateLeft;
                    case ConsoleKey.DownArrow: return KeyboardInput.RotateRight;
                    case ConsoleKey.Spacebar: return KeyboardInput.Drop;
                    case ConsoleKey.Escape: return KeyboardInput.Exit;
                }
            }
            return KeyboardInput.Nothing;
        }

        private static void CalculateNext(KeyboardInput input, World world, int width, int height, int step)
        {
            switch (input)
            {
                case KeyboardInput.Left:
                    if(CanMove(world , false))
                        world.CurrentShapeX--;
                    break;
                case KeyboardInput.Right:
                    if (CanMove(world, true))
                        world.CurrentShapeX++;
                    break;
                case KeyboardInput.RotateLeft:
                    world.CurrentShapeState = (world.CurrentShapeState + 3) % 4;
                    break;
                case KeyboardInput.RotateRight:
                    world.CurrentShapeState = (world.CurrentShapeState + 1) % 4;
                    break;
                case KeyboardInput.Drop:
                    break;
            }
            //var buffer = world.Buffer;
            //for (int y = 0; y < buffer.Length; y++)
            //    for (int x = 0; x < buffer[y].Length; x++)
            //        buffer[y][x] = (y + x) % 2 == step % 2;
        }

        private static bool CanMove(World world, bool toRight)
        {
            var shape = world.Shapes[world.CurrentShapeIndex][world.CurrentShapeState];

            if (toRight)
                return world.CurrentShapeX + shape.OffsetRight < world.Width - 1;
            return world.CurrentShapeX + shape.OffsetLeft > 0;
        }

        private static void Draw(World world, int width, int height)
        {
            var buffer = world.Buffer;
            var shape = world.Shapes[world.CurrentShapeIndex][world.CurrentShapeState];

            var lines = new string[height];
            for (int y = 0; y < height; y++)
            {
                var line = new char[width * 2];
                for (int x = 0; x < width; x++)
                {
                    var c = buffer[y][x] ? '@' : ' ';
                    line[2 * x] = c;
                    line[2 * x + 1] = c;
                }

                if (y >= world.CurrentShapeY && y < world.CurrentShapeY + world.ShapeSize)
                {
                    for (int x = 0; x < world.ShapeSize; x++)
                    {
                        if (shape.Cells[y - world.CurrentShapeY][x])
                        {
                            line[2 * (x + world.CurrentShapeX)] = '@';
                            line[2 * (x + world.CurrentShapeX) + 1] = '@';
                        }
                    }
                }

                lines[y] = new string(line);
            }

            for (int y = 0; y < lines.Length; y++)
            {
                Console.CursorTop = y;
                Console.CursorLeft = 10;
                Console.Write(lines[y]);
            }
        }

        private static bool[][] InitializeBuffer(int width, int height)
        {
            var buffer = new bool[height][];
            for (int i = 0; i < height; i++)
                buffer[i] = new bool[width];
            return buffer;
        }
        private static void InitializeScreen(int width, int height)
        {
            var buffer = new bool[height][];
            for (int y = 0; y < height; y++)
            {
                Console.CursorTop = y;
                Console.CursorLeft = 9;
                Console.Write('|');
                Console.CursorLeft = 10 + width * 2;
                Console.Write('|');
            }
        }
    }
}
