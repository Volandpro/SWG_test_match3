namespace Infrastructure.Misc
{
    internal static class GlobalCachedParameters
    {
        public static float CellScaleMod;
        public static float CellWidth;
        public static readonly int NeededCountInRow = 3;
        public static int CountObstacles = 3;
        public static int FieldLength = 6;
        public static int FieldHeight = 6;
        public static int ScoresForAdditionalCells = 5;
        public static int BaseScoreForRow = 10;
        public static float CellsMoveSpeed=1.5f;
    }
}