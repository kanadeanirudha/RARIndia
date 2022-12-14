namespace RARIndia.Utilities.Helper
{
    public static class RARIndiaHelperUtility
    {
        //Returns true if the passed value is not null, else return false.
        public static bool IsNotNull(object value)
            => !Equals(value, null);

        //Returns true if the passed value is null else false.
        public static bool IsNull(object value)
            => Equals(value, null);
    }
}
