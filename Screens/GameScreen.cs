using System.Collections.Generic;
using GDPlatformer.Character;
using GDPlatformer.Character.Base;
using GDPlatformer.Gameplay;
using GDPlatformer.Gameplay.Base;
using GDPlatformer.Managers;
using GDPlatformer.Managers.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Screens
{
  public class GameScreen: Screen
  {
    #region Properties
    public Player Player;
    private HUD hud;
    private Level level;
    private List<Enemy> enemies;
    #endregion

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      level = new Level();
      level.LoadContent(content);
      hud = new HUD();
      hud.LoadContent();
      Player = new Player(new Vector2(100, 100));
      Player.LoadContent();
      enemies = new List<Enemy>();
      // list.RemoveAt(i);
      enemies.Add(new Bee(new Vector2(400, 430)));
      enemies.Add(new Bee(new Vector2(80, 430)));
      foreach (Enemy enemie in enemies)
      {
        enemie.LoadContent();
        CollisionManager.Instance.AddEnemyCollider(enemie);
      }

    }

    public override void UnloadContent()
    {
      base.UnloadContent();
      Player.UnloadContent();
      hud.UnloadContent();
      foreach (Enemy enemie in enemies)
      {
        enemie.UnloadContent();
      }
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      Player.Update(gameTime);
      hud.UpdateHealth(Player.GetHealth());
      hud.Update(gameTime);
      foreach (Enemy enemie in enemies)
      {
        enemie.Update(gameTime);
      }

    }

    public void Draw(SpriteBatch spriteBatch, Camera camera)
    {
      base.Draw(spriteBatch);
      level.Draw(spriteBatch);
      Player.Draw(spriteBatch);
      hud.Draw(spriteBatch, camera);
      foreach (Enemy enemie in enemies)
      {
        enemie.Draw(spriteBatch);
      }

    }
    #endregion
  }
}
