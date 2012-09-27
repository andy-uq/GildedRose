using System;

namespace GildedRose.Console.Quality
{
	public class BackStagePassQuality : IQualityStrategy
	{
		public int CalculateQuality(int sellIn, int quality)
		{
			if (sellIn < 1)
			{
				return 0;
			}

			if (sellIn > 10)
			{
				quality ++;
			}
			else
			{
				if (sellIn <= 5)
				{
					quality += 3;
				}
				else
				{
					quality += 2;
				}
			}

			return Math.Min(quality, 50);
		}
	}
}