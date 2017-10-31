namespace NumericUtils
{
    public class DigitInNumberPredicate : IPredicate<int>
    {
        private int soughtDigit;

        public DigitInNumberPredicate(int soughtDigit)
        {
            this.soughtDigit = soughtDigit;
        }

        public bool IsTrue(int item)
        {
            return item.ToString().Contains(soughtDigit.ToString());
        }
    }
}
