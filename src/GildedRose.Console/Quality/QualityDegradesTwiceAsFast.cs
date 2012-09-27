namespace GildedRose.Console.Quality
{
	public class QualityDegradesTwiceAsFast : IQualityStrategy
	{
		public int CalculateQuality(int sellIn, int quality)
		{
			quality = quality - 2;
			return quality > 0 ? quality : 0;
		}
	}
}