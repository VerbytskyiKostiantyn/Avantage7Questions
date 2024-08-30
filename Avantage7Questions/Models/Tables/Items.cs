using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avantage7Questions.Models.Tables
{
    [AtLeastOneFieldValidator]
    //[AnotherWithoutText(ErrorMessage = "Заповніть поле Інше")]
    public class Items
    {
        [Key]
        public int Id { get; set; }

        public bool Computer { get; set; }
        public bool Laptop { get; set; }
        public bool Mouse { get; set; }
        public bool Keyboard { get; set; }
        public bool Monitor { get; set; }
        public bool Phone { get; set; }
        public bool Another { get; set; }
        public string? AnotherText { get; set; }
    }

    public class AtLeastOneFieldValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var items = value as Items;
            if (items == null)
            {
                return false;
            }
            if (!items.Computer &&
            !items.Laptop &&
            !items.Mouse &&
            !items.Keyboard &&
            !items.Monitor &&
            !items.Phone &&
            !items.Another)
            {
                ErrorMessage = "Виберіть хоча б один варіант";
                return false;
            }

            if (items.Another && items.AnotherText == null)
            {
                ErrorMessage = "Заповніть поле Інше";
                return false;
            }

            return true;
        }
    }
    //public class AnotherWithoutText : ValidationAttribute
    //{
    //    public override bool IsValid(object value)
    //    {
    //        var items = value as Items;

    //        if (items == null)
    //        {
    //            return false;
    //        }

    //        if (items.Another && items.AnotherText == null)
    //        {
    //            return false;
    //        }

    //        return true;
    //    }
    //}
}
