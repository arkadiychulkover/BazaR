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
    public enum SellerType
    {
        bazar = 1,
        otherSeller = 2,
        otherSellerOnSklad = 3
    }

    public enum ProductionCountry
    {
        Ukraine = 1,
        USA = 2,
        Germany = 3,
        France = 4,
        Poland = 5,
        Italy = 6,
        Spain = 7,
        Canada = 8,
        Japan = 9,
        China = 10
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
