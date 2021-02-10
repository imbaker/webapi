namespace WebApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "");
        TEntity GetById(object id);

        int Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entity);

        void Save();
    }
}