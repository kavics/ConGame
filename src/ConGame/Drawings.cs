using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConGame
{
    public static class Drawings
    {
        // ShapeIndex, ShapeState
        public static Shape[][] CreateShapes()
        {
            var shapes = new Shape[Lines.Length / (Shape.Size + 1)][];
            for (int shapeIndex = 0; shapeIndex < shapes.Length; shapeIndex++)
            {
                shapes[shapeIndex] = new Shape[4];
                var startLineIndex = shapeIndex * (Shape.Size + 1);
                for (int i = 0; i < 4; i++)
                {
                    var shape = new Shape {Cells = new bool[Shape.Size][]};
                    shapes[shapeIndex][i] = shape;
                    for (int y = 0; y < Shape.Size; y++)
                    {
                        shape.Cells[y] = new bool[Shape.Size];
                        for (int x = 0; x < Shape.Size; x++)
                        {
                            var c = Lines[startLineIndex + y][((Shape.Size + 1) * i + x) * 2];
                            shape.Cells[y][x] = c != '.';
                        }
                    }

                    SetOffsets(shape);
                }
            }
            return shapes;
        }

        private static void SetOffsets(Shape shape)
        {
            // shape.OffsetLeft is the first non-empty column on the left side
            for (int x = 0; x < Shape.Size; x++)
            {
                var found = false;
                for (int y = 0; y < Shape.Size; y++)
                {
                    if (shape.Cells[y][x])
                    {
                        shape.OffsetLeft = x;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }

            // shape.OffsetRight is the first non-empty column on the right side
            for (int x = Shape.Size - 1; x >= 0; x--)
            {
                var found = false;
                for (int y = 0; y < Shape.Size; y++)
                {
                    if (shape.Cells[y][x])
                    {
                        shape.OffsetRight = x;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
            }
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
