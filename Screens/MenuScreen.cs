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
      Vector2 pos = new Vector2(ScreenManager.Instance.Dimensions.X / 2, 0);
      spriteBatch.DrawString(arialFont, "Menu Screen", pos, Color.White);
      base.Draw(spriteBatch);
    }
  }
}
