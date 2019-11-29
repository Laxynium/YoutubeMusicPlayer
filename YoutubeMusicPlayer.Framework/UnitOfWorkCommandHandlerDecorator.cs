using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Framework
{
    public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ICommandHandler<T> _innerHandler;

        public UnitOfWorkCommandHandlerDecorator(UnitOfWork unitOfWork, ICommandHandler<T> innerHandler)
        {
            _unitOfWork = unitOfWork;
            _innerHandler = innerHandler;
        }
        public async Task HandleAsync(T command)
        {
            await _innerHandler.HandleAsync(command);
            await _unitOfWork.Commit();
        }
    }
}