using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GDPlatformer.Character.Animate
{
  public class Animation
  {
    #region Properties
    private List<AnimationFrame> frames;
    public AnimationFrame CurrentFrame { get; set; }
    public int MovementsPerSecond { get; set; }
    private int counter = 0;
    private double x = 0;
    private double offset = 0;
    private int totalWidth = 0;
    #endregion

    #region Methods
    public Animation()
    {
      frames = new List<AnimationFrame>();
      MovementsPerSecond = 8;
    }

    public void AddFrame(Rectangle rectangle)
    {
      AnimationFrame newFrame = new AnimationFrame()
      {
        SourceRectangle = rectangle
      };

      frames.Add(newFrame);
      CurrentFrame = frames[0];
      offset = CurrentFrame.SourceRectangle.Width;
      foreach (AnimationFrame f in frames)
        totalWidth += f.SourceRectangle.Width;
    }
    #endregion

    #region Game Methods
    public void Update(GameTime gameTime)
    {
      double temp = CurrentFrame.SourceRectangle.Width * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);
      x += temp;
      if (x >= CurrentFrame.SourceRectangle.Width / MovementsPerSecond)
      {
        x = 0;
        counter++;
        if (counter >= frames.Count)
          counter = 0;
        CurrentFrame = frames[counter];
        offset += CurrentFrame.SourceRectangle.Width;
      }
      if (offset >= totalWidth)
        offset = 0;
    }
    #endregion
  }

  public class AnimationFrame
  {
    public Rectangle SourceRectangle { get; set; }
  }
}
