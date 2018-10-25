using System;
using System.Collections.Generic;
using System.Linq;
using Auction.Common.Constant;
using Auction.Common.Constant.Messages;
using Auction.DAL.Data;
using Auction.DAL.Factories;
using Auction.WebSite.Infrastructure.Interfaces;
using Auction.WebSite.Models;

namespace Auction.WebSite.Infrastructure.Implementations
{
    public class AuctionProvider : IAuctionProvider
    {
        #region Members

        protected readonly IDbFactory DbFactory;

        #endregion

        #region Constructor

        public AuctionProvider(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        #endregion

        #region Notifications

        #region Auctions

        public List<AuctionTemplateViewModel> GetActiveAuctions(string filterByName)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                var auctionTemplates = DbFactory.AuctionTemplates.GetActive(filterByName)
                    .Select(
                        at =>
                            new AuctionTemplateViewModel
                            {
                                Id = at.Id,
                                Name = at.Name,
                                CreateDateTime = at.CreateDateTime,
                                ModifidedDateTime = at.ModifidedDateTime,
                                AuthorID = at.AuthorID,
                                Author = at.Author,
                                IsStarted = !at.IsStopped,
                                StartedDateTime = at.StartedDateTime
                            }).ToList();
                foreach (var auctionTemplate in auctionTemplates)
                {
                    auctionTemplate.LotsCount = DbFactory.Lots.GetCountAuctionTemplate(auctionTemplate.Id);
                }
                return auctionTemplates;
            }
        }

