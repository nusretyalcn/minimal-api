using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<OperationClaim>> GetClaims(User user);
    IResult Add(User user);
    IDataResult<User> GetByEmail(string email);
    IDataResult<List<User>> GetAll();
}