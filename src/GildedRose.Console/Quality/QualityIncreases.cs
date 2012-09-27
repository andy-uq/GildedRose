namespace GildedRose.Console.Quality
{
	public class QualityIncreases : IQualityStrategy
	{
		public int CalculateQuality(int sellIn, int quality)
		{
			quality = sellIn > 0 
				? quality + 1
				: quality + 2;

			return quality < 50 
				? quality 
				: 50;
		}
	}
}