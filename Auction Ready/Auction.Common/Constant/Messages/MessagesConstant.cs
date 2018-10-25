namespace Auction.Common.Constant.Messages
{
    public static partial class Messages
    {
        public static class MessagesConstant
        {
            #region Autetntification

            public static readonly MessageString TheUsernameOrPasswordIsIncorrect = new MessageString("Неверное имя пользователя или пароль.");
            public static readonly MessageString TheUserBlocked = new MessageString("Пользователь \"{0}\" - блокирован.");

            #endregion

            #region Create User

            public static readonly MessageString TheEmailAlreadyPresent = new MessageString("Электронная почта уже зарегистрирована.");
            public static readonly MessageString TheUserCreatedMistake = new MessageString("Ошибка создания пользователя \"{0}\". Ошибка - {1}");

            #endregion

            #region Common

            public static readonly MessageString Yes = new MessageString("Да");
            public static readonly MessageString No = new MessageString("Нет");
            public static readonly MessageString CreateNew = new MessageString("Создать новый");
            public static readonly MessageString Edit = new MessageString("Изменить");
            public static readonly MessageString Delete = new MessageString("Удалить");
            public static readonly MessageString Details = new MessageString("Детали");
            public static readonly MessageString Save = new MessageString("Сохранить");
            public static readonly MessageString FilterBy = new MessageString("Фильтр по \"{0}\"");
            public static readonly MessageString BackToList = new MessageString("Вернуться к списку");
            public static readonly MessageString Confirm = new MessageString("Вы подтверждаете {0}?");
            public static readonly MessageString Filter = new MessageString("Фильтр");

            #endregion

            #region Auction templates

            public static readonly MessageString CreateTemplate = new MessageString("Cоздание аукциона.");
            public static readonly MessageString TheAuctionCreatedMistake = new MessageString("Ошибка создания аукциона {0}. Автор - {1}. Ошибка - {2}");
            public static readonly MessageString AuctionTemplates = new MessageString("Шаблоны аукционов");
            public static readonly MessageString TheAuctionEditMistake = new MessageString("Ошибка изменения аукциона {0}. Автор - {1}. Ошибка - {2}");
            public static readonly MessageString EditTemplate = new MessageString("Изменение аукциона.");
            public static readonly MessageString DeleteTemplate = new MessageString("Удаление аукциона.");
            public static readonly MessageString TheAuctionDeleteMistake = new MessageString("Ошибка удаления аукциона {0}. Автор - {1}. Ошибка - {2}");
            public static readonly MessageString Deletion = new MessageString("удаление");
            public static readonly MessageString StartingAuctionTemplate = new MessageString("запуск аукциона");
            public static readonly MessageString StopingAuctionTemplate = new MessageString("остановку аукциона");

            #endregion

            #region Lots

            public static readonly MessageString LotsOfAuction = new MessageString("Лоты:");
            public static readonly MessageString Increase = new MessageString("Ставка на повышение");
            public static readonly MessageString Decrease = new MessageString("Ставка на понижение");
            public static readonly MessageString CreateLot = new MessageString("Создать лот");
            public static readonly MessageString TheLotCreatedMistake = new MessageString("Ошибка создания лота \"{0}\". Автор - {1}. Ошибка - {2}");
            public static readonly MessageString EditLot = new MessageString("Изменить лот");
            public static readonly MessageString DeleteLot = new MessageString("Удалить лот");
            public static readonly MessageString TheLotEditedMistake = new MessageString("Ошибка изменения лота \"{0}\". Автор - {1}. Ошибка - {2}");
            public static readonly MessageString TheLotDeletedMistake = new MessageString("Ошибка удаления лота \"{0}\". Автор - {1}. Ошибка - {2}");

            #endregion

            #region Auctions

            public static readonly MessageString ActiveAuctions = new MessageString("Активные аукционы");
            public static readonly MessageString ActiveLots = new MessageString("Активные лоты");
            public static readonly MessageString Start = new MessageString("Старт");
            public static readonly MessageString Stop = new MessageString("Стоп");
            public static readonly MessageString StartStopAuctionTemplate = new MessageString("Запуск/остановка");
            public static readonly MessageString TheAuctionStartMistake = new MessageString("Ошибка запуска аукциона {0}. Автор - {1}. Ошибка - {2}");
            public static readonly MessageString TheAuctionStopMistake = new MessageString("Ошибка остановки аукциона {0}. Автор - {1}. Ошибка - {2}");
            public static readonly MessageString DetailsTemplate = new MessageString("Детали аукциона");
            public static readonly MessageString Participation = new MessageString("Участие");
            public static readonly MessageString SelectedLotClosed = new MessageString("Выбранный лот закрыт - {0}.");
            public static readonly MessageString NewRate = new MessageString("Новая ставка");
            public static readonly MessageString TheRateCreatedMistake = new MessageString("Ошибка создания ставки. Лот: \"{0}\".");
            public static readonly MessageString TheDirectionIncreaseAndStartingPriceMoreThanRate = new MessageString("Направление аукциона на возрастание, но ставка меньше начальной цены!");
            public static readonly MessageString TheDirectionDecreaseAndStartingPriceLessThanRate = new MessageString("Направление аукциона на убывание, но ставка больше начальной цены!");

            #endregion

            public static readonly MessageString a1 = new MessageString("Ошибка создания пользователя \"{0}\"");
        }
    }
}
