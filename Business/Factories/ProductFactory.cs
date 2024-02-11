﻿using Business.Dtos;
using Infrastructure.Entities;

namespace Business.Factories;

public class ProductFactory
{
    public static BrandEntity Create(string brandName)
    {
        return new BrandEntity
        {
            BrandName = brandName
        };
    }

    public static CategoryEntity Create(int? parentCategoryId, string categoryName)
    {
        return new CategoryEntity
        {
            ParentCategoryId = parentCategoryId != null ? parentCategoryId : null!,
            CategoryName = categoryName
        };
    }

    public static ProductEntity Create(string productId, string productName, CategoryEntity categoryEntity)
    {
        return new ProductEntity
        {
            Id = productId,
            ProductName = productName,
            Categories = new List<CategoryEntity> { categoryEntity }
        };
    }

    public static ProductDetailEntity Create(string productId, int brandId, decimal unitPrice, string? color, string? size)
    {
        return new ProductDetailEntity
        {
            ProductId = productId,
            BrandId = brandId,
            UnitPrice = unitPrice,
            Color = color,
            Size = size
        };
    }

    public static ProductDto Create(ProductEntity productEntity)
    {
        return new ProductDto 
        {
            ProductId = productEntity.Id,
            ProductName = productEntity.ProductName,

            UnitPrice = productEntity.ProductDetail?.UnitPrice ?? 0,
            Color = productEntity.ProductDetail?.Color,
            Size = productEntity.ProductDetail?.Size,

            BrandId = productEntity.ProductDetail != null ? productEntity.ProductDetail.BrandId : 0,
            BrandName = productEntity.ProductDetail != null ? productEntity.ProductDetail.Brand.BrandName : null!,

            CategoryId = productEntity.Categories
                          .Select(c => c.Id)
                          .FirstOrDefault(),
            ParentCategoryId = productEntity.Categories
                        .Where(c => c.ParentCategoryId != null) 
                        .Select(c => c.ParentCategoryId.Value) 
                        .FirstOrDefault(),
            CategoryName = productEntity.Categories
                      .Select(c => c.CategoryName)
                      .FirstOrDefault() ?? null!
        };
    }


    public static ProductDto Create(ProductEntity productEntity, ProductDetailEntity? productDetailEntity, BrandEntity? brandEntity, CategoryEntity categoryEntity)
    {
        return new ProductDto
        {
            ProductId = productEntity.Id,
            ProductName = productEntity.ProductName,

            UnitPrice = productDetailEntity != null ? productDetailEntity.UnitPrice : 0,
            Color = productDetailEntity != null ? productDetailEntity.Color : null!,
            Size = productDetailEntity != null ? productDetailEntity.Size : null!,

            BrandId = brandEntity != null ? brandEntity.Id : 0,
            BrandName = brandEntity != null ? brandEntity.BrandName : null!,

            CategoryId = categoryEntity.Id,
            ParentCategoryId = (int)(categoryEntity.ParentCategoryId != null ? categoryEntity.ParentCategoryId : null!),
            CategoryName = categoryEntity.CategoryName,
        };
    }

    public static IEnumerable<ProductDto> Create(IEnumerable<ProductEntity> productEntities) 
    {
        List<ProductDto> dtoList = [];
        foreach (ProductEntity productEntity in productEntities)
        {
            dtoList.Add(Create(productEntity));
        }
        return dtoList;
    }

}
