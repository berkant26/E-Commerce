﻿using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
           IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExist(product.ProductName));

            if(result != null)
            {
                return result;
            }
                _productDal.Add(product);   
            return new SuccessResult(Messages.ProductAdded);
        }   

        public IDataResult< List<Product>> GetAll()
        {
            if(DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(),Messages.ProductListed);
        }

        public  IDataResult< Product >GetById(int productId)
        {
            return new SuccessDataResult<Product> (_productDal.Gett(p => p.ProductId == productId)); 
        }

        public IDataResult< List<ProductDetailDto>> GetProductDetails()
        {
            return  new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        public IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if(result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfProductError);
            }
            return new SuccessResult();
        }
        public IResult CheckIfProductNameExist(string productName)
        {

            var result = _productDal.GetAll(p=> p.ProductName == productName).Any();
            if(result)
                    return new ErrorResult(Messages.ProductCountOfProductError);
            return new SuccessResult();
        }
    }
}
