// Teaching reference for CD 101 — NOT a restored/buildable project.
// A legacy assessment late-fee calculation from the .NET monolith. It has no
// tests: it has been "tested in production" by years of real HOA billing runs.
// We are about to change the late-fee rule — so first we CHARACTERIZE the current
// behavior (see LateFeeCalculatorCharacterizationTests.cs), then add the new rule
// behind a feature flag so the change can integrate to trunk before it is verified.

public interface IFeatureFlags
{
    bool IsEnabled(string key);
}

public class LateFeeCalculator
{
    private readonly IFeatureFlags _flags;

    public LateFeeCalculator(IFeatureFlags flags) => _flags = flags;

    // The amount a homeowner owes once an assessment is past due.
    public decimal CalculateLateFee(decimal balance, int daysLate)
    {
        if (_flags.IsEnabled("late-fee-v2"))
        {
            return CalculateLateFeeV2(balance, daysLate);
        }

        // --- The legacy rule, exactly as it has run for years -----------------
        // Quirks preserved on purpose — this is what the characterization test pins:
        //   - no fee unless STRICTLY more than 30 days late (30 days => no fee)
        //   - a flat $25 plus 1.5% of the balance, but the percentage part is
        //     silently capped at $100
        //   - zero or negative balances never incur a fee
        if (daysLate <= 30 || balance <= 0m)
        {
            return 0m;
        }

        decimal percentPart = System.Math.Min(balance * 0.015m, 100m);
        return 25m + percentPart;
    }

    // The NEW rule, dark until "late-fee-v2" is flipped on. It is new code, so it
    // arrives WITH tests. It deliberately changes real behavior: the grace period
    // drops to 15 days and the percentage cap rises to $250.
    private static decimal CalculateLateFeeV2(decimal balance, int daysLate)
    {
        if (daysLate <= 15 || balance <= 0m)
        {
            return 0m;
        }

        decimal percentPart = System.Math.Min(balance * 0.015m, 250m);
        return 25m + percentPart;
    }
}
