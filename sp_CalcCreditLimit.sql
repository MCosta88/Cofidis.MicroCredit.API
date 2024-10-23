CREATE PROCEDURE sp_CalcCreditLimit
 @baseSalary DECIMAL(18, 2),
 @creditLimit DECIMAL(18, 2) OUTPUT
AS
BEGIN
    IF @baseSalary <= 1000
    BEGIN
        SET @creditLimit = 1000;
    END
    ELSE IF @baseSalary > 1000 AND @baseSalary <= 2000
    BEGIN
        SET @creditLimit = 2000;
    END
    ELSE
    BEGIN
        SET @creditLimit = 5000;
    END
END