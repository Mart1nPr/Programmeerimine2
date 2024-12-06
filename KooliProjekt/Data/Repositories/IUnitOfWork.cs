<<<<<<< HEAD
﻿namespace KooliProjekt.Data.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();

        ITodoListRepository TodoListRepository { get; }
=======
﻿using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        Task<int> CompleteAsync();
>>>>>>> d741a0326c07780bfd56b54fabb4e0b7705beee3
    }
}
