﻿using System.Threading;
using System.Threading.Tasks;
using Take.Elephant.Specialized.Synchronization;

namespace Take.Elephant.Specialized.Replication
{
    public class ReplicationQueue<T> : ReplicationStrategy<IQueue<T>>, IQueue<T>
    {        
        public ReplicationQueue(IQueue<T> master, IQueue<T> slave)
            : base(master, slave, new CopyQueueSynchronizer<T>())
        {

        }

        public virtual Task EnqueueAsync(T item, CancellationToken cancellationToken = default)
        {
            return ExecuteWithFallbackAsync(q => q.EnqueueAsync(item));
        }

        public virtual Task<T> DequeueOrDefaultAsync(CancellationToken cancellationToken = default)
        {
            return ExecuteWithFallbackAsync(q => q.DequeueOrDefaultAsync());
        }

        public virtual Task<long> GetLengthAsync(CancellationToken cancellationToken = default)
        {
            return ExecuteWithFallbackAsync(q => q.GetLengthAsync());
        }
    }
}