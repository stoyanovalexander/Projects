SELECT emp.FirstName + ' ' + emp.LastName AS EmployeeName, d.Name, emp.HireDate FROM Employees emp
JOIN Departments d ON emp.DepartmentID = d.DepartmentID
WHERE d.Name IN ('Sales', 'Finance') AND (YEAR(emp.HireDate) > 1995 AND YEAR(emp.HireDate) < 2000)