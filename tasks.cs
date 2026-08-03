Task t = Task.Run(()=>
{
    int result = 1+2;

    Task.Delay(2000).Wait();

    Console.WriteLine("result {0}", result);
});

t.Wait();
