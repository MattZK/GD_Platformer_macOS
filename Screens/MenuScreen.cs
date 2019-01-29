using System;
using System.Collections.Generic;
using GDPlatformer.Managers;
using GDPlatformer.Managers.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDPlatformer.Screens
{
  public class MenuScreen: Screen
  {
    #region Properties
    SpriteFont arialFont;
    private List<string> options = new List<string> {"Play Game", "Exit"};
    private int selected = 0;
    bool delayOver = true;
    double delay;
    bool allowStartGame;
    #endregion

    #region Game Methods
    public override void LoadContent()
    {
      base.LoadContent();
      arialFont = content.Load<SpriteFont>("Fonts/ArialBig");
    }

    public override void UnloadContent()
    {
      base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);

      KeyboardState keyboardState = Keyboard.GetState();

      checkDelay(gameTime);

      Navigate(keyboardState);

      if (keyboardState.IsKeyDown(Keys.Enter))
      {
        if (selected == 0)
          allowStartGame = true;
        else
          isGameOver = true;
      }

      Console.WriteLine(selected);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      DrawText(spriteBatch);

      if (allowStartGame) StartGame();
    }
    #endregion
    private void checkDelay(GameTime gameTime)
    {
      delay += gameTime.ElapsedGameTime.TotalMilliseconds;
      if (delay >= 500)
        delayOver = true;
    }
    private void resetDelay()
    {
      delayOver = false;
      delay = 0;
    }
    private void Navigate(KeyboardState keyboardState)
    {
      if ((keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Down)) && delayOver)
      {
        if (selected == 1)
          selected = 0;
        else
          selected = 1;
        resetDelay();
      }
    }
    private void StartGame()
    {
      ScreenManager.Instance.CurrentScreen.UnloadContent();
      ScreenManager.Instance.CurrentScreen = new GameScreen(0);
      ScreenManager.Instance.CurrentScreen.LoadContent();
    }

    private void DrawText(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      Vector2 position = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
      Vector2 origin = new Vector2(arialFont.MeasureString("Very Fun Game").X / 2, arialFont.MeasureString("Very Fun Game").Y / 2 + 110);
      spriteBatch.DrawString(arialFont, "Very Fun Game", position, Color.Black, 0, origin, 2f, SpriteEffects.None, 0);

      position = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
      origin = new Vector2(arialFont.MeasureString("Start").X / 2, arialFont.MeasureString("Start").Y / 2);
      Color color = Color.White;
      if (selected == 0)
        color = Color.Red;
      spriteBatch.DrawString(arialFont, "Start", position, color, 0, origin, 1f, SpriteEffects.None, 0);

      position = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2 + 40);
      origin = new Vector2(arialFont.MeasureString("Exit").X / 2, arialFont.MeasureString("Exit").Y / 2);
      color = Color.White;
      if (selected == 1)
        color = Color.Red;
      spriteBatch.DrawString(arialFont, "Exit", position, color, 0, origin, 1f, SpriteEffects.None, 0);
    }
  }
}
