--FOR TEST

SELECT *
FROM Products p
JOIN ProductDetails pd ON p.Id = pd.ProductId
JOIN Brands b ON pd.BrandId = b.Id
JOIN ProductCategories pc ON p.Id = pc.ProductId
JOIN Categories c ON pc.CategoryId = c.Id
