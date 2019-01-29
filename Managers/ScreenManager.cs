using System;
using GDPlatformer.Managers.Base;
using GDPlatformer.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Managers
{
  public class ScreenManager
  {
    #region Singleton Properties
    private static ScreenManager instance;
    public static ScreenManager Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new ScreenManager();
        }
        return instance;
      }
    }
    #endregion

    #region Properties
    public Vector2 Dimensions { private set; get; }
    public ContentManager Content { private set; get; }
    public Screen CurrentScreen;
    public bool isGameOver;
    #endregion

    #region Constructor
    public ScreenManager() {
      Dimensions = new Vector2(1200, 700);
      CurrentScreen = new MenuScreen();
    }
    #endregion

    #region Game Methods
    public void LoadContent(ContentManager Content) {
      this.Content = new ContentManager(Content.ServiceProvider, "Content");
      CurrentScreen.LoadContent();
    }

    public void UnloadContent() { CurrentScreen.UnloadContent(); }

    public void Update(GameTime gameTime) {
      CurrentScreen.Update(gameTime);
      isGameOver = CurrentScreen.isGameOver;
    }

    public void Draw(SpriteBatch spriteBatch) { CurrentScreen.Draw(spriteBatch); }
    #endregion
  }
}
