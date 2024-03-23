
#region MultiThreading

#region Thread starting

//Thread thread = new(() =>
//{
//	for (int i = 0; i < 10; i++)
//	{
//		Console.WriteLine($"thread1 worked:{i}");
//	}
//});
//Thread thread2 = new(() =>
//{
//    for (int i = 0; i < 10; i++)
//    {
//        Console.WriteLine($"thread2 worked:{i}");
//    }
//});
//thread.Start();
//thread2.Start();
//Console.WriteLine("finshed");
#endregion

#region Thread id
/*
Console.WriteLine(Environment.CurrentManagedThreadId);
Console.WriteLine(AppDomain.GetCurrentThreadId());
Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
Console.WriteLine("\n");
Thread thread1 = new(() =>
{
    Console.WriteLine(Environment.CurrentManagedThreadId);
    Console.WriteLine(AppDomain.GetCurrentThreadId());
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
    Console.WriteLine("\n");
});

Thread thread2 = new(() =>
{
    Console.WriteLine(Environment.CurrentManagedThreadId);
    Console.WriteLine(AppDomain.GetCurrentThreadId());
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
});

thread1.Start();
thread2.Start();
*/
#endregion

#region IsBackground
/*
int i = 1;
Thread thread1 = new(() =>
{
    while (i < 10)
    {
        i++;
        Console.WriteLine($"thread 1 worked:{i}");
        Thread.Sleep(1000);
    }
    Console.WriteLine("thread1 finshed");
});

thread1.Start();
thread1.IsBackground = true;
Console.WriteLine("Main thread finshed");
*/
#endregion

#region Thread state
/*
int i = 0;
Thread thread = new(() =>
{
    while (i < 10)
    {
        i++;
        Thread.Sleep(1000);
    }
    Console.WriteLine("thread finished");
});
ThreadState state = ThreadState.Running;
thread.Start();

//while (true)
//{
//    if (state==ThreadState.Stopped)
//    {
//        break;
//    }
//    state = thread.ThreadState;
//    Console.WriteLine(state);
//}
while (true)
{
    if (state == ThreadState.Stopped)
    {

        break;
    }
    if (state != thread.ThreadState)
    {
        state = thread.ThreadState;
        Console.WriteLine(state);
    }
}
Console.WriteLine("main thread finshed");
*/
#endregion


#region RaceCondition
/*
//int num = 0;
//object _lock = new();
//Thread thread1 = new(() =>
//{
//    lock (_lock)
//    {
//        for (int i = 0; i < 100; i++)
//        {
//            Console.WriteLine("thread1:" + ++num);
//        }
//    }

//});

//Thread thread2 = new(() =>
//{
//    lock (_lock)
//    {
//        for (int i = 0; i < 100; i++)
//        {
//            Console.WriteLine("thread2:" + --num);
//        }
//    }
//});

//thread1.Start();
//thread2.Start();

int num = 0;
Thread thread1 = new(() =>
{
    for (int i = 0; i < 100; i++)
    {
        Console.WriteLine("thread1:" + ++num);
    }

});

Thread thread2 = new(() =>
{
    for (int i = 0; i < 100; i++)
    {
        Console.WriteLine("thread2:" + --num);
    }
});

thread1.Start();
thread1.Join();
thread2.Start();

*/
#endregion

#region Thread Cancel
/*
//bool stop = false;

//Thread thread = new(() =>
//{
//    while (true)
//    {
//        if (stop)
//        {
//            break;
//        }
//        Console.WriteLine("thread is working");
//    }

//    Console.WriteLine("thread finshed");
//});

//thread.Start();
//Thread.Sleep(5000);
//stop = true;

Thread thread = new((cancellationToken) =>
{
    var cancel = (CancellationTokenSource)cancellationToken;
    while (true)
    {
        if (cancel.IsCancellationRequested) break;
        Console.WriteLine("thread worked");
    }
    Console.WriteLine("thread finshed");
});

CancellationTokenSource cancellationToken = new();
thread.Start(cancellationToken);
Thread.Sleep(5000);
cancellationToken.Cancel();
*/
#endregion

#region thread interrupt

/*
Thread thread = new(() =>
{
    try
    {
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine("thread is sleep:" + i);
            if (i == 50)
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
    catch (ThreadInterruptedException ex)
    {
        Console.WriteLine("thread  continiue");
        
    }
});

thread.Start();
thread.Interrupt();
*/
#endregion
#endregion

#region Sinxronization

#region Spinning
/*
bool threadCandition = true;
int n = 0;
Thread thread1 = new(() =>
{
    while (true)
    {
        threadCandition = true;
        if (threadCandition)
        {
            for (int i = 0; i < 10; i++)
            {
                n++;
                Console.WriteLine("thread1:"+n);
                
            }
            threadCandition = false;
            break;
        }
    }
});
Thread thread2 = new(() =>
{
    while (true)
    {
        threadCandition = false;
        if (!threadCandition)
        {
            for (int i = 0; i < 10; i++)
            {
                n--;
                Console.WriteLine("thread2:" + n);

            }
            threadCandition = true;
            break;
        }
    }
});
thread1.Start();
thread2.Start();
*/
#endregion

