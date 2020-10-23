using Microsoft.Xna.Framework;
using System;

static class Camera
{
    private static Vector2 Position, CenteredPosition;
    private static Point LevelSize;
    public static Matrix GetTranslation()
    { 
        return Matrix.CreateTranslation(-CenteredPosition.X, -CenteredPosition.Y, 0);
    }

    public static void Update(Vector2 PlayerPosition)
    {
        Position = PlayerPosition;
        CenteredPosition = new Vector2(Position.X - TickTick.Screen.X / 2, Position.Y - TickTick.Screen.Y / 2);
        
    }

    public static void SetLevelSize(int cellWidth, int cellHeight)
    {
        LevelSize = new Point(cellWidth, cellHeight);
    }
}

