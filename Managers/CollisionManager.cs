using System;
using System.Collections.Generic;
using GDPlatformer.Gameplay.Collision;

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
    private List<ICollide> enemyColliders;
    #endregion

    #region Methods
    CollisionManager()
    {
      levelColliders = new List<ICollide>();
      enemyColliders = new List<ICollide>();
    }

    public void AddLevelCollider(ICollide levelCollider)
    {
      levelColliders.Add(levelCollider);
    }

    public List<ICollide> GetLevelColliders() {
      return levelColliders;
    }


    public void AddEnemyCollider(ICollide enemyCollider)
    {
      enemyColliders.Add(enemyCollider);
    }

    public List<ICollide> GetEnemyColliders()
    {
      return enemyColliders;
    }
    #endregion
  }
}
