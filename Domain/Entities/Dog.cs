namespace Domain.Entities
{
    public class Dog : BaseEntity
    {
        public string Color { get; set; }

        public int TailLength { get; set; }

        public int Weight { get; set; }
    }
}
