using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _contex;
            public Handler(DataContext contex)
            {
                _contex = contex;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _contex.Activities.Add(request.Activity);
                await _contex.SaveChangesAsync();
                
                return Unit.Value;
            }
        }
    }
}