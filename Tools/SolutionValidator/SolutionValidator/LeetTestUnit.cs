namespace SolutionValidator
{
    public class LeetTestUnit
    {
        public LeetTestUnit(object[] parameters, object answer)
        {
            Parameters = parameters;
            Answer = answer;
        }
        public LeetTestUnit(object parameter, object answer)
        {
            Parameters = new object[] { parameter };
            Answer = answer;
        }
        public object[] Parameters { get; private set; }
        public object Answer { get; private set; }
    }
}