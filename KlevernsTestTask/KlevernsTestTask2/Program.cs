using System.Threading;

public static class Server
{
    private static int count = 0;

    private static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

    public static int GetCount()
    {
        rwLock.EnterReadLock();
        try
        {
            return count;
        }
        finally
        {
            rwLock.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        rwLock.EnterWriteLock();
        try
        {
            count += value;
        }
        finally
        {
            rwLock.ExitWriteLock();
        }
    }
}

class Program
{
    static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            int readerId = i;
            Task.Run(() =>
            {
                for (int j = 0; j < 10; j++)
                {
                    int value = Server.GetCount();
                    Console.WriteLine($"[Reader {readerId}] count = {value}");
                    Thread.Sleep(100);
                }
            });
        }

        for (int i = 0; i < 2; i++)
        {
            int writerId = i;
            Task.Run(() =>
            {
                for (int j = 0; j < 5; j++)
                    {
                    Server.AddToCount(1);
                    Console.WriteLine($"[Writer {writerId}] added 1");
                    Thread.Sleep(200);
                }
            });
        }
            Console.ReadLine();
    }
}