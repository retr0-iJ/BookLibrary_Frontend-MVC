using System;
using System.Collections.Generic;

namespace Binus.SampleWebAPI.Model.Base
{
    [Serializable]
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public Result()
        {
            Success = false;
            Message = "";
            ErrorCode = 500;
        }

       
    }
    public class GetOneResult<TEntity> : Result where TEntity : class, new()
    {
        public TEntity Entity { get; set; }
    }

    public class GetManyResult<TEntity> : Result where TEntity : class, new()
    {
        public IEnumerable<TEntity> Entities { get; set; }
    }

    public class GetListResult<T> : Result
    {
        public List<T> Entities { get; set; }
    }
}
