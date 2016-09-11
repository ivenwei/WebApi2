using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI0911.Models
{
    public class ProdoctsPatchViewModels : IValidatableObject
    {
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Stock { get; set; }


        //Model內出現錯誤  BINDING錯誤  只能用內部的訊息回應 TRY CATCH不到
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        

    }
}