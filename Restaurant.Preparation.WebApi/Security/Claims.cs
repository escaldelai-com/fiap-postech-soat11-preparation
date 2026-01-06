namespace Restaurant.Preparation.WebApi.Security;

public class Claims
{

    public class Order
    {
        public const string GetWaiting = "preparation:order:get-waiting";
        public const string Confirm = "preparation:order:confirm";
        public const string Prepare = "preparation:order:prepare";
        public const string Delivery = "preparation:order:delivery";
        public const string Finalize = "preparation:order:finalize";
    }


    public static string[] All = [
        Order.GetWaiting,
        Order.Confirm,
        Order.Prepare,
        Order.Delivery,
        Order.Finalize
    ];

}
