SET NOCOUNT ON;
BEGIN TRY
    PRINT '==> Seed start';
    BEGIN TRANSACTION;

    DECLARE @CategoryTarget INT;  SET @CategoryTarget = 1000;   -- target categories (adjust)
    DECLARE @ProductCount   INT;  SET @ProductCount   = 10000;  -- target products when empty
    DECLARE @OrderCount     INT;  SET @OrderCount     = 10000;  -- target orders when empty

    --------------------------------------------------
    -- Categories
    --------------------------------------------------
    DECLARE @ExistingCategories INT; 
    SELECT @ExistingCategories = COUNT(*) FROM dbo.Categories;
    IF (@ExistingCategories < @CategoryTarget)
    BEGIN
        DECLARE @i INT; SET @i = @ExistingCategories + 1;
        WHILE (@i <= @CategoryTarget)
        BEGIN
            INSERT INTO dbo.Categories (Name, Description)
            VALUES ('Category ' + CAST(@i AS NVARCHAR(20)), 'Sample description for category ' + CAST(@i AS NVARCHAR(20)));
            SET @i = @i + 1;
            IF (@i % 200 = 0) PRINT '   Categories inserted so far: ' + CAST(@i AS NVARCHAR(20));
        END
    END
    ELSE PRINT 'Categories already sufficient: ' + CAST(@ExistingCategories AS NVARCHAR(20));

    -- Refresh count after potential inserts
    SELECT @ExistingCategories = COUNT(*) FROM dbo.Categories;

    --------------------------------------------------
    -- Orders
    --------------------------------------------------
    DECLARE @OrderTotal INT; SELECT @OrderTotal = COUNT(*) FROM dbo.Orders;
    IF (@OrderTotal = 0)
    BEGIN
        DECLARE @o INT; SET @o = 1;
        WHILE (@o <= @OrderCount)
        BEGIN
            INSERT INTO dbo.Orders (TotalPrice, DeliveryAddress, Payed)
            VALUES (
                50 + (@o * 5),
                'Delivery Address ' + CAST(@o AS NVARCHAR(20)),
                CASE WHEN (@o % 2) = 0 THEN 1 ELSE 0 END
            );
            SET @o = @o + 1;
            IF (@o % 1000 = 0) PRINT '   Orders inserted so far: ' + CAST(@o AS NVARCHAR(20));
        END
        SELECT @OrderTotal = COUNT(*) FROM dbo.Orders;
        PRINT 'Inserted ' + CAST(@OrderTotal AS NVARCHAR(20)) + ' orders';
    END
    ELSE PRINT 'Orders already exist: ' + CAST(@OrderTotal AS NVARCHAR(20));
    IF (@OrderTotal = 0) RAISERROR('No orders available for product links.', 16, 1);

    --------------------------------------------------
    -- Products (assign CategoryId round-robin)
    --------------------------------------------------
    IF NOT EXISTS (SELECT 1 FROM dbo.Products)
    BEGIN
        -- Build an indexable list of current Category IDs (handles non-sequential identities)
        DECLARE @CatIds TABLE(RowNum INT IDENTITY(1,1), CategoryId INT NOT NULL);
        INSERT INTO @CatIds(CategoryId)
            SELECT Id FROM dbo.Categories ORDER BY Id;

        DECLARE @CatTotal INT; SELECT @CatTotal = COUNT(*) FROM @CatIds;
        IF (@CatTotal = 0) RAISERROR('No categories available to assign to products.', 16, 1);

        DECLARE @p INT; SET @p = 1;
        WHILE (@p <= @ProductCount)
        BEGIN
            DECLARE @catIndex INT; SET @catIndex = ((@p - 1) % @CatTotal) + 1;
            DECLARE @catId INT; SELECT @catId = CategoryId FROM @CatIds WHERE RowNum = @catIndex;
            INSERT INTO dbo.Products (Name, Description, Price, Stock, CategoryId)
            VALUES (
                'Product ' + CAST(@p AS NVARCHAR(20)),
                'Sample description for product ' + CAST(@p AS NVARCHAR(20)),
                CAST(999 + (@p % 50) * 3 AS DECIMAL(10,2)),
                10 + (@p % 25) * 2,
                @catId
            );
            SET @p = @p + 1;
            IF (@p % 1000 = 0) PRINT '   Products inserted so far: ' + CAST(@p AS NVARCHAR(20));
        END
    END
    ELSE PRINT 'Products already exist';

    --------------------------------------------------
    -- Many-to-Many Join Seeding (OrderModelProductModel)
    --------------------------------------------------
    IF OBJECT_ID('dbo.OrderModelProductModel','U') IS NOT NULL
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.OrderModelProductModel)
        BEGIN
            PRINT '   Seeding join table (first pass)...';
            ;WITH P AS (
                SELECT Id AS ProductId, ROW_NUMBER() OVER (ORDER BY Id) rn FROM dbo.Products
            ), O AS (
                SELECT Id AS OrderId, ROW_NUMBER() OVER (ORDER BY Id) rn FROM dbo.Orders
            )
            INSERT INTO dbo.OrderModelProductModel (OrdersId, ProductsId)
            SELECT O.OrderId, P.ProductId
            FROM P
            JOIN O ON O.rn = ((P.rn - 1) % @OrderTotal) + 1;

            PRINT '   Seeding join table (second pass offset)...';
            DECLARE @Half INT; SET @Half = CASE WHEN @OrderTotal < 2 THEN 0 ELSE (@OrderTotal / 2) END;
            ;WITH P AS (
                SELECT Id AS ProductId, ROW_NUMBER() OVER (ORDER BY Id) rn FROM dbo.Products
            ), O AS (
                SELECT Id AS OrderId, ROW_NUMBER() OVER (ORDER BY Id) rn FROM dbo.Orders
            )
            INSERT INTO dbo.OrderModelProductModel (OrdersId, ProductsId)
            SELECT O2.OrderId, P.ProductId
            FROM P
            JOIN O O2 ON O2.rn = (((P.rn - 1 + @Half) % @OrderTotal) + 1)
            WHERE NOT EXISTS (
                SELECT 1 FROM dbo.OrderModelProductModel E
                WHERE E.OrdersId = O2.OrderId AND E.ProductsId = P.ProductId
            );
            DECLARE @JoinCount INT; SELECT @JoinCount = COUNT(*) FROM dbo.OrderModelProductModel;
            PRINT 'Join rows inserted: ' + CAST(@JoinCount AS NVARCHAR(20));
        END
        ELSE PRINT 'Join table already populated';
    END
    ELSE PRINT 'Join table OrderModelProductModel not found - skipped';

    COMMIT TRANSACTION;
    PRINT '==> Seed completed successfully';
END TRY
BEGIN CATCH
    IF (XACT_STATE() <> 0) ROLLBACK TRANSACTION;
    DECLARE @ErrMsg NVARCHAR(4000); SET @ErrMsg = ERROR_MESSAGE();
    DECLARE @ErrSeverity INT;       SET @ErrSeverity = ERROR_SEVERITY();
    DECLARE @ErrState INT;          SET @ErrState = ERROR_STATE();
    RAISERROR('Seed script failed: %s', @ErrSeverity, @ErrState, @ErrMsg);
END CATCH;

SET NOCOUNT OFF;
