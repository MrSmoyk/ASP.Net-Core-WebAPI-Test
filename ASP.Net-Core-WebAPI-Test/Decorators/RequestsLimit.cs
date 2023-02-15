namespace ASP.Net_Core_WebAPI_Test.Decorators
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestsLimit : Attribute
    {
        public int TimeWindow { get; set; }
        public int MaxRequests { get; set; }
    }

}
