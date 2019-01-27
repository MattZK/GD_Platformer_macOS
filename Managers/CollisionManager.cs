﻿using System;
using System.Collections.Generic;
using GDPlatformer.Character.Base;
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
    private List<Enemy> enemyColliders;
    #endregion

    #region Methods
    CollisionManager()
    {
      levelColliders = new List<ICollide>();
      enemyColliders = new List<Enemy>();
    }

    public void AddLevelCollider(ICollide levelCollider)
    {
      levelColliders.Add(levelCollider);
    }

    public List<ICollide> GetLevelColliders() {
      return levelColliders;
    }


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
  }
}
