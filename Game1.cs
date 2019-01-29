using System;
using GDPlatformer.Gameplay.Base;
using GDPlatformer.Managers;
using GDPlatformer.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDPlatformer.MacOS
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    #region Properties
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Camera camera;
    GameScreen gameScreen;
    Color background;
    Matrix currentViewMatrix;
    #endregion

    #region Constructor
    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsFixedTimeStep = false;
    }
    #endregion

    #region Game Methods
    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      base.Initialize();
      graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
      graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
      graphics.ApplyChanges();
      camera = new Camera(GraphicsDevice.Viewport);
      background = new Color(208, 244, 247);
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      base.LoadContent();
      spriteBatch = new SpriteBatch(GraphicsDevice);
      ScreenManager.Instance.LoadContent(Content);
    }

    protected override void UnloadContent()
    {
      base.UnloadContent();
      ScreenManager.Instance.UnloadContent();
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      base.Update(gameTime);
      ScreenManager.Instance.Update(gameTime);
      // TODO: Fix Preformance
      if (ScreenManager.Instance.CurrentScreen is GameScreen)
      {
        gameScreen = (GameScreen)ScreenManager.Instance.CurrentScreen;
        camera.SetReference(gameScreen.Player);
      }
      currentViewMatrix = camera.GetViewMatrix();
      if (ScreenManager.Instance.isGameOver) Exit();
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      base.Draw(gameTime);
      graphics.GraphicsDevice.Clear(background);
      spriteBatch.Begin(transformMatrix: currentViewMatrix);

      if (ScreenManager.Instance.CurrentScreen is GameScreen)
      {
        gameScreen.Draw(spriteBatch, camera);
      }
      else
      {
        ScreenManager.Instance.Draw(spriteBatch);
      }

      spriteBatch.End();
    }
    #endregion
  }
}
