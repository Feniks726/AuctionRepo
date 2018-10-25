using System;
using System.ComponentModel.DataAnnotations;
using Auction.DAL.Data;

namespace Auction.WebSite.Models
{
    public class AuctionViewModel
    {
        [Required(ErrorMessage = "!")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "LotId")]
        public Guid LotId { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "AuctionTemplateId")]
        public Guid AuctionTemplateId { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Контекст")]
        public string Context { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Направление")]
        public string DirectionText { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "UserId")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Пользователь")]
        public User User { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = " Ставка")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Создан")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDateTime { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Изменен")]
        [DataType(DataType.DateTime)]
        public DateTime ModifidedDateTime { get; set; }
    }
}