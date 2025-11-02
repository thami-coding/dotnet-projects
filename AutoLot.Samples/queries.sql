CREATE VIEW [dbo].[CustomerOrderView]
AS
 SELECT dbo.Customers.FirstName, dbo.Customers.LastName, dbo.Inventory.Color,
  dbo.Inventory.PetName, dbo.Makes.Name AS Make
 FROM dbo.Orders
  INNER JOIN dbo.Customers ON dbo.Orders.CustomerId=dbo.Customers.Id
  INNER JOIN dbo.Inventory ON dbo.Orders.CarId=dbo.Inventory.Id
  INNER JOIN dbo.Makes ON dbo.Makes.Id=dbo.Inventory.MakeId;