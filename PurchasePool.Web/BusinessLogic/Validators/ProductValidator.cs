using System;
using PurchasePool.Common.Interfaces;
using PurchasePool.Common.Models;
using PurchasePool.Web.BusinessLogic.Exceptions.ValidationExeptions;

namespace PurchasePool.Web.BusinessLogic.Validators
{
    public class ProductValidator: IValidator
    {
        Product Product { get; set; }

        public ProductValidator(Product product)
        {
            Product = product;
        }

        public void Valid()
        {
            ValidName();
            ValidLink();
            ValidCategories();
        }

        protected void ValidName()
        {
            if(Product.Name == null || Product.Name?.Length == 0)
            {
                throw new ProductNameValidationException("The product hasn't name");
            }
        }

        protected void ValidLink()
        {
            if (Product.WebLink== null || Product.WebLink?.Length == 0 || !IsUrl(Product.WebLink))
            {
                throw new ProductWebLinkValidationException("The product hasn't right web-link");
            }
        }

        protected void ValidCategories()
        {
            if (Product.Categories == null || Product.Categories?.Count == 0)
            {
                throw new ProductCategoriesValidationException("The product hasn't any category");
            }
        }

        protected bool IsUrl(string urlCandidate)
        {
            return Uri.IsWellFormedUriString(urlCandidate, UriKind.Absolute);
        }

    }
}