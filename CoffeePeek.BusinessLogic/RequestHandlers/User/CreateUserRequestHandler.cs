using CoffeePeek.BusinessLogic.Abstractions;
using CoffeePeek.BusinessLogic.Services;
using CoffeePeek.Contract.Dtos.User;
using CoffeePeek.Contract.Requests.User;
using CoffeePeek.Contract.Response;
using CoffeePeek.Data;
using CoffeePeek.Data.Databases;
using MapsterMapper;
using MediatR;

namespace CoffeePeek.BusinessLogic.RequestHandlers;

public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, CreateEntityResponse<UserDto>>
{
    private readonly IUnitOfWork<CoffeePeekDbContext> _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidationStrategy<UserDto> _validationStrategy;
    private readonly IUserPasswordService _passwordService;

    public CreateUserRequestHandler(
        IUnitOfWork<CoffeePeekDbContext> unitOfWork, 
        IMapper mapper, 
        IValidationStrategy<UserDto> validationStrategy,
        IUserPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationStrategy = validationStrategy;
        _passwordService = passwordService;
    }
    
    public async Task<CreateEntityResponse<UserDto>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userDto = _mapper.Map<UserDto>(request);
        
        var validationResult = _validationStrategy.Validate(userDto);
        if (!validationResult.IsValid)
        {
            return CreateEntityResponse<UserDto>.ErrorResponse($"Invalid request: {validationResult.ErrorMessage}");
        }
        
        var password = _passwordService.HashPassword(request.Password);
        userDto.Password = password;
        
        var user = _mapper.Map<Data.Models.Users.User>(userDto);

        _unitOfWork.DbContext.Add(user);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreateEntityResponse<UserDto>.SuccessResponse(_mapper.Map<UserDto>(user), entityId:user.Id);
    }
}