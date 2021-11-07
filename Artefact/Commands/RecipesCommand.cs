using Artefact.Commands.Misc;
using Artefact.CraftingSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class RecipesCommand : ICommand
    {
        public string Name => "recipes";

        public string[] Aliases => new string[] { "recipebook" };

        public bool HasArguments => false;

        public string NoArgsResponse => "";

        const int MAX_CHARS_PER_PAGE_LINE = 40;

        const int ITEMS_PER_PAGE = 5;

        int page = 0;

        public void OnRun(List<string> args)
        {
            bool inRecipeBook = true;
            while (inRecipeBook)
            {
                Utils.WriteCenter(CreateBook(page));

                List<string> options = new List<string>();
                List<Action> actions = new List<Action>();

                if ((Item.CraftableItems.Count - page * ITEMS_PER_PAGE) > ITEMS_PER_PAGE * 2)
                {
                    options.Add("Next");
                    actions.Add(() => page += 2);
                }

                if (page > 0)
                {
                    options.Add("Previous");
                    actions.Add(() => page -= 2);
                }

                options.Add("Exit Recipe Book");
                actions.Add(() => inRecipeBook = false);

                int selection = Utils.GetSelection(options.ToArray());

                actions[selection]();
            }
            page = 0;
        }

        string CreateBook(int page)
        {
            List<string> leftPage = CreatePage(true, Item.CraftableItems.GetRange(page * ITEMS_PER_PAGE, Math.Min(ITEMS_PER_PAGE, Item.CraftableItems.Count - page * ITEMS_PER_PAGE)), page + 1);
            List<string> rightPage = CreateBlankPage(false, page + 2);
            if (Item.CraftableItems.Count > (page + 1) * ITEMS_PER_PAGE)
                rightPage = CreatePage(false, Item.CraftableItems.GetRange((page + 1) * ITEMS_PER_PAGE, Math.Min(ITEMS_PER_PAGE, Math.Max(0, Item.CraftableItems.Count - (page + 1) * ITEMS_PER_PAGE))), page + 2);
            List<string> book = new List<string>();
            if (rightPage.Count < leftPage.Count)
            {
                int iters = leftPage.Count - rightPage.Count;
                for (int i = 0; i < iters; i++)
                {
                    rightPage.Insert(rightPage.Count - 2, CreateLine(false, ""));
                }
            }

            for (int i = 0; i < leftPage.Count; i++)
            {
                book.Add(leftPage[i] + rightPage[i]);
            }
            return book.Join("\n");
        }

        List<string> CreateBlankPage(bool left, int pageNumber)
        {
            List<string> lines = new List<string>();
            string top = "";
            int toSub = left ? 0 : 1;
            for (int i = 0; i < MAX_CHARS_PER_PAGE_LINE - toSub; i++)
            {
                top += "_";
            }
            lines.Add(top);

            lines.Add(CreateLine(left, pageNumber.ToString()));

            string bottom = "";
            if (left) bottom += "|";

            for (int i = 0; i < MAX_CHARS_PER_PAGE_LINE - 2; i++)
            {
                bottom += "_";
            }
            bottom += "|";
            lines.Add(bottom);

            return lines;
        }

        List<string> CreatePage(bool left, List<Item> items, int pageNumber)
        {
            List<string> lines = new List<string>();
            string top = "";
            int toSub = left ? 0 : 1;
            for (int i = 0; i < MAX_CHARS_PER_PAGE_LINE - toSub; i++)
            {
                top += "_";
            }
            lines.Add(top);

            foreach (Item item in items)
            {
                string itemName = item.Name;
                itemName = itemName.Substring(0, Math.Min(itemName.Length, MAX_CHARS_PER_PAGE_LINE - 2 - toSub));

                lines.Add(CreateLine(left, $"[{ColorConstants.ITEM_COLOR}]{itemName}"));

                for (int i = 0; i < item.CraftData.Count; i++)
                {
                    CraftData craftData = item.CraftData[i];
                    lines.Add(CreateLine(left, $"Recipe #{i + 1}:"));
                    foreach (ItemData itemData in craftData.CraftItems)
                    {
                        lines.Add(CreateLine(left, $"- {itemData.ToColoredString()}"));
                    }
                    lines.Add(CreateLine(left, $"Crafted Amount: [{ColorConstants.GOOD_COLOR}]{craftData.Amount}"));
                }

                lines.Add(CreateLine(left, ""));
            }

            lines.Add(CreateLine(left, pageNumber.ToString()));

            string bottom = "";
            if (left) bottom += "|";

            for (int i = 0; i < MAX_CHARS_PER_PAGE_LINE - 2; i++)
            {
                bottom += "_";
            }
            bottom += "|";
            lines.Add(bottom);

            return lines;
        }

        string CreateLine(bool left, string text)
        {
            string line = "";
            if (left) line += "|";
            line += $"{text}[/]";
            int lineLength = line.TrimColor().Length;

            int toSub = left ? 1 : 2;
            for (int i = 0; i < MAX_CHARS_PER_PAGE_LINE - lineLength - toSub; i++)
            {
                line += " ";
            }

            line += "|";
            return line;
        }
    }
}
