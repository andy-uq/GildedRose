using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
	[TestFixture]
	public class BusinessRulesTest
	{
		protected const string AGED_BRIE = "Aged Brie";
		protected const string SULFURAS_HAND_OF_RAGNAROS = "Sulfuras, Hand of Ragnaros";
		protected const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
		protected const string CONJURED_MANA_CAKE = "Conjured Mana Cake";

		[Test]
		public void RunCycle()
		{
			Program.Items = new List<Item>
			{
				new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
				new Item {Name = AGED_BRIE, SellIn = 2, Quality = 0},
				new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
				new Item {Name = SULFURAS_HAND_OF_RAGNAROS, SellIn = 0, Quality = 80},
				new Item {Name = BACKSTAGE_PASSES, SellIn = 15, Quality = 20},
				new Item {Name = CONJURED_MANA_CAKE, SellIn = 3, Quality = 6}
			};

			Program.Items = Program.UpdateQuality(Program.Items).ToList();

			var item = Program.Items[0];
			Assert.That(item.Name, Is.EqualTo("+5 Dexterity Vest"));
			Assert.That(item.Quality, Is.EqualTo(19));
			Assert.That(item.SellIn, Is.EqualTo(9));

			item = Program.Items[1];
			Assert.That(item.Name, Is.EqualTo(AGED_BRIE));
			Assert.That(item.Quality, Is.EqualTo(1));
			Assert.That(item.SellIn, Is.EqualTo(1));

			item = Program.Items[2];
			Assert.That(item.Name, Is.EqualTo("Elixir of the Mongoose"));
			Assert.That(item.Quality, Is.EqualTo(6));
			Assert.That(item.SellIn, Is.EqualTo(4));

			item = Program.Items[3];
			Assert.That(item.Name, Is.EqualTo(SULFURAS_HAND_OF_RAGNAROS));
			Assert.That(item.Quality, Is.EqualTo(80));
			Assert.That(item.SellIn, Is.EqualTo(0));

			item = Program.Items[4];
			Assert.That(item.Name, Is.EqualTo(BACKSTAGE_PASSES));
			Assert.That(item.Quality, Is.EqualTo(21));
			Assert.That(item.SellIn, Is.EqualTo(14));

			item = Program.Items[5];
			Assert.That(item.Name, Is.EqualTo("Conjured Mana Cake"));
			Assert.That(item.Quality, Is.EqualTo(4));
			Assert.That(item.SellIn, Is.EqualTo(2));
		}

		[Test]
		public void ItemDegradeNormally()
		{
			var item = new Item() { SellIn = 100, Quality = 100 };
			item = UpdateItemQuality(item);

			Assert.That(item.SellIn, Is.EqualTo(99));
			Assert.That(item.Quality, Is.EqualTo(99));
		}

		protected virtual Item UpdateItemQuality(Item item)
		{
			return Program.UpdateQuality(item);
		}

		[Test]
		public void ItemDegradeTwiceAsFastAfterSellInEnds()
		{
			var item = new Item() { SellIn = 0, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.Quality, Is.EqualTo(8));
		}

		[Test]
		public void ItemQualityIsNeverNegative()
		{
			var item = new Item() { SellIn = 100, Quality = 0 };
			item = UpdateItemQuality(item);

			Assert.That(item.Quality, Is.EqualTo(0));
		}

		[Test]
		public void ItemQualityIsNeverHigherThan50()
		{
			var item = new Item() { Name = AGED_BRIE, Quality = 50 };
			item = UpdateItemQuality(item);

			Assert.That(item.Quality, Is.EqualTo(50));
		}

		[Test]
		public void AgedBrieIncreaseQuality()
		{
			var item = new Item() { Name = AGED_BRIE, SellIn = 1, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.Quality, Is.EqualTo(11));
		}

		[Test]
		public void AgedBrieIncreaseQualityAtTwiceTheRate()
		{
			var item = new Item() { Name = AGED_BRIE, SellIn = 0, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.Quality, Is.EqualTo(12));
		}

		[Test]
		public void SulfurasNeverAges()
		{
			var item = new Item() { Name = SULFURAS_HAND_OF_RAGNAROS, SellIn = 10, Quality = 80 };
			item = UpdateItemQuality(item);

			Assert.That(item.SellIn, Is.EqualTo(10));
		}

		[Test]
		public void SulfurasNeverDegrades()
		{
			var item = new Item() { Name = SULFURAS_HAND_OF_RAGNAROS, SellIn = 10, Quality = 80 };
			item = UpdateItemQuality(item);

			Assert.That(item.Quality, Is.EqualTo(80));
		}

		[Test]
		public void BackstagePassesIncreaseInQuality()
		{
			var item = new Item() { Name = BACKSTAGE_PASSES, SellIn = 20, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.SellIn, Is.EqualTo(19));
			Assert.That(item.Quality, Is.EqualTo(11));
		}

		[Test]
		public void BackstagePassesIncreaseInAtTwiceRate()
		{
			var item = new Item() { Name = BACKSTAGE_PASSES, SellIn = 10, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.SellIn, Is.EqualTo(9));
			Assert.That(item.Quality, Is.EqualTo(12));
		}

		[Test]
		public void BackstagePassesIncreaseInAtTrippleRate()
		{
			var item = new Item() { Name = BACKSTAGE_PASSES, SellIn = 5, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.SellIn, Is.EqualTo(4));
			Assert.That(item.Quality, Is.EqualTo(13));
		}

		[Test]
		public void BackstagePassesExpire()
		{
			var item = new Item() { Name = BACKSTAGE_PASSES, SellIn = 0, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.SellIn, Is.EqualTo(-1));
			Assert.That(item.Quality, Is.EqualTo(0));
		}

		[Test]
		public void ConjuredItemsDegradeTwiceAsFast()
		{
			var item = new Item() { Name = CONJURED_MANA_CAKE, SellIn = 10, Quality = 10 };
			item = UpdateItemQuality(item);

			Assert.That(item.Quality, Is.EqualTo(8));			
		}
	}
}