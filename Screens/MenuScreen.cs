using System;
using GDPlatformer.Managers;
using GDPlatformer.Managers.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDPlatformer.Screens
{
  public class MenuScreen: Screen
  {
    #region Properties
    SpriteFont arialFont;
    #endregion

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      arialFont = content.Load<SpriteFont>("Fonts/ArialBig");
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      Vector2 position = new Vector2((ScreenManager.Instance.Dimensions.X / 2), (ScreenManager.Instance.Dimensions.Y / 2));
      Vector2 origin = new Vector2((arialFont.MeasureString("Menu Screen").X / 2), (arialFont.MeasureString("Menu Screen").Y / 2));
      spriteBatch.DrawString(arialFont, "Menu Screen", position, Color.White, 0, origin, 2f, SpriteEffects.None, 0);
      base.Draw(spriteBatch);

      KeyboardState keyboardState = Keyboard.GetState();

      if (keyboardState.IsKeyDown(Keys.Enter))
      {
        ScreenManager.Instance.CurrentScreen.UnloadContent();
        ScreenManager.Instance.CurrentScreen = new GameScreen(0);
        ScreenManager.Instance.CurrentScreen.LoadContent();
      }
    }
    #endregion
  }
}
