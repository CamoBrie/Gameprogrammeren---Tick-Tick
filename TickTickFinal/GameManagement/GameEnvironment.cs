using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class GameEnvironment : Game
{
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    protected InputHelper inputHelper;
    protected Matrix spriteScale;
    protected Point windowSize;

    protected static Point screen;
    protected static GameStateManager gameStateManager;
    protected static Random random;
    protected static AssetManager assetManager;
    protected static GameSettingsManager gameSettingsManager;

    //Total levels:
    public static int LevelAmount = 11;

    public GameEnvironment()
    {
        graphics = new GraphicsDeviceManager(this);

        inputHelper = new InputHelper();
        gameStateManager = new GameStateManager();
        spriteScale = Matrix.CreateScale(1, 1, 1);
        random = new Random();
        assetManager = new AssetManager(Content);
        gameSettingsManager = new GameSettingsManager();
    }

    public static Point Screen
    {
        get { return screen; }
        set { screen = value; }
    }

    public static Random Random
    {
        get { return random; }
    }

    public static AssetManager AssetManager
    {
        get { return assetManager; }
    }

    public static GameStateManager GameStateManager
    {
        get { return gameStateManager; }
    }

    public static GameSettingsManager GameSettingsManager
    {
        get { return gameSettingsManager; }
    }

    public bool FullScreen
    {
        get { return graphics.IsFullScreen; }
        set
        {
            ApplyResolutionSettings(value);
        }
    }

    public void ApplyResolutionSettings(bool fullScreen = false)
    {
        if (!fullScreen)
        {
            graphics.PreferredBackBufferWidth = windowSize.X;
            graphics.PreferredBackBufferHeight = windowSize.Y;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }
        else
        {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        float targetAspectRatio = (float)screen.X / (float)screen.Y;
        int width = graphics.PreferredBackBufferWidth;
        int height = (int)(width / targetAspectRatio);
        if (height > graphics.PreferredBackBufferHeight)
        {
            height = graphics.PreferredBackBufferHeight;
            width = (int)(height * targetAspectRatio);
        }

        Viewport viewport = new Viewport();
        viewport.X = (graphics.PreferredBackBufferWidth / 2) - (width / 2);
        viewport.Y = (graphics.PreferredBackBufferHeight / 2) - (height / 2);
        viewport.Width = width;
        viewport.Height = height;
        GraphicsDevice.Viewport = viewport;

        inputHelper.Scale = new Vector2((float)GraphicsDevice.Viewport.Width / screen.X,
                                        (float)GraphicsDevice.Viewport.Height / screen.Y);
        inputHelper.Offset = new Vector2(viewport.X, viewport.Y);
        spriteScale = Matrix.CreateScale(inputHelper.Scale.X, inputHelper.Scale.Y, 1);
    }

    protected override void LoadContent()
    {
        DrawingHelper.Initialize(this.GraphicsDevice);
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    // update the camera matrix translation
    public void UpdateCamera()
    {;
        PlayingState CurrentState = GameStateManager.CurrentGameState as PlayingState;
        if (CurrentState is PlayingState || GameStateManager.CurrentGameState is GameOverState || GameStateManager.CurrentGameState is LevelFinishedState)
        {
            Camera.SetScreenSize(screen.X, screen.Y);
            if (CurrentState is PlayingState)
            {
                Camera.Update(CurrentState.CurrentLevel.Find("player").Position);
            }
            spriteScale = Camera.GetTranslation() * Matrix.CreateScale(inputHelper.Scale.X, inputHelper.Scale.Y, 1);
        }
        else
        {
            spriteScale = Matrix.CreateScale(inputHelper.Scale.X, inputHelper.Scale.Y, 1);
        }
    }

    protected void HandleInput()
    {
        inputHelper.Update();
        if (inputHelper.KeyPressed(Keys.Escape))
        {
            Exit();
        }
        if (inputHelper.KeyPressed(Keys.F5))
        {
            FullScreen = !FullScreen;
        }
        gameStateManager.HandleInput(inputHelper);
    }

    protected override void Update(GameTime gameTime)
    {
        HandleInput();
        gameStateManager.Update(gameTime);
        UpdateCamera();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(72, 130, 255));
        spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);
        gameStateManager.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}