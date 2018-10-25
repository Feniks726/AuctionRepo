using System;
using System.Collections.Generic;
using Auction.WebSite.Models;

namespace Auction.WebSite.Infrastructure.Interfaces
{
    public interface IAuctionProvider
    {
        #region Auctions

        /// <summary>
        /// Get Active Auctions
        /// </summary>
        /// <param name="filterByName"></param>
        /// <returns></returns>
        List<AuctionTemplateViewModel> GetActiveAuctions(string filterByName);

        /// <summary>
        /// Get Active Lots For Auction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filterByName"></param>
        /// <returns></returns>
        List<ActiveLotViewModel> GetActiveLotsForAuction(Guid id, string filterByName);

        /// <summary>
        /// Create Rate
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool CreateRate(AuctionViewModel viewModel, out string error);

        /// <summary>
        /// Check Rate
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        int CheckRate(AuctionViewModel viewModel, out string error);


        #endregion

        #region AuctionTemplates

        /// <summary>
        /// Get all Auction Template created by user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filterByName"></param>
        /// <returns></returns>
        List<AuctionTemplateViewModel> GetMyAuctions(long userId, string filterByName);

        /// <summary>
        /// Get viewModel by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filterLotByContext"></param>
        /// <returns></returns>
        AuctionTemplateViewModel GetAuctionTemplateModelById(Guid? id, string filterLotByContext = null);

        /// <summary>
        /// Create new Auction Template
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool CreateAuctionTemplate(AuctionTemplateViewModel viewModel, out string error);

        /// <summary>
        /// Edit Auction Template
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool EditAuctionTemplate(AuctionTemplateViewModel viewModel, out string error);

        /// <summary>
        /// Delete Auction Template
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteAuctionTemplate(AuctionTemplateViewModel viewModel, out string error);

        #endregion

        #region Lots

        /// <summary>
        /// Get lot model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        LotViewModel GetLotModelById(Guid id);

        /// <summary>
        /// Create new lot
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool CreateLot(LotViewModel viewModel, out string error);

        /// <summary>
        /// Edit Lot
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool EditLot(LotViewModel viewModel, out string error);

        /// <summary>
        /// Delete Lot
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool DeleteLot(LotViewModel viewModel, out string error);
        #endregion
    }
}