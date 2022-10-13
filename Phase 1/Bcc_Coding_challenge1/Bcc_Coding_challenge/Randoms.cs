namespace Bcc_Coding_challenge
{
    public class Randoms
    {
        public static int[] nums()
        {
            Random rnd = new();
            int[] array = { rnd.Next(), rnd.Next(), rnd.Next()};
            return array;
        }
    }
}
