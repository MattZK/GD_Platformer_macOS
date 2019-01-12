using System;
using GDPlatformer.Gameplay;
using GDPlatformer.Managers.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Screens
{
  public class GameScreen: Screen
  {
    #region Properties
    Level level;
    #endregion

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      level = new Level();
      level.LoadContent(content);
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
      base.Draw(spriteBatch);
      level.Draw(spriteBatch);
    }
    #endregion
  }
}
