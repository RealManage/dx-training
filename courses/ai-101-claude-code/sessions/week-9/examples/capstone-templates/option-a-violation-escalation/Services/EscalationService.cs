using RealManage.ViolationEscalation.Models;

namespace RealManage.ViolationEscalation.Services;

/// <summary>
/// Implementation of violation escalation service.
///
/// TODO: Implement all methods. This is a starter template with TODOs.
/// The learner should complete this using TDD with Claude Code.
/// </summary>
public class EscalationService : IEscalationService
{
    public (Violation UpdatedViolation, AuditEntry AuditEntry)? EscalateViolation(Violation violation)
    {
        // TODO: Implement escalation logic
        // 1. Check if ShouldEscalate
        // 2. Get next stage
        // 3. Calculate new fine
        // 4. Create updated violation
        // 5. Create audit entry
        // 6. Return tuple
        throw new NotImplementedException("Implement this method using TDD");
    }

    public decimal CalculateFine(ViolationStage stage, int daysSinceReport)
    {
        // TODO: Implement fine calculation
        // - Warning: $0
        // - FirstNotice: $50
        // - SecondNotice: $100 base + compound interest
        // - BoardReview: $200 base + compound interest
        // - LegalAction: $500 base + compound interest
        //
        // Compound interest: 10% monthly, starting after day 30
        // Formula: baseFine * Math.Pow(1.10, monthsOverGracePeriod)
        throw new NotImplementedException("Implement this method using TDD");
    }

    public bool ShouldEscalate(Violation violation)
    {
        // TODO: Implement escalation check
        // - Check if violation is resolved (no escalation)
        // - Check if already at LegalAction (no escalation)
        // - Calculate days at current stage
        // - Return true if >= 30 days at current stage
        throw new NotImplementedException("Implement this method using TDD");
    }

    public ViolationStage GetNextStage(ViolationStage currentStage)
    {
        // TODO: Implement stage progression
        // Warning -> FirstNotice -> SecondNotice -> BoardReview -> LegalAction
        // LegalAction and Resolved stay the same
        throw new NotImplementedException("Implement this method using TDD");
    }
}
