namespace WakfuBuider;

public class Calculator
{
    public static int CalculateDamage(Entity actor, Entity target, EntityAction spell, Positioning positioning)
    {
        static float CalculateMasteryMultiplier(Entity stats, int elementMastery, bool isCrit, bool isDistance, bool isRear, bool isBerserk)
        {
            float masteryMultiplier = elementMastery;
            masteryMultiplier += isCrit ? stats.CriticalMastery : 0;
            masteryMultiplier += isDistance ? stats.DistanceMastery : stats.MeleeMastery;
            masteryMultiplier += isRear ? stats.RearMastery : 0;
            masteryMultiplier += isBerserk ? stats.BerserkMastery : 0;
            return 1 + masteryMultiplier / 100;
        }

        var elementMastery = actor.GetElementalMastery(spell.Element);
        var percentResist = target.ResistPercent(spell.Element);
        var totalDmgInflicted = 1 + (float)actor.DamageInflicted / 100;

        // Calculate normal damage
        int damage = (int)Math.Round(spell.Damage *
            CalculateMasteryMultiplier(actor, elementMastery, false, positioning.Distance, positioning.Rear, true) *
            (1 + positioning.Multiplier) *
            (1 - percentResist) *
            totalDmgInflicted);

        // Calculate critical damage
        int critDamage = (int)Math.Round(spell.CritDamage *
            CalculateMasteryMultiplier(actor, elementMastery, true, positioning.Distance, positioning.Rear, true) *
            (1 + positioning.Multiplier) *
            (1 - percentResist) *
            totalDmgInflicted);

        // Calculate average damage
        int avgDamage = (int)Math.Round(damage + (float)actor.CriticalHits / 100 * (critDamage - damage));

        return avgDamage;
    }
}