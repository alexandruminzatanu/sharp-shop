BEGIN TRANSACTION;

IF NOT EXISTS (SELECT 1 FROM [Products])
BEGIN
    ;WITH Tally AS (
        SELECT TOP (100) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
        FROM sys.all_objects
    )
    INSERT INTO [Products] ([Name], [Description], [Price], [Stock])
    SELECT
        CONCAT('Product ', n) AS [Name],
        CONCAT('Sample description for product ', n) AS [Description],
        CAST(9.99 + (n - 1) * 0.5 AS DECIMAL(10,2)) AS [Price],
        10 + ((n - 1) % 20) * 5 AS [Stock]
    FROM Tally;
END

COMMIT;
