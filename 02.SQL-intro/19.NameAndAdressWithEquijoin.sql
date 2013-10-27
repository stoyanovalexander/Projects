SELECT e.FirstName, e.LastName, 
d.AddressText
FROM Employees e, Addresses d
WHERE e.AddressID=d.AddressID