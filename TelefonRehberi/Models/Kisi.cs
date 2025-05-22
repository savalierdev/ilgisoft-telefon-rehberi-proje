using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TelefonRehberi.Models
{
    [Index(nameof(TelefonNumarasi), IsUnique = true)]
    [Index(nameof(Ad), nameof(Soyad), IsUnique = true)]
    // Telefon numarasının benzersiz olmasını sağlamak için bir index ekledim.
    public class Kisi
    {
        [Key]
        public int KisiId { get; set; }
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Telefon numarası alanı boş bırakılamaz.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        public string TelefonNumarasi { get; set; }
    }
}