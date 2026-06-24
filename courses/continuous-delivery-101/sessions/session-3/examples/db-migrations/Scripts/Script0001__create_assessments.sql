-- Baseline schema for a fresh database (the local playground starts empty).
-- Idempotent create: safe even though DbUp's journal already prevents re-application.
IF OBJECT_ID('dbo.Assessments', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Assessments (
        AssessmentId INT          NOT NULL PRIMARY KEY,
        HomeownerId  INT          NOT NULL,
        AmountCents  INT          NOT NULL,
        Status       VARCHAR(20)  NOT NULL
    );
END;

IF OBJECT_ID('dbo.AssessmentStatus', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AssessmentStatus (
        Code        VARCHAR(20) NOT NULL PRIMARY KEY,
        DisplayName VARCHAR(50) NOT NULL
    );
END;
