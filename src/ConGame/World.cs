namespace ConGame
{
    public class World
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ShapeSize { get; set; }
        public int CurrentShapeX { get; set; }
        public int CurrentShapeY { get; set; }
        public int CurrentShapeIndex { get; set; }
        public int CurrentShapeState { get; set; }
        public bool[][][][] Shapes { get; set; }
        public bool[][] Buffer { get; set; }
    }
}
