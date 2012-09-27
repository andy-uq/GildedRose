namespace GildedRose.Console.Quality
{
	public class LegendaryQuality : IQualityStrategy
	{
		public int CalculateQuality(int sellIn, int quality)
		{
			return quality;
		}
	}
}