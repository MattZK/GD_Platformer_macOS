using System;
using GDPlatformer.Gameplay.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Gameplay.Items
{
  public class Coin: ICollide
  {
    private readonly Texture2D _texture;
    private readonly Vector2 _frame;
    private readonly Vector2 _position;
    private readonly int _value;
    private bool isCollected;

    public Coin(Texture2D texture, Vector2 pos, Vector2 frame, int value)
    {
      _texture = texture;
      _position = pos;
      _frame = frame;
      _value = value;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if(!isCollected)
        spriteBatch.Draw(_texture, _position, new Rectangle((int)_frame.X, (int)_frame.Y, 128, 128), Color.White, 0f, Vector2.Zero, 0.546f, SpriteEffects.None, 0f);
    }

    public int GetValue()
    {
      if(!isCollected)
        return _value;
      return 0;
    }

    public void Collect() {
      isCollected = true;
    }

    public Rectangle GetCollisionRectangle()
    {
      return new Rectangle((int)_position.X, (int)_position.Y, 70, 70);
    }
  }
}
