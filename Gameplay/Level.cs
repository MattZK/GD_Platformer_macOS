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
    public Texture2D levelDecorators;

    private List<List<int>> _level = new List<List<int>>() {
      new List<int>{9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9 },
      new List<int>{9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,3,0,0,0,0,0,0,0,0,0,0,0,0,9 },
      new List<int>{9,0,0,2,3,0,0,0,0,0,0,2,3,0,0,0,0,0,0,0,0,0,2,1,3,0,0,0,0,0,0,9 },
      new List<int>{9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9 },
      new List<int>{9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9 },
      new List<int>{1,1,1,1,1,1,6,0,0,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
    };

    private List<List<int>> _prop = new List<List<int>>() {
      new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
      new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
      new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
      new List<int>{0,0,7,8,0,0,0,0,0,0,0,0,0,6,0,0,1,0,0,5,0,0,2,0,0,0,3,0,0,4,0,0 },
      new List<int>{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
    };

    private Block[,] _blocks;
    private Prop[,] _props;
    #endregion

    #region Game Methods
    public void LoadContent(ContentManager content)
    {
      LoadTextures(content);
      GenerateLevel();
      GenerateProps();
    }

    public void Draw(SpriteBatch spriteBatch)
    {

      DrawBackground(spriteBatch, 5);
      DrawBlocks(spriteBatch);
      DrawProps(spriteBatch);
    }
    #endregion

    #region Generation & Loading Methods
    private void LoadTextures(ContentManager content)
    {
      texture = content.Load<Texture2D>("Items/grass_blocks");
      background = content.Load<Texture2D>("Background/blue");
      levelDecorators = content.Load<Texture2D>("Items/props");
    }
    private void GenerateLevel()
    {
      float height = ScreenManager.Instance.Dimensions.Y;
      _blocks = new Block[_level.Count, _level[_level.Count - 1].Count];
      for (int x = 0; x < _level.Count; x++)
      {
        for (int y = 0; y < _level[x].Count; y++)
        {
          int v = (int)height - (_level.Count - x) * 70;
          int h = y * 70 - 70;
          switch (_level[x][y])
          {
            case 1:
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(280, 210));
              break;
            case 2:
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(280, 280));
              break;
            case 3:
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(280, 140));
              break;
            case 4:
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(0, 350));
              break;
            case 5:
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(280, 70));
              break;
            case 6:
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(140, 280));
              break;
            case 7:
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(140, 350));
              break;
            case 9:
              // Invisible
              _blocks[x, y] = new Block(texture, new Vector2(h, v), new Vector2(350, 350));
              break;
          }
          if (_level[x][y] != 0)
            CollisionManager.Instance.AddLevelCollider(_blocks[x, y]);
        }
      }
    }
    private void GenerateProps()
    {
      float height = ScreenManager.Instance.Dimensions.Y;
      _props = new Prop[_prop.Count, _prop[_prop.Count - 1].Count];
      for (int x = 0; x < _prop.Count; x++)
      {
        for (int y = 0; y < _prop[x].Count; y++)
        {
          int v = (int)height - (_prop.Count - x) * 70;
          int h = y * 70 - 70;
          switch (_prop[x][y])
          {
            // Grass
            case 1:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(384, 640));
              break;
            // Stone
            case 2:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(256, 640));
              break;
            // Cactus
            case 3:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(384, 1664));
              break;
            // Arrow Right
            case 4:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(256, 128));
              break;
            // Bush
            case 5:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(384, 1792));
              break;
            // Mushroom
            case 6:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(256, 896));
              break;
            // Fence
            case 7:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(384, 896));
              break;
            // Fence Broken
            case 8:
              _props[x, y] = new Prop(levelDecorators, new Vector2(h, v), new Vector2(384, 768));
              break;
          }
        }
      }
    }
    #endregion

    #region Draw Methods
    private void DrawBackground(SpriteBatch spriteBatch, int times = 1) {
      float hScale = ScreenManager.Instance.Dimensions.Y / 1024;
      for (int i = 0; i < times; i++)
      {
        spriteBatch.Draw(background, new Vector2(i * 1024 * hScale, 0), null, Color.White, 0f, Vector2.Zero, hScale, SpriteEffects.None, 0f);
      }
    }
    private void DrawBlocks(SpriteBatch spriteBatch)
    {
      for (int x = 0; x < _level.Count; x++)
      {
        for (int y = 0; y < _level[x].Count; y++)
        {
          if (_blocks[x, y] != null)
            _blocks[x, y].Draw(spriteBatch);
        }
      }
    }
    private void DrawProps(SpriteBatch spriteBatch)
    {
      for (int x = 0; x < _prop.Count; x++)
      {
        for (int y = 0; y < _prop[x].Count; y++)
        {
          if (_props[x, y] != null)
            _props[x, y].Draw(spriteBatch);
        }
      }
    }
    #endregion
  }
}
