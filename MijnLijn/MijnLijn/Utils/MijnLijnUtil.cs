namespace MijnLijn
{
    public static class MijnLijnUtil
    {
        public static bool IsArrayNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }
    }
}
