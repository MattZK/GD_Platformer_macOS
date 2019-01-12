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
    private static ScreenManager instance;

    public Vector2 Dimensions { private set; get; }
    public ContentManager Content { private set; get; }
    public Screen CurrentScreen;

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

    public ScreenManager() {
      Dimensions = new Vector2(1200, 700);
      CurrentScreen = new GameScreen();
    }

    public void LoadContent(ContentManager Content) {
      this.Content = new ContentManager(Content.ServiceProvider, "Content");
      CurrentScreen.LoadContent();
    }

    public void UnloadContent() { CurrentScreen.UnloadContent(); }

    public void Update(GameTime gameTime) { CurrentScreen.Update(gameTime); }

    public void Draw(SpriteBatch spriteBatch) { CurrentScreen.Draw(spriteBatch); }
  }
}
