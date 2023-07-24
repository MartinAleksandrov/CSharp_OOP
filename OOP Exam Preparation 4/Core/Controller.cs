using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private readonly BoothRepository booths;
        IBooth booth;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count+1;

            booth = new Booth(boothId,capacity);
            booths.AddModel(booth);

            return string.Format(OutputMessages.NewBoothAdded,boothId,capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            booth = booths.Models.FirstOrDefault(i => i.BoothId == boothId);

            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return string.Format(OutputMessages.InvalidCocktailType,cocktailTypeName);
            }
            else if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }
            else if (booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == cocktailName && c.Size == size) != null)
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size,cocktailName);
            }
            else
            {
                ICocktail cocktail = null;

                if (cocktailTypeName == nameof(Hibernation))
                {
                    cocktail = new Hibernation(cocktailName,size);
                }
                else if (cocktailTypeName == nameof(MulledWine))
                {
                    cocktail = new MulledWine(cocktailName,size);
                }

                booth.CocktailMenu.AddModel(cocktail);
            }
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName,cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            booth = booths.Models.FirstOrDefault(i=>i.BoothId == boothId);

            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName!= nameof(Stolen))
            {
                return string.Format(OutputMessages.InvalidDelicacyType,delicacyTypeName);
            }
            else if (booth.DelicacyMenu.Models.FirstOrDefault(d=>d.Name == delicacyName) != null)
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }
            else
            {
                IDelicacy delicacy = null;

                if (delicacyTypeName == nameof(Gingerbread))
                {
                    delicacy = new Gingerbread(delicacyName);
                }
                else if (delicacyTypeName == nameof(Stolen))
                {
                    delicacy = new Stolen(delicacyName);
                }
                booth.DelicacyMenu.AddModel(delicacy);

                return string.Format(OutputMessages.NewDelicacyAdded,delicacyTypeName, delicacyName);
            }
        }

        public string BoothReport(int boothId)
        {
            booth = booths.Models.FirstOrDefault(i => i.BoothId == boothId);

            return booth.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            booth = booths.Models.FirstOrDefault(i => i.BoothId == boothId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            sb.AppendLine($"Booth {boothId} is now available!");

            booth.Charge();
            booth.ChangeStatus();

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)   
        {
            List<IBooth> reservedBooths = booths.Models.Where(b => b.IsReserved == false && b.Capacity >= countOfPeople).
                OrderBy(c=>c.Capacity).
                ThenByDescending(b=>b.BoothId).
                ToList();

            booth = reservedBooths.FirstOrDefault();
            if (booth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            booth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId , countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] splittedOrder = order.Split('/',StringSplitOptions.RemoveEmptyEntries);

            string itemTypeName = splittedOrder[0];
            string itemName = splittedOrder[1];
            double orderedPiecesCount = double.Parse(splittedOrder[2]);

            booth = booths.Models.FirstOrDefault(i => i.BoothId == boothId);

            IDelicacy delicacy;
            ICocktail cocktail;

            if (itemTypeName != nameof(Gingerbread) && itemTypeName != nameof(Stolen) && 
                itemTypeName != nameof(Hibernation) && itemTypeName != nameof(MulledWine))
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            else if (booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName) == null &&
                    booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == itemName) == null)
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            if (itemTypeName == nameof(Hibernation) || itemTypeName == nameof(MulledWine))
            {
                return OrderCocktail(boothId, splittedOrder, itemName, orderedPiecesCount, out cocktail);
            }
            else
            {
                return OrderDelicacy(boothId, itemTypeName, itemName, orderedPiecesCount, out delicacy);
            }
        }

        private string OrderDelicacy(int boothId, string itemTypeName, string itemName, double orderedPiecesCount, out IDelicacy delicacy)
        {
            delicacy = booth.DelicacyMenu.Models.FirstOrDefault(c => c.Name == itemName);

            if (delicacy == null)
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            double sum = delicacy.Price * orderedPiecesCount;
            booth.UpdateCurrentBill(sum);

            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, orderedPiecesCount, itemName);
        }

        public string OrderCocktail(int boothId, string[] splittedOrder, string itemName, double orderedPiecesCount, out ICocktail cocktail)
        {
            string cocktailSize = splittedOrder[3];

            cocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == cocktailSize);

            if (cocktail == null)
            {
                return string.Format(OutputMessages.NotRecognizedItemName, cocktailSize, itemName);
            }

            double sum = cocktail.Price * orderedPiecesCount;
            booth.UpdateCurrentBill(sum);

            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, orderedPiecesCount, itemName);
        }
    }
}
