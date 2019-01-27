using System.Collections.Generic;
using GDPlatformer.Character;
using GDPlatformer.Character.Base;
using GDPlatformer.Character.Enemies;
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
      enemies = new List<Enemy>
      {
        new Bee(new Vector2(400, 430)),
        new Bee(new Vector2(440, 430)),
        new Bee(new Vector2(80, 430))
      };
      for (int i = 0; i < enemies.Count; i++)
      {
        enemies[i].LoadContent();
        CollisionManager.Instance.AddEnemyCollider(enemies[i]);
      }
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
      Player.UnloadContent();
      hud.UnloadContent();
      foreach (Enemy enemy in enemies)
      {
        enemy.UnloadContent();
      }
      CollisionManager.Instance.Reset();
  }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      Player.Update(gameTime);
      hud.UpdateHealth(Player.GetHealth());
      hud.UpdateScore(Player.GetScore());
      hud.Update(gameTime);
      for (int i = 0; i < enemies.Count; i++)
      {
        enemies[i].Update(gameTime);
        if (enemies[i].IsDead())
        {
          CollisionManager.Instance.RemoveEnemyCollider(enemies[i]);
          enemies.RemoveAt(i);
        }
      }

    }

    public void Draw(SpriteBatch spriteBatch, Camera camera)
    {
      base.Draw(spriteBatch);
      level.Draw(spriteBatch);
      Player.Draw(spriteBatch);
      hud.Draw(spriteBatch, camera);
      foreach (Enemy enemy in enemies)
      {
        enemy.Draw(spriteBatch);
      }
      if (Player.IsDead()) hud.ShowGameOver(spriteBatch);
    }
    #endregion
  }
}
