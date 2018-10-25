using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.WebSite.Models
{
    public class LotViewModel
    {

        [Required(ErrorMessage = "!")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "AuctionTemplateId")]
        public Guid AuctionTemplateId { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Содержание")]
        public string Context { get; set; }

        [Display(Name = "Продолжительность")]
        [DataType(DataType.Duration)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Duration { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Начальная ставка")]
        [DataType(DataType.Currency)]
        public decimal StartingPrice { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Направление")]
        public short Direction { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Направление")]
        public string DirectionText { get; set; }

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