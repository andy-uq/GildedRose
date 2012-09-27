namespace GildedRose.Console.Quality
{
	public class NormalQuality : IQualityStrategy
	{
		public int CalculateQuality(int sellIn, int quality)
		{
			if ( quality == 0 )
				return 0;

			if (sellIn < 1)
			{
				return quality - 2;
			}

			return quality - 1;
		}
	}
}