#region Monitor.Enter Monitor.Exist
/*
int n = 0;
object _lock = new();
bool isLocked = false;
Thread thread1 = new(() =>
{
    Monitor.Enter(_lock, ref isLocked);
    if (isLocked)
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                n++;
                Console.WriteLine("thread1:" + n);
            }
        }
        finally
        {
            Monitor.Exit(_lock);
        }
    }
   
   
});

Thread thread2 = new(() =>
{
    Monitor.Enter(_lock, ref isLocked);
    if (isLocked)
    {
        try
        {

            for (int i = 0; i < 10; i++)
            {
                n--;
                Console.WriteLine("thread2:" + n);
            }
        }
        finally
        {
            Monitor.Exit(_lock);
        }
    }
});
thread1.Start();
thread2.Start();
*/
#endregion

#region Monitor.TryEnter
/*
object _lock = new();
bool isLocked = false;
int n = 1;
Thread thread1 = new(() =>
{
    Monitor.TryEnter(_lock, 20, ref  isLocked);
    if (isLocked)
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                n++;
                Console.WriteLine("thread1:" + n);
            }
        }
        finally
        {
            Monitor.Exit(_lock);
        }
    }
});
Thread thread2 = new(() =>
{
    Monitor.TryEnter(_lock, 200, ref isLocked);
    if (isLocked)
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                n--;
                Console.WriteLine("thread2:" + n);
            }
        }
        finally
        {
            Monitor.Exit(_lock);
        }
    }
});
thread1.Start();
thread2.Start();
*/
#endregion

#region Mutex
/*
internal class Program
{
    static Mutex mutex;
    static string programName;
    private static void Main(string[] args)
    {
        Mutex.TryOpenExisting(programName, out mutex);
        if (mutex==null)
        {
            mutex = new(true, programName);
            Console.WriteLine("start up project");
        }
        else
        {
            mutex.Close();
            return;
        }
    }
}
*/
#endregion
#endregion

#region Semaphore and SemaphoreSlim

#region Semaphore
/*
Semaphore semaphore = new(2, 4);
int n = 0;
Thread thread1 = new(() =>
{
    semaphore.WaitOne();
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine("thread1:"+ ++n);
        Thread.Sleep(300);
    }
    semaphore.Release();

});
Thread thread2 = new(() =>
{
    semaphore.WaitOne();
    for (int i = 0; i < 12; i++)
    {
        Console.WriteLine("thread2:" + --n);
        Thread.Sleep(300);
    }
    semaphore.Release();
});
Thread thread3 = new(() =>
{
    semaphore.WaitOne();
    for (int i = 0; i < 15; i++)
    {
        Console.WriteLine("thread3:" + ++n);
        Thread.Sleep(300);
    }
    semaphore.Release();
});
Thread thread4 = new(() =>
{
    semaphore.WaitOne();
    for (int i = 0; i < 8; i++)
    {
        Console.WriteLine("thread4:" + --n);
        Thread.Sleep(300);
    }
    semaphore.Release();
});
thread1.Start();
thread2.Start();
thread3.Start();
thread4.Start();
thread1.Join();
thread2.Join();
thread3.Join();
thread4.Join();
Console.WriteLine(n);

*/
#endregion

#region SemaphoreSlim
/*
SemaphoreSlim semaphore = new(2, 4);
int n = 0;
Thread thread1 = new( () =>
{
     semaphore.Wait();
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine("thread1:" + ++n);
        Thread.Sleep(300);
    }
    semaphore.Release();

});
Thread thread2 = new( () =>
{
     semaphore.Wait();
    for (int i = 0; i < 12; i++)
    {
        Console.WriteLine("thread2:" + --n);
        Thread.Sleep(300);
    }
    semaphore.Release();
});
Thread thread3 = new( () =>
{
     semaphore.Wait();
    for (int i = 0; i < 15; i++)
    {
        Console.WriteLine("thread3:" + ++n);
        Thread.Sleep(300);
    }
    semaphore.Release();
});
Thread thread4 = new( () =>
{
     semaphore.Wait();
    for (int i = 0; i < 8; i++)
    {
        Console.WriteLine("thread4:" + --n);
        Thread.Sleep(300);
    }
    semaphore.Release();
});
thread1.Start();
thread2.Start();
thread3.Start();
thread4.Start();
thread1.Join();
thread2.Join();
thread3.Join();
thread4.Join();
Console.WriteLine(n);
*/
#endregion
#endregion

#region Volatile 

