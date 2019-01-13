using System;
using GDPlatformer.Character;
using GDPlatformer.Gameplay;
using GDPlatformer.Managers.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Screens
{
  public class GameScreen: Screen
  {
    #region Properties
    public Player Player;
    private Level level;
    #endregion

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      level = new Level();
      level.LoadContent(content);
      Player = new Player(new Vector2(100, 546));
      Player.LoadContent();
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
      Player.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      Player.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      level.Draw(spriteBatch);
      Player.Draw(spriteBatch);
    }
    #endregion
  }
}
