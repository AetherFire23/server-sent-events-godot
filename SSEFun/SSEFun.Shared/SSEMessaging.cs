using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text;

namespace SSEFun.SSEThings;

public static class SSEMessaging
{
    private const string EndMessageToken = "[END]";
    public static string ToReadableLine<T>(T obj)
    {
        try
        {
            var json = JsonConvert.SerializeObject(obj);
            var sb = new StringBuilder();

            sb.Append(json);
            sb.Append(EndMessageToken);
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return string.Empty;
    }

    public static T ParseRawMessage<T>(string rawMessage)
    {
        // Takes the raw message until it meets a "[".
        // AND Will keep the message only if the remaining characters contain a end MEssage token.
        // The reason is the message we receive might contain serialized data and therefore contain reserved characters
        var readableSection = CharEnumerableToString(rawMessage.TakeWhile((x, i) => ThePredicate2(rawMessage, x, i)));
        T obj = JsonConvert.DeserializeObject<T>(readableSection);
        return obj;
    }

    private static bool ThePredicate2(string rawMessage, char current, int index)
    {
        if (current != '[')
        {
            return true;
        }

        int i = 0;

        return CHeckEndTOken(rawMessage, index);
    }

    private static Func<char, int, bool> ThePredicate(string rawMessage)
    {
        return (x, i) => x != '[' && CHeckEndTOken(rawMessage, i);
    }

    private static bool CHeckEndTOken(string rawMessage, int i)
    {
        var taken = CharEnumerableToString(rawMessage.Skip(i));


        var theoreticalEndToken = taken.Take(EndMessageToken.Length);
        var test = CharEnumerableToString(theoreticalEndToken);
        var s = test.Contains(EndMessageToken);
        return !s;
    }

    private static string CharEnumerableToString(IEnumerable<char> chars)
    {
        string str = new string(chars.ToArray());
        return str;
    }
}
