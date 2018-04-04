using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Witcherius.Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private readonly IList<Action> _afterCommitActions = new List<Action>();

        public async Task Commit()
        {
            await CommitCore();
            foreach (var action in _afterCommitActions)
            {
                action();
            }
            _afterCommitActions.Clear();
        }

        public abstract void Dispose();

        protected abstract Task CommitCore();

        public void RegisterAction(Action action)
        {
            _afterCommitActions.Add(action);
        }
    }
}
