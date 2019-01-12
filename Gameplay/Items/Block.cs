using System;
using GDPlatformer.Gameplay.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Gameplay.Items
{
  public class Block : ICollide
  {
    private Texture2D _texture { get; set; }
    private Vector2 _frame { get; set; }
    private Vector2 _position { get; set; }

    public Block(Texture2D texture, Vector2 pos, Vector2 frame)
    {
      _texture = texture;
      _position = pos;
      _frame = frame;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(_texture, _position, new Rectangle((int)_frame.X, (int)_frame.Y, 70, 70), Color.White);
    }

    public Rectangle GetCollisionRectangle()
    {
      return new Rectangle((int)_position.X, (int)_position.Y, 70, 70);
    }
  }
}