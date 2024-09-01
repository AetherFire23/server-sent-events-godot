using SSEConsumer;
using SSEFun;
using SSEFun.SSEThings;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Client console");


        //await InitiateStuff();

        var client = new HttpClient();


        using var stream = await client.GetStreamAsync($"http://localhost:5056/SSE?userId={Guid.NewGuid()}");
        using var reader = new StreamReader(stream);

        while (true)
        {
            var raw = reader.ReadLine();

            var something = SSEMessaging.ParseRawMessage<GameState>(raw);


            
            Thread.Sleep(90);

        }
    }

    public static async Task InitiateStuff()
    {
        var clients = Enumerable.Range(0, 4).Select(x => new HttpClient());

        var tasks = clients.Select(x => x.GetStreamAsync($"http://localhost:5056/SSE?userId={Guid.NewGuid()}"));

        var streams = await Task.WhenAll(tasks);

        var readers = streams.Select(x => new StreamReader(x));


        var tasks2 = readers.Select<StreamReader, Func<Task>>(x => async () =>
        {
            while (true)
            {
                Thread.Sleep(1000);
                var raw = x.ReadLine();

                var something = SSEMessaging.ParseRawMessage<GameState>(raw);
                Console.WriteLine(something);
            }
        })
            .Select(x => x.Invoke());

        await Task.WhenAll(tasks2);
    }
}
