namespace _05.PlanckConstant
{
    public class Calculation
    {
        public const double PlanckConstant = 6.62606896e-34;
        public const double Pi = 3.14159;

        public static double ReducedPlanckConstant()
        {
            return PlanckConstant / (2 * Pi);
        }
    }
}