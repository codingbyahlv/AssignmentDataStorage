--FOR TEST

SELECT * FROM Customers c JOIN CustomerProfiles cp ON cp.CustomerId = c.Id JOIN Addresses a ON a.Id = cp.AddressId

