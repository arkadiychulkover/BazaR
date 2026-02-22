namespace BazaR.Models
{
    public enum Cities
    {
        Kyiv = 1,
        Kharkiv,
        Odesa,
        Dnipro,
        Lviv,
        Zaporizhzhia,
        Mykolaiv,
        Vinnytsia,
        Kherson,
        Poltava,
        Chernihiv,
        Cherkasy,
        Zhytomyr,
        Sumy,
        Rivne,
        Ternopil,
        Lutsk,
        Uzhhorod,
        Chernivtsi,
        IvanoFrankivsk,
        Kropyvnytskyi,
        Khmelnytskyi
    }
    public enum PaymentType
    {
        Cash,
        Card,
        Online,
        PostPayment
    }

    public enum FilterValueType
    {
        String,
        Number,
        Boolean,
        Range
    }
}
