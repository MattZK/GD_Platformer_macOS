using System;
using System.Collections.Generic;
using GDPlatformer.Character.Base;
using GDPlatformer.Gameplay.Collision;
using GDPlatformer.Gameplay.Items;

namespace GDPlatformer.Managers
{
  public class CollisionManager
  {
    #region Singleton Properties
    private static CollisionManager instance;
    public static CollisionManager Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new CollisionManager();
        }
        return instance;
      }
    }
    #endregion

    #region Properties
    private List<ICollide> levelColliders;
    private List<Enemy> enemyColliders;
    private List<Coin> coinColliders;
    #endregion

    CollisionManager()
    {
      Reset();
    }

    public void Reset()
    {
      levelColliders = new List<ICollide>();
      enemyColliders = new List<Enemy>();
      coinColliders = new List<Coin>();
    }

    #region Level Methods
    public void AddLevelCollider(ICollide levelCollider)
    {
      levelColliders.Add(levelCollider);
    }

    public List<ICollide> GetLevelColliders()
    {
      return levelColliders;
    }
    #endregion

    #region Enemy Methods
    public void AddEnemyCollider(Enemy enemy)
    {
      enemyColliders.Add(enemy);
    }
    public void RemoveEnemyCollider(Enemy enemy)
    {
      for (int i = 0; i < enemyColliders.Count; i++)
      {
        if (enemyColliders[i] == enemy)
          enemyColliders.RemoveAt(i);
      }
    }
    public List<Enemy> GetEnemyColliders()
    {
      return enemyColliders;
    }
    #endregion

    #region Coins Methods
    public void AddCoinCollider(Coin coin)
    {
      coinColliders.Add(coin);
    }
    public void RemoveCoinCollider(Coin coin)
    {
      for (int i = 0; i < coinColliders.Count; i++)
      {
        if (coinColliders[i] == coin)
          coinColliders.RemoveAt(i);
      }
    }
    public List<Coin> GetCoinColliders()
    {
      return coinColliders;
    }
    #endregion
  }
}
