namespace ProductBacklog
{
    public class Smoke : ISmoke
    {
        public int Run(string s)
        {
            return s.Length;
        }
    }
}
