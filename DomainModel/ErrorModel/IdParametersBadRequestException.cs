namespace DomainModel.ErrorModel;
public sealed class IdParametersBadRequestException : BadRequestException
{
    public IdParametersBadRequestException()
    : base("Parameter ids is null")
    { }
}
