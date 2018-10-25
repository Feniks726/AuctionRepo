using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Auction.DAL.Data;

namespace Auction.WebSite.Models
{
    public class AuctionTemplateViewModel
    {
        [Required(ErrorMessage = "!")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Создан")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDateTime { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Изменен")]
        [DataType(DataType.DateTime)]
        public DateTime ModifidedDateTime { get; set; }

        [Display(Name = "Автор")]
        public long AuthorID { get; set; }

        [Display(Name = "Автор")]
        public User Author { get; set; }

        [Display(Name = "Старт")]
        public bool IsStarted { get; set; }

        [Display(Name = "Запущен")]
        public string Started { get; set; }

        [Display(Name = "Лоты")]
        public List<ActiveLotViewModel> Lots { get; set; }

        [Display(Name = "Время старта")]
        public DateTime? StartedDateTime { get; set; }

        [Display(Name = "Количество лотов")]
        public int LotsCount { get; set; }
    }
}