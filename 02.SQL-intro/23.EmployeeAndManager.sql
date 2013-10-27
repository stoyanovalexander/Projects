SELECT emp.FirstName + ' ' + emp.LastName AS EmployeeName, m.FirstName + ' ' + m.LastName AS Manager FROM Employees m
RIGHT JOIN Employees emp ON emp.ManagerID = m.EmployeeID