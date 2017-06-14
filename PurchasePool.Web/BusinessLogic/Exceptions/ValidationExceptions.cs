using System;

namespace PurchasePool.Web.BusinessLogic.Exceptions.ValidationExeptions
{
    public class ProductNameValidationException: Exception
    {                
        public ProductNameValidationException()
        {}
        public ProductNameValidationException(string message): base(message)
        { }
    }
    public class ProductWebLinkValidationException : Exception
    {
        public ProductWebLinkValidationException()
        { }
        public ProductWebLinkValidationException(string message): base(message)
        { }
    }
    public class ProductCategoriesValidationException : Exception
    {
        public ProductCategoriesValidationException()
        { }
        public ProductCategoriesValidationException(string message): base(message)
        { }
    }
}