SELECT em.FirstName, em.LastName, ad.AddressText
FROM Employees em inner Join Addresses ad ON em.AddressID=ad.AddressID