using System;
using System.ComponentModel.DataAnnotations;

namespace Testovoe_zadaniye.Models.OperationModels
{
    public class NewTourForm
    {
        [Required]
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Name { get; set; }


        //[DataRange (DateTime.Now, DateTime.Now.AddYears(1))]
        public DateTime Date { get; set; }
    }

    public class DataRangeAttribute : ValidationAttribute
    {
        private DateTime _beginDate;
        private DateTime _endDate;
        public DataRangeAttribute(DateTime begin, DateTime end)
        {
            _beginDate = begin;
            _endDate = end;
        }

        public override bool IsValid(object value)
        {
            DateTime dateToValidate;
            DateTime.TryParse(value.ToString(), out dateToValidate);

            // сделать проверку что дата корректна итп 

            return dateToValidate > _beginDate && dateToValidate < _endDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(
                "В указанном поле {0} введена не правильная дата , введите дату в диапозоне {1} - {2}", name, _beginDate,
                _endDate);
        }
    }
}
