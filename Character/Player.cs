using System;
using System.Collections.Generic;
using GDPlatformer.Character.Animate;
using GDPlatformer.Character.Base;
using GDPlatformer.Gameplay.Collision;
using GDPlatformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GDPlatformer.Character
{
  public class Player : Entity
  {
    #region Properties
    public new Vector2 Position;
    public new readonly Vector2 Dimensions = new Vector2(70, 86);
    private Texture2D texture;
    private Animation walkAnimation;
    private readonly float speed = 300f;
    private Rectangle collisionRectangle;
    private KeyboardState keyboardState;
    // Movement
    private bool allowVerticalMovement;
    private bool isOnGround;
    #endregion

    public Player (Vector2 startPosition) {
      Position = startPosition;
      walkAnimation = new Animation();
      walkAnimation.AddFrame(new Rectangle(0, 339, 68, 83));
      walkAnimation.AddFrame(new Rectangle(0, 0, 70, 86));
    }

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      texture = content.Load<Texture2D>("Character/alien_yellow");
    }

    public override void UnloadContent() => base.UnloadContent();

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      keyboardState = Keyboard.GetState();
      allowVerticalMovement = true;

      // Get all levelColliders
      List<ICollide> levelColliders = CollisionManager.Instance.GetLevelColliders();

      // Do Stuff
      if(keyboardState.IsKeyDown(Keys.A))
      {
        Position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }
      else if (keyboardState.IsKeyDown(Keys.D))
      {
        Position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
      }

      // Get Player Collision Rectangle
      collisionRectangle = GetCollisionRectangle();

      // Loop through levelColliders
      foreach (ICollide collider in levelColliders)
      {
        if (CheckCollision(collider)) {
          allowVerticalMovement = false;
        }
      }

      if(!allowVerticalMovement)
      {
        if (keyboardState.IsKeyDown(Keys.A))
        {
          Position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (keyboardState.IsKeyDown(Keys.D))
        {
          Position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
      }
      else
      {
        if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.D))
        {
          walkAnimation.Update(gameTime);
        }
      }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      spriteBatch.Draw(texture, Position, walkAnimation.CurrentFrame.SourceRectangle, Color.White);
    }

    private Rectangle GetCollisionRectangle()
    {
      return new Rectangle((int)Position.X, (int)Position.Y, (int)Dimensions.X, (int)Dimensions.Y - 1);
    }

    private bool CheckCollision(ICollide collider)
    {
      Rectangle colliderRect = collider.GetCollisionRectangle();
      if (collisionRectangle.Right < colliderRect.Left ||
          collisionRectangle.Left > colliderRect.Right ||
          collisionRectangle.Bottom < colliderRect.Top ||
          collisionRectangle.Top > colliderRect.Bottom)
      {
        return false;
      }
      return true;
    }
    #endregion
  }
}
