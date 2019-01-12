using System;
using Microsoft.Xna.Framework;

namespace GDPlatformer.Gameplay.Collision
{
  public interface ICollide
  {
    Rectangle GetCollisionRectangle();
  }
}
