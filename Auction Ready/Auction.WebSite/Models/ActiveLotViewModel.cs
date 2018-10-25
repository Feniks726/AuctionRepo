using System.ComponentModel.DataAnnotations;
using Auction.DAL.Data;

namespace Auction.WebSite.Models
{
    public class ActiveLotViewModel : LotViewModel
    {

        [Required(ErrorMessage = "!")]
        [Display(Name = "Лидирующая ставка")]
        [DataType(DataType.Currency)]
        public decimal LiderRate { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Лидер ID")]
        public long LiderID { get; set; }

        [Required(ErrorMessage = "!")]
        [Display(Name = "Лидер")]
        public User Lider { get; set; }
    }
}