using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Auction.Common.Constant;
using Auction.Common.Constant.Messages;
using Auction.DAL.Factories;
using Auction.WebSite.Infrastructure;
using Auction.WebSite.Infrastructure.Interfaces;
using Auction.WebSite.Models;
using NLog;

namespace Auction.WebSite.Controllers
{
    public class AuctionsController : BaseController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IAuctionProvider _auctionProvider;

        private readonly IAuthProvider _authProvider;

        public AuctionsController(IDbFactory dbFactory,
            IAuctionProvider auctionProvider,
            IAuthProvider authProvider)
            : base(dbFactory)
        {
            _auctionProvider = auctionProvider;
            _authProvider = authProvider;
        }

        #region Auctions

        // GET: Auction
        [HttpGet]
        public async Task<ActionResult> ActiveAuctions(string filterByName)
        {
            var model = await GetFullAndPartialAuctionViewModel(filterByName);
            return View(model);
        }
 
        [HttpGet]
        public async Task<ActionResult> GetActiveAuctionsResults(string filterByName)
        {
            var model = await GetFullAndPartialAuctionViewModel(filterByName);
            return PartialView("ActiveAuctionsResults", model);
        }

        private async Task<List<AuctionTemplateViewModel>> GetFullAndPartialAuctionViewModel(string filterByName)
        {
            var model = _auctionProvider.GetActiveAuctions(filterByName);
            foreach (var m in model)
            {
                m.Started = m.IsStarted
                    ? Messages.MessagesConstant.Yes.ToString()
                    : Messages.MessagesConstant.No.ToString();
            }
            return model;
        }