#region keyword
/*
MyClass.Run();
 class MyClass
{
    volatile static int n = 1;
    public static void Run()
    {
        Thread thread1 = new(() =>
        {
            for (int i = 0; true;)
            {
                Console.WriteLine("thread1:");
                n++;
                Thread.Sleep(100);
            }
        });
        Thread thread2 = new(() =>
        {
            for (int i = 0; true;)
            {
                Console.WriteLine(n);
                Thread.Sleep(100);
            }
        });
        Thread thread3 = new(() =>
        {
            for (int i = 0; true;)
            {
                Console.WriteLine("thread2:");
                n--;
                Thread.Sleep(100);
            }
        });

        thread1.Start();
        thread2.Start();
        thread3.Start();
    }
}
*/
#endregion
#region Class
/*
MyClass.Run();
class MyClass
{
    static int i = 1;
    public static void Run()
    {
        Thread thread1 = new(() =>
        {
            while (true)
            {
                Console.WriteLine("thread1:");
                Volatile.Write(ref i, Volatile.Read(ref i)+1);
                Thread.Sleep(100);
            }
        });
        Thread thread2 = new(() =>
        {
            while (true)
            {
                Console.WriteLine(Volatile.Read(ref i));
                Thread.Sleep(100);
            }
        });
        Thread thread3 = new(() =>
        {
            while (true)
            {
                Console.WriteLine("thread2:");
                Volatile.Write(ref i, Volatile.Read(ref i) - 1);
                Thread.Sleep(100);
            }
        });
        thread1.Start();
        thread2.Start();
        thread3.Start();
    }
}
*/
#endregion
#endregion

#region Interlock,MemoryBarier

#region Interlock
/*
int n = 1;
Interlocked.Add(ref n, 4);
int prewValue= Interlocked.Exchange(ref n, 0);
int prewValue2 = Interlocked.CompareExchange(ref n, 10, 0);
Console.WriteLine("prewValue1:"+prewValue) ;
Console.WriteLine("prewValue1:" + prewValue2);
Console.WriteLine(n);
Thread thread1 = new(() =>
{
    while (true)
    {
        Interlocked.Increment(ref n);
        Console.WriteLine("thread1");
        Thread.Sleep(50);
    }
});
Thread thread2 = new(() =>
{
    while (true)
    {
        Console.WriteLine(n);
        Thread.Sleep(50);
    }
});
Thread thread3 = new(() =>
{
    while (true)
    {
        Interlocked.Decrement(ref n);
        Console.WriteLine("thread2");
        Thread.Sleep(50);
    }
});
//thread1.Start();
//thread2.Start();
//thread3.Start();
*/
#endregion
#region MemoryBarier
/*
int n = 0;
Thread read = new(() =>
{
    while (true)
    {
        Thread.MemoryBarrier();
        Console.WriteLine(n);
        Thread.Sleep(50);
    }
});

Thread write = new(() =>
{
    while (true)
    {
        Interlocked.Increment(ref n);
        Console.WriteLine("proses:");
        Thread.MemoryBarrier();
        Thread.Sleep(50);
    }
});
read.Start();
write.Start();
*/
#endregion
#endregion

#region SpinLock,SpinWait

#region SpinLock
/*
SpinLock spinLock = new();
int n = 0;
bool isLocked = false;
Thread thread1 = new(() =>
{
    spinLock.Enter(ref isLocked);
    try
    {
        if (isLocked)
        {
            for (int i = 0; i < 999; i++)
            {
                n++;
                Console.WriteLine("thread1");
                Thread.Sleep(10);
            }
        }
    }
    finally
    {
        spinLock.Exit();
    }
});
Thread thread2 = new(() =>
{
    for (int i = 0; i < 999*2; i++)
    {
        Console.WriteLine(n);
        Thread.Sleep(10);
    }
});
Thread thread3 = new(() =>
{
    spinLock.Enter(ref isLocked);
    try
    {
        if (isLocked)
        {
            for (int i = 0; i < 999; i++)
            {
                n--;
                Console.WriteLine("thread2");
                Thread.Sleep(10);
            }
        }
    }
    finally
    {
        spinLock.Exit();
    }
    
});

thread1.Start();
thread2.Start();
thread3.Start();
*/
#endregion

#region SpinWait
/*
bool condition = false, locked = false;

Thread thread1 = new(() =>
{
    while (true)
    {
        if (condition||locked)
        {
            continue;
        }
        Console.WriteLine("thread1 worked");
    }
});

Thread thread2 = new(() =>
{
    while (true)
    {
        SpinWait.SpinUntil(() =>
        {
            return condition || locked;
        });
        Console.WriteLine("thread2 worked");
    }
});
thread1.Start();
thread2.Start();
*/
#endregion

#endregion

#region ReaderWriterLock,ReaderWriterLockSlim
/*
ReaderWriterLockSlim readerWriterLockSlim = new();
int count = 0;
for (int i = 0; i < 10; i++)
{
    new Thread(Read).Start();
}
for (int i = 0; i < 10; i++)
{
    new Thread(Write).Start();
}
void Read()
{
    try
    {
        readerWriterLockSlim.EnterReadLock();
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"R:{Thread.CurrentThread.ManagedThreadId} is reading:{count}");
            Thread.Sleep(1000);
        }
    }
    finally
    {
        readerWriterLockSlim.ExitReadLock();
    }
    
}

void Write()
{
    try
    {
        readerWriterLockSlim.EnterWriteLock();
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"W:{Environment.CurrentManagedThreadId} is writing:{++count}");
            Thread.Sleep(1000);
        }
    }
    finally
    {
        readerWriterLockSlim.ExitWriteLock();
    }
    
}
*/
#endregion