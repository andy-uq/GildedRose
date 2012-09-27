using GildedRose.Console;
using GildedRose.Console.Quality;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture]
	public class ItemQualityStrategyTests
	{
		private ItemUpgrade _itemUpgrade;

		[SetUp]
		public void CreateInstance()
		{
			_itemUpgrade = new ItemUpgrade(item => true);
		}

		[Test]
		public void UpgradeReturnsNewItem()
		{
			var item = new Item { SellIn = 10, Quality = 10 };
			var result = _itemUpgrade.Upgrade(item);

			Assert.That(item, Is.Not.SameAs(result));
		}

		[Test]
		public void DoesNotAge()
		{
			var doesNotAge = new DoesNotAge();
			Assert.That(doesNotAge.CalculateNewSellIn(10), Is.EqualTo(10));
		}

		[Test]
		public void NormalAging()
		{
			var normalAging = new NormalAging();
			Assert.That(normalAging.CalculateNewSellIn(10), Is.EqualTo(9));
			Assert.That(normalAging.CalculateNewSellIn(0), Is.EqualTo(-1));
		}

		[Test]
		public void NormalQuality()
		{
			var doesNotAge = new NormalQuality();
			Assert.That(doesNotAge.CalculateQuality(1, 10), Is.EqualTo(9));
			Assert.That(doesNotAge.CalculateQuality(0, 10), Is.EqualTo(8));
			Assert.That(doesNotAge.CalculateQuality(0, 0), Is.EqualTo(0));
		}


		[Test]
		public void QualityIncreases()
		{
			var doesNotAge = new QualityIncreases();
			Assert.That(doesNotAge.CalculateQuality(1, 10), Is.EqualTo(11));
			Assert.That(doesNotAge.CalculateQuality(0, 10), Is.EqualTo(12));
			Assert.That(doesNotAge.CalculateQuality(0, 49), Is.EqualTo(50));
		}

		[Test]
		public void QualityDegradesTwiceAsFast()
		{
			var doesNotAge = new QualityDegradesTwiceAsFast();
			Assert.That(doesNotAge.CalculateQuality(1, 10), Is.EqualTo(8));
			Assert.That(doesNotAge.CalculateQuality(0, 10), Is.EqualTo(8));
			Assert.That(doesNotAge.CalculateQuality(0, 1), Is.EqualTo(0));
			Assert.That(doesNotAge.CalculateQuality(0, 0), Is.EqualTo(0));
		}

		[Test]
		public void CloneItem()
		{
			var item = new Item {SellIn = 10, Quality = 10};
			var result = _itemUpgrade.Clone(item);

			Assert.That(item, Is.Not.SameAs(result));
			Assert.That(item.Name, Is.EqualTo(result.Name));
			Assert.That(item.SellIn, Is.EqualTo(result.SellIn));
			Assert.That(item.Quality, Is.EqualTo(result.Quality));
		}
	}
}