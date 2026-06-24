// Teaching reference for CD 101 — NOT a restored/buildable project.
// CHARACTERIZATION tests pin the CURRENT behavior of CalculateLateFee before we
// touch it. They assert what the code DOES today (quirks included), not what it
// SHOULD do — so any UNINTENDED change to the legacy path fails loudly. With the
// flag OFF, the calculator must behave exactly as it always has.
//
// The NEW behavior (flag ON) is NEW code, so it ships WITH its own intent-stating
// tests. This is rule 1 (new code gets tests) and rule 3 (characterize legacy
// before you change it) in one file.

using Xunit;

public class LateFeeCalculatorCharacterizationTests
{
    private static LateFeeCalculator Build(bool v2On) =>
        new LateFeeCalculator(new StubFlags(v2On));

    // ---- Legacy behavior, flag OFF: pin it exactly as it is today -----------

    [Theory]
    [InlineData(30)] // 30 days is NOT late enough — the quirk we must preserve
    [InlineData(16)] // also within the legacy grace period — the exact case V2 changes
    [InlineData(0)]  // not late at all
    public void Legacy_NoFee_WhenNotStrictlyOver30Days(int daysLate)
    {
        var fee = Build(v2On: false).CalculateLateFee(1000m, daysLate);
        Assert.Equal(0m, fee);
    }

    [Fact]
    public void Legacy_NoFee_WhenBalanceIsZeroOrNegative()
    {
        var fee = Build(v2On: false).CalculateLateFee(-50m, 90);
        Assert.Equal(0m, fee);
    }

    [Fact]
    public void Legacy_FlatPlusPercent_BelowTheCap()
    {
        // 31 days late, $1,000 balance: $25 + 1.5% of 1000 ($15) = $40
        var fee = Build(v2On: false).CalculateLateFee(1000m, 31);
        Assert.Equal(40m, fee);
    }

    [Fact]
    public void Legacy_PercentPart_IsCappedAt100()
    {
        // $20,000 balance: 1.5% = $300, but the legacy cap is $100 => $25 + $100 = $125
        var fee = Build(v2On: false).CalculateLateFee(20000m, 31);
        Assert.Equal(125m, fee);
    }

    // ---- New behavior, flag ON: state the INTENDED contract -----------------

    [Fact]
    public void V2_GracePeriodDropsTo15Days()
    {
        // 16 days late now incurs a fee (it would not under the legacy rule)
        var fee = Build(v2On: true).CalculateLateFee(1000m, 16);
        Assert.Equal(40m, fee);
    }

    [Fact]
    public void V2_PercentCapRisesTo250()
    {
        // $20,000 balance: 1.5% = $300, capped at the new $250 => $25 + $250 = $275
        var fee = Build(v2On: true).CalculateLateFee(20000m, 31);
        Assert.Equal(275m, fee);
    }

    private sealed class StubFlags : IFeatureFlags
    {
        private readonly bool _on;
        public StubFlags(bool on) => _on = on;
        public bool IsEnabled(string key) => _on; // single-flag stub for the example
    }
}
