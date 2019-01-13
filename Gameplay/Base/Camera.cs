using System;
using GDPlatformer.Character.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Gameplay.Base
{
  public class Camera
  {
    private Viewport _viewport;
    private Vector2 _position { get; set; }
    private Entity _reference { get; set; }

    public Camera(Viewport viewport)
    {
      _viewport = viewport;
    }

    public void SetReference(Entity reference)
    {
      _reference = reference;
    }

    public Matrix GetViewMatrix()
    {
      if(_reference != null) {
        if (_reference.Position.X > 2100 - _viewport.Width * .5f - _reference.Dimensions.X * .5f)
          _position = new Vector2(2100 - _viewport.Width, 0);
        else if (_reference.Position.X > _viewport.Width * .5f - _reference.Dimensions.X * .5f)
          _position = new Vector2(_reference.Position.X + _reference.Dimensions.X * .5f - _viewport.Width * .5f, 0);
        else
          _position = new Vector2(0, 0);
      }
      else
      {
        _position = new Vector2(0, 0);
      }
      return Matrix.CreateTranslation(new Vector3(-_position, 0)) * Matrix.CreateRotationZ(0) * Matrix.CreateScale(1, 1, 1);
    }
  }
}
