using CoffeePeek.BusinessLogic.Services;
using CoffeePeek.Contract.Requests.Auth;
using CoffeePeek.Contract.Response;
using CoffeePeek.Contract.Response.Login;
using CoffeePeek.Data;
using CoffeePeek.Data.Models.Users;
using MediatR;

namespace CoffeePeek.BusinessLogic.RequestHandlers.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserPasswordService _userPasswordService;

    public LoginRequestHandler(IUserPasswordService userPasswordService, IRepository<User> userRepository)
    {
        _userPasswordService = userPasswordService;
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user == null)
        {
            return Response.ErrorResponse<LoginResponse>("Account does not exist");
        }

        var passwordVerificationResult = _userPasswordService.VerifyPassword(request.Password, user.Password);
        if (!passwordVerificationResult)
        {
            return Response.ErrorResponse<LoginResponse>("Password is incorrect.");
        }
        
        return Response.SuccessResponse<LoginResponse>();
    }
}