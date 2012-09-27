namespace GildedRose.Console.Quality
{
	public interface IQualityStrategy
	{
		int CalculateQuality(int sellIn, int quality);
	}
}