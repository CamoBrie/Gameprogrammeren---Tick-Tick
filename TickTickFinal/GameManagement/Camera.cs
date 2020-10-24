using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

static class Camera
{
    private static Vector2 TopLeftPosition;
    private static Point LevelSize, ScreenSize;
    public static Matrix GetTranslation()
    { 
        return Matrix.CreateTranslation(-TopLeftPosition.X, -TopLeftPosition.Y, 0);
    }

    public static void Update(Vector2 PlayerPosition)
    {

        //clamp the camera to the current playing field, so that we dont see outside of the playing field.
        Vector2 clampedPosition = Vector2.Clamp(PlayerPosition - ScreenSize.ToVector2() / 2, Vector2.Zero,
            LevelSize.ToVector2() - ScreenSize.ToVector2());

        TopLeftPosition = clampedPosition;
        
    }

    public static void SetLevelSize(int cellWidth, int cellHeight)
    {
        LevelSize = new Point(cellWidth, cellHeight);
    }

    public static void SetScreenSize(int sX, int sY)
    {
        ScreenSize = new Point(sX, sY);
    }
}