        [HttpGet]
        public async Task<ActionResult> ActiveLots(Guid id, string filterByContext)
        {
            var model = await GetFullAndPartialLotsViewModel(id, filterByContext);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetActiveLotsResults(Guid id, string filterByContext)
        {
            var model = await GetFullAndPartialLotsViewModel(id, filterByContext);
            return PartialView("ActiveLotsResults", model);
        }

        private async Task<List<ActiveLotViewModel>> GetFullAndPartialLotsViewModel(Guid id, string filterByContext)
        {
            var model = _auctionProvider.GetActiveLotsForAuction(id, filterByContext);
            return model;
        }

        [HttpGet]
        public ActionResult CreateRate(Guid? id, Guid? auctionTemplateId)
        {
            if (!id.HasValue)
                return RedirectToAction("ActiveLots", "Auctions");
            if (!auctionTemplateId.HasValue)
                return RedirectToAction("ActiveLots", "Auctions");
            var lot =
                _auctionProvider.GetActiveLotsForAuction(auctionTemplateId.Value, null)
                    .FirstOrDefault(_ => _.Id == id);
            if (lot == null)
            {
                var viewModel = _auctionProvider.GetLotModelById(id.Value);

                Logger.Debug(Messages.LogsConstant.SelectedLotClosed.Format(viewModel.Context, Session.GetUserId()));
                ModelState.AddModelError("",
                    Messages.MessagesConstant.SelectedLotClosed.Format(viewModel.Context, Session.GetUserId())); 

            }
            var model = new AuctionViewModel
            {
                LotId = id.Value,
                AuctionTemplateId = auctionTemplateId.Value,
                Context = lot.Context,
                DirectionText = lot.DirectionText,
                UserId = Session.GetUserId(),
                User = _authProvider.GetUserById(Session.GetUserId())
             };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRate(AuctionViewModel viewModel, string returnUrl)
        {
            viewModel.Id = Guid.NewGuid();
            viewModel.CreateDateTime = DateTime.Now;
            viewModel.ModifidedDateTime = DateTime.Now;

            string error;

            var checkRate = _auctionProvider.CheckRate(viewModel, out error);
            if (checkRate != 0)
            {
                Logger.Debug(error);
                if (checkRate == 1)
                    ModelState.AddModelError("",
                        Messages.MessagesConstant.TheDirectionIncreaseAndStartingPriceMoreThanRate.ToString());
                if (checkRate == -1)
                    ModelState.AddModelError("",
                        Messages.MessagesConstant.TheDirectionDecreaseAndStartingPriceLessThanRate.ToString());
                var model = new AuctionViewModel
                {
                    LotId = viewModel.LotId,
                    AuctionTemplateId = viewModel.AuctionTemplateId,
                    Context = viewModel.Context,
                    DirectionText = viewModel.DirectionText,
                    UserId = Session.GetUserId(),
                    User = _authProvider.GetUserById(Session.GetUserId()),
                    Rate = viewModel.Rate
                };
                return View(model);
            }
            if (!_auctionProvider.CreateRate(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheRateCreated.Format(viewModel.Context, Session.GetUserId()));
                return RedirectToAction("ActiveLots", "Auctions", new {id = viewModel.AuctionTemplateId});
            }
            Logger.Debug(Messages.LogsConstant.TheRateCreateddMistake.Format(viewModel.Context, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheRateCreatedMistake.Format(viewModel.Context, Session.GetUserId(), error));

            return View(viewModel);
        }

        #endregion

        #region AuctionTemplates

        public ActionResult AuctionTemplates(string filterByName)
        {
            var model = _auctionProvider.GetMyAuctions(Session.GetUserId(), filterByName);
            foreach (var m in model)
            {
                m.Started = m.IsStarted
                    ? Messages.MessagesConstant.Yes.ToString()
                    : Messages.MessagesConstant.No.ToString();
            }
            return View(model);
        }

        public ActionResult CreateTemplate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTemplate(AuctionTemplateViewModel viewModel, string returnUrl)
        {
            viewModel.CreateDateTime = DateTime.Now;
            viewModel.ModifidedDateTime = DateTime.Now;
            viewModel.AuthorID = Session.GetUserId();
            viewModel.IsStarted = false;

            string error;
            if (!_auctionProvider.CreateAuctionTemplate(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheAuctionCreated.Format(viewModel.Name, Session.GetUserId()));
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            Logger.Debug(Messages.LogsConstant.TheAuctionCreatedMistake.Format(viewModel.Name, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheAuctionCreatedMistake.Format(viewModel.Name, Session.GetUserId(), error));

            return View();
        }

        [HttpGet]
        public ActionResult EditTemplate(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            var model = _auctionProvider.GetAuctionTemplateModelById(id);
            if (model != null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditTemplate(AuctionTemplateViewModel viewModel, string returnUrl)
        {
            viewModel.ModifidedDateTime = DateTime.Now;

            string error;
            if (!_auctionProvider.EditAuctionTemplate(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheAuctionEdit.Format(viewModel.Name, Session.GetUserId()));
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            Logger.Debug(Messages.LogsConstant.TheAuctionEditMistake.Format(viewModel.Name, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheAuctionEditMistake.Format(viewModel.Name, Session.GetUserId(), error));

            return View();
        }

        [HttpGet]
        public ActionResult DeleteTemplate(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            var model = _auctionProvider.GetAuctionTemplateModelById(id);
            if (model != null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult DeleteTemplate(AuctionTemplateViewModel viewModel, string returnUrl)
        {
            string error;
            if (!_auctionProvider.DeleteAuctionTemplate(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheAuctionDelete.Format(viewModel.Name, Session.GetUserId()));
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            Logger.Debug(Messages.LogsConstant.TheAuctionDeleteMistake.Format(viewModel.Name, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheAuctionDeleteMistake.Format(viewModel.Name, Session.GetUserId(), error));

            return View();
        }

        [HttpGet]
        public ActionResult DetailsTemplate(Guid? id, string filterByContext)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var model = _auctionProvider.GetAuctionTemplateModelById(id, filterByContext);
            if (model != null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult DetailsTemplate(AuctionTemplateViewModel viewModel, string returnUrl)
        {
            string error;
            if (!_auctionProvider.DeleteAuctionTemplate(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheAuctionDelete.Format(viewModel.Name, Session.GetUserId()));
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            Logger.Debug(Messages.LogsConstant.TheAuctionDeleteMistake.Format(viewModel.Name, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheAuctionDeleteMistake.Format(viewModel.Name, Session.GetUserId(), error));

            return View();
        }

        [HttpGet]
        public ActionResult StartStopTemplate(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            var model = _auctionProvider.GetAuctionTemplateModelById(id);
            return model != null ? (ActionResult)View(model) : HttpNotFound();
        }

        [HttpPost]
        public ActionResult StartStopTemplate(AuctionTemplateViewModel viewModel, string returnUrl)
        {
            viewModel = _auctionProvider.GetAuctionTemplateModelById(viewModel.Id);
            viewModel.ModifidedDateTime = DateTime.Now;
            viewModel.IsStarted = !viewModel.IsStarted;
            if (viewModel.IsStarted)
                viewModel.StartedDateTime = DateTime.Now;

            string error;
            if (!_auctionProvider.EditAuctionTemplate(viewModel, out error))
            {
                Logger.Debug(viewModel.IsStarted
                    ? Messages.LogsConstant.TheAuctionStart.Format(viewModel.Name, Session.GetUserId())
                    : Messages.LogsConstant.TheAuctionStop.Format(viewModel.Name, Session.GetUserId()));
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            Logger.Debug(viewModel.IsStarted
                    ? Messages.LogsConstant.TheAuctionStartMistake.Format(viewModel.Name, Session.GetUserId(), error)
                    : Messages.LogsConstant.TheAuctionStopMistake.Format(viewModel.Name, Session.GetUserId(), error));
            ModelState.AddModelError("",
                viewModel.IsStarted
                    ? Messages.MessagesConstant.TheAuctionStartMistake.Format(viewModel.Name, Session.GetUserId(), error)
                    : Messages.MessagesConstant.TheAuctionStopMistake.Format(viewModel.Name, Session.GetUserId(), error));

            return View();
        }

        #region Lots

        public ActionResult CreateLot(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("AuctionTemplates", "Auctions");
            var model = new LotViewModel { AuctionTemplateId = id.Value };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateLot(LotViewModel viewModel, string returnUrl)
        {
            viewModel.Id = Guid.NewGuid();
            viewModel.CreateDateTime = DateTime.Now;
            viewModel.ModifidedDateTime = DateTime.Now;
            viewModel.Direction = viewModel.DirectionText == Messages.MessagesConstant.Increase.ToString()
                ? DirectionOfLot.Increase
                : DirectionOfLot.Decrease;
            viewModel.Duration = viewModel.Duration.HasValue
                ? DateTimeConstant.MinDate.AddHours(viewModel.Duration.Value.Hour)
                    .AddMinutes(viewModel.Duration.Value.Minute)
                    .AddSeconds(viewModel.Duration.Value.Second)
                : (DateTime?)null;

            string error;
            if (!_auctionProvider.CreateLot(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheLotCreated.Format(viewModel.Context, Session.GetUserId()));
                return RedirectToAction("DetailsTemplate", "Auctions", new { id = viewModel.AuctionTemplateId });
            }
            Logger.Debug(Messages.LogsConstant.TheLotCreatedMistake.Format(viewModel.Context, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheLotCreatedMistake.Format(viewModel.Context, Session.GetUserId(), error));

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult EditLot(Guid? id)
        {
            if (!id.HasValue)
                return RedirectToAction("AuctionTemplates", "Auctions");

            var model = _auctionProvider.GetLotModelById(id.Value);
            if (model != null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditLot(LotViewModel viewModel, string returnUrl)
        {
            viewModel.ModifidedDateTime = DateTime.Now;
            viewModel.Direction = viewModel.DirectionText == Messages.MessagesConstant.Increase.ToString()
                ? DirectionOfLot.Increase
                : DirectionOfLot.Decrease;
            viewModel.Duration = viewModel.Duration.HasValue
                ? DateTimeConstant.MinDate.AddHours(viewModel.Duration.Value.Hour)
                    .AddMinutes(viewModel.Duration.Value.Minute)
                    .AddSeconds(viewModel.Duration.Value.Second)
                : (DateTime?)null;

            string error;
            if (!_auctionProvider.EditLot(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheLotEdited.Format(viewModel.Context, Session.GetUserId()));
                return RedirectToAction("DetailsTemplate", "Auctions", new { id = viewModel.AuctionTemplateId });
            }
            Logger.Debug(Messages.LogsConstant.TheLotEditedMistake.Format(viewModel.Context, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheLotEditedMistake.Format(viewModel.Context, Session.GetUserId(), error));

            return View();
        }

        [HttpGet]
        public ActionResult DeleteLot(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("AuctionTemplates", "Auctions");
            }
            var model = _auctionProvider.GetLotModelById(id.Value);
            if (model != null)
            {
                return View(model);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult DeleteLot(LotViewModel viewModel, string returnUrl)
        {
            string error;
            var model = _auctionProvider.GetLotModelById(viewModel.Id);
            if (!_auctionProvider.DeleteLot(viewModel, out error))
            {
                Logger.Debug(Messages.LogsConstant.TheLotDeleted.Format(viewModel.Context, Session.GetUserId()));
                return RedirectToAction("DetailsTemplate", "Auctions", new { id = model.AuctionTemplateId });
            }
            Logger.Debug(Messages.LogsConstant.TheLotDeletedMistake.Format(viewModel.Context, Session.GetUserId(), error));
            ModelState.AddModelError("",
                Messages.MessagesConstant.TheLotDeletedMistake.Format(viewModel.Context, Session.GetUserId(), error));

            return View();
        }
        #endregion

        #endregion
    }
}