SELECT a.FirstName, a.LastName
, b.FirstName ,b.LastName
FROM Employees a, Employees b
WHERE a.ManagerID=b.EmployeeID