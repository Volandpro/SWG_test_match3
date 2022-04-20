using Infrastructure.Misc;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFieldPositionCalculator
    {
        private float cellWidth;
        private float endPointX;
        private float endPointY;
        private float startPointX;
        private float startPointY;
        private float scaleMod;

        private const float offsetX=15;
        private const float offsetY=25;
        
        public void CalculateSize(int fieldWidth, int fieldHeight, float cellWidth)
        {
            if (this.cellWidth == 0)
            {
                this.cellWidth = cellWidth;
                CalculateTopRightPoint();
                CalculateBottomLeftPoint();
                CalculateCellSize(fieldWidth, fieldHeight);
            }
        }

        private void CalculateCellSize(int fieldWidth, int fieldHeight)
        {
            scaleMod = (endPointX - startPointX) / (fieldWidth - 1) / cellWidth;
            if ((endPointY - startPointY) / (fieldHeight - 1) / cellWidth < scaleMod)
            {
                scaleMod = (endPointY - startPointY) / (fieldHeight - 1) / cellWidth;
                startPointX += (endPointX - startPointX - (fieldWidth - 1) * scaleMod * cellWidth) / 2f;
            }
            else
                startPointY += (endPointY - startPointY - (fieldHeight - 1) * scaleMod * cellWidth) / 2f;

            GlobalCachedParameters.CellScaleMod = scaleMod;
            GlobalCachedParameters.CellWidth = cellWidth;
        }

        private void CalculateBottomLeftPoint()
        {
            startPointX = Camera.main.ScreenToWorldPoint(new Vector3(offsetX * Screen.width / 100, 0)).x;
            startPointY = Camera.main.ScreenToWorldPoint(new Vector3(0, offsetY * Screen.height / 100)).y;
        }

        private void CalculateTopRightPoint()
        {
            endPointX = Camera.main.ScreenToWorldPoint(new Vector2((100 - offsetX) * Screen.width / 100, 0)).x;
            endPointY = Camera.main.ScreenToWorldPoint(new Vector3(0, (100 - offsetY) * Screen.height / 100)).y;
        }

        public Vector3 CalculatePosition(int i, int j)
        {
            return new Vector3(startPointX + i * cellWidth * scaleMod,
                startPointY + j * cellWidth * scaleMod);
        }

        public Vector3 ReturnScale()
        {
            return Vector3.one*scaleMod;
        }
    }
}