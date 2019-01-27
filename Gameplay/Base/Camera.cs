using System;
using GDPlatformer.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Gameplay.Base
{
  public class Camera
  {
    private Viewport _viewport;
    private Vector2 _position;
    private Player _reference { get; set; }

    public Camera(Viewport viewport)
    {
      _viewport = viewport;
    }

    public void SetReference(Player reference)
    {
      _reference = reference;
    }

    public Matrix GetViewMatrix()
    {
      float y = 0;
      _position = new Vector2(0, 0);
      if(_reference != null) {
        if (_reference.Position.Y < 100)
          y = _reference.Position.Y - 100;
        if (_reference.Position.X > 2100 - _viewport.Width * .5f - _reference.Dimensions.X * .5f)
          _position = new Vector2(2100 - _viewport.Width, y);
        else if (_reference.Position.X > _viewport.Width * .5f - _reference.Dimensions.X * .5f)
          _position = new Vector2(_reference.Position.X + _reference.Dimensions.X * .5f - _viewport.Width * .5f, y);
        else
          _position = new Vector2(0, y);
      }
      // Fix Tearing
      _position.X = (float)Math.Round(_position.X);
      return Matrix.CreateTranslation(new Vector3(-_position, 0)) * Matrix.CreateRotationZ(0) * Matrix.CreateScale(1, 1, 1);
    }

    public Vector2 GetOrigin()
    {
      return _position;
    }

    public Viewport GetViewPort()
    {
      return _viewport;
    }

  }
}
