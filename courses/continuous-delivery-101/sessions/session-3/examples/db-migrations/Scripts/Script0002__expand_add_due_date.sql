-- EXPAND (parallel change, step 1): add DueDate as NULLABLE so code that predates the
-- column keeps inserting rows without it. Backward-compatible -> deployable any time,
-- and the app can roll back independently of this schema change.
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Assessments') AND name = 'DueDate'
)
BEGIN
    ALTER TABLE dbo.Assessments ADD DueDate DATE NULL;
END;

-- CONTRACT (step 3) happens LATER, as its own forward migration, only after every
-- reader/writer uses DueDate. DbUp is forward-only: there is no down-script.
-- Reversibility comes from expand/contract, not from rolling the database back.
