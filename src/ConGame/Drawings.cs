using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConGame
{
    public static class Drawings
    {
        public const int Size = 5;

        public static bool[][][][] CreateShapes()
        {
            var shapes = new bool[Lines.Length / (Size + 1)][][][];
            for (int shapeIndex = 0; shapeIndex < shapes.Length; shapeIndex++)
            {
                shapes[shapeIndex] = new bool[4][][];
                var startLineIndex = shapeIndex * (Size + 1);
                for (int i = 0; i < 4; i++)
                {
                    shapes[shapeIndex][i] = new bool[Size][];
                    for (int y = 0; y < Size; y++)
                    {
                        shapes[shapeIndex][i][y] = new bool[Size];
                        for (int x = 0; x < Size; x++)
                        {
                            var c = Lines[startLineIndex + y][((Size + 1) * i + x) * 2];
                            shapes[shapeIndex][i][y][x] = c != '.';
                        }
                    }
                }
            }

            return shapes;
        }

        private static readonly string[] Lines = 
        {
            "..........  ..........  ..........  ..........  ",
            "..........  ..........  ..........  ..........  ",
            "....&&....  ....&&....  ....&&....  ....&&....  ",
            "..........  ..........  ..........  ..........  ",
            "..........  ..........  ..........  ..........  ",
            "                                                ",
            "..........  ..........  ..........  ..........  ",
            "..........  ..@@@@....  ..@@......  ....@@@@..  ",
            "..@@&&@@..  ....&&....  ..@@&&@@..  ....&&....  ",
            "..@@......  ....@@....  ..........  ....@@....  ",
            "..........  ..........  ..........  ..........  ",
            "                                                ",
        };

        /*

        ..........
        ..@@@@@@..
        ....&&....
        ....@@....
        ..........

        ..........
        ....@@....
        ..@@&&@@..
        ....@@....
        ..........

        ....@@....
        ....@@....
        @@@@&&@@@@
        ....@@....
        ....@@....

         */
    }
}
