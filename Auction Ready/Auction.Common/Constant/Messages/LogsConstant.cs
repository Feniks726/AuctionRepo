namespace Auction.Common.Constant.Messages
{
    public static partial class Messages
    {
        public static class LogsConstant
        {
            //Autetntification
            public static readonly MessageString AuthorizationSuccessful = new MessageString("Authorization successful. User {0}");
            public static readonly MessageString AuthorizationFailed = new MessageString("Authorization failed");
            public static readonly MessageString OutUser = new MessageString("Out user - {0}");
            public static readonly MessageString TheUserBlocked = new MessageString("The user \"{0}\" - blocked");

            //Registration
            public static readonly MessageString TheEmailAlreadyPresent = new MessageString("The email: {0} already present");
            public static readonly MessageString TheUserCreated = new MessageString("The user \"{0}\" - created");
            public static readonly MessageString TheUserCreatedMistake = new MessageString("Mistake creation of user \"{0}\". Mistake - {1}");

            //Auction template
            public static readonly MessageString TheAuctionCreated = new MessageString("Auction {0} - created. Author - {1}");
            public static readonly MessageString TheAuctionCreatedMistake = new MessageString("Mistake creation of auction  - {0}. Author - {1}. Mistake - {2}");
            public static readonly MessageString TheAuctionEdit = new MessageString("Auction {0} - edited. Author - {1}");
            public static readonly MessageString TheAuctionEditMistake = new MessageString("Mistake edited of auction  - {0}. Author - {1}. Mistake - {2}");
            public static readonly MessageString TheAuctionDelete = new MessageString("Auction {0} - deleted. Author - {1}");
            public static readonly MessageString TheAuctionDeleteMistake = new MessageString("Mistake deleted of auction  - {0}. Author - {1}. Mistake - {2}");
            public static readonly MessageString TheAuctionStart = new MessageString("The auction  \"{0}\" started. The user \"{1}\"");
            public static readonly MessageString TheAuctionStop = new MessageString("The auction  \"{0}\" stoped. The user \"{1}\"");
            public static readonly MessageString TheAuctionStartMistake = new MessageString("The auction started mistake\"{0}\". The user \"{1}\"");
            public static readonly MessageString TheAuctionStopMistake = new MessageString("The auction stoped mistake\"{0}\". The user \"{1}\"");
            public static readonly MessageString CantStopTemplate = new MessageString("The auction already started");

            //Lots
            public static readonly MessageString TheLotCreated = new MessageString("The lot \"{0}\" created. Author - {1}.");
            public static readonly MessageString TheLotCreatedMistake = new MessageString("Mistake creation of lot  - {0}. Author - {1}. Mistake - {2}");
            public static readonly MessageString TheLotEdited = new MessageString("The lot \"{0}\" edited. Author - {1}.");
            public static readonly MessageString TheLotEditedMistake = new MessageString("Mistake edition of lot  - {0}. Author - {1}. Mistake - {2}");
            public static readonly MessageString TheLotDeleted = new MessageString("The lot \"{0}\" deleted. Author - {1}.");
            public static readonly MessageString TheLotDeletedMistake = new MessageString("Mistake deletion of lot  - {0}. Author - {1}. Mistake - {2}");
            public static readonly MessageString SelectedLotClosed = new MessageString("Selected lot - {0} closed. User - {1}.");

            //Rates
            public static readonly MessageString TheRateCreated = new MessageString("The rate created lot - {0}. User - {1}.");
            public static readonly MessageString TheRateCreateddMistake = new MessageString("The rate created mistake. Lot: \"{0}\". The user \"{1}\". Mistake - {2}");
            public static readonly MessageString TheDirectionIncreaseAndStartingPriceMoreThanRate = new MessageString("The direction: \"Increase\" and starting price more than rate.The lot: \"{0}\". The user \"{1}\"");
            public static readonly MessageString TheDirectionDecreaseAndStartingPriceLessThanRate = new MessageString("The direction: \"Decrease\" and starting price less than rate.The lot: \"{0}\". The user \"{1}\"");

            public static readonly MessageString a1 = new MessageString("Ошибка создания пользователя \"{0}\"");
       }
    }
}
