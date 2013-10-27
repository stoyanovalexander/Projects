SELECT e.FirstName, e.LastName, m.FirstName 
, m.LastName, a.AddressText
FROM Employees e, Employees m, Addresses a
WHERE e.ManagerID=m.EmployeeID 
AND e.AddressID=a.AddressID