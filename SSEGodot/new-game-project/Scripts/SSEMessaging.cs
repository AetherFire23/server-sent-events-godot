using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SSEFun.SSEThings;

public static class SSEMessaging
{
    private const string EndMessageToken = "[END]";

    public static T ParseRawMessage<T>(string rawMessage)
    {
        // Takes the raw message until it meets a "[".
        // AND Will keep the message only if the remaining characters contain a end MEssage token.
        // The reason is the message we receive might contain serialized data and therefore contain reserved characters


        var readableSection = CharEnumerableToString(rawMessage.TakeWhile((x, i) => ThePredicate2(rawMessage, x, i)));

        
        T obj = JsonSerializer.Deserialize<T>(readableSection);
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
