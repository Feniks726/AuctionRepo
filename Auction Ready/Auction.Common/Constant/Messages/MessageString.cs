namespace Auction.Common.Constant.Messages
{
    public partial class Messages
    {
        public class MessageString
        {
            private readonly string _value;

            public MessageString(string value)
            {
                _value = value;
            }

            public override string ToString()
            {
                return _value;
            }

            public string Format(params object[] p)
            {
                return string.Format(_value, p);
            }

        }
    }
}
