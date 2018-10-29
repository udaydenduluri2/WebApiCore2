namespace WebApi.Model
{
    public class ExerciseWrapper
    {
        public ExerciseStatus Status { get; set; }
        public Exercise Exercise { get; set; }
        public int TimeOut { get; set; } = 30000;

        public string StatusStr
        {
            get { return Status.ToString(); }
        }
    }
}
