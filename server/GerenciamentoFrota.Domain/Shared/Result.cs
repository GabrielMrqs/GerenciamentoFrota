namespace GerenciamentoFrota.Domain.Shared
{
    public struct Result<TFailure, TSuccess>
    {
        public TFailure Failure { get; internal set; }

        public TSuccess Success { get; internal set; }

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        internal Result(TFailure failure)
        {
            IsFailure = true;
            Failure = failure;
            Success = default;
        }

        internal Result(TSuccess success)
        {
            IsFailure = false;
            Failure = default;
            Success = success;
        }

        public static implicit operator Result<TFailure, TSuccess>(TFailure failure)
        {
            return new Result<TFailure, TSuccess>(failure);
        }

        public static implicit operator Result<TFailure, TSuccess>(TSuccess success)
        {
            return new Result<TFailure, TSuccess>(success);
        }
    }
}
