using System;
using System.Collections.Generic;
using GDPlatformer.Gameplay.Items;
using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Gameplay
{
  public class Level
  {
    #region Properties
    public Texture2D texture;
    public Texture2D background;

    private List<List<int>> _level = new List<List<int>>() {
      new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,3 },
      new List<int>{1 },
      new List<int>{4,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,3,0,0,0,0,0 },
      new List<int>{4,0,0,0,0,0 },
      new List<int>{4,0,0,0,0,0 },
      new List<int>{4,1,1,1,1,1,6,0,0,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4 }
    };

    private Block[,] _blocks;
    #endregion

    #region Game Methods
    public void LoadContent(ContentManager content)
    {
      float height = ScreenManager.Instance.Dimensions.Y;
      texture = content.Load<Texture2D>("Items/grass_blocks");
      background = content.Load<Texture2D>("Background/blue");
      _blocks = new Block[_level.Count, _level[_level.Count - 1].Count];
      for (int x = 0; x < _level.Count; x++)
      {
        for (int y = 0; y < _level[x].Count; y++)
        {
          int v = (int)height - (_level.Count - x) * 70;
          switch (_level[x][y])
          {
            case 1:
              _blocks[x, y] = new Block(texture, new Vector2(y * 70, v), new Vector2(280, 210));
              break;
            case 2:
              _blocks[x, y] = new Block(texture, new Vector2(y * 70, v), new Vector2(280, 280));
              break;
            case 3:
              _blocks[x, y] = new Block(texture, new Vector2(y * 70, v), new Vector2(280, 140));
              break;
            case 4:
              _blocks[x, y] = new Block(texture, new Vector2(y * 70, v), new Vector2(0, 350));
              break;
            case 5:
              _blocks[x, y] = new Block(texture, new Vector2(y * 70, v), new Vector2(280, 70));
              break;
            case 6:
              _blocks[x, y] = new Block(texture, new Vector2(y * 70, v), new Vector2(140, 280));
              break;
            case 7:
              _blocks[x, y] = new Block(texture, new Vector2(y * 70, v), new Vector2(140, 350));
              break;
          }
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      float hScale = ScreenManager.Instance.Dimensions.Y / 1024;
      for (int i = 0; i < 5; i++)
      {
        spriteBatch.Draw(background, new Vector2(i * 1024 * hScale, 0), null, Color.White, 0f, Vector2.Zero, hScale, SpriteEffects.None, 0f);
      }
      for (int x = 0; x < _level.Count; x++)
      {
        for (int y = 0; y < _level[x].Count; y++)
        {
          if (_blocks[x, y] != null)
            _blocks[x, y].Draw(spriteBatch);
        }
      }
    }
    #endregion
  }
}
