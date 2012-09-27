using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Console.Quality;

namespace GildedRose.Console
{
	public class Program
	{
		private const string AGED_BRIE = "Aged Brie";
		private const string SULFURAS_HAND_OF_RAGNAROS = "Sulfuras, Hand of Ragnaros";
		private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
		private const string CONJURED_MANA_CAKE = "Conjured Mana Cake";

		public static IList<Item> Items = new List<Item>
		{
			new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
			new Item {Name = AGED_BRIE, SellIn = 2, Quality = 0},
			new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
			new Item {Name = SULFURAS_HAND_OF_RAGNAROS, SellIn = 0, Quality = 80},
			new Item
			{
				Name = BACKSTAGE_PASSES,
				SellIn = 15,
				Quality = 20
			},
			new Item {Name = CONJURED_MANA_CAKE, SellIn = 3, Quality = 6}
		};

		private static ItemUpgrade _defaultStrategy = new ItemUpgrade(item => true);
		private static List<ItemUpgrade> _itemUpgrades = new List<ItemUpgrade>
			{
				new ItemUpgrade(_ => _.Name == SULFURAS_HAND_OF_RAGNAROS, new DoesNotAge(), new LegendaryQuality()),
				new ItemUpgrade( _ => _.Name == AGED_BRIE, itemAging: null, qualityStrategy: new QualityIncreases()),
				new ItemUpgrade( _ => _.Name == BACKSTAGE_PASSES, itemAging: null, qualityStrategy: new BackStagePassQuality()),
				new ItemUpgrade( _ => _.Name != null && _.Name.StartsWith("Conjured"), itemAging: null, qualityStrategy: new QualityDegradesTwiceAsFast()),
			};

		public static void Main(string[] args)
		{
			System.Console.WriteLine("OMGHAI!");
			Items = UpdateQuality(Items).ToList();
			System.Console.ReadKey();
		}

		public static IEnumerable<Item> UpdateQuality(IEnumerable<Item> items)
		{
			return items.Select(UpdateQuality);
		}

		public static Item UpdateQuality(Item t)
		{
			foreach (var x in _itemUpgrades.Where(x => x.IsMatch(t)))
			{
				return x.Upgrade(t);
			}

			return _defaultStrategy.Upgrade(t);
		}
	}
}
