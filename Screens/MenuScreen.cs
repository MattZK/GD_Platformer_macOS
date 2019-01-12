using System;
using GDPlatformer.Managers;
using GDPlatformer.Managers.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Screens
{
  public class MenuScreen: Screen
  {
    SpriteFont arialFont;

    public override void LoadContent()
    {
      base.LoadContent();
      arialFont = content.Load<SpriteFont>("Fonts/Arial");
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
      spriteBatch.DrawString(arialFont, "Menu Screen", position, Color.White, 0, origin, 4f, SpriteEffects.None, 0);
      base.Draw(spriteBatch);
    }
  }
}