        public List<ActiveLotViewModel> GetActiveLotsForAuction(Guid id, string filterByName)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                var lots = DbFactory.Lots.GetActiveForAuction(id, filterByName)
                    .Select(
                        lot =>
                            new ActiveLotViewModel
                            {
                                Id = lot.Id,
                                AuctionTemplateId = lot.AuctionTemplateId,
                                Context = lot.Context,
                                Duration = lot.Duration,
                                Direction = lot.Direction,
                                DirectionText = lot.Direction==DirectionOfLot.Increase ? Messages.MessagesConstant.Increase.ToString() : Messages.MessagesConstant.Decrease.ToString(),
                                StartingPrice = lot.StartingPrice,
                                CreateDateTime = lot.CreateDateTime,
                                ModifidedDateTime = lot.ModifidedDateTime
                            }).ToList();
                foreach (var lot in lots)
                {
                    if (DbFactory.Auctions.GetCountForLot(lot.Id) == 0) continue;
                    var lider= DbFactory.Auctions.GetLiderForLot(lot.Id, lot.Direction).FirstOrDefault();
                    if (lider == null) continue;
                    lot.LiderRate = lider.LiderRate;
                    lot.LiderID = lider.LiderId;
                    lot.Lider = lider.Lider;
                }
                return lots;
            }
        }

        public bool CreateRate(AuctionViewModel viewModel, out string error)
        {
            var res = false;
            error = null;
            try
            {

                using (DbFactory.CreateUnitOfWork())
                {
                    var auction = new DAL.Data.Auction
                    {
                        Id = Guid.NewGuid(),
                        LotId = viewModel.LotId,
                        UserId = viewModel.UserId,
                        Rate = viewModel.Rate,
                        CreateDateTime = viewModel.CreateDateTime,
                        ModifidedDateTime = viewModel.ModifidedDateTime
                    };

                    DbFactory.Auctions.Save(auction);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        public int CheckRate(AuctionViewModel viewModel, out string error)
        {
            var res = 0;
            error = null;
            using (DbFactory.CreateUnitOfWork())
            {
                var lot = DbFactory.Lots.GetById(viewModel.LotId);

                if (lot.Direction == DirectionOfLot.Increase & lot.StartingPrice > viewModel.Rate)
                {
                    res = 1;
                    error = Messages.LogsConstant.TheDirectionIncreaseAndStartingPriceMoreThanRate.Format(viewModel.Context, viewModel.UserId);
                }


                if (lot.Direction == DirectionOfLot.Decrease & lot.StartingPrice > viewModel.Rate)
                {
                    res = -1;
                    error = Messages.LogsConstant.TheDirectionDecreaseAndStartingPriceLessThanRate.Format(viewModel.Context, viewModel.UserId);
                }
            }
            return res;
        }

        #endregion

        #region AuctionTemplates

        public List<AuctionTemplateViewModel> GetMyAuctions(long userId, string filterByName)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                var auctionTemplates = DbFactory.AuctionTemplates.GetForUser(userId, filterByName)
                    .Select(
                        at =>
                            new AuctionTemplateViewModel
                            {
                                Id = at.Id,
                                Name = at.Name,
                                CreateDateTime = at.CreateDateTime,
                                ModifidedDateTime = at.ModifidedDateTime,
                                AuthorID = at.AuthorID,
                                Author = at.Author,
                                IsStarted = !at.IsStopped,
                                StartedDateTime = at.StartedDateTime
                            }).ToList();
                foreach (var auctionTemplate in auctionTemplates)
                {
                    auctionTemplate.LotsCount = DbFactory.Lots.GetCountAuctionTemplate(auctionTemplate.Id);
                }
                return auctionTemplates;

            }
        }

        public AuctionTemplateViewModel GetAuctionTemplateModelById(Guid? id, string filterLotByContext = null)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                var auctionTemplate = DbFactory.AuctionTemplates.GetById(id);
                if (auctionTemplate == null) return null;
                var model = new AuctionTemplateViewModel
                {
                    Id = auctionTemplate.Id,
                    Name = auctionTemplate.Name,
                    CreateDateTime = auctionTemplate.CreateDateTime,
                    ModifidedDateTime = auctionTemplate.ModifidedDateTime,
                    AuthorID = auctionTemplate.AuthorID,
                    Author = auctionTemplate.Author,
                    IsStarted = !auctionTemplate.IsStopped,
                    StartedDateTime = auctionTemplate.StartedDateTime,
                    Started = !auctionTemplate.IsStopped
                        ? Messages.MessagesConstant.Yes.ToString()
                        : Messages.MessagesConstant.No.ToString()
                };
                var lots = DbFactory.Lots.GetForAuctionTemplate(id, filterLotByContext);
                model.Lots = new List<ActiveLotViewModel>();
                foreach (var l in lots)
                {
                    var lot = new ActiveLotViewModel
                    {
                        Id = l.Id,
                        AuctionTemplateId = l.AuctionTemplateId,
                        Context = l.Context,
                        CreateDateTime = l.CreateDateTime,
                        ModifidedDateTime = l.ModifidedDateTime,
                        StartingPrice = l.StartingPrice,
                        Duration = l.Duration,
                        Direction = l.Direction,
                        DirectionText =
                            l.Direction == DirectionOfLot.Increase
                                ? Messages.MessagesConstant.Increase.ToString()
                                : Messages.MessagesConstant.Decrease.ToString()
                    };
                    if (DbFactory.Auctions.GetCountForLot(l.Id) == 0)
                    {
                        model.Lots.Add(lot);
                        continue;
                    }
                    var lider = DbFactory.Auctions.GetLiderForLot(l.Id, l.Direction).FirstOrDefault();
                    if (lider == null)
                    {
                        model.Lots.Add(lot);
                        continue;
                    }
                    lot.LiderRate = lider.LiderRate;
                    lot.LiderID = lider.LiderId;
                    lot.Lider = lider.Lider;
                    model.Lots.Add(lot);
                }
                return model;
            }
        }

        public bool CreateAuctionTemplate(AuctionTemplateViewModel viewModel, out string error)
        {
            var res = false;
            error = null;
            try
            {

                using (DbFactory.CreateUnitOfWork())
                {
                    var auctionTemplate = new AuctionTemplate
                    {
                        Id = Guid.NewGuid(),
                        Name = viewModel.Name,
                        CreateDateTime = viewModel.CreateDateTime,
                        ModifidedDateTime = viewModel.ModifidedDateTime,
                        IsStopped = !viewModel.IsStarted,
                        AuthorID = viewModel.AuthorID
                    };

                    DbFactory.AuctionTemplates.Save(auctionTemplate);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        public bool EditAuctionTemplate(AuctionTemplateViewModel viewModel, out string error)
        {
            var res = false;
            error = null;
            try
            {
                using (DbFactory.CreateUnitOfWork())
                {
                    var auctionTemplate = DbFactory.AuctionTemplates.GetById(viewModel.Id);
                    auctionTemplate.Name = viewModel.Name;
                    auctionTemplate.ModifidedDateTime = viewModel.ModifidedDateTime;
                    auctionTemplate.IsStopped = !viewModel.IsStarted;
                    auctionTemplate.StartedDateTime = viewModel.StartedDateTime;

                    DbFactory.AuctionTemplates.Save(auctionTemplate);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        public bool DeleteAuctionTemplate(AuctionTemplateViewModel viewModel, out string error)
        {
            var res = false;
            error = null;
            try
            {
                using (DbFactory.CreateUnitOfWork())
                {
                    var auctionTemplate = DbFactory.AuctionTemplates.GetById(viewModel.Id);
                    DbFactory.AuctionTemplates.Delete(auctionTemplate);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        #endregion

        #region Lots

        public LotViewModel GetLotModelById(Guid id)
        {
            using (DbFactory.CreateUnitOfWork())
            {
                var lot = DbFactory.Lots.GetById(id);
                var model = new LotViewModel
                {
                    Id = lot.Id,
                    AuctionTemplateId = lot.AuctionTemplateId,
                    Context = lot.Context,
                    CreateDateTime = lot.CreateDateTime,
                    ModifidedDateTime = lot.ModifidedDateTime,
                    StartingPrice = lot.StartingPrice,
                    Duration = lot.Duration,
                    Direction = lot.Direction,
                    DirectionText =
                        lot.Direction == DirectionOfLot.Increase
                            ? Messages.MessagesConstant.Increase.ToString()
                            : Messages.MessagesConstant.Decrease.ToString()
                };
                return model;
            }
        }

        public bool CreateLot(LotViewModel viewModel, out string error)
        {
            var res = false;
            error = null;
            try
            {

                using (DbFactory.CreateUnitOfWork())
                {
                    var lot = new Lot
                    {
                        Id = Guid.NewGuid(),
                        AuctionTemplateId = viewModel.AuctionTemplateId,
                        Context = viewModel.Context,
                        Duration = viewModel.Duration,
                        Direction = viewModel.Direction,
                        StartingPrice = viewModel.StartingPrice,
                        CreateDateTime = viewModel.CreateDateTime,
                        ModifidedDateTime = viewModel.ModifidedDateTime
                    };

                    DbFactory.Lots.Save(lot);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        public bool EditLot(LotViewModel viewModel, out string error)
        {
            var res = false;
            error = null;
            try
            {
                using (DbFactory.CreateUnitOfWork())
                {
                    var lot = DbFactory.Lots.GetById(viewModel.Id);

                    lot.Context = viewModel.Context;
                    lot.Duration = viewModel.Duration;
                    lot.Direction = viewModel.Direction;
                    lot.StartingPrice = viewModel.StartingPrice;
                    lot.ModifidedDateTime = viewModel.ModifidedDateTime;

                    DbFactory.Lots.Save(lot);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        public bool DeleteLot(LotViewModel viewModel, out string error)
        {
            var res = false;
            error = null;
            try
            {
                using (DbFactory.CreateUnitOfWork())
                {
                    var lot = DbFactory.Lots.GetById(viewModel.Id);
                    DbFactory.Lots.Delete(lot);
                }
            }
            catch (Exception e)
            {
                res = true;
                error = e.Message;
            }
            return res;
        }

        #endregion

        #endregion
    }
}