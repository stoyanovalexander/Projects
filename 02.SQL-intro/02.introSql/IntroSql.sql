		/*BEFORE START TO CHECK CHANGE IT TO
	     TELERIK ACADEMY DB*/
/*I write this because hapend on me to forgot it*/

/*01.WhatIsSQLandSoOn
	SQL-Structured Query  Language
	DML-Data Manipulation Language
	DDL-Data Definition Language
	SQL(comands)- select, insert, update, delete
	*/

/*02.WhatIsTSQL
	TSQL - Transact SQL(structured Query Language). 
	Standart language used in MS SQL Server, which 
	suport if statements, loops and exceptions.
	*/
		

/*04.DepartmentsAllData */
	SELECT * FROM Departments

/*05.DepartmentNames*/
	SELECT
	Name
	FROM Departments

/*06.AllSalaries*/
	SELECT Salary, FirstName, LastName
	FROM Employees
	
/*07.FullName*/
	SELECT FirstName + ' ' + LastName AS [Full Name]
	FROM Employees

/*08.Emails*/
	SELECT FirstName + '.' + LastName + '@telerik.com' as [Full Email Addresses] 
	FROM Employees

/*09.TheDifferentSalaries*/
	SELECT 
	DISTINCT Salary
	FROM Employees

/*10.SalesRepresentativeAllInfo*/
	SELECT * FROM
	Employees WHERE JobTitle='Sales Representative'

/*11.NamesStartsWithSa*/
	SELECT FirstName + ' ' + LastName AS Name FROM Employees
	Where FirstName LIKE 'SA%'

/*12.LastNamesContainsEi*/
	SELECT FirstName + ' ' + LastName AS Name FROM Employees
	WHERE LASTNAME LIKE '%ei%'

/*13.SalariesInRange*/
	SELECT Salary FROM Employees
	WHERE Salary BETWEEN 20000 AND 30000

/*14.EmplWithSomeSalaries*/
	SELECT FirstName, LastName FROM Employees 
	WHERE Salary IN (25000, 1400, 12500)

/*15.EmployeesWitOutManager*/
	SELECT lastname, firstName 
	FROM Employees
	WHERE ManagerID IS NULL

/*16.SalaryMoreThan50000*/
	SELECT em.LastName, em.FirstName, em.Salary
	FROM Employees em
	WHERE  em.Salary>50000 
	ORDER BY em.Salary DESC

/*17.TopFive*/
	SELECT top 5 em.LastName, em.FirstName, em.Salary
	FROM Employees em  
	ORDER BY em.Salary DESC

/*18.EmployeesAndAddress*/
	SELECT em.FirstName, em.LastName, ad.AddressText
	FROM Employees em 
	inner Join Addresses ad 
	ON em.AddressID=ad.AddressID

/*19.NameAndAdressWithEquijoin*/
	SELECT e.FirstName, e.LastName, 
	a.AddressText
	FROM Employees e, Addresses a
	WHERE e.AddressID=a.AddressID

/*20.EmployeeAndManager*/
	SELECT e.FirstName, e.LastName
	, m.FirstName ,m.LastName
	FROM Employees e, Employees m
	WHERE e.ManagerID=m.EmployeeID

/*21.EmplManagerAdress*/
	SELECT e.FirstName, e.LastName
	, m.FirstName , m.LastName
	, a.AddressText
	FROM Employees e, Employees m, Addresses a
	WHERE e.ManagerID=m.EmployeeID 
	AND e.AddressID=a.AddressID

/*22.CityNames*/
	SELECT Name 
	FROM Departments
	UNION 
	SELECT Name 
	FROM Towns

/*23.EmployeeAndManager*/
	SELECT emp.FirstName + ' ' + emp.LastName AS EmployeeName
	, m.FirstName + ' ' + m.LastName AS Manager 
	FROM Employees m
	RIGHT JOIN Employees emp 
	ON emp.ManagerID = m.EmployeeID

	/*WithLeftOuterJoin*/
		select m.FirstName+' '+m.LastName as Manager
		, emp.FirstName+' '+emp.LastName as EmployeeName
		from Employees emp
		left outer join Employees m
		on emp.ManagerID=m.EmployeeID

/*24.HireYearBetween*/
	SELECT emp.FirstName + ' ' + emp.LastName AS EmployeeName
	, d.Name, emp.HireDate 
	FROM Employees emp
	JOIN Departments d 
	ON emp.DepartmentID = d.DepartmentID
	WHERE d.Name IN ('Sales', 'Finance') 
	AND (YEAR(emp.HireDate) > 1995 
	AND YEAR(emp.HireDate) < 2000)