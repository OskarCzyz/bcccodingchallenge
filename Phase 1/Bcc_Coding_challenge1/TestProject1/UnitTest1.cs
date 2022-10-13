namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public int[] Test1()
        {         
            Random rnd = new();
            int[] array = { rnd.Next(), rnd.Next(), rnd.Next() };
            return array;
        }
    }
}