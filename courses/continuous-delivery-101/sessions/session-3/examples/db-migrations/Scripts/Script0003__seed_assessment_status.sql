-- Baseline (reference) data, versioned and shipped exactly like schema. MERGE makes it
-- idempotent: the same script run against an empty or already-seeded database converges
-- to the same rows, so re-running the last good deployment never duplicates data.
MERGE dbo.AssessmentStatus AS target
USING (VALUES
    ('pending', 'Pending'),
    ('paid',    'Paid'),
    ('overdue', 'Overdue')
) AS source (Code, DisplayName)
ON target.Code = source.Code
WHEN MATCHED THEN
    UPDATE SET DisplayName = source.DisplayName
WHEN NOT MATCHED THEN
    INSERT (Code, DisplayName) VALUES (source.Code, source.DisplayName);
