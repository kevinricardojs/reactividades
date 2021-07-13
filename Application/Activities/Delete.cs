using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var activity = await _contex.Activities.FindAsync(request.Id);
                
                _contex.Remove(activity);
                await _contex.SaveChangesAsync();
                
                return Unit.Value;
            }
        }
    }
}