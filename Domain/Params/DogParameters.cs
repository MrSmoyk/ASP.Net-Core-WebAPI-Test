namespace Domain.Params
{
    public class DogParameters : QueryStringParameters
    {
        public DogParameters()
        {
            OrderBy = "name desc";
        }
    }
}